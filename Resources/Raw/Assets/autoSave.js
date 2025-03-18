function subscribe(nutrientEventHandler) {
    const currentDocument = PSPDFKit.Maui.MauiBridge.currentDocument;
    currentDocument.addEventListener("document.saveStateChange", (event) => {
        if (!event.hasUnsavedChanges) {
            nutrientEventHandler.invokeMethodAsync("detectedUnsavedChanges");
        }
    });
}

async function getDocumentBuffer() {
    const currentDocument = PSPDFKit.Maui.MauiBridge.currentDocument;
    var docBuffer = await currentDocument.exportPDF();
    return new Uint8Array(docBuffer);
}
