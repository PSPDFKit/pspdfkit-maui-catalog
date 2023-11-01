using PSPDFKit.Maui.Catalog.Examples.ViewModels;

namespace PSPDFKit.Maui.Catalog.Examples.Views
{
    public partial class Playground : ExampleViewBase
    {
        public Playground(PlaygroundViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private PlaygroundViewModel ViewModel => GetViewModel<PlaygroundViewModel>();
        
        private void OnPDFViewInitialized(object sender, EventArgs e)
        {
            ViewModel.PSPDFKitController = PDFView.Controller;
            ViewModel.LoadDemoDocument();
        }
    }
}
