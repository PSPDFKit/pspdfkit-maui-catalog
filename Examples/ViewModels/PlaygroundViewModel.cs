// Copyright © 2023-2024 PSPDFKit GmbH. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class PlaygroundViewModel : ExampleViewModelBase
{
    public PlaygroundViewModel() : base("https://pspdfkit.com/guides/maui/intro/")
    {
        DemoFile = "playground.pdf";
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
}
