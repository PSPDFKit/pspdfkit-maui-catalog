namespace PSPDFKit.Maui.Catalog.Examples.ViewModels
{
    public class PlaygroundViewModel : ExampleViewModelBase
    {
        public PlaygroundViewModel() : base("https://pspdfkit.com/guides/maui/intro/") 
        {
            DemoFile = "playground.pdf";
        }

        public async void LoadDemoDocument()
        {
            try
            {
                await PSPDFKitController.LoadDocumentFromAssetsAsync(DemoFile);
            }
            catch (Exception ex)
            {
                RaiseExceptionThrownEvent("Loading document failed", ex);
            }
        }
    }
}
