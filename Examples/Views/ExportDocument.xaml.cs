using CommunityToolkit.Maui.Core;
using PSPDFKit.Maui.Catalog.Examples.ViewModels;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Platform;
using PSPDFKit.Maui.Catalog.Popups;

namespace PSPDFKit.Maui.Catalog.Examples.Views
{
    public partial class ExportDocument : ExampleViewBase
    {
        public ExportDocument(ExportDocumentViewModel viewModel) : base(viewModel) 
        {
            InitializeComponent();
        }

        private ExportDocumentViewModel ViewModel => GetViewModel<ExportDocumentViewModel>();

        private async void OnPDFViewInitialized(object sender, EventArgs e)
        {
            ViewModel.PSPDFKitController = PDFView.Controller;
            await ViewModel.LoadDemoDocument();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static IMauiContext GetMauiContext(Page page)
        {
            return page.Handler?.MauiContext ?? throw new InvalidOperationException("Could not locate MauiContext.");
        }
        
        private void OnConfigureExportButtonClicked(object sender, EventArgs e)
        {
            var optionsPopup = new ExportOptionsPopup(ViewModel.OptionsPopupViewModel);
            var mauiContext = GetMauiContext(this);
            optionsPopup.Parent = this;
            var platformPopup = optionsPopup.ToHandler(mauiContext);
            platformPopup.Invoke(nameof(IPopup.OnOpened));
        }
    }
}
