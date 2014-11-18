package src.deedplotter.components.tractPrintViewClasses
{
import flash.display.Graphics;
import flash.events.Event;
import flash.geom.Point;
import flash.geom.Rectangle;
import flash.printing.PrintJob;
import flash.printing.PrintJobOptions;
import flash.system.Capabilities;
import flash.system.System;

import mx.containers.Box;
import mx.containers.TitleWindow;
import mx.containers.VBox;
import mx.controls.CheckBox;
import mx.controls.ComboBox;
import mx.core.Application;
import mx.core.EdgeMetrics;
import mx.core.UIComponent;
import mx.effects.Effect;
import mx.effects.Move;
import mx.effects.Parallel;
import mx.effects.Resize;
import mx.effects.Zoom;
import mx.events.EffectEvent;
import mx.events.FlexEvent;
import mx.events.ListEvent;
import mx.printing.FlexPrintJob;
import mx.printing.FlexPrintJobScaleType;

import src.deedplotter.components.ScaleBar;
import src.deedplotter.components.ScaleInput;
import src.deedplotter.components.TractView;
import src.deedplotter.containers.GeoCanvas;
import src.deedplotter.domain.Tract;
import src.deedplotter.utils.GeoPosition;
import src.deedplotter.utils.ScaleValue;
import flash.display.BitmapData;
import src.deedplotter.utils.BitmapUtil;
import flash.geom.Matrix;
import src.deedplotter.events.TractExportEvent;
import src.deedplotter.utils.UOMUtil;

[Event(name="close", type="mx.events.Event")]

public class TractPrintView_code extends VBox
{

    public function TractPrintView_code() 
    {
    }

    [Bindable] public var previewArea:Box;
    [Bindable] public var tractView:TractView;
    [Bindable] public var dataFrame:GeoCanvas;
    [Bindable] public var printPage:VBox;
    [Bindable] public var pageOrientation:ComboBox;
    [Bindable] public var pageSize:ComboBox;
    [Bindable] public var tractMoveEffect:Move;
    [Bindable] public var resizeMove:Parallel;
    [Bindable] public var tractMoveEffectParalel:Move;
    [Bindable] public var pageResizeEffectParalel:Resize;
    [Bindable] public var pageZoomEffect:Zoom;
    [Bindable] public var previewScale:ComboBox;
    [Bindable] public var bitmapPrinting:CheckBox;
    [Bindable] public var scaleBar:ScaleBar;

    //--------------------------------------------------------------------------
    //
    //  Class properties
    //
    //--------------------------------------------------------------------------

    //----------------------------------
    //  tract
    //----------------------------------

    private var _tract:Tract;
    [Bindable] public function get tract():Tract { return _tract };
    public function set tract(value:Tract):void 
    {
        _tract = value;
        tractView.tract = value;
        callLater(fitToPage);
    }

    //--------------------------------------------------------------------------
    //
    //  Class methods
    //
    //--------------------------------------------------------------------------

    public function zoom(scaleDelta:Number):void 
    {
        var tractBounds:Rectangle = tractView.getComponentBounds();
        
        var zoomPoint:Point = new Point (
            tractBounds.x + (tractBounds.width / 2),
            tractBounds.y  + (tractBounds.height / 2)
        );
        
        var zoomPositionBefore:GeoPosition = dataFrame.GetGeoPosition(zoomPoint);

        dataFrame.Scale.PointsInOneFeet += scaleDelta;
        dataFrame.Scale = dataFrame.Scale; //TODO: 

        var zoomPositionAfter:GeoPosition = dataFrame.GetGeoPosition(zoomPoint);
    
        var deltaX:Number = (zoomPositionAfter.Easting - zoomPositionBefore.Easting) 
            * dataFrame.Scale.PointsInOneFeet;
        var deltaY:Number = - (zoomPositionAfter.Northing - zoomPositionBefore.Northing) 
            * dataFrame.Scale.PointsInOneFeet;

        dataFrame.scroll(deltaX, deltaY);
    }
    
    public function zoomOut():void 
    {
        zoom( -2 );
    }

    public function zoomIn():void 
    {
        zoom( 2 );
    }

    public function fitToPage():void 
    {
        var dataFrameWidth:Number = dataFrame.width;
        var dataFrameHeight:Number = dataFrame.height;

        dataFrame.Scale = tractView.calcaluateRequiredScaling(
            dataFrameWidth, dataFrameHeight, dataFrame.Scale.uom);

        doTractCentering();
    }

    public function showScaleBarProperties():void
    {
        var view:ScaleBarPropertiesView = ScaleBarPropertiesView.Open(this, true);
        view.scaleBar = scaleBar;
        view.printDataFrame = dataFrame; //TODO: refactore this !!!
    }

    public function close():void 
    {
        dispatchEvent(new Event("close"));
    }

    public function getPrintPageBitmapData():BitmapData
    {
//        printPage.visible = false;
        printPage.scaleX = printPage.scaleY = 1;
        printPage.setStyle("borderStyle", "none");
        printPage.setStyle("dropShadowEnabled", false);
        printPage.validateSize(true);
        printPage.validateDisplayList();
        printPage.validateNow();

        var result:BitmapData = BitmapUtil.getUIComponentBitmapData(printPage, new Matrix());

/*         //return to page preview
        printPage.scaleY = printPage.scaleX = previewScale.selectedItem.scaleValue;
        printPage.rotation = 0;
        printPage.setStyle("borderStyle", "solid");
        printPage.setStyle("dropShadowEnabled", true);
        printPage.visible = true;
 */
        return result;
    }

    public function doPrinting():void 
    {
        printPage.visible = false;
        if (pageOrientation.selectedLabel == "Landscape") 
        {
            printPage.rotation = 90;
        }

        printPage.scaleX = printPage.scaleY = 1;
        printPage.setStyle("borderStyle", "none");
        printPage.setStyle("dropShadowEnabled", false);
        
        previewArea.verticalScrollPosition = 0;
        previewArea.horizontalScrollPosition = 0;

        callLater(print);
    }

    public function doTractCentering():void 
    {
        var tractStartPoint:Point = calculateTractCenterPosition(
            dataFrame.width, dataFrame.height);
        
        if (tractMoveEffect.isPlaying) tractMoveEffect.end();
        tractMoveEffect.xTo = tractStartPoint.x;
        tractMoveEffect.yTo = tractStartPoint.y;
        tractMoveEffect.play();
    }

    private function print():void 
    {
        var fpj:FlexPrintJob = new FlexPrintJob();
        
//        var pj:PrintJob = new PrintJob();
        
        var options:PrintJobOptions = new PrintJobOptions();
        options.printAsBitmap = bitmapPrinting.selected;

        var shadowDistance:Number = printPage.getStyle("shadowDistance");
        
        var printRectangle:Rectangle = new Rectangle(
            printPage.borderMetrics.left,
            printPage.borderMetrics.top,
            printPage.width - printPage.borderMetrics.right - shadowDistance,
            printPage.height - printPage.borderMetrics.bottom - shadowDistance);

        if (fpj.start()) {
            try {
                fpj.addObject(printPage, FlexPrintJobScaleType.SHOW_ALL);
//                pj.addPage(printPage, printRectangle, options);
                fpj.send();

            } catch (e:Error) {
            }
        }

        //return to page preview
        printPage.scaleY = printPage.scaleX = previewScale.selectedItem.scaleValue;
        printPage.rotation = 0;
        printPage.setStyle("borderStyle", "solid");
        printPage.setStyle("dropShadowEnabled", true);
        printPage.visible = true;
        
        close();
    }

    private function doPrintPageSizing():void 
    {
        var printPageSize:Rectangle = calculatePrintPageSize();

        var pageVM:EdgeMetrics = printPage.viewMetricsAndPadding;
        var tractStartPoint:Point = calculateTractCenterPosition(
            (printPageSize.width / printPage.scaleX) - (pageVM.left + pageVM.right), 
            (printPageSize.height / printPage.scaleY) - 90 - (pageVM.top + pageVM.bottom) );

        
        pageResizeEffectParalel.widthTo = printPageSize.width;
        pageResizeEffectParalel.heightTo = printPageSize.height;
        
        tractMoveEffectParalel.xTo = tractStartPoint.x;
        tractMoveEffectParalel.yTo = tractStartPoint.y;

        resizeMove.play();
    }

    private function doPrintPageScaling():void 
    {
        pageZoomEffect.zoomWidthTo = previewScale.selectedItem.scaleValue;
        pageZoomEffect.zoomHeightTo = previewScale.selectedItem.scaleValue;
        
        pageZoomEffect.play();
    }
    
/*     private function refreshGraphicScale():void 
    {
        graphicScaleShape.width = dataFrame.Scale.PointsInOneFeet;
        
        var g:Graphics = graphicScaleShape.graphics;
        g.clear();
        g.lineStyle(2, 0, 0.5);
        g.lineTo(graphicScaleShape.width, 0);
    } 
 */    
    private function calculateTractCenterPosition(pageWidth:Number, pageHeight:Number):Point 
    {
        var tractBounds:Rectangle = tractView.getComponentBounds();
        
        var pageMiddlePoint:Point = new Point (
            (pageWidth / dataFrame.scaleX) / 2,
            (pageHeight / dataFrame.scaleY) / 2
        );
        
        var tractMiddlePoint:Point = new Point (
            tractBounds.x + (tractBounds.width / 2),
            tractBounds.y  + (tractBounds.height / 2)
        );

        return new Point (
            pageMiddlePoint.x - (tractMiddlePoint.x * tractView.scaleX),
            pageMiddlePoint.y - (tractMiddlePoint.y * tractView.scaleY)
        );
    }
    
    private function calculatePrintPageSize():Rectangle 
    {
        var shadowDistance:Number = printPage.getStyle("shadowDistance");

        var pageBorderWidth:Number = shadowDistance + printPage.borderMetrics.left + 
            printPage.borderMetrics.right;
        var pageBorderHeight:Number = shadowDistance + printPage.borderMetrics.bottom + 
            printPage.borderMetrics.bottom;
        
        var newPageWidth:Number;
        var newPageHeight:Number;
        
        if (pageOrientation.selectedLabel == "Portrait") 
        {
            newPageWidth = (pageSize.selectedItem.width + pageBorderWidth) * printPage.scaleX;
            newPageHeight = (pageSize.selectedItem.height + pageBorderHeight) * printPage.scaleY;
        } else 
        {
            newPageWidth = (pageSize.selectedItem.height + pageBorderHeight) * printPage.scaleX;
            newPageHeight = (pageSize.selectedItem.width + pageBorderWidth) * printPage.scaleY;
        }
        
        return new Rectangle(0, 0, newPageWidth, newPageHeight);
    }

    public function exportTractToPdf():void
    {
        var oldScale:ScaleValue = dataFrame.Scale;

        //letter page format
        var printTractWidth:Number = (8.5 * 96) - 96;
        var printTractHeight:Number = ((11.69 * 96) * (3/4)) - 96;

        var newScale:ScaleValue = tractView.calcaluateRequiredScaling(
            printTractWidth, printTractHeight, dataFrame.Scale.uom);

        dataFrame.Scale = newScale;

        var event:TractExportEvent = new TractExportEvent(tract, 
            getTractBitmapData(), getScaleBarBitmapData());

        Application.application.dispatchEvent(event);

        dataFrame.Scale = oldScale;
        fitToPage();
    }

    private function getTractBitmapData():BitmapData
    {
        tractView.validateNow();

        var tractBounds:Rectangle = tractView.getComponentBounds();
        if (tractBounds.width == 0) tractBounds.width = 1;
        if (tractBounds.height == 0) tractBounds.height = 1;

	    //TODO: Need refactoring: TractView should follow the correct positioning and sizing
	    var m:Matrix = new Matrix(1,0,0,1, -tractBounds.x, -tractBounds.y);
	    var bd:BitmapData = new BitmapData(tractBounds.width, tractBounds.height, true, 0);
	    bd.draw(tractView, m);

	    return bd;
    }
    
    private function getScaleBarBitmapData():BitmapData
    {
        scaleBar.validateProperties();
        scaleBar.validateSize(true);
        scaleBar.validateDisplayList();
        scaleBar.validateNow();

        return BitmapUtil.getUIComponentBitmapData(scaleBar, new Matrix());
    }
    
    //--------------------------------------------------------------------------
    //
    //  Event handlers
    //
    //--------------------------------------------------------------------------

    protected function view_creationCompleteHandler(event:FlexEvent):void 
    {
        var printPageSize:Rectangle = calculatePrintPageSize();

        printPage.width = printPageSize.width;
        printPage.height = printPageSize.height;
        printPage.scaleX = printPage.scaleY = previewScale.selectedItem.scaleValue;

        printPage.validateNow();
    }

    protected function resizeEffect_endHandler(event:Event):void 
    {
        fitToPage();
    }

    protected function zoomEffect_endHandler(event:Event):void
    {
        previewArea.validateSize();
        previewArea.invalidateDisplayList();
        previewArea.invalidateProperties();
    }

    protected function dataFrame_scaleChangedHandler(event:Event):void 
    {
//        refreshGraphicScale();
    }
    
    protected function pageSize_changeHandler(event:ListEvent):void
    {
        doPrintPageSizing();
    }
    
    protected function previewScale_changeHandler(event:ListEvent):void 
    {
        doPrintPageScaling();
    }
}
}