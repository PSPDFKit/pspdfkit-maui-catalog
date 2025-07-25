const advanceConfiguration = {
    enableHistory: true
}

let globalInstance;

//To iterate through forms
function formElementsIterator(index, rect) {
    console.log(globalInstance)
    console.log(globalInstance.contentDocument)
  const page = globalInstance.contentDocument
      .getRootNode()
      .querySelector(`[data-page-index="${index}"]`),
    arrow = document.createElement("span");
  arrow.classList.add("blob"),
    (arrow.style.cssText +=
      'background: url(https://pspdfkit.com/assets/images/icons/arrowRightLightBg-edaf26b7.svg) 50% no-repeat;\n    content: "";\n    height: 1.2em;\n    left: -2rem;\n    position: absolute;\n    top: 0;\n    width: 1rem;');
  const item = new PSPDFKit.CustomOverlayItem({
    id: "arrow",
    node: arrow,
    pageIndex: index,
    position: new PSPDFKit.Geometry.Point({ x: rect.left + 15, y: rect.top }),
    onAppear() {
      // console.log("rendered!");
    },
  });
  globalInstance.setCustomOverlayItem(item),
    window.requestAnimationFrame(() => {
      page.scrollIntoView({
        block: "end",
        inline: "nearest",
        behavior: "smooth",
      });
    });
}

async function scrollSmoothly() {
    const currentDocument = PSPDFKit.Maui.MauiBridge.currentDocument;
    globalInstance = currentDocument;

    // Initialize form counter
    localStorage.setItem("form-counter", "0");

    // Get annotations with Signer1 role
    const pageAnnotations = (
      await Promise.all(
        Array.from({ length: currentDocument.totalPageCount }).map((_, pageIndex) =>
          currentDocument.getAnnotations(pageIndex)
        )
      )
    ).flatMap((annotations) =>
      annotations.reduce(
        (acc, annotation) =>
          annotation?.customData?.role?.includes("Signer1")
            ? acc.concat(annotation)
            : acc,
        []
      )
    );

    // Start form iteration if annotations exist
    setTimeout(() => {
      pageAnnotations.length > 0 &&
        formElementsIterator(
          pageAnnotations[0].pageIndex,
          pageAnnotations[0].boundingBox
        );
    }, 1000);

    // Listen for annotation changes to navigate through forms
    currentDocument.addEventListener("annotations.willChange", async (event) => {
      const { annotations: annotations, reason: reason } = event;
      let counter = parseInt(localStorage.getItem("form-counter"));
      annotations.get(0).isSignature &&
        "SELECT_END" === reason &&
        (currentDocument.removeCustomOverlayItem("arrow"),
        (counter += 1),
        localStorage.setItem("form-counter", counter.toString()),
        counter < pageAnnotations.length &&
          setTimeout(() => {
            formElementsIterator(
              pageAnnotations[counter].pageIndex,
              pageAnnotations[counter].boundingBox
            );
          }, 1000));
    });
}
