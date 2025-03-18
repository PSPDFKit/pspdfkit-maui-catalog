// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Sdk.MVVM;

namespace PSPDFKit.Maui.Catalog.Examples.Models;

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
            if (value == _documentPath) return;

            _documentPath = value;
            OnPropertyChanged();
        }
    }
}
