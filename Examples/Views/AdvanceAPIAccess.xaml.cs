using PSPDFKit.Maui.Catalog.Examples.ViewModels;

namespace PSPDFKit.Maui.Catalog.Examples.Views;

public partial class AdvanceAPIAccess : ExampleViewBase
{
	public AdvanceAPIAccess(AdvanceAPIAccessViewModel viewModel): base(viewModel)
	{
		InitializeComponent();
    }

    private AdvanceAPIAccessViewModel ViewModel => GetViewModel<AdvanceAPIAccessViewModel>();

    private void OnPDFViewInitialized(object sender, EventArgs e)
    {
        ViewModel.PSPDFKitController = PDFView.Controller;
        ViewModel.LoadDemoDocument();
    }

    private void OnRemoveExportDocumentButtonClicked(object sender, EventArgs e)
    {
        ViewModel.RemoveExportDocumentButton();
    }
}