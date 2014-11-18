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
	var removedFeaturesNodes = xml.getElementsByTagName('RemovedFeatures');
	if (removedFeaturesNodes.length > 0){
	    var features = removedFeaturesNodes[0].getElementsByTagName('Feature');
	    
	    for (var i = 0; i < features.length; i++)
	    {   
	        var featureId = GetInnerText(features[i].getElementsByTagName('Id')[0]);	        

            var table = document.getElementById('m_tblFeatures');
            var rowToDelete = document.getElementById('feature_row_' + featureId);
            if (rowToDelete != null){
                table.deleteRow(rowToDelete.rowIndex);    	 
                var currentControl = document.getElementById(featureId);  
                
	            if (currentControl != null){        	        
	                currentControl.checked = false;	                    	            
	            }                         
            }
                
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

	var combinableProductIds = new Array();	
	var combinableProductsNodes = xml.getElementsByTagName('CombinableProducts');
	if (combinableProductsNodes.length > 0) {
	    var combinableProducts = combinableProductsNodes[0].getElementsByTagName('CombinableProduct');	    
	    for (var i = 0; i < combinableProducts.length; i++) {			
	        combinableProductIds[i] = GetInnerText(combinableProducts[i].getElementsByTagName('Id')[0]);
	    }        			    
	}
	
	var optionalProductIds = new Array();	
	var optionalProductsNodes = xml.getElementsByTagName('OptionalProducts');
	if (optionalProductsNodes.length > 0) {
	    var optionalProducts = optionalProductsNodes[0].getElementsByTagName('OptionalProduct');	    
	    for (var i = 0; i < optionalProducts.length; i++) {			
	        optionalProductIds[i] = GetInnerText(optionalProducts[i].getElementsByTagName('Id')[0]);
	    }        			    
	}
	
	for (i = 0; i < optionalProductIds.length; i++) {
		var combinable = false;
		
		for (j = 0; j < combinableProductIds.length; j++) {
			if (optionalProductIds[i] == combinableProductIds[j]) {
				combinable = true;
				break;
			}
		}
		
		var checkBox = document.getElementById(optionalProductIds[i]);
		if (!checkBox.checked) {
			checkBox.disabled = !combinable;
		}
	}
	
	if (DoesFinalProductExist(xml)) {
		HideMinimalCombinableProducts(optionalProductIds);
		
		if (IsPinAvailable(xml)) {
			EnableProceedButton();
			HideProductMessage();
		} else {
			DisableProceedButton();
			ShowProductMessage('No pin is available for the selected products');
		}
	} else {
		DisableProceedButton();
		ShowMinimalCombinableProducts(xml);
		ShowProductMessage('Select one of the highlighted products to proceed');
	}
	
	var featureTotalNodes = xml.getElementsByTagName('FeaturesTotal');					
	document.getElementById("m_lblTotalUpgrages").innerText = GetInnerText(featureTotalNodes[0]);
	
	var grandTotalNodes = xml.getElementsByTagName('GrandTotal');	
	document.getElementById("m_lblGrandTotal").innerText = GetInnerText(grandTotalNodes[0]);		
	document.forms[0].disabled = false;
}

//
//
//
function DoesFinalProductExist(xml)
{
	var nodeList = xml.getElementsByTagName('FinalProductExists');
	if (nodeList.length > 0) {
		var value = GetInnerText(nodeList[0]);
		return value == 'true';
	}
}

//
//
//
function IsPinAvailable(xml) 
{
	var nodeList = xml.getElementsByTagName('IsPinAvailable');
	if (nodeList.length > 0) {
		var value = GetInnerText(nodeList[0]);
		return value == 'true';
	}
}

//
//
//
function EnableProceedButton()
{
	var button = document.getElementById('m_btnNext');	
	button.disabled = false;
    button.src = "../../images/btn_proceed.gif";	
}

//
//
//
function DisableProceedButton()
{
	var button = document.getElementById('m_btnNext');	
	button.disabled = true;
    button.src = "../../images/btn_proceed_disabled.gif";
}

//
//
//
function HideMinimalCombinableProducts(optionalProductIds) 
{
	for (i = 0; i < optionalProductIds.length; i++) {	
		var productNameCell = document.getElementById('span_pn_' + optionalProductIds[i]);
		var asteriskImg = document.getElementById('img_ast_pn_' + optionalProductIds[i]);
		productNameCell.style.color = '';
		asteriskImg.style.display = 'none';
	}
}

//
//
//
function ShowMinimalCombinableProducts(xml) 
{
	var combinableProductIds = new Array();
	var combinableProductsNodes = xml.getElementsByTagName('MinimalCombinableProducts');
	if (combinableProductsNodes.length > 0) {
		var combinableProducts = combinableProductsNodes[0].getElementsByTagName('MinimalCombinableProduct');
		for (var i = 0; i < combinableProducts.length; i++) {
			combinableProductIds[i] = GetInnerText(combinableProducts[i].getElementsByTagName('Id')[0]);
		}
	}
	
	for (i = 0; i < combinableProductIds.length; i++) {
		var productNameCell = document.getElementById('span_pn_' + combinableProductIds[i]);
		var asteriskImg = document.getElementById('img_ast_pn_' + combinableProductIds[i]);
		productNameCell.style.color = 'red';
		asteriskImg.style.display = 'inline';
	}
}

//
//
//
function ShowProductMessage(message)
{
	var divMessage = document.getElementById('divMessage');
	divMessage.style.display = 'inline';
	
	var snpMessage = document.getElementById('spnMessage');
	snpMessage.innerText = message;
}

//
//
//
function HideProductMessage() 
{
	var divMessage = document.getElementById('divMessage');
	divMessage.style.display = 'none';
}


//
//
//
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
