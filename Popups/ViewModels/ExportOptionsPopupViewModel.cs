using PSPDFKit.Maui.Catalog.MVVM;

namespace PSPDFKit.Maui.Catalog.Popups.ViewModels
{
    public class ExportOptionsPopupViewModel: BindableBase
    {
        private bool _excludeAnnotations;
        private bool _exportForPrinting;
        private bool _exportIncrementally;
        private bool _flatten;

        public bool ExcludeAnnotations
        {
            get => _excludeAnnotations;
            set
            {
                _excludeAnnotations = value;
                OnPropertyChanged();
            }
        }

        public bool ExportForPrinting
        {
            get => _exportForPrinting;
            set
            {
                _exportForPrinting = value;
                OnPropertyChanged();
            }
        }


        public bool ExportIncrementally
        {
            get => _exportIncrementally;
            set
            {
                _exportIncrementally = value;
                OnPropertyChanged();
            }
        }

        public bool Flatten
        {
            get => _flatten;
            set
            {
                _flatten = value;
                OnPropertyChanged();
            }
        }

    }
}
