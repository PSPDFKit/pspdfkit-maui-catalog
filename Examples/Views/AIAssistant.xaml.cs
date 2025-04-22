// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using Nutrient.Maui.Catalog.Examples.ViewModels;
using PSPDFKit.Maui.Catalog.Examples.Views;

namespace Nutrient.Maui.Catalog.Examples.Views;

public partial class AIAssistant : ExampleViewBase
{
	public AIAssistant(AIAssistantViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}

	private AIAssistantViewModel ViewModel => GetViewModel<AIAssistantViewModel>();

    private void OnPDFViewInitialized(object sender, EventArgs e)
    {
        ViewModel.PSPDFKitController = PDFView.Controller;
        ViewModel.LoadDemoDocument();
    }

}