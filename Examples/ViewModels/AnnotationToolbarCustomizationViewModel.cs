// Copyright © 2023-2025 PSPDFKit GmbH d/b/a Nutrient. All rights reserved.
// 
// THIS SOURCE CODE AND ANY ACCOMPANYING DOCUMENTATION ARE PROTECTED BY INTERNATIONAL COPYRIGHT LAW
// AND MAY NOT BE RESOLD OR REDISTRIBUTED. USAGE IS BOUND TO THE PSPDFKIT LICENSE AGREEMENT.
// UNAUTHORIZED REPRODUCTION OR DISTRIBUTION IS SUBJECT TO CIVIL AND CRIMINAL PENALTIES.
// This notice may not be removed from this file.

using System.Windows.Input;
using PSPDFKit.Api.Enums.Annotations;
using static PSPDFKit.Sdk.Models.Toolbar.AnnotationToolbarItems;

namespace PSPDFKit.Maui.Catalog.Examples.ViewModels;

public class AnnotationToolbarCustomizationViewModel : ExampleViewModelBase
{
    private readonly Command _addItemsCommand;
    private readonly Command _removeItemsCommand;
    private readonly Command _shuffleItemsCommand;
    private readonly Command _updateIconCommand;

    public AnnotationToolbarCustomizationViewModel() : base(
        "https://www.nutrient.io/guides/maui/user-interface/annotation-toolbar/customize-existing-tools/")
    {
        _addItemsCommand = new Command(OnAddItemsRequested);
        _removeItemsCommand = new Command(OnRemoveItemsRequested, CanRemoveItems);
        _updateIconCommand = new Command(OnUpdateIconItemsRequested, CanUpdateIcon);
        _shuffleItemsCommand = new Command(OnShuffleItemRequested, CanShuffleItems);
    }

    public ICommand AddItemsCommand => _addItemsCommand;
    public ICommand RemoveItemsCommand => _removeItemsCommand;
    public ICommand UpdateIconCommand => _updateIconCommand;
    public ICommand ShuffleItemsCommand => _shuffleItemsCommand;

    public async void LoadDemoDocument()
    {
        try
        {
            await PSPDFKitController.LoadDocumentFromAssetsAsync(
                DemoFile, PSPDFKitController.CreateViewerConfiguration());

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
        var toolbarItem = new CustomAnnotationToolbarItem(Guid.NewGuid().ToString())
        {
            Tooltip = "Button",
            Icon = "icons/status_completed.svg"
        };
        toolbarItem.Clicked += (s, e) => RaiseDisplayMessageEvent(
            "Annotation button clicked", "Custom annotation button was clicked");

        PSPDFKitController.AnnotationToolbar.ToolbarItems[AnnotationType.Ink].Add(toolbarItem);

        _removeItemsCommand.ChangeCanExecute();
    }

    #endregion

    #region Remove Items Command

    private bool CanRemoveItems(object arg)
    {
        return PSPDFKitController?.AnnotationToolbar?.ToolbarItems.Count != 0;
    }

    private void OnRemoveItemsRequested(object obj)
    {
        PSPDFKitController.AnnotationToolbar.ToolbarItems[AnnotationType.Ink].RemoveAt(0);
        _removeItemsCommand.ChangeCanExecute();
        _updateIconCommand.ChangeCanExecute();
    }

    #endregion

    #region Update Icon Command

    private void OnUpdateIconItemsRequested(object obj)
    {
        var deleteButton = PSPDFKitController.AnnotationToolbar.ToolbarItems[AnnotationType.Ink].First(
            item => item is DeleteAnnotationToolbarItem);
        deleteButton.Icon = "icons/thrash_can.png";
    }

    private bool CanUpdateIcon(object arg)
    {
        return PSPDFKitController?.AnnotationToolbar?.ToolbarItems[AnnotationType.Ink]
            ?.Any(item => item is DeleteAnnotationToolbarItem) ?? false;
    }

    #endregion

    #region Shuffle Items Command

    private bool CanShuffleItems(object arg)
    {
        return PSPDFKitController?.AnnotationToolbar?.ToolbarItems[AnnotationType.Ink].Count > 2;
    }

    private void OnShuffleItemRequested(object obj)
    {
        PSPDFKitController.AnnotationToolbar.ToolbarItems[AnnotationType.Ink]
            .Move(0, PSPDFKitController.AnnotationToolbar.ToolbarItems[AnnotationType.Ink].Count - 1);
    }

    #endregion
}
