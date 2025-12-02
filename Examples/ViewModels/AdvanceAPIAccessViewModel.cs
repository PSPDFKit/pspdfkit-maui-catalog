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
    }

    public async void LoadDemoDocument()
    {
        try
        {
            await PSPDFKitController.LoadDocumentFromAssetsAsync(
                DemoFile, PSPDFKitController.CreateViewerConfiguration());

            await SetCssVariableAsync("--bui-color-border-subtle", "#000");
            await SetCssVariableAsync("--bui-color-border-medium", "#000");
            await SetCssVariableAsync("--bui-color-border-strong", "#000");
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
            await PSPDFKitController.ExecuteJavaScriptFunctionAsync("removeExportButton", new object[] { });
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Remove export document button failed", ex);
        }
    }

    public async Task SetCssVariableAsync(string variableName, string value)
    {
        try
        {
            await PSPDFKitController.ExecuteJavaScriptFunctionAsync("setCssVariable", new object[] { variableName, value });
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Set CSS variable failed", ex);
        }
    }
}
