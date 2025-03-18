// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Collections.Immutable;
using System.Windows.Input;
using PSPDFKit.Api;
using PSPDFKit.Api.Enums;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class ActivateToolsViewModel : ExampleViewModelBase
{
    private InteractionMode? _selectedInteractionMode;
    private SidebarMode? _selectedSidebarMode;
    private IViewerConfiguration _viewerConfiguration;

    public ActivateToolsViewModel() :
        base("https://www.nutrient.io/guides/maui/user-interface/main-toolbar/activate-or-deactivate-tools/")
    {
        DemoFile = "form.pdf";
        ResetCommand = new Command(OnResetRequested);
    }

    public ImmutableList<InteractionMode> InteractionModes { get; } =
        ImmutableList.CreateRange(Enum.GetValues(typeof(InteractionMode)).Cast<InteractionMode>());

    public ImmutableList<SidebarMode> SidebarModes { get; } =
        ImmutableList.CreateRange(Enum.GetValues(typeof(SidebarMode)).Cast<SidebarMode>());

    public ICommand ResetCommand { get; }

    public InteractionMode? SelectedInteractionMode
    {
        get => _selectedInteractionMode;
        set => SetField(ref _selectedInteractionMode, value);
    }

    public SidebarMode? SelectedSidebarMode
    {
        get => _selectedSidebarMode;
        set => SetField(ref _selectedSidebarMode, value);
    }

    public async void LoadDemoDocument()
    {
        try
        {
            _viewerConfiguration = PSPDFKitController.CreateViewerConfiguration();
            await PSPDFKitController.LoadDocumentFromAssetsAsync(
                DemoFile, _viewerConfiguration);
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    private void OnResetRequested()
    {
        SelectedInteractionMode = null;
        SelectedSidebarMode = null;
    }
}
