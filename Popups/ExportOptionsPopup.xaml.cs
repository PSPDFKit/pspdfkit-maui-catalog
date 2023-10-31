using CommunityToolkit.Maui.Views;
using PSPDFKit.Maui.Catalog.Popups.ViewModels;

namespace PSPDFKit.Maui.Catalog.Popups
{
    public partial class ExportOptionsPopup : Popup
    {
        public ExportOptionsPopup(ExportOptionsPopupViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
