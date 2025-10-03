// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using CommunityToolkit.Maui.Storage;
using PSPDFKit.Api;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class AdvanceAPIAccessViewModel : ExampleViewModelBase
{
    private IDocument _document;

    public AdvanceAPIAccessViewModel() : base("https://www.nutrient.io/guides/maui/advanced-access-apis/")
    {
        DemoFile = "scrollDocument.pdf";
    }

    public async void LoadDemoDocument()
    {
        try
        {
            _document = await PSPDFKitController.LoadDocumentFromAssetsAsync(
                DemoFile, PSPDFKitController.CreateViewerConfiguration());
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    public async Task<string> ExportInstantJsonAsync()
    {
        try
        {
            var annotationManager = _document.AnnotationManager;
            if (annotationManager == null)
            {
                RaiseExceptionThrownEvent("Export Instant JSON failed", new InvalidOperationException("AnnotationManager not available"));
                return null;
            }

            var instantJson = await annotationManager.ExportInstantJsonAsync();
            return instantJson?.ToString() ?? "{}";
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Export Instant JSON failed", ex);
            return null;
        }
    }

    public async Task ExportDocumentAsync()
    {
        try
        {
            if (_document == null)
            {
                RaiseExceptionThrownEvent("Export failed", new InvalidOperationException("No document loaded"));
                return;
            }

            var exportConfiguration = _document.CreateExportConfiguration();
            var exportedDocumentContent = await _document.ExportDocumentAsync(exportConfiguration);

            var result = await FileSaver.Default.SaveAsync(
                "exported_document.pdf",
                new MemoryStream(exportedDocumentContent),
                CancellationToken.None);

            if (!result.IsSuccessful && result.Exception != null)
            {
                RaiseExceptionThrownEvent("Export failed", result.Exception);
            }
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Export failed", ex);
        }
    }
}
