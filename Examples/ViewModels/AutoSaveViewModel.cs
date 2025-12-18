// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using Microsoft.JSInterop;
using PSPDFKit.Api;
using PSPDFKit.Maui.Catalog.Examples.Services;
using System.Windows.Input;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

/* 
 * NOTE:
 * This example is only tested for Windows and Mac (desktops). 
 * Extra steps would be needed to implement it on mobiles. 
*/ 

public enum AutoSaveStatus
{
    DetectedUnsavedChanges,
    InProgress,
    Failed,
    Succeeded
}

public class AutoSaveViewModel : ExampleViewModelBase
{
    private IDocument _document;
    private AutoSaveStatus _autoSaveStatus = AutoSaveStatus.Succeeded;
    private string _filePath;

    public AutoSaveViewModel() :
        base("https://www.nutrient.io/guides/web/features/saving/")
    {
        OpenFileCommand = new Command(OnOpenFileRequested);
    }

    public ICommand OpenFileCommand { get; init; }

    public AutoSaveStatus AutoSaveStatus 
    { 
        get => _autoSaveStatus;
        private set => SetField(ref _autoSaveStatus, value);
    }

    private async Task DetectedUnsavedChanges()
    {
        try
        {
            Dispatcher.GetForCurrentThread().Dispatch(() =>
            {
                AutoSaveStatus = AutoSaveStatus.DetectedUnsavedChanges;
            });

            // make the message redable
            // await Task.Delay(500);

            var documentBuffer = await PSPDFKitController.ExecuteJavaScriptFunctionAsync<byte[]>("getDocumentBuffer", new object[] { });

            Dispatcher.GetForCurrentThread().Dispatch(() =>
            {
                AutoSaveStatus = AutoSaveStatus.InProgress;
            });

            // simlate large file by adding delay
            // await Task.Delay(500);

            // save instantJSON to file
            File.WriteAllBytes(_filePath, documentBuffer);

            Dispatcher.GetForCurrentThread().Dispatch(() =>
            {
                AutoSaveStatus = AutoSaveStatus.Succeeded;
            });
        }
        catch (Exception ex)
        {
            Dispatcher.GetForCurrentThread().Dispatch(() =>
            {
                AutoSaveStatus = AutoSaveStatus.Failed;
            });

            RaiseExceptionThrownEvent("Saving failed", ex);
        }
    }

    private async void OnOpenFileRequested(object obj)
    {
        FileResult result = null;

        await Application.Current!.Dispatcher.DispatchAsync(
            async () => { result = await FilePicker.Default.PickAsync(); });

        if (result == null) return;

        _filePath = result.FullPath;
        try
        {
            _document = await PSPDFKitController.LoadDocumentAsync(result.FullPath,
                PSPDFKitController.CreateViewerConfiguration());
            var nutrientEventHandler = new NutrientEventHandler(PSPDFKitController, _document);
            var handler = DotNetObjectReference.Create(nutrientEventHandler);
            await PSPDFKitController.ExecuteJavaScriptFunctionAsync("subscribe", new object[] { handler });
            nutrientEventHandler.DetectedUnsavedChanges += DetectedUnsavedChanges;
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }
}
