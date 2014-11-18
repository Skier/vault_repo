package com.llsvc.util
{
import mx.collections.ArrayCollection;
import mx.controls.Alert;

public class TractDescriptionUtil
{

    public static function parse(description:String):ArrayCollection {
        var result:ArrayCollection = new ArrayCollection();
        // strip comments (comment is everything in round brackets (blah, blahh..))
        description = description.replace(/\(.*?\)/gi, "");
        description = description.replace(/lots/gi, "");
        description = description.replace(/lot/gi, "");
//Alert.show("TractDescriptionUtil.parse: description=" + description);     
        var tokens:Array = description.split(/[,;.&]/gi);
        for each (var t:String in tokens) {
            var tr:ArrayCollection = new ArrayCollection();
            process(StringUtil.trim(t), tr);
            for each (var e:String in tr) {
                result.addItem(e);
            }
        }
        return result;
    }

    public static function process(part:String, outResult:ArrayCollection):void {
        var reg:RegExp = /[NSWE]{1}[WE]?\/?[24]?/gi;
        var fag:Array = reg.exec(part);
//Alert.show("process: fag=" + fag);            
        if ( null != fag ) {
            var bag:Array = reg.exec(part);
//Alert.show("process: bag=" + bag);            
            if ( null != bag ) {
                generate(bag[0], outResult);
                truncate(fag[0], bag[0], outResult);
            } else {
                generate(fag[0], outResult);
            }
        } else {
            generate(part, outResult);
        }
    }
    
    protected static function halfGenerator(d1:String, outResult:ArrayCollection):void {
        if ( "S" == d1 || "N" == d1 ) {
            outResult.addItem("NW"+d1+"W");
            outResult.addItem("NE"+d1+"W");
            outResult.addItem("NW"+d1+"E");
            outResult.addItem("NE"+d1+"E");
            outResult.addItem("SW"+d1+"W");
            outResult.addItem("SE"+d1+"W");
            outResult.addItem("SW"+d1+"E");
            outResult.addItem("SE"+d1+"E");
        } else {
            outResult.addItem("NWN"+d1);
            outResult.addItem("NEN"+d1);
            outResult.addItem("SWN"+d1);
            outResult.addItem("SEN"+d1);
            outResult.addItem("NWS"+d1);
            outResult.addItem("NES"+d1);
            outResult.addItem("SWS"+d1);
            outResult.addItem("SES"+d1);
        }
    }
    
    protected static function quoterGenerator(d2:String, outResult:ArrayCollection):void {
        outResult.addItem("NW"+d2);
        outResult.addItem("NE"+d2);
        outResult.addItem("SW"+d2);
        outResult.addItem("SE"+d2);
    }
    
    protected static function isDirection(token:String):Boolean {
        return (token == "N" || token == "S" || token == "W" || token == "E");
    }
    
    protected static function isAxis(d1:String, d2:String):Boolean {
        if ( d1 == "N" || d1 == "S" ) {
            return (d2 == "N" || d2 == "S");
        } else {
            return (d2 == "W" || d2 == "E");
        }
    }
    
    public static function generate(partDesc:String, outResult:ArrayCollection):void {
//      Alert.show("TractDescriptionUtil.generate: partDesc=" + partDesc + ", index of the / =" + partDesc.indexOf("/"));
        if ( "ALL" == partDesc ) {
            halfGenerator("N", outResult);
            halfGenerator("S", outResult);
        } else {
            var f:String = "";
            var s:String = "";
            if ( partDesc.length >= 2 ) {
                f = partDesc.substr(0, 1);
                s = partDesc.substr(1, 1);
            } else if ( partDesc.length == 1 ) {
                f = partDesc;
            }
            
            if ( isDirection(f) && isDirection(s) ) {
                quoterGenerator(f+s, outResult);
            } else if ( isDirection(f) ) {
                halfGenerator(f, outResult);
            } else {
                var qqTest:RegExp = /[NSEW]{4}/gi; 
//Alert.show("match?:" + qqTest.test(partDesc));
                if ( qqTest.test(partDesc) ) {
                    outResult.addItem(partDesc);
                } else {
                    var lot:String = partDesc.replace(/[^0-9]*/gi, "");
                    if ( "" != lot ) {
                        outResult.addItem(lot);
                    }
                }   
            }
        }
    }

    public static function truncate(filter:String, base:String, outResult:ArrayCollection):void {
        var preparedFilter:String = "";
        var filterStart:int = 0;
        var f:String = "";
        var s:String = "";
        if ( filter.length >= 2 ) {
            f = filter.substr(0, 1);
            s = filter.substr(1, 1);
        } else if ( filter.length == 1 ) {
            f = filter;
        }
        
        if ( isDirection(f) && isDirection(s) ) {
            preparedFilter = f+s;
        } else if ( isDirection(f) ) {
            preparedFilter = f;
            if ( "N" == preparedFilter || "S" == preparedFilter ) {
                filterStart = 0;
            } else {
                filterStart = 1;
            }
            
            // where to apply filter depends from base  
            var fb:String = "";
            var sb:String = "";
            if ( base.length >= 2 ) {
                fb = base.substr(0, 1);
                sb = base.substr(1, 1);
            } else if ( base.length == 1 ) {
                fb = base;
            }
            if ( !isDirection(sb) && !isAxis(f, fb) ) {
                filterStart += 2;   
            }
        }
             
        var arr:Array = outResult.toArray();
        for each (var qq:String in arr) {
//Alert.show("TractDescriptionUtil.truncate: qq=" + qq + ", preparedFilter=" + preparedFilter + ", filterStart=" + filterStart + ", remove?=" + (preparedFilter != qq.substr(filterStart, preparedFilter.length)));
            if ( preparedFilter != qq.substr(filterStart, preparedFilter.length) ) {
                outResult.removeItemAt(outResult.getItemIndex(qq));
            }
        }
    }
}

}
