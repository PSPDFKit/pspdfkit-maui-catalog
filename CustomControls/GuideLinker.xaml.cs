// Copyright © 2023-2024 PSPDFKit GmbH. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

namespace PSPDFKit.Maui.Catalog.CustomControls;

public partial class GuideLinker : ContentView
{
    public GuideLinker()
    {
        InitializeComponent();
    }

    private async void OnOpenGuideRequested(object sender, EventArgs e)
    {
        await Browser.OpenAsync(GuideUrl);
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
}
