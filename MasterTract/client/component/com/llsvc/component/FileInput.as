package com.llsvc.component
{
import flash.net.FileReference;
import flash.net.URLRequest;
import mx.containers.HBox;  
import mx.controls.Image;
import mx.controls.Alert;
import mx.controls.Button;
import flash.events.TextEvent;
import flash.events.MouseEvent;
import flash.events.KeyboardEvent;
import flash.events.FocusEvent;
import flash.events.Event;
import flash.ui.Keyboard;
import mx.formatters.NumberFormatter;
import mx.core.UIComponent;

[Event(name="inputMaskEnd")]

public class FileInput extends HBox
{
    [Embed(source="/assets/attach_delete.png")]
    [Bindable]
    private var deleteImg:Class;
    
    [Embed(source="/assets/attach_upload.png")]
    [Bindable]
    private var uploadImg:Class;
    
    [Embed(source="/assets/attach_download.png")]
    [Bindable]
    private var downloadImg:Class;
    
    public var fileInput:FileReference;
    public var uploadButton:Image;    
    public var downloadButton:Image;    
    public var deleteButton:Image;    
	public var anchor:String;
	    
    public function FileInput() {
        super();
        
        fileInput = new FileReference();
        fileInput.addEventListener(Event.SELECT, onFileInputSelect);
        fileInput.addEventListener(Event.COMPLETE, onFileInputComplete);

        uploadButton = new Image();
        uploadButton.source = uploadImg;
        addChild(uploadButton);
        uploadButton.addEventListener(MouseEvent.CLICK, onUploadButtonClick);
        
        downloadButton = new Image();
        downloadButton.source = downloadImg;
        addChild(downloadButton);
        downloadButton.addEventListener(MouseEvent.CLICK, onDownloadButtonClick);

        deleteButton = new Image();
        deleteButton.source = deleteImg;
        addChild(deleteButton);
        deleteButton.addEventListener(MouseEvent.CLICK, onDeleteButtonClick);
    }

    private function onUploadButtonClick(event:Event):void {
        fileInput.browse();
    }

    private function onDownloadButtonClick(event:Event):void {
        var ur:URLRequest = new URLRequest("download?anchor=" + anchor);
        flash.net.navigateToURL(ur, "_blank");
    }

    private function onDeleteButtonClick(event:Event):void {
//        fileInput.browse();
    }

    private function onFileInputSelect(event:Event):void {
//        Alert.show("FileInput.onFileInputSelect:");
        var request:URLRequest = new URLRequest("upload?anchor=" + anchor)
        try {
            fileInput.upload(request);
        } catch (error:Error) {
            Alert.show("Unable to upload file.");
        }
    }
    
    private function onFileInputComplete(event:Event):void {
        Alert.show("FileInput.onFileInputComplete:");
    }
    
}
}
