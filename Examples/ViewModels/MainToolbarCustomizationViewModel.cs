// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Windows.Input;
using PSPDFKit.Api.Enums;
using PSPDFKit.Api.Toolbar;
using static PSPDFKit.Sdk.Models.Toolbar.MainToolbarItems;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class MainToolbarCustomizationViewModel : ExampleViewModelBase
{
    private readonly Command _addItemsCommand;
    private readonly Command _removeItemsCommand;
    private readonly Command _shuffleItemsCommand;
    private readonly Command _updateIconCommand;
    private bool _showToolbar = true;

    public MainToolbarCustomizationViewModel()
        : base("https://www.nutrient.io/guides/maui/user-interface/main-toolbar/customize-existing-tools/")
    {
        _addItemsCommand = new Command(OnAddItemsRequested);
        _removeItemsCommand = new Command(OnRemoveItemsRequested, CanRemoveItems);
        _updateIconCommand = new Command(OnUpdateIconCommand, CanUpdateIcon);
        _shuffleItemsCommand = new Command(OnShuffleItemRequested, CanShuffleItems);
    }

    public ICommand AddItemsCommand => _addItemsCommand;
    public ICommand RemoveItemsCommand => _removeItemsCommand;
    public ICommand UpdateIconCommand => _updateIconCommand;
    public ICommand ShuffleItemsCommand => _shuffleItemsCommand;

    public bool ShowToolbar
    {
        get => _showToolbar;
        set => SetField(ref _showToolbar, value);
    }

    public async void LoadDemoDocument()
    {
        try
        {
            var config = PSPDFKitController.CreateViewerConfiguration();
            if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone) config.ToolbarPlacement = ToolbarPlacement.Bottom;
            await PSPDFKitController.LoadDocumentFromAssetsAsync(DemoFile, config);
            OnPropertyChanged(nameof(ShowToolbar));

            _removeItemsCommand.ChangeCanExecute();
            _updateIconCommand.ChangeCanExecute();
            _shuffleItemsCommand.ChangeCanExecute();
        }
        catch (Exception ex)
        {
            RaiseExceptionThrownEvent("Loading document failed", ex);
        }
    }

    #region Add Items Command

    private void OnAddItemsRequested(object obj)
    {
        var toolbarItem = new CustomMainToolbarToggleButton(Guid.NewGuid().ToString())
        {
            Tooltip = "Button",
            Icon = "icons/status_completed.svg"
        };
        toolbarItem.Clicked += (s, e) => RaiseDisplayMessageEvent("Toggle button clicked",
            $"Custom toggle button is in Selected = {toolbarItem.IsSelected} state");

        PSPDFKitController.MainToolbar.ToolbarItems.Add(toolbarItem);

        _removeItemsCommand.ChangeCanExecute();
    }

    #endregion

    #region Remove Items Command

    private bool CanRemoveItems(object arg)
    {
        return PSPDFKitController?.MainToolbar?.ToolbarItems.Count != 0;
    }

    private void OnRemoveItemsRequested(object obj)
    {
        PSPDFKitController.MainToolbar.ToolbarItems.RemoveAt(0);
        _removeItemsCommand.ChangeCanExecute();
        _updateIconCommand.ChangeCanExecute();
    }

    #endregion

    #region Update Icon Command

    private void OnUpdateIconCommand(object obj)
    {
        var inkButton = (IMainToolbarToggleButton)PSPDFKitController.MainToolbar.ToolbarItems.First(
            item => item is InkToggleButton);
        inkButton.Icon = "icons/ink.png";
    }

    private bool CanUpdateIcon(object arg)
    {
        return PSPDFKitController?.MainToolbar?.ToolbarItems?.Any(item => item is InkToggleButton) ?? false;
    }

    #endregion

    #region Shuffle Items Command

    private bool CanShuffleItems(object arg)
    {
        return PSPDFKitController?.MainToolbar?.ToolbarItems.Count > 2;
    }

    private void OnShuffleItemRequested(object obj)
    {
        PSPDFKitController.MainToolbar.ToolbarItems.Move(0, PSPDFKitController.MainToolbar.ToolbarItems.Count - 1);
    }

    #endregion
}
