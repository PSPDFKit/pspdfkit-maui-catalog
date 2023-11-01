using PSPDFKit.Maui.Catalog.Examples.ViewModels;

namespace PSPDFKit.Maui.Catalog.Examples.Views
{
    public partial class AboutPage : ContentPage
    {
        private readonly AboutPageViewModel _viewModel;

        public AboutPage(AboutPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        private async void OnAboutPageLoaded(object sender, EventArgs e)
        {
            await _viewModel.Initialize();
        }
    }
}
