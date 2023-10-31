using System.Windows.Input;
using PSPDFKit.Maui.Catalog.MVVM;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels
{
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
            Uri uri = new Uri("https://pspdfkit.com/guides/maui/about/changelog/");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}
