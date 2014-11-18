package com.llsvc.maps.storage
{
import com.llsvc.domain.Layer;

import mx.rpc.Responder;

public interface IStorage
{
    function getSRSes(responder:Responder):void;
    
    function getLayers(responder:Responder):void;
    
    function saveLayer(layer:Layer, responder:Responder):void;

    function removeLayer(layerId:int, responder:Responder):void;
    
}
}