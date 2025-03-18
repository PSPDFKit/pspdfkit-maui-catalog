// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using Microsoft.JSInterop;
using PSPDFKit.Api;

namespace PSPDFKit.Maui.Catalog.Examples.Services;

public class NutrientEventHandler
{
    IDocument _document;
    private readonly IController _controller;

    public NutrientEventHandler(IController controller, IDocument document)
    {
        _document = document;
        _controller = controller;
    }

    public event Func<Task> DetectedUnsavedChanges;

    [JSInvokable("detectedUnsavedChanges")]
    public void DetectedUnsavedChangesFromWeb()
    {
        DetectedUnsavedChanges?.Invoke();
    }
}
