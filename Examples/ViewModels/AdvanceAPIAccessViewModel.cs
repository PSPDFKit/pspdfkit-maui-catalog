using PSPDFKit.Api;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels
{
    public class AdvanceAPIAccessViewModel : ExampleViewModelBase
    {
        private IDocument _document;

        public AdvanceAPIAccessViewModel() : base("https://pspdfkit.com/guides/maui/advanced-access-apis/")
        {
        }

        public async void LoadDemoDocument()
        {
            try
            {
                _document = await PSPDFKitController.LoadDocumentFromAssetsAsync(DemoFile);
            }
            catch (Exception ex)
            {
                RaiseExceptionThrownEvent("Loading document failed", ex);
            }
        }

        public async void RemoveExportDocumentButton()
        {
            try
            {
                await _document.ExecuteJavaScriptFunctionAsync("removeExportButton", new object[] { });
            }
            catch (Exception ex)
            {
                RaiseExceptionThrownEvent("Remove export document button failed", ex);
            }
        }
    }
}
