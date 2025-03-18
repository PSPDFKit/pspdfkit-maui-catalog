// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using PSPDFKit.Api;
using PSPDFKit.Sdk.MVVM;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public abstract class ExampleViewModelBase : BindableBase
{
    public delegate void OnDisplayAlertEventHandler(string title, string message);

    public ExampleViewModelBase(string guideUrl)
    {
        GuideUrl = guideUrl;
    }

    public IController PSPDFKitController { get; set; }

    public string DemoFile { get; protected set; } = "demo.pdf";

    public string GuideUrl { get; init; }
    public event OnDisplayAlertEventHandler ExceptionThrown;
    public event OnDisplayAlertEventHandler DisplayMessage;

    protected void RaiseExceptionThrownEvent(string title, string message)
    {
        ExceptionThrown?.Invoke(title, message);
    }

    protected void RaiseExceptionThrownEvent(string title, Exception ex)
    {
        ExceptionThrown?.Invoke(title, $"{ex.Message}.\nDetails: {ex.InnerException?.Message}");
    }

    protected void RaiseDisplayMessageEvent(string title, string message)
    {
        DisplayMessage?.Invoke(title, message);
    }
}
