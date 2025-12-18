// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Maui.Catalog.Examples.ViewModels;
using static PSPDFKit.Sdk.Models.Toolbar.MainToolbarItems;

namespace Nutrient.Maui.Catalog.Examples.ViewModels
{
    public class AIAssistantViewModel : ExampleViewModelBase
    {
        public AIAssistantViewModel() : base("https://www.nutrient.io/guides/maui/ai-assistant/")
        {
            DemoFile = "invoice.pdf";
        }
        
        public async void LoadDemoDocument()
        {
            try
            {
                var config = PSPDFKitController.CreateViewerConfiguration();
                config.SetAIAssistantConfiguration("your-session-id", "your-jwt", "your-backend-url");
                await PSPDFKitController.LoadDocumentFromAssetsAsync(
                    DemoFile, config);
                PSPDFKitController.MainToolbar.ToolbarItems.Add(new AIAssistantButton());
            }
            catch (Exception ex)
            {
                RaiseExceptionThrownEvent("Loading document failed", ex);
            }
        }

    }
}
