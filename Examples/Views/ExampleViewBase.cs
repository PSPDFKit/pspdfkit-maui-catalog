using PSPDFKit.Maui.Catalog.Examples.ViewModels;

namespace PSPDFKit.Maui.Catalog.Examples.Views
{
    public class ExampleViewBase : ContentPage
    {
        private ExampleViewModelBase _viewModel;

        protected ExampleViewBase(ExampleViewModelBase viewModel)
        {
            _viewModel = viewModel;
            viewModel.ExceptionThrown +=
                (title, message) =>
                    Application.Current!.Dispatcher.Dispatch(
                        () => DisplayAlert(title, message, "Ok"));

            BindingContext = _viewModel;
        }

        protected TViewModel GetViewModel<TViewModel>() where TViewModel : ExampleViewModelBase
        {
            return (TViewModel)_viewModel;
        }
    }
}
