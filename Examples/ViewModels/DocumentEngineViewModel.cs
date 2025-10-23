// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
//
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Windows.Input;
using System.Text.Json;
using PSPDFKit.Sdk.MVVM;

namespace Nutrient.Maui.Catalog.Examples.ViewModels;

public class DocumentEngineViewModel : BindableBase
{
    private const string DocumentEngineUrl = "http://localhost:5001";
    private const string AuthToken = "secret";

    private string _documentId = string.Empty;
    private string _jsonResult = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isLoading;
    private string _guideText = string.Empty;
    private readonly Command _fetchInstantJsonCommand;

    public DocumentEngineViewModel()
    {
        _fetchInstantJsonCommand = new Command(async () => await FetchInstantJson(), () => CanFetchJson);
    }

    public string GuideText
    {
        get => _guideText;
        set
        {
            _guideText = value;
            OnPropertyChanged();
        }
    }

    public string DocumentId
    {
        get => _documentId;
        set
        {
            _documentId = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanFetchJson));
            _fetchInstantJsonCommand.ChangeCanExecute();
        }
    }

    public string JsonResult
    {
        get => _jsonResult;
        set
        {
            _jsonResult = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasJsonResult));
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasError));
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanFetchJson));
            _fetchInstantJsonCommand.ChangeCanExecute();
        }
    }

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool HasJsonResult => !string.IsNullOrEmpty(JsonResult);
    public bool CanFetchJson => !string.IsNullOrWhiteSpace(DocumentId) && !IsLoading;

    public ICommand OpenDocumentation => new Command(OnOpenDocumentation);
    public ICommand FetchInstantJsonCommand => _fetchInstantJsonCommand;

    internal async Task Initialize()
    {
        await using var fileStream = await FileSystem.Current.OpenAppPackageFileAsync("DocumentEngineGuide.txt");
        using var reader = new StreamReader(fileStream);
        GuideText = await reader.ReadToEndAsync();
    }

    private async void OnOpenDocumentation()
    {
        var uri = new Uri("https://www.nutrient.io/guides/document-engine/");
        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }

    private async Task FetchInstantJson()
    {
        if (string.IsNullOrWhiteSpace(DocumentId))
        {
            ErrorMessage = "Please enter a document ID";
            return;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;
        JsonResult = string.Empty;

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Token token={AuthToken}");

            var url = $"{DocumentEngineUrl}/api/documents/{DocumentId.Trim()}/document.json";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                // Pretty print the JSON
                var jsonDocument = JsonDocument.Parse(jsonString);
                var prettyJson = JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                JsonResult = prettyJson;
                ErrorMessage = string.Empty;
            }
            else
            {
                ErrorMessage = $"Failed to fetch Instant JSON. Status: {response.StatusCode}. " +
                              $"Reason: {response.ReasonPhrase}. " +
                              $"Make sure Document Engine is running and the document ID is correct.";
            }
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}. Make sure Document Engine is running at {DocumentEngineUrl}";
        }
        catch (JsonException ex)
        {
            ErrorMessage = $"Invalid JSON response: {ex.Message}";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
