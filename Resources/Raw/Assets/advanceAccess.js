const advanceConfiguration = {
    enableHistory: true
}

function removeExportButton() {
    const currentDocument = PSPDFKit.Maui.MauiBridge.currentDocument;
    const items = currentDocument.toolbarItems;

    // Hide the toolbar item with the `id` "export-pdf" by removing it from the array of items.
    currentDocument.setToolbarItems(items.filter((item) => item.type !== "export-pdf"));
}