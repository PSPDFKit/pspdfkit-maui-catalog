// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Windows.Input;
using PSPDFKit.Api.Annotation;
using PSPDFKit.Sdk.Models.Geometry;
using Line = PSPDFKit.Sdk.Models.Annotation.Shape.Line;
using PointF = System.Drawing.PointF;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class AnnotationsViewModel : ExampleViewModelBase
{
    private readonly Command _createAnnotationCommand;
    private readonly Command _resetExampleCommand;
    private readonly Command _selectAnnotationsCommand;
    private readonly IEnumerable<string> _testAnnotations = ["295", "291"];
    private readonly Command _updateAnnotationCommand;
    private IAnnotationManager _annotationManager;
    private bool _areAnnotationsSelected;
    private string _createdAnnotationId;
    private bool _isAnnotationCreated;

    public AnnotationsViewModel() : base("https://www.nutrient.io/guides/maui/annotations/")
    {
        DemoFile = "menu.pdf";
        _createAnnotationCommand = new Command(OnCreateAnnotationsRequested, CanCreateAnnotations);
        _selectAnnotationsCommand = new Command(OnSelectAnnotationsRequested, CanSelectAnnotations);
        _resetExampleCommand = new Command(OnResetExampleRequested);
        _updateAnnotationCommand = new Command(OnUpdateAnnotationRequested, CanUpdateAnnotation);
    }

    public bool AreAnnotationsSelected
    {
        get => _areAnnotationsSelected;
        set => SetField(ref _areAnnotationsSelected, value);
    }

    public bool IsAnnotationCreated
    {
        get => _isAnnotationCreated;
        set => SetField(ref _isAnnotationCreated, value);
    }

    public ICommand CreateAnnotationCommand => _createAnnotationCommand;
    public ICommand SelectAnnotationsCommand => _selectAnnotationsCommand;
    public ICommand ResetExampleCommand => _resetExampleCommand;
    public ICommand UpdateAnnotationCommand => _updateAnnotationCommand;

    public async void LoadDemoDocument()
    {
        try
        {
            _annotationManager = (await PSPDFKitController.LoadDocumentFromAssetsAsync(
                DemoFile, PSPDFKitController.CreateViewerConfiguration())).AnnotationManager;
            UpdateCanExecuteForCommands();
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    private void UpdateCanExecuteForCommands()
    {
        _createAnnotationCommand.ChangeCanExecute();
        _selectAnnotationsCommand.ChangeCanExecute();
        _updateAnnotationCommand.ChangeCanExecute();
    }

    private async void OnCreateAnnotationsRequested(object obj)
    {
        if (IsAnnotationCreated)
        {
            await _annotationManager.DeleteAnnotationsAsync([_createdAnnotationId]);
        }
        else
        {
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
            _createdAnnotationId = await _annotationManager.AddAnnotationAsync(annotations);
        }

        IsAnnotationCreated = !IsAnnotationCreated;
    }

    private bool CanCreateAnnotations(object arg)
    {
        return _annotationManager != null;
    }

    private async void OnSelectAnnotationsRequested(object obj)
    {
        if (AreAnnotationsSelected)
            await _annotationManager.DeselectAnnotationsAsync();
        else
            await _annotationManager.SetSelectedAnnotationsAsync(_testAnnotations);

        AreAnnotationsSelected = !AreAnnotationsSelected;
    }

    private bool CanSelectAnnotations(object arg)
    {
        return _annotationManager != null;
    }

    private async void OnUpdateAnnotationRequested(object obj)
    {
        var annotations = await _annotationManager.GetAnnotationsOnPageAsync(0);
        var annotationToUpdate = annotations[0];
        annotationToUpdate.ShouldRender = !annotationToUpdate.ShouldRender;
        await _annotationManager.UpdateAnnotation(annotationToUpdate);
    }

    private bool CanUpdateAnnotation(object arg)
    {
        return _annotationManager != null;
    }

    private void OnResetExampleRequested(object obj)
    {
        LoadDemoDocument();
        UpdateCanExecuteForCommands();
    }
}
