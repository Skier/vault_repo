package com.tmnc.components.treeDataClasses
{
public interface ITreeItem
{
    
    function get itemId():String;
    
    function get parentId():String;

    function get hasChildren():Boolean;
    function set hasChildren(value:Boolean):void;
    
    function get isOpened():Boolean;
    function set isOpened(value:Boolean):void;
    
    function get depthLevel():int;
    function set depthLevel(value:int):void;
    
}
}