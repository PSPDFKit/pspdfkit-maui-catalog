using PSPDFKit.Maui.Catalog.MVVM;

namespace PSPDFKit.Maui.Catalog.Examples.Models
{
    public class DocumentSource : BindableBase
    {
        private string _documentPath;

        public string Source { get; init; }

        public bool IsPathReadOnly { get; init; }

        public string PlaceholderForPath { get; init; }

        public string DocumentPath
        {
            get => _documentPath;
            set
            {
                if (value == _documentPath)
                {
                    return;
                }

                _documentPath = value;
                OnPropertyChanged();
            }
        }
    }

}
