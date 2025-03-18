// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PSPDFKit.Api.Annotation;
using PSPDFKit.Sdk.Models.Geometry;
using Line = PSPDFKit.Sdk.Models.Annotation.Shape.Line;
using PointF = System.Drawing.PointF;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class InstantJsonViewModel : ExampleViewModelBase
{
    private readonly Command _exportInstantJsonCommand;
    private readonly Command _importInstantJsonCommand;
    private IAnnotationManager _annotationManager;
    private bool _documentLoaded;
    private string _instantJson;

    public InstantJsonViewModel() : base("https://www.nutrient.io/guides/maui/annotations/instant-json/")
    {
        DemoFile = "default.pdf";
        _exportInstantJsonCommand = new Command(OnExportInstantJsonRequested, CanExportInstantJson);
        _importInstantJsonCommand = new Command(OnImportInstantJsonRequested, CanImportInstantJson);
    }

    public string InstantJson
    {
        get => _instantJson;
        set => SetField(ref _instantJson, value);
    }

    public ICommand ExportInstantJsonCommand => _exportInstantJsonCommand;
    public ICommand ImportInstantJsonCommand => _importInstantJsonCommand;

    public async void LoadDemoDocument()
    {
        try
        {
            var doc = await PSPDFKitController.LoadDocumentFromAssetsAsync(
                DemoFile, PSPDFKitController.CreateViewerConfiguration());
            _annotationManager = doc.AnnotationManager;
            var annotations = _annotationManager.AnnotationFactory.CreateAnnotation<Line>();
            annotations.StartPoint = new PointF(10, 10);
            annotations.EndPoint = new PointF(100, 100);
            annotations.BoundingBox = new Rectangle
            {
                Left = 0,
                Top = 0,
                Width = 100,
                Height = 100
            };
            await _annotationManager.AddAnnotationAsync(annotations);
            _documentLoaded = true;

            _exportInstantJsonCommand.ChangeCanExecute();
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    private async void OnExportInstantJsonRequested(object obj)
    {
        var exportedInstantJson = await _annotationManager.ExportInstantJsonAsync();
        var jObj = JObject.Parse(exportedInstantJson);
        jObj.Remove("pdfId");
        await Clipboard.Default.SetTextAsync(JsonConvert.SerializeObject(jObj));
        RaiseDisplayMessageEvent(
            "Instant json exported",
            $"InstantJSON = '{exportedInstantJson}'\r\n\r\nRemoved 'pdfId' and copied rest of the content.");

        var doc = await PSPDFKitController.LoadDocumentFromAssetsAsync(
            DemoFile, PSPDFKitController.CreateViewerConfiguration());
        _annotationManager = doc.AnnotationManager;
        _importInstantJsonCommand.ChangeCanExecute();
    }

    private bool CanExportInstantJson(object arg)
    {
        return _documentLoaded;
    }

    private async void OnImportInstantJsonRequested(object obj)
    {
        await _annotationManager.ImportInstantJsonAsync(JObject.Parse(InstantJson));
        InstantJson = null;
        await Clipboard.Default.SetTextAsync(null);
    }

    private bool CanImportInstantJson(object arg)
    {
        return _annotationManager != null;
    }
}
