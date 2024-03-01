// Copyright © 2023-2024 PSPDFKit GmbH. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Maui.Catalog.Examples.ViewModels;

namespace PSPDFKit.Maui.Catalog.Examples.Views;

public class ExampleViewBase : ContentPage
{
    private readonly ExampleViewModelBase _viewModel;

    protected ExampleViewBase(ExampleViewModelBase viewModel)
    {
        _viewModel = viewModel;
        viewModel.ExceptionThrown += DisplayCatalogAlert;
        viewModel.DisplayMessage += DisplayCatalogAlert;

        BindingContext = _viewModel;
    }

    private void DisplayCatalogAlert(string title, string message)
    {
        Application.Current!.Dispatcher.Dispatch(
            () => DisplayAlert(title, message, "Ok"));
    }

    protected TViewModel GetViewModel<TViewModel>() where TViewModel : ExampleViewModelBase
    {
        return (TViewModel)_viewModel;
    }
}
