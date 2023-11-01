namespace PSPDFKit.Maui.Catalog.CustomControls;

public partial class GuideLinker : ContentView
{
	public GuideLinker()
	{
		InitializeComponent();
	}

    #region Guide Url Dependency Property

    public static readonly BindableProperty GuideUrlProperty =
        BindableProperty.Create("GuideUrl", typeof(string), typeof(GuideLinker), string.Empty,
            BindingMode.TwoWay);

    public string GuideUrl
    {
        get => (string)GetValue(GuideUrlProperty);
        set => SetValue(GuideUrlProperty, value);
    }

    #endregion

    private async void OnOpenGuideRequested(object sender, EventArgs e)
    {
        await Browser.OpenAsync(GuideUrl);
    }
}
