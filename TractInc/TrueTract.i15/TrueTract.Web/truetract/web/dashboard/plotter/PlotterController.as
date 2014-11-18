package truetract.web.dashboard.plotter
{
import flash.events.Event;
import flash.utils.ByteArray;

import mx.controls.Alert;
import mx.events.CloseEvent;
import mx.events.ItemClickEvent;
import mx.events.MenuEvent;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.plotter.domain.Tract;
import truetract.plotter.domain.settings.UserSettingsRegistry;
import truetract.plotter.events.TractExportEvent;
import truetract.plotter.utils.PNGEncoder;
import truetract.web.AppModel;
import truetract.web.dashboard.tractEditor.TractEditorView;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;
import truetract.web.util.TractConverter;
import truetract.web.wizards.editTractWizard.EditTractWizardView;

public class PlotterController
{

    [Bindable] public var model:PlotterModel = new PlotterModel();

    [Bindable] public var appModel:AppModel = AppModel.getInstance();

    public var view:PlotterView;

    private var ttService:TrueTractService = TrueTractService.getInstance();

    private var plotterSettings:UserSettingsRegistry = UserSettingsRegistry.getInstance()

    public function openTract(tract:Tract):void 
    {
        if (tract.TractId > 0 && !tract.IsLoaded)
        {
            var loadTractResponder:TokenResponder = new TokenResponder(
                function (event:ResultEvent):void
                {
                    var loadedTract:Tract = Tract(event.result);
                    tract.Calls = loadedTract.Calls;
                    tract.TextObjects = loadedTract.TextObjects;
                    tract.IsLoaded = true;
                    tract.IsDirty = false;

                    view.plotter.initTract(tract); //TODO: plotter should bind model.tract
                },
                "Unable to open Tract"
            );

            ttService.service.LoadTract(tract.TractId).addResponder(loadTractResponder);
        } 
        else 
        {
            view.plotter.initTract(tract); //TODO: plotter should bind model.tract
        }

        model.tract = tract;
        model.resetMenu();
    }

    public function printTract():void
    {
        if (model.tract.TractId > 0)
        {
            view.plotter.Print();
        } 
        else
        {
            Alert.show("In order to print you must save the tract, would you like to save now?",
                "True Tract", Alert.YES | Alert.NO, null, 
                    function (event:CloseEvent):void 
                    {
                        if (event.detail == Alert.NO) return;
    
                        if (event.detail == Alert.YES)
                        {
                            saveTract(model.tract).addResponder ( new Responder(
                                function (event:ResultEvent):void
                                {
                                    view.plotter.Print();
                                }, 
                                function():void {}
                            ));
                        }
                    }, 
                null, Alert.YES
            );
        }
    }

    public function saveTract(tract:Tract):AsyncToken
    {
        if (tract.TractId == 0)
            tract.CreatedBy = appModel.user.UserId;

        var asyncToken:AsyncToken = ttService.service.SaveTract(tract, appModel.user.UserId);
        asyncToken.addResponder ( new TokenResponder(
            function (event:ResultEvent):void {
                var serverTract:Tract = Tract(event.result);

                if (tract.TractId == 0) {
                    tract.TractId = serverTract.TractId;
                    
                    if (tract.ParentDocument) {
                        tract.ParentDocument.TractsList.addItem(tract);
                    } else {
                        view.dashboardController.model.myItemsGroup.groupDrawingsList.addItem(tract);
                    }
                }

                tract.IsDirty = false;
            },
            "Unable to Save Tract"));
        
        return asyncToken;
    }

    public function plotterMenu_itemClickHandler(event:MenuEvent):void
    {
        switch (event.item.id) {
            case PlotterModel.FILE_CLOSE:
                view.dispatchEvent(new Event("close"));
                break;

            case PlotterModel.FILE_PRINT:
                printTract();
                break;

            case PlotterModel.FILE_SAVE:
                saveTract(model.tract);
                break;

            case PlotterModel.VIEW_ZOOM_IN:
                view.plotter.ZoomIn();
                break;

            case PlotterModel.VIEW_ZOOM_OUT:
                view.plotter.ZoomOut();
                break;

            case PlotterModel.VIEW_ZOOM_ALL:
                view.plotter.ZoomAll();
                break;

            case PlotterModel.SETTINGS_SHOW_AREA:
                plotterSettings.ShowAnnotations = event.item.toggled;
                view.plotter.tractView.showCallAnnotations = event.item.toggled;
                break;

            case PlotterModel.SETTINGS_SHOW_ANNOTATION:
                plotterSettings.ShowArea = event.item.toggled;
                view.plotter.tractView.showArea = event.item.toggled;
                break;

            case PlotterModel.SETTINGS_MORE:
                break;
        }
    }

    public function plotter_printRequestHandler():void
    {
        printTract();
    }

    public function plotter_editTractInfoRequestHandler():void
    {
        var popup:TractEditorView = TractEditorView.open(view, true);
        popup.tract = model.tract;
        
        if (model.tract.ParentDocument) {
            popup.oneLevelTractsList = model.tract.ParentDocument.TractsList;
        } else {
            popup.oneLevelTractsList = 
                view.dashboardController.model.myItemsGroup.groupDrawingsList;
        }
    }

    public function plotter_pdfExportRequestHandler(event:TractExportEvent):void
    {
        var tractPng:ByteArray= PNGEncoder.encode(event.tractBitmapData);
        var scaleBarPng:ByteArray = PNGEncoder.encode(event.scaleBarBitmapData);
        
        var converter:TractConverter = new TractConverter();
        converter.convertToPdf(event.tract, tractPng, scaleBarPng);
    }

    
}
}