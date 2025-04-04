// Copyright � 2023-2024 PSPDFKit GmbH. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using CommunityToolkit.Maui.Views;
using PSPDFKit.Maui.Catalog.Popups.ViewModels;

namespace PSPDFKit.Maui.Catalog.Popups;

public partial class ExportOptionsPopup : Popup
{
    public ExportOptionsPopup(ExportOptionsPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
