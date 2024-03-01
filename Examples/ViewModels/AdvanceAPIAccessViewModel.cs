// Copyright © 2023-2024 PSPDFKit GmbH. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Api;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class AdvanceAPIAccessViewModel : ExampleViewModelBase
{
    private IDocument _document;

    public AdvanceAPIAccessViewModel() : base("https://pspdfkit.com/guides/maui/advanced-access-apis/")
    {
    }

    public async void LoadDemoDocument()
    {
        try
        {
            _document = await PSPDFKitController.LoadDocumentFromAssetsAsync(
                DemoFile, PSPDFKitController.CreateViewerConfiguration());
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    public async void RemoveExportDocumentButton()
    {
        try
        {
            await _document.ExecuteJavaScriptFunctionAsync("removeExportButton", new object[] { });
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Remove export document button failed", ex);
        }
    }
}
