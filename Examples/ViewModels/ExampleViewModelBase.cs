using PSPDFKit.Api;
using PSPDFKit.Maui.Catalog.MVVM;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels
{
    public abstract class ExampleViewModelBase : BindableBase
    {
        public ExampleViewModelBase(string guideUrl)
        {
            GuideUrl = guideUrl;
        }

        public delegate void OnExceptionRaised(string title, string message);
        public event OnExceptionRaised ExceptionThrown;

        public IController PSPDFKitController { get; set; }

        public string DemoFile { get; protected set; } = "demo.pdf";

        public string GuideUrl { get; init; }

        protected void RaiseExceptionThrownEvent(string title, string message)
        {
            ExceptionThrown?.Invoke(title, message);
        }

        protected void RaiseExceptionThrownEvent(string title, Exception ex)
        {
            ExceptionThrown?.Invoke(title, $"{ex.Message}.\nDetails: {ex.InnerException?.Message}");
        }
    }
}
