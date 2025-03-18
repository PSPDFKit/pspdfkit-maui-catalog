// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Net.Http.Headers;
using System.Windows.Input;
using CommunityToolkit.Maui.Storage;
using PSPDFKit.Api;
using PSPDFKit.Maui.Catalog.Examples.Models;
using PSPDFKit.Maui.Catalog.Popups.ViewModels;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class ExportDocumentViewModel : ExampleViewModelBase
{
    private readonly Command _exportCommand;
    private IDocument _document;
    private DocumentSourceWithExportHandler _selectedDestination;

    public ExportDocumentViewModel() : base("https://www.nutrient.io/guides/maui/save-a-document/")
    {
        DemoFile = "Invoice.pdf";
        DocumentSources = new DocumentSourceWithExportHandler[]
        {
            new()
            {
                DocumentSource = new DocumentSource
                {
                    Source = "Local Storage",
                    IsPathReadOnly = true,
                    PlaceholderForPath = "Click on Export to select a location"
                },
                ExportAsync = OnExportToLocalStorageRequested
            },
            new()
            {
                DocumentSource = new DocumentSource
                {
                    Source = "Remote Server",
                    PlaceholderForPath = "Location on remote server"
                },
                ExportAsync = OnExportToRemoteServerRequested
            }
        };
        _exportCommand = new Command(OnExportRequested, CanExportDocument);
        OptionsPopupViewModel = new ExportOptionsPopupViewModel();

        SelectedDestination = DocumentSources.First();
    }

    public ICommand ExportCommand => _exportCommand;
    public ExportOptionsPopupViewModel OptionsPopupViewModel { get; }

    public IEnumerable<DocumentSourceWithExportHandler> DocumentSources { get; }

    public DocumentSourceWithExportHandler SelectedDestination
    {
        get => _selectedDestination;
        set
        {
            _selectedDestination = value;
            OnPropertyChanged();
        }
    }

    public async Task LoadDemoDocument()
    {
        _document = null;
        _exportCommand.ChangeCanExecute();

        _document = await PSPDFKitController.LoadDocumentFromAssetsAsync(
            DemoFile, PSPDFKitController.CreateViewerConfiguration());

        _exportCommand.ChangeCanExecute();
    }

    private async Task OnExportToLocalStorageRequested()
    {
        try
        {
            var exportedDocumentContent = await _document.ExportDocumentAsync(GetRequestedExportConfiguration());
            var result = await FileSaver.Default.SaveAsync(
                "download.pdf", new MemoryStream(exportedDocumentContent), CancellationToken.None);
            if (!string.IsNullOrEmpty(result.FilePath) && !result.IsSuccessful) throw result.Exception;
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Export failed", ex);
        }
    }

    // To create a local server, you can use Resources/Server/exportServer.py script.
    private async Task OnExportToRemoteServerRequested()
    {
        var client = new HttpClient();

        var form = new MultipartFormDataContent();

        // Read the file into a byte array
        var fileBytes =
            await _document.ExportDocumentAsync(GetRequestedExportConfiguration());

        // Create a ByteArrayContent from the byte array
        var fileContent = new ByteArrayContent(fileBytes);

        // Add the file content to the form
        form.Add(fileContent, "file", "download.pdf");
        form.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            FileName = "download.pdf"
        };

        // Send the POST request to the server
        var response = await client.PostAsync(SelectedDestination.DocumentSource.DocumentPath, form);

        // Check the response status
        if (response.IsSuccessStatusCode)
            // File uploaded successfully
            Console.WriteLine("File uploaded successfully");
        else
            // File upload failed
            Console.WriteLine("File upload failed. Status: " + response.StatusCode);
    }

    private IExportConfiguration GetRequestedExportConfiguration()
    {
        var exportConfiguration = _document.CreateExportConfiguration();
        exportConfiguration.ExcludeAnnotations = OptionsPopupViewModel.ExcludeAnnotations;
        exportConfiguration.ExportForPrinting = OptionsPopupViewModel.ExportForPrinting;
        exportConfiguration.ExportIncrementally = OptionsPopupViewModel.ExportForPrinting;
        exportConfiguration.Flatten = OptionsPopupViewModel.Flatten;
        exportConfiguration.PDFAConformance = OptionsPopupViewModel.SelectedPDFAConformance;

        return exportConfiguration;
    }

    public class DocumentSourceWithExportHandler
    {
        public DocumentSource DocumentSource { get; init; }
        public Func<Task> ExportAsync { get; set; }
    }

    #region Export Command

    private async void OnExportRequested()
    {
        await SelectedDestination.ExportAsync();
    }

    private bool CanExportDocument()
    {
        return _document != null;
    }

    #endregion
}
