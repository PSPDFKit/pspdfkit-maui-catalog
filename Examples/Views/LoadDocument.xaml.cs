using PSPDFKit.Maui.Catalog.Examples.ViewModels;

namespace PSPDFKit.Maui.Catalog.Examples.Views
{
    public partial class LoadDocument : ExampleViewBase
    {
        public LoadDocument(LoadDocumentViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private LoadDocumentViewModel ViewModel => GetViewModel<LoadDocumentViewModel>();

        private void OnPDFViewInitialized(object sender, EventArgs e)
        {
            ViewModel.PSPDFKitController = PDFView.Controller;
        }
    }
}
