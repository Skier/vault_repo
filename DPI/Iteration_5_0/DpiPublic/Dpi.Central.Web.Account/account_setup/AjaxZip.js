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

var latestEnteredZip;
function OnZipChanged() 
{    
	var enteredZip = document.getElementById("m_txtZip").value;	
	if (latestEnteredZip == enteredZip){
	    return;
	}
	
	ClearProvidersList();		
	HideLowIncome();	
	CheckButtonsVisibility();
	
	latestEnteredZip = enteredZip;
	document.getElementById("m_lblZipError").innerText = "";
	document.getElementById("m_hdnError").value = "";
	document.getElementById('m_lblWirelessProducts').style.display='none';
	document.getElementById("m_hdnIsShowWirelessString").value = "false";
	
	if (enteredZip.length != 5)
	    return;
	    
	
	if (!IsAllDigits(enteredZip)){
	    document.getElementById("m_lblZipError").innerText = "Incorrect zip";
	    document.getElementById("m_hdnError").value = "Incorrect zip";
	    return;
	} 
	
	var requestUrl = "zip_request_processor.aspx?ZipCode=" + enteredZip;
	CreateXmlHttp();
	
	if(XmlHttp)
	{
		XmlHttp.onreadystatechange = HandleResponse;
		
		XmlHttp.open("GET", requestUrl,  true);
		
		XmlHttp.send(null);		
	}
}


function HandleResponse()
{
	if(XmlHttp.readyState == 4)
	{
		if(XmlHttp.status == 200)
		{
			ClearAndSetProviders(XmlHttp.responseXML.documentElement);
		}
		else
		{
			alert("There was a problem retrieving data from the server." );
		}
	}
}

function ClearAndSetProviders(xml)
{    
    var providerChanged = xml.getElementsByTagName('ProviderChangedOnly');
    if (providerChanged.length > 0) {    
	    var isAllowed = GetInnerText(providerChanged[0]);
	    if (isAllowed == 'true') {
	        ShowLowIncome();
	    } else {
			HideLowIncome(); 
	    }	        	        	        
        CheckButtonsVisibility();
        return;
    }

    var providerList = document.getElementById("m_cmbProviders");
    var cmbProviderValuesVs = document.getElementById("m_hdnProviderValues");
    var cmbProviderTextsVs = document.getElementById("m_hdnProviderTexts");
	
	var providerNameNodes = xml.getElementsByTagName('name');
	var providerCodeNodes = xml.getElementsByTagName('code');
	var errors = xml.getElementsByTagName('error');
	var isLowIncomeAllowed = xml.getElementsByTagName('IsLowIncomeQualifyAllowedForFirstProvider');
	
	if (errors.length != 0){	    
	    document.getElementById("m_lblZipError").innerText = GetInnerText(errors[0]);
	    document.getElementById("m_hdnError").value = document.getElementById("m_lblZipError").innerText;
	    document.getElementById('m_lblWirelessProducts').style.display='block';
	    document.getElementById("m_hdnIsShowWirelessString").value = "true";
	    CheckButtonsVisibility();	
	    return;
	}
	
	var text; 
	var value; 
	var optionItem;
		    
	document.getElementById('m_rowProviderField').style.display='block';
	for (var count = 0; count < providerNameNodes.length; count++)
	{	    
   		text = GetInnerText(providerNameNodes[count]);
   		value = GetInnerText(providerCodeNodes[count]);
   		
		cmbProviderTextsVs.value = cmbProviderTextsVs.value + text + ":";
	    cmbProviderValuesVs.value = cmbProviderValuesVs.value + value + ":";
   		
		optionItem = new Option( text, value,  false, false);
		providerList.options[providerList.length] = optionItem;
	}	
	
	if (isLowIncomeAllowed.length > 0) {
	    var isAllowed = GetInnerText(isLowIncomeAllowed[0]);
	    if (isAllowed == 'true') {
	        ShowLowIncome();
	    } else {
			HideLowIncome();
	    }	        	        	        
	}
	
    CheckButtonsVisibility();	
}

function GetInnerText (node)
{
	 return (node.textContent || node.innerText || node.text) ;
}

function IsAllDigits (zipCode)
{    
	for (var i = 0; i < zipCode.length; i++)
	{
	    if (!isDigit(zipCode.charAt(i))){
	        return false;
	    }
	} 
	
	return true;
}

function isDigit(c)
{
  return "0123456789.".indexOf(c) == -1 ? false : true
}

function OnProviderChanged()
{
    document.getElementById("m_hdnProviderSelectedIndex").value = document.getElementById("m_cmbProviders").selectedIndex;
        
	var requestUrl = "zip_request_processor.aspx?ZipCode=" + document.getElementById("m_txtZip").value 
	    + "&SelectedProvider=" + document.getElementById("m_cmbProviders").value;
	
	CreateXmlHttp();
	
	if(XmlHttp)
	{
		XmlHttp.onreadystatechange = HandleResponse;
		
		XmlHttp.open("GET", requestUrl,  true);
		
		XmlHttp.send(null);		
	}
    
}

function ClearProvidersList(){
    var providerList = document.getElementById("m_cmbProviders");
    
	for (var count = providerList.options.length-1; count >-1; count--)
	{
		providerList.options[count] = null;
	}
	
	document.getElementById("m_hdnProviderValues").value = "";
	document.getElementById("m_hdnProviderTexts").value = "";
	document.getElementById('m_rowProviderField').style.display='none';
}

function HideLowIncome() 
{
	document.getElementById('m_rowLowIncome').style.display = 'none';
	document.getElementById('m_hdnIsLowIncomeRowVisible').value = 'false';
	document.getElementById('m_rowLowIncomeLink').style.display = 'none';
}

function ShowLowIncome() 
{
	document.getElementById('m_rowLowIncome').style.display = 'block';
	document.getElementById('m_hdnIsLowIncomeRowVisible').value = 'true';
	        
    if (document.getElementById('m_rbnLowIncomeYes').checked) {            
        document.getElementById('m_rowLowIncomeLink').style.display = 'block';
    } else {
        document.getElementById('m_rowLowIncomeLink').style.display = 'none'; 
    }
}

function CheckButtonsVisibility(){

    if ((document.getElementById('m_rowLowIncome').style.display == 'none' || (document.getElementById('m_rowLowIncome').style.display == 'block' && (document.all['m_rbnLowIncomeYes'].checked || document.all['m_rbnLowIncomeNo'].checked)))
        && document.getElementById("m_hdnProviderValues").value != "" && document.getElementById('m_rowProviderField').style.display == 'block')
        {
            if (document.getElementById('m_btnNext').disabled == true){
                document.getElementById('m_btnNext').disabled = false;
                document.getElementById('m_btnNext').src = "../images/btn_proceed.gif";
            }
                            
        } else
        {
            if (document.getElementById('m_btnNext').disabled == false){
                document.getElementById('m_btnNext').disabled = true;
                document.getElementById('m_btnNext').src = "../images/btn_proceed_disabled.gif";            
            }
        }
}




