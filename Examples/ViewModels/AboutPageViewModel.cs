// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Windows.Input;
using PSPDFKit.Sdk.MVVM;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class AboutPageViewModel : BindableBase
{
    private string _license;

    public string License
    {
        get => _license;
        set
        {
            _license = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenChangelog => new Command(OnOpenChangelog);

    internal async Task Initialize()
    {
        await using var fileStream = await FileSystem.Current.OpenAppPackageFileAsync("License.txt");
        using var reader = new StreamReader(fileStream);
        License = await reader.ReadToEndAsync();
    }

    private async void OnOpenChangelog()
    {
        var uri = new Uri("https://www.nutrient.io/guides/maui/changelog/");
        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}
