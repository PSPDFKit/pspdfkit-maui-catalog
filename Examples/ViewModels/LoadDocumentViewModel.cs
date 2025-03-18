// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Windows.Input;
using PSPDFKit.Api;
using PSPDFKit.Maui.Catalog.Examples.Models;
using PSPDFKit.Sdk.MVVM;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class LoadDocumentViewModel : ExampleViewModelBase
{
    private readonly AsyncCommand _loadCommand;

    private IController _controller;
    private DocumentSourceWithLoadHandler _selectedDocumentSource;

    public LoadDocumentViewModel() : base("https://www.nutrient.io/guides/maui/open-a-document/")
    {
        DocumentSources = new DocumentSourceWithLoadHandler[]
        {
            new()
            {
                DocumentSource = new DocumentSource
                {
                    Source = "Application Assets",
                    PlaceholderForPath = "Document asset path (demo.pdf / menu.pdf)"
                },
                LoadAsync = OnLoadDocumentFromAssetsCommand
            },
            new()
            {
                DocumentSource = new DocumentSource
                {
                    Source = "Built In File Picker",
                    PlaceholderForPath = "Placeholder asset path (blank.pdf)"
                },
                LoadAsync = OnLoadSDKWithBuiltInFilePickerRequested
            },
            new()
            {
                DocumentSource = new DocumentSource
                {
                    Source = "Local Document",
                    IsPathReadOnly = true,
                    PlaceholderForPath = "Click on Load to select a local document"
                },
                LoadAsync = OnOpenFilePickerAndLoadDocumentRequested
            },
            new()
            {
                DocumentSource = new DocumentSource
                {
                    Source = "Url",
                    PlaceholderForPath = "URL of document"
                },
                LoadAsync = OnLoadDocumentFromURLCommand
            },
            new()
            {
                DocumentSource = new DocumentSource
                {
                    Source = "Buffer/ Byte Array",
                    PlaceholderForPath = "Document as Buffer"
                },
                LoadAsync = OnLoadDocumentFromBufferCommand
            },
            new()
            {
                DocumentSource = new DocumentSource
                {
                    Source = "Base64 String",
                    PlaceholderForPath = "Document as Base64 string"
                },
                LoadAsync = OnLoadDocumentFromBase64StringCommand
            }
        };

        _loadCommand = new AsyncCommand(OnLoadDocumentRequested, CanLoadDocument);

        SelectedDocumentSource = DocumentSources.First();
    }

    public new IController PSPDFKitController
    {
        get => _controller;
        set
        {
            _controller = value;
            _loadCommand.NotifyCanExecuteChanged();
        }
    }

    public IEnumerable<DocumentSourceWithLoadHandler> DocumentSources { get; }

    public DocumentSourceWithLoadHandler SelectedDocumentSource
    {
        get => _selectedDocumentSource;
        set
        {
            if (_selectedDocumentSource != null) _selectedDocumentSource.DocumentSource.DocumentPath = string.Empty;

            _selectedDocumentSource = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadCommand => _loadCommand;

    private async Task OnLoadSDKWithBuiltInFilePickerRequested()
    {
        if (string.IsNullOrWhiteSpace(SelectedDocumentSource.DocumentSource.DocumentPath))
        {
            RaiseExceptionThrownEvent(
                "Loading document failed",
                "Placeholder document path should not be empty");
            return;
        }

        try
        {
            var configuration = PSPDFKitController.CreateViewerConfiguration();
            configuration.UseInBuiltFilePicker = true;
            await PSPDFKitController.LoadDocumentFromAssetsAsync(
                SelectedDocumentSource.DocumentSource.DocumentPath, configuration);
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    private async Task OnOpenFilePickerAndLoadDocumentRequested()
    {
        FileResult result = null;

        await Application.Current!.Dispatcher.DispatchAsync(
            async () => { result = await FilePicker.Default.PickAsync(); });

        if (result == null) return;

        SelectedDocumentSource.DocumentSource.DocumentPath = result.FullPath;

        try
        {
            await PSPDFKitController.LoadDocumentAsync(result.FullPath,
                PSPDFKitController.CreateViewerConfiguration());
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    private async Task OnLoadDocumentFromAssetsCommand()
    {
        if (string.IsNullOrWhiteSpace(SelectedDocumentSource.DocumentSource.DocumentPath))
        {
            RaiseExceptionThrownEvent("Loading document failed", "Document path should not be empty");
            return;
        }

        try
        {
            await PSPDFKitController.LoadDocumentFromAssetsAsync(
                SelectedDocumentSource.DocumentSource.DocumentPath, PSPDFKitController.CreateViewerConfiguration());
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    private async Task OnLoadDocumentFromURLCommand()
    {
        // 1. For sample, you can start a local server using https://www.npmjs.com/package/serve with cors flag
        // 2. To load authenticated document, please use HTTPClient to fetch the document using right headers
        //    and use PSPDFKitController.LoadDocumentFromBuffer method to load the document.
        // 3. To load encrypted document, please decrypt it and use PSPDFKitController.LoadDocumentFromBuffer
        //    or PSPDFKitController.LoadDocumentFromBase64String method to load the document.
        //
        // For details please refer to our guides
        if (string.IsNullOrWhiteSpace(SelectedDocumentSource.DocumentSource.DocumentPath))
        {
            RaiseExceptionThrownEvent("Loading document failed", "Document URL should not be empty");
            return;
        }

        try
        {
            await PSPDFKitController.LoadDocumentFromURLAsync(SelectedDocumentSource.DocumentSource.DocumentPath,
                PSPDFKitController.CreateViewerConfiguration());
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    private async Task OnLoadDocumentFromBase64StringCommand()
    {
        // Comment the following lines if you want to load a document and convert it to base64 string.
        if (string.IsNullOrWhiteSpace(SelectedDocumentSource.DocumentSource.DocumentPath))
        {
            RaiseExceptionThrownEvent("Loading document failed",
                "String representing document should not be empty");
            return;
        }

        var documentAsBase64String = SelectedDocumentSource.DocumentSource.DocumentPath;

        // Uncomment the code below if you want to load a document and convert it to base 64 string.
        // 
        // FileResult result = null;

        // await Application.Current!.Dispatcher.DispatchAsync(
        //    async () =>
        //    {
        //        result = await FilePicker.Default.PickAsync();
        //    });

        // if (result == null)
        // {
        //     return;
        // }

        // // Read the contents of selected file 
        // var bytes = await File.ReadAllBytesAsync(result.FullPath);
        // var documentAsBase64String = Convert.ToBase64String(bytes);

        try
        {
            await PSPDFKitController.LoadDocumentFromBase64StringAsync(
                documentAsBase64String, PSPDFKitController.CreateViewerConfiguration());
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    private async Task OnLoadDocumentFromBufferCommand()
    {
        // Comment the following lines if you want to load a document and convert it to byte array.
        if (string.IsNullOrWhiteSpace(SelectedDocumentSource.DocumentSource.DocumentPath))
        {
            RaiseExceptionThrownEvent("Loading document failed",
                "Byte array representing document should not be empty");
            return;
        }

        var documentAsStringArray = SelectedDocumentSource.DocumentSource.DocumentPath.Split(',');
        var documentAsBuffer = new byte[documentAsStringArray.Length];

        for (var i = 0; i < documentAsStringArray.Length; i++)
            documentAsBuffer[i] = byte.Parse(documentAsStringArray[i]);

        // Uncomment the code below if you want to load a document and convert it to byte array.
        // 
        // FileResult result = null;

        // await Application.Current!.Dispatcher.DispatchAsync(
        //    async () =>
        //    {
        //        result = await FilePicker.Default.PickAsync();
        //    });

        //if (result == null)
        //{
        //    return;
        //}

        //// Read the contents of selected file 
        //var documentAsBuffer = await File.ReadAllBytesAsync(result.FullPath);

        try
        {
            await PSPDFKitController.LoadDocumentFromBufferAsync(documentAsBuffer,
                PSPDFKitController.CreateViewerConfiguration());
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    public class DocumentSourceWithLoadHandler
    {
        public DocumentSource DocumentSource { get; init; }

        public Func<Task> LoadAsync { get; init; }
    }

    #region Commands

    private Task OnLoadDocumentRequested()
    {
        return SelectedDocumentSource.LoadAsync();
    }

    private bool CanLoadDocument()
    {
        return PSPDFKitController != null;
    }

    #endregion
}
