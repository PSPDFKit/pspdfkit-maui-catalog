// Copyright © 2023-2024 PSPDFKit GmbH. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Maui.Catalog.Examples.ViewModels;
using PSPDFKit.Maui.Catalog.Examples.Views;
#if DEBUG
using Microsoft.Extensions.Logging;
#endif

namespace PSPDFKit.Maui.Catalog;

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
        builder.Services.AddTransient<ActivateToolsViewModel>();
        builder.Services.AddTransient<AdvanceAPIAccessViewModel>();
        builder.Services.AddTransient<AnnotationsViewModel>();
        builder.Services.AddTransient<AnnotationToolbarCustomizationViewModel>();
        builder.Services.AddTransient<ExportDocumentViewModel>();
        builder.Services.AddTransient<InstantJsonViewModel>();
        builder.Services.AddTransient<LoadDocumentViewModel>();
        builder.Services.AddTransient<MainToolbarCustomizationViewModel>();
        builder.Services.AddTransient<PlaygroundViewModel>();
        return builder;
    }

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<AboutPage>();
        builder.Services.AddTransient<ActivateTools>();
        builder.Services.AddTransient<AdvanceAPIAccess>();
        builder.Services.AddTransient<Annotations>();
        builder.Services.AddTransient<AnnotationToolbarCustomization>();
        builder.Services.AddTransient<ExportDocument>();
        builder.Services.AddTransient<InstantJson>();
        builder.Services.AddTransient<LoadDocument>();
        builder.Services.AddTransient<MainToolbarCustomization>();
        builder.Services.AddTransient<Playground>();
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
