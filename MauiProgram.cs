#if DEBUG
using Microsoft.Extensions.Logging;
#endif

using PSPDFKit.Maui.Catalog.Examples.ViewModels;
using PSPDFKit.Maui.Catalog.Examples.Views;

namespace PSPDFKit.Maui.Catalog
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            return builder
                  .UseMauiApp<App>()
                   // https://learn.microsoft.com/en-us/dotnet/architecture/maui/dependency-injection#registration
                  .RegisterPSPDFKitSdk()
                  .RegisterViewModels()
                  .RegisterViews()
                  .RegisterLogging()
                  .UseMauiCommunityToolkit()
                  .Build();
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AboutPageViewModel>();
            builder.Services.AddTransient<PlaygroundViewModel>();
            builder.Services.AddTransient<LoadDocumentViewModel>();
            builder.Services.AddTransient<ExportDocumentViewModel>();
            builder.Services.AddTransient<AdvanceAPIAccessViewModel>();
            return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AboutPage>();
            builder.Services.AddTransient<Playground>();
            builder.Services.AddTransient<LoadDocument>();
            builder.Services.AddTransient<ExportDocument>();
            builder.Services.AddTransient<AdvanceAPIAccess>();
            return builder;
        }

        private static MauiAppBuilder RegisterLogging(this MauiAppBuilder builder)
        {
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder;
        }
    }
}
