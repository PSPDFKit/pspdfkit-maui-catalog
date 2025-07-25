// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Api;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class AdvanceAPIAccessViewModel : ExampleViewModelBase
{
    public AdvanceAPIAccessViewModel() : base("https://www.nutrient.io/guides/maui/advanced-access-apis/")
    {
        DemoFile = "scrollDocument.pdf";
    }

    public async void LoadDemoDocument()
    {
        try
        {
            await PSPDFKitController.LoadDocumentFromAssetsAsync(
                DemoFile, PSPDFKitController.CreateViewerConfiguration());
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    public async void ScrollSmoothly()
    {
        try
        {
            await PSPDFKitController.ExecuteJavaScriptFunctionAsync("scrollSmoothly", new object[] { });
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Scroll smoothly failed", ex);
        }
    }
}
