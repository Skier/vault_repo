/////////////////////////////////////////////////////////////////////////////////
// PhoneNumberBox function
//
function AllowJumpForward()
{ 
	if (window.event.keyCode != 8 && window.event.keyCode != 9 && window.event.keyCode != 16) {
		var keySymbol = String.fromCharCode(event.keyCode);
		if (isChar(keySymbol)) {
			return true;		
		}
	}
	
	return false;
}
/////////////////////////////////////////////////////////////////////////////////
// PhoneNumberBox function
//
function SetJumpForwardDown(fromCtrl, length, toCtrl)
{ 
	if (AllowJumpForward()) {
		if (fromCtrl.length > length && GetSelection() != fromCtrl) {
			document.getElementById(toCtrl).focus();
		}
	}
}
/////////////////////////////////////////////////////////////////////////////////
// PhoneNumberBox function
//
function SetJumpForwardUp(fromCtrl, length, toCtrl)
{ 
	if (AllowJumpForward()) {
		if (fromCtrl.length == (length + 1) && GetSelection() != fromCtrl) {
			document.getElementById(toCtrl).focus();
		}
	}
}
/////////////////////////////////////////////////////////////////////////////////
// PhoneNumberBox function
//
function SetJumpBackward(fromCtrl, toCtrl)
{ 
	if (window.event.keyCode == 8) { // backspace
		if (fromCtrl.length == 0) { 
			var ctrl = document.getElementById(toCtrl);
			var rng = ctrl.createTextRange();
			var pos = ctrl.value.length;
			if (pos != -1 && rng) {
				rng.moveStart("character", pos + 1);
				rng.collapse();
				rng.select();
			}
		}
	}
}
/////////////////////////////////////////////////////////////////////////////////
// PhoneNumberBox function
//
function ValidatePhoneNumber(source, arguments)
{
	if (source.controltovalidate) {		
		ctrlPhNum1Id = source.controltovalidate + "_area_code";
		ctrlPhNum2Id = source.controltovalidate + "_prefix";
		ctrlPhNum3Id = source.controltovalidate + "_line_number";
		
		arguments.IsValid = 
			ValidateWithReguralExpression(ctrlPhNum1Id, "^\\d{3}$") && 
			ValidateWithReguralExpression(ctrlPhNum2Id, "^\\d{3}$") && 
			ValidateWithReguralExpression(ctrlPhNum3Id, "^\\d{4}$");	
	}		
}
/////////////////////////////////////////////////////////////////////////////////
// Helper function
//
function GetSelection()
{
	var txt = '';
	if (window.getSelection)
	{
		txt = window.getSelection();
	}
	else if (document.getSelection)
	{
		txt = document.getSelection();
	}
	else if (document.selection)
	{
		txt = document.selection.createRange().text;
	}
	else return;
	
	return txt;
}
/////////////////////////////////////////////////////////////////////////////////
// Helper function
//
function ValidateWithReguralExpression(ctrlId, re)
{
	var value = ValidatorGetValue(ctrlId);	
	if (ValidatorTrim(value).length == 0) {
		return false;
	}
	
	var rx = new RegExp(re);
	var matches = rx.exec(value);				
	var res = matches != null && value == matches[0];
	return res;
}
/////////////////////////////////////////////////////////////////////////////////
// Helper function
//
function isChar(sChar) 
{
	return (sChar>="a" && sChar<="z") || (sChar>="A" && sChar<="Z") || (sChar>="0" && sChar <="9");
}
/////////////////////////////////////////////////////////////////////////////////
// Effect function
//
function ToggleProductDescription(panelId, durationValue)
{
	Effect.toggle(panelId, 'Appear', {duration: durationValue});
}
/////////////////////////////////////////////////////////////////////////////////
// Effect function
//
function TogglePanel(panelId, durationValue, triggerIdValue)
{
	scrollDelay = 2;
	scrollStep = 10;				
	triggerId = triggerIdValue;
	
	Effect.toggle(panelId, 'slide', {duration: durationValue, afterFinish:ScrollCallBack, beforeStart: DisableTriggerCallBack});
}
///////////////////////////////////////////////////////////////////////////////
// Effect function 
//
function DisableTriggerCallBack(obj)
{
	document.getElementById(triggerId).disabled = true;	
}
///////////////////////////////////////////////////////////////////////////////
// Effect function 
//
function ScrollCallBack(obj)
{
	scrollTimeout = setTimeout('ScrollPage()', scrollDelay);	
	
	oldScrollTop = f_scrollTop();	
	
	document.getElementById(triggerId).disabled = false;	
}
/////////////////////////////////////////////////////////////////////////////////
// Effect function
//
function ScrollPage() 
{
	window.scrollBy(0, scrollStep);			
	
	if (f_scrollTop() == oldScrollTop)
	{
		window.scrollBy(0, scrollStep * 2);				
		window.scrollBy(0, scrollStep);				
		clearTimeout(scrollTimeout);
	}
	else {
		oldScrollTop = f_scrollTop();
		scrollTimeout = setTimeout('ScrollPage()', scrollDelay);
	}
}
/////////////////////////////////////////////////////////////////////////////////
// Helper function
//
function f_scrollTop() {
	return f_filterResults (
		window.pageYOffset ? window.pageYOffset : 0,
		document.documentElement ? document.documentElement.scrollTop : 0,
		document.body ? document.body.scrollTop : 0
	);
}
/////////////////////////////////////////////////////////////////////////////////
// Helper function
//
function f_filterResults(n_win, n_docel, n_body) {
	var n_result = n_win ? n_win : 0;
	if (n_docel && (!n_result || (n_result > n_docel)))
		n_result = n_docel;
	return n_body && (!n_result || (n_result > n_body)) ? n_body : n_result;
}		
/////////////////////////////////////////////////////////////////////////////////
// Helper function
//
function ConfirmAction() 
{
	return confirm('Are you sure?');
}
/////////////////////////////////////////////////////////////////////////////////
// Helper function
//
function ClickButtonOnEnter(e, buttonId)
{ 
      var button = document.getElementById(buttonId); 
      
      if (typeof button == 'object')
      { 
            if (navigator.appName.indexOf("Netscape") > (-1)) { 
                  if (e.keyCode == 13) { 
                        button.click(); 
                        return false; 
                  } 
            } 
            
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) { 
                  if (window.event.keyCode == 13) { 
                        button.click(); 
                        return false; 
                  } 
            } 
      } 
}
/////////////////////////////////////////////////////////////////////////////////
// Used in Account Setup Process
//
function OpenLifeLineApplication() 
{
	var load = window.open('Lifeline%20Application.pdf','','scrollbars=no,menubar=no,height=600,width=800,resizable=yes,toolbar=no,location=no,status=no');
}
