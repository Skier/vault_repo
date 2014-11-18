var XmlHttp;

function CreateXmlHttp()
{
	try
	{
		XmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
	}
	catch(e)
	{
		try
		{
			XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
		} 
		catch(oc)
		{
			XmlHttp = null;
		}
	}
	
	if(!XmlHttp && typeof XMLHttpRequest != "undefined") 
	{
		XmlHttp = new XMLHttpRequest();
	}
}

function HandleResponse()
{
	if(XmlHttp.readyState == 4)
	{
		if(XmlHttp.status == 200)
		{
			ProcessResponse(XmlHttp.responseXML.documentElement);
		}
		else
		{
			alert("There was a problem retrieving data from the server." );
		}
	}
}


function OnServiceChanged(productId) 
{
    var requestUrl;
    document.forms[0].disabled = true;
    
    if (document.getElementById(productId).checked)    
	    requestUrl = "select_services_request_processor.aspx?Operation=Add&ServiceId=" + productId;
	else 
	    requestUrl = "select_services_request_processor.aspx?Operation=Remove&ServiceId=" + productId;
	
	CreateXmlHttp();
	
	if(XmlHttp)
	{
		XmlHttp.onreadystatechange = HandleResponse;
		
		XmlHttp.open("GET", requestUrl,  true);
		
		XmlHttp.send(null);		
	}
}


function ProcessResponse(xml)
{    		
	var featuresStatusNodes = xml.getElementsByTagName('FeaturesStatus');
	if (featuresStatusNodes.length > 0){
	    var features = featuresStatusNodes[0].getElementsByTagName('Feature');
                
	    for (var i = 0; i < features.length; i++)
	    {   
	        var featureId = GetInnerText(features[i].getElementsByTagName('Id')[0]);
	        var featureIsChecked = GetInnerText(features[i].getElementsByTagName('IsChecked')[0]);
	        var featureIsEnabled = GetInnerText(features[i].getElementsByTagName('IsEnabled')[0]);	        
	        
		    var currentControl = document.getElementById(featureId);
		    
	        if (currentControl != null){
    	        
	            if (featureIsChecked == 'true'){	            
	                currentControl.checked = true;	            
	            }	            
	            else{
	                currentControl.checked = false;	            
	            }
    	            
	            if (featureIsEnabled == 'true'){	            
	                currentControl.parentNode.disabled = false;
	                currentControl.disabled = false;
	            }	            
	            else{
	                currentControl.parentNode.disabled = true;
	                currentControl.disabled = true;
	            }	            
	        }
	    }        		
	}


	var removedFeaturesNodes = xml.getElementsByTagName('RemovedFeatures');
	if (removedFeaturesNodes.length > 0){
	    var features = removedFeaturesNodes[0].getElementsByTagName('Feature');
	    
	    for (var i = 0; i < features.length; i++)
	    {   
	        var featureId = GetInnerText(features[i].getElementsByTagName('Id')[0]);	        

            var table = document.getElementById('m_tblFeatures');
            var rowToDelete = document.getElementById('feature_row_' + featureId);
            if (rowToDelete != null)
                table.deleteRow(rowToDelete.rowIndex);    	            
	    }        			    
	}

	
	var addedFeaturesNodes = xml.getElementsByTagName('AddedFeatures');
	if (addedFeaturesNodes.length > 0){
	    var features = addedFeaturesNodes[0].getElementsByTagName('Feature');
	    
	    for (var i = 0; i < features.length; i++)
	    {   
	        var id = GetInnerText(features[i].getElementsByTagName('Id')[0]);	        
	        var name = GetInnerText(features[i].getElementsByTagName('Name')[0]);
	        var price = GetInnerText(features[i].getElementsByTagName('Price')[0]);	        
	        var isRemoveAllowed = GetInnerText(features[i].getElementsByTagName('IsRemoveAllowed')[0]);                
	        var prefix = GetInnerText(features[i].getElementsByTagName('Prefix')[0]);
	        var nameCssClass = GetInnerText(features[i].getElementsByTagName('NameCssClass')[0]);
	        var priceCssClass = GetInnerText(features[i].getElementsByTagName('PriceCssClass')[0]);
	        var removeCssClass = GetInnerText(features[i].getElementsByTagName('RemoveCssClass')[0]);
	        
            AddRow(id, name, price, isRemoveAllowed, prefix, nameCssClass, priceCssClass, removeCssClass);
	    }        			    
	}
							
	var featureTotalNodes = xml.getElementsByTagName('FeaturesTotal');					
	document.getElementById("m_lblTotalUpgrages").innerText = GetInnerText(featureTotalNodes[0]);
	
	var grandTotalNodes = xml.getElementsByTagName('GrandTotal');	
	document.getElementById("m_lblGrandTotal").innerText = GetInnerText(grandTotalNodes[0]);		
	document.forms[0].disabled = false;
}

function AddRow(id, name, price, isRemoveAllowed, prefix, nameCssClass, priceCssClass, removeCssClass)
{
    var table = document.getElementById('m_tblFeatures');
    
    var row = table.insertRow(table.rows.length);
    row.id = prefix + id;
    
    var nameCell = row.insertCell(0);
    nameCell.innerHTML = name;
    nameCell.className = nameCssClass;
    
    var removeCell = row.insertCell(1);
    removeCell.className = removeCssClass;
     
    if (isRemoveAllowed == 'true'){
        removeCell.innerHTML = "<a href=\"javascript:RemoveRow('" + id + "')\">remove</a>";        
    }
    
    var priceCell = row.insertCell(2);
    priceCell.innerHTML = price;
    priceCell.className = priceCssClass;
}

function RemoveRow(featureId){       
    if (document.forms[0].disabled == true)
        return;
    
    document.forms[0].disabled = true;     
	var requestUrl = "select_services_request_processor.aspx?Operation=Remove&ServiceId=" + featureId;	

	CreateXmlHttp();	
	if(XmlHttp)
	{
		XmlHttp.onreadystatechange = HandleResponse;		
		XmlHttp.open("GET", requestUrl,  true);		
		XmlHttp.send(null);		
	}    
}

function GetInnerText (node)
{
	 return (node.textContent || node.innerText || node.text) ;
}
