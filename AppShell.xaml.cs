using System.Reflection;

namespace PSPDFKit.Maui.Catalog
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            AppVersion.Text = Assembly.GetAssembly(typeof(PSPDFKit.Sdk.PDFView)).GetName().Version.ToString(2);
        }
    }
}
