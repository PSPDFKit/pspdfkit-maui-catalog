// Copyright © 2023-2024 PSPDFKit GmbH. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Maui.Catalog.Examples.ViewModels;

namespace PSPDFKit.Maui.Catalog.Examples.Views;

public partial class ActivateTools : ExampleViewBase
{
    public ActivateTools(ActivateToolsViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    private ActivateToolsViewModel ViewModel => GetViewModel<ActivateToolsViewModel>();

    private void OnPDFViewInitialized(object sender, EventArgs e)
    {
        ViewModel.PSPDFKitController = PDFView.Controller;
        ViewModel.LoadDemoDocument();
    }
}
