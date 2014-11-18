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

import truetract.domain.*;
import truetract.plotter.domain.settings.UserSettingsRegistry;
import truetract.plotter.events.TractExportEvent;
import truetract.plotter.utils.PNGEncoder;
import truetract.web.AppModel;
import truetract.web.dashboard.documentPanel.tractEditor.TractEditorView;
import truetract.web.services.TractConverter;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;
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

//                  TODO: method tract.clone() implemented but not tested yet
//                  view.plotter.initTract(tract.clone());

                    view.plotter.initTract(tract); //TODO: plotter should bind model.tract
                },
                "Unable to open Tract"
            );

            ttService.service.LoadTract(tract.TractId).addResponder(loadTractResponder);
        } 
        else 
        {
//          TODO: method tract.clone() implemented but not tested yet
//          view.plotter.initTract(tract.clone());
            view.plotter.initTract(tract); //TODO: plotter should bind model.tract
        }

        model.tract = tract;
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
        var isTractNew:Boolean = tract.TractId == 0;

        if (isTractNew)
            tract.CreatedBy = appModel.user.UserId;

        var asyncToken:AsyncToken = ttService.saveTract(tract, appModel.user.UserId);
        asyncToken.addResponder(new TokenResponder(
            function(event:ResultEvent):void 
            {
                //TODO: Refactore this. This should not be here
                if (!tract.ParentDocument)
                {
                    var drawingsGroup:UserGroup = view.dashboardController.model.myDrawingsGroup;

                    if (isTractNew)
                        drawingsGroup.groupItemsList.addItem(tract);
                    
                    drawingsGroup.applyFilter();
                }
            }, "Unable to Save Tract"));

        return asyncToken;
    }

    public function plotter_closeRequestHandler():void
    {
        if (view.plotter.Model.IsDirty) {
            Alert.show("Tract has been modified. Save changes ?",
                "True Tract", Alert.YES | Alert.NO | Alert.CANCEL, null, 
                function (event:CloseEvent):void 
                {
                    switch (event.detail) {
                        
                        case Alert.CANCEL:
                            break;

                        case Alert.NO:
                            view.dispatchEvent(new Event("close"));
                            break;

                        case Alert.YES:
                            saveTract(model.tract).addResponder ( new Responder(
                                function (event:ResultEvent):void
                                {
                                    view.dispatchEvent(new Event("close"));
                                }, 
                                function():void {}
                            ));
                            break;
                    }
                }, null, Alert.YES
            );
        } 
        else 
        {
            view.dispatchEvent(new Event("close"));
        }
    }

    public function plotter_printRequestHandler():void
    {
        printTract();
    }

    public function plotter_saveRequestHandler():void
    {
        saveTract(model.tract);
    }

    public function plotter_editTractInfoRequestHandler():void
    {
        var popup:TractEditorView = TractEditorView.open(view, true);
        popup.tract = model.tract;
        
        if (model.tract.ParentDocument) 
        {
            popup.oneLevelTractsList = model.tract.ParentDocument.TractsList;
        } 
        else 
        {
            popup.oneLevelTractsList = 
                view.dashboardController.model.myDrawingsGroup.groupItemsList;
        }
    }

    public function plotter_pdfExportRequestHandler(event:TractExportEvent):void
    {
        var tractPng:ByteArray = PNGEncoder.encode(event.tractBitmapData);
        var scaleBarPng:ByteArray = PNGEncoder.encode(event.scaleBarBitmapData);
        var pageWidth:Number = event.pageWidth;
        var pageHeight:Number = event.pageHeight;
        
        var converter:TractConverter = new TractConverter();
        converter.convertToPdf(event.tract, tractPng, scaleBarPng, 
            appModel.user, pageWidth, pageHeight);
    }
}
}