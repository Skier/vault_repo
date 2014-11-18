package com.llsvc.coverage.storage
{
import com.llsvc.domain.CoverageTract;

import mx.rpc.Responder;

public interface ICoverageStorage
{
    function findCoverageTracts(mask:CoverageTract, responder:Responder):void;
    
    function saveCoverageTract(coverage:CoverageTract, responder:Responder):void;

    function removeCoverageTract(coverageTractId:int, responder:Responder):void;
    
}
}