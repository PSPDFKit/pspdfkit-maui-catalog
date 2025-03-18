// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Api.Enums;
using PSPDFKit.Sdk.MVVM;
using System.Collections.Immutable;

namespace PSPDFKit.Maui.Catalog.Popups.ViewModels;

public class ExportOptionsPopupViewModel : BindableBase
{
    private bool _excludeAnnotations;
    private bool _exportForPrinting;
    private bool _exportIncrementally;
    private bool _flatten;
    private PDFAConformance? _selectedConformance;

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

    public ImmutableList<PDFAConformance> PDFAConformances { get; } =
        ImmutableList.CreateRange(Enum.GetValues(typeof(PDFAConformance)).Cast<PDFAConformance>());

    public PDFAConformance? SelectedPDFAConformance
    {
        get => _selectedConformance;
        set
        {
            _selectedConformance = value;
            OnPropertyChanged();
        }
    }
}
