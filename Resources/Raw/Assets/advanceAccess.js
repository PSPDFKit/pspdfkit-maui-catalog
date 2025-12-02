const advanceConfiguration = {
    enableHistory: true
}

function removeExportButton() {
    const currentDocument = PSPDFKit.Maui.MauiBridge.currentDocument;
    const items = currentDocument.toolbarItems;

    // Hide the toolbar item with the `id` "export-pdf" by removing it from the array of items.
    currentDocument.setToolbarItems(items.filter((item) => item.type !== "export-pdf"));
}


function setCssVariable(variableName, value) {
    // Helper to find elements in both regular DOM and Shadow DOM
    function findAllElements(selector, root = document) {
        let results = [...root.querySelectorAll(selector)];

        // Search in shadow roots
        root.querySelectorAll('*').forEach(el => {
            if (el.shadowRoot) {
                results = results.concat(findAllElements(selector, el.shadowRoot));
            }
        });

        return results;
    }

    const themeProviders = findAllElements('.BaselineUI-ThemeProvider');
    console.log(`[setCssVariable] Found ${themeProviders.length} theme providers`);

    themeProviders.forEach(el => {
        el.style.setProperty(variableName, value);
    });

    // Also set on :root as fallback
    document.documentElement.style.setProperty(variableName, value);
}