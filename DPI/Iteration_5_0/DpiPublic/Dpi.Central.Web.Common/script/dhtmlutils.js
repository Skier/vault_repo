// set this vaiable
var blankSrc = "images/blank.gif";

// initialize this object in your code
var iconInfos = {
    wait: {height: 16, width: 16, src: null},
    info: {height: 16, width: 16, src: null},

    createImage: function(name){
        var info = this[name];
        if (typeof(info) == "undefined" || !info.src)
            return null;
        var img = ce("img");
        img.border = 0;
        img.alt = "";
        img.height = info.height;
        img.width = info.width;
        img.src = info.src;
        return img;
    }
};

var preloadedImageUrls = [];
var preloadedImages = [];

var opera = navigator.userAgent.toLowerCase().indexOf("opera") != -1;
var ie = !opera && document.all != null;
var moz = !ie && document.getElementById != null && document.layers == null;
var iePngSupported = ie && navigator.platform == "Win32" && /MSIE (5\.5)|[6789]/.test(navigator.userAgent);

if (typeof [].push == 'undefined') {
    Array.prototype.push=function(i) {
        this[this.length] = i;
		return this;
    }
}

if (typeof [].append == 'undefined') {
    Array.prototype.append=function(arr) {
		for (var i = 0; i < arr.length; i++)
			this.push(arr[i]);
		return this;
    }
}

if (typeof [].splice == 'undefined') {
    Array.prototype.splice=function(i,k) {
        for (var z=i; z<this.length-k; z++)
            this[z] = this[z+k];
        this.length = this.length - k;
    }
}

String.prototype.trim=function(){
    return this.replace(/(^\s*)|(\s*$)/g, '');
}

if (typeof document.createElement == "undefined" && (typeof document.createElementNS == 'function' || typeof document.createElementNS == 'object')) {
    document.createElement = function(tagName) {
        return this.createElementNS('http://www.w3.org/1999/xhtml', tagName);
    }
}

/*  Splash message stuff  */

var splashMessage = {

    MIN_TIMEOUT: 400,
    table: null,
    start: null,

    createTable: function(){
        this.table = ce("table", document.body, 0);
        with(this.table){
            className = "waitingMessage";
            cellSpacing = 0;
            cellPadding = 0;
            with(style){
                position = "absolute";
                visibility = "hidden";
                top = "0px";
                left = "0px";
            }
        }
        var row = insertRow(this.table);
        row.className = "tableRow1";
        var imgCell = insertCell(row);
        this.messageCell = insertCell(row, 1);
        this.icon = ce("img", imgCell);
        with(this.icon){
            alt = "";
            border = 0;
            height = 24;
            width = 24;
        }
        this.messageCell.innerHTML = "Please wait...";
    },

    show: function(hideTimeout, iconInfo, centerElement){
        if (this.start)
            return;
        this.start = new Date();
        if (!centerElement)
            centerElement = document.body;
        var pos = findNodePosition(centerElement);
        this.table.style.left = centerElement.clientWidth / 2 - 120 + centerElement.scrollLeft + pos[0];
        this.table.style.top = centerElement.clientHeight / 2 - 50 + centerElement.scrollTop + pos[1];
        if (typeof (iconInfo) == "string") {
            iconInfo = iconInfos[iconInfo];
            if (!iconInfo || !iconInfo.src){
                this.icon.removeAttribute("src");
                iconInfo = null;
            }
        }
        this.icon.style.display = "inline";
        if (iconInfo){
            this.icon.height = iconInfo.height;
            this.icon.width = iconInfo.width;
            this.icon.setAttribute("src", iconInfo.src);
        } else if (!this.icon.src)
            this.icon.style.display = "none";
        this.table.style.visibility = "visible";
        if (hideTimeout)
            window.setTimeout("splashMessage.hide()", hideTimeout);
    },

    hide: function(callback){
        if (!this.start)
            return;
        var duration = new Date() - this.start;
        var hideFunc = new Function("splashMessage.table.style.visibility = \"hidden\";");
        if (duration > this.MIN_TIMEOUT){
            hideFunc();
            if (callback)
                callback();
        } else {
            window.setTimeout(hideFunc, this.MIN_TIMEOUT - duration);
            if (callback)
                window.setTimeout(callback, this.MIN_TIMEOUT - duration + 1);
        }
        this.start = null;
    }
}

/* Tag label */

TagLabel = function(){
    this.DIV_ID = "TagLabelDIV";
    this.DEFAULT_TEXT = "Saving&#0133;";
    this._topDoc = top.document;

    this._div = top.tagLabelDiv;
    if (!this._div){
        this._div = this._topDoc.createElement("div");
        with(this._div){
            id = this.DIV_ID;
            with (style){
                left = 0;
                top = 0;
                color = "white";
                position = "absolute";
                visibility = "hidden";
                backgroundColor = "#cc0000";
                zIndex = 999;
                opacity = 0.75;
                filter = "alpha(opacity=75)";
                fontFamily = "Arial, Tahoma, Verdana, sans-serif";
                fontSize = "12px";
                padding = "1px";
            }
            innerHTML = this.DEFAULT_TEXT;
        }
        this._div.inc = function(){
            var i = 1 * this.getAttribute("showCount") + 1;
            this.setAttribute("showCount", i);
            return i;
        };
        this._div.dec = function(){
            var i = 1 * this.getAttribute("showCount") - 1;
            this.setAttribute("showCount", i);
            return i;
        }
        this._div.texts = [];
        this._div.texts[0] = this.DEFAULT_TEXT;
        this._topDoc.body.insertBefore(this._div, this._topDoc.body.firstChild);
        top.tagLabelDiv = this._div;
    }
}

TagLabel.prototype = {
    show: function(innerHtml){
        if (innerHtml)
            this._div.innerHTML = innerHtml;
        else
            this._div.innerHTML = this.DEFAULT_TEXT;

        this._div.style.top = this._topDoc.body.scrollTop + "px";

        var idx = this._div.inc();
        this._div.texts[idx] = this._div.innerHTML;
        if (idx == 1){
            pushHandler(top, "scroll", this._body_Scroll);
            this._div.style.visibility = "visible";
        }
    },

    hide: function(){
        var idx = this._div.dec();
        this._div.innerHTML = this._div.texts[idx];
        if (idx == 0){
            this._div.style.visibility = "hidden";
            removeHandler(top, "scroll", this._body_Scroll);
        }
    },

    isVisible: function(){
        return this._div.style.visibility == "visible";
    },

    _body_Scroll: function(e){
        this.tagLabelDiv.style.top = this.document.body.scrollTop + "px";
    }
}

/* JS helper */

function isTrue(value){
    switch (typeof value){
        case "string":
            return value.toLowerCase() == "true";
        case "boolean":
            return value;
        case "number":
            return value != 0;
        default:
            return new Boolean(value);
    }
}

/* DOM etc */

function findElementById(parent, id){
    if (document.getElementById && parent.getElementById)
        return parent.getElementById(id);
    else {
        if (parent.all)
            return parent.all.item(id);
        return findElementByIdRecurseDOM(parent, id);
    }
}

function el(id){
	return findElementById(document, id);
}

function ce(tagName, parent, idx){
	var el = document.createElement(tagName.toLowerCase());
	if (parent){
		var l = parent.childNodes.length;
		var eb;
		if (typeof idx == "object" && typeof idx.parentNode == "object" && idx.parentNode == parent) eb = idx;
		else if (typeof idx != "number" || idx > l) eb = null;
		else if (idx < 0) eb = parent.childNodes[0];
		else eb = parent.childNodes[idx];
		if (eb == null)
            parent.appendChild(el);
        else
            parent.insertBefore(el, eb);
	}
	return el;
}

function elTN(tagName){
	var els = document.getElementsByTagName(tagName);
	var ar = [];
	for(var i = 0; i < els.length; i++)
		ar.push(els[i]);
	return ar;
}

function findElementByIdRecurseDOM(parent, id) {
    for (var i = 0; i < parent.childNodes.length; i++) {
        var child = parent.childNodes.item(i);
        if (child.id == id)
            return child;
        child = findElementByIdRecurseDOM(child, id);
        if (child)
            return child;
    }
    return null;
}

function isChildElement(el, parent){
    if (parent.contains)
        return parent.contains(el);
    else{
        while (el.parentNode && el.parentNode != el) {
            if (el.parentNode == parent)
                return true;
            el = el.parentNode;
        }
        return false;
    }
}

function insertRow(table, idx){
	var tb = table.getElementsByTagName("tbody")[0];
	if (!tb) tb = ce("tbody", table, 0);
	return ce("tr", tb, idx);
}

function insertCell(row, idx, isTh){
	return ce(isTh ? "th" : "td", row, idx);
}

function getClosestParentNode(node, parentTagName){
    var nodeType = node.ELEMENT_NODE || 1;
    node = node.parentNode;
    while (node){
        if (node.nodeType == nodeType){
            if (node.tagName == parentTagName)
                return node;
        }
        node = node.parentNode;
    }
    return null;
}

function disableElement(id, value){
    var el = findElementById(document, id);
    if (el)
        el.disabled = value;
}

function findNodePosition(node) {
    if(node.offsetParent) {
        for(var posX = 0, posY = 0; node.offsetParent; node = node.offsetParent) {
            posX += node.offsetLeft;
            posY += node.offsetTop;
        }
        return [posX, posY];
    } else
        return [(node.x ? node.x : 0), (node.y ? node.y : 0)];
}

function getWindowHeight() {
	if (window.self && self.innerHeight)
		return self.innerHeight;
	else if (document.documentElement && document.documentElement.clientHeight)
		return document.documentElement.clientHeight;
	else return 0;
}

/* Events */

function getEventElement(e){
    e = e || window.event;
    if (e){
        var node = e.target || e.srcElement;
        if (!node)
            return null;
        var nodeType = node.ELEMENT_NODE || 1;
        while(node && node.nodeType != nodeType)
            node = node.parentNode;
        return node;
    } else
        return null;
}

function cancelEvent(e){
    e = e || window.event;
    if (!e)
        return false;
    e.returnValue = false;
    e.cancelBubble = true;
    if (typeof e.stopPropagation != "undefined")
        e.stopPropagation();
    if (typeof e.preventDefault != "undefined")
        e.preventDefault();
}

function pushHandler(object, event, handler) {
    if (typeof object.addEventListener != 'undefined')
        object.addEventListener(event, handler, false);
    else if (typeof object.attachEvent != 'undefined')
        object.attachEvent('on' + event, handler);
    else {
        var handlersProp = '_handlerStack_' + event;
        var eventProp = 'on' + event;
        if (typeof object[handlersProp] == 'undefined') {
            object[handlersProp] = [];
            if (typeof object[eventProp] != 'undefined')
                object[handlersProp].push(object[eventProp]);
            object[eventProp] = function(e) {
                var ret = true;
                for (var i = 0; ret != false && i < object[handlersProp].length; i++)
                    ret = object[handlersProp][i](e);
                return ret;
            }
        }
        object[handlersProp].push(handler);
    }
}

function removeHandler(object, event, handler) {
    if (typeof object.removeEventListener != 'undefined')
        object.removeEventListener(event, handler, false);
    else if (typeof object.detachEvent != 'undefined')
        object.detachEvent('on' + event, handler);
    else {
        var handlersProp = '_handlerStack_' + event;
        if (typeof object[handlersProp] != 'undefined') {
            for (var i = 0; i < object[handlersProp].length; i++) {
                if (object[handlersProp][i] == handler) {
                    object[handlersProp].splice(i, 1);
                    return;
                }
            }
        }
    }
}

/*  Service stuff  */

function openURLInOpener(url) {
    if (self.opener && !self.opener.closed)
        self.opener.location=url;
    else
        window.location=url;
}

function escapeUnicode(element, name) {
    var text = element.value;

    var regexp = "[^\u0001-\u007F\u00A0\u00A4\u00A6\u00A7\u00A9\u00AB-\u00AE\u00B0\u00B1\u00B5-\u00B7\u00BB\u0401-\u040C\u040E-\u044F\u0451-\u045C\u045E\u045F\u0490\u0491\u2013\u2014\u2018-\u201A\u201C-\u201E\u2020-\u2022\u2026\u2030\u2039\u203A\u20AC\u2116\u2122]";
    var f = function(c) {return '&&#' + c.charCodeAt(0) + '&&'};

    if ('a'.replace(/./, f) == '&&#97&&')
        text = text.replace(new RegExp(regexp, 'g'), f);
    else {
        regexp = new RegExp(regexp);
        var pos;
        while ((pos = text.search(regexp)) != -1)
            text = text.substr(0, pos) + f(text.substr(pos, 1)) + text.substr(pos + 1);
    }

    if (typeof element._hiddenField == 'undefined') {
        var tmp = document.createElement('input');
        tmp.type = 'hidden';
        element.form.appendChild(tmp);
        tmp.name = element.name;
        element.removeAttribute('name');
        element._hiddenField = tmp;
    }
    element._hiddenField.value = text;
}

function emulateIE(){
    if (moz){
        document.write("<SCRIPT type=\"text/javascript\" src=\"" + getScriptsDir() + "emulateIE.js\"></SCRIPT>");
        document.write("<SCRIPT type=\"text/javascript\"><!--\nemulateHTMLModel();\n// --></SCRIPT>");
    }
}

function getScriptsDir(){
    var scripts = document.getElementsByTagName("script");
    for (var i = 0; i < scripts.length; i++){
        var script = scripts[i];
        var src = script.getAttribute("src");
        if (!src)
            continue;
        src = src.replace(/\\/g, "/");
        var p = src.lastIndexOf("/");
        if (p == -1){
            if (src == "dhtmlutils.js")
                return "";
        } else if (src.substring(p + 1) == "dhtmlutils.js"){
            return src.substring(0, p + 1);
        }
    }
    return null;
}

function getCssRules(tagName, withAttr){
	var rules = document.cssRuleArray;
	if (typeof selectors == "undefined"){
		rules = [];

		function ss(s){
			for (var k = 0; k < s.rules.length; k++)
				rules.push(s.rules[k]);
		}

		for(var i = 0; i < document.styleSheets.length; i++){
			var s = document.styleSheets[i];
			ss(s);
			for (var j = 0; j < s.imports.length; j++)
				ss(s.imports[j]);
		}
		document.cssRuleArray = rules;
	}

	if (typeof tagName != "string")
		return rules;

	var tagRules = [];
	var re = new RegExp("(.+[ \t]+)?" + tagName + "([^A-Z].*)?");
	for(var i = 0; i < rules.length; i++){
		var r = rules[i];
		if (re.test(r.selectorText)){
			if (typeof withAttr == "string"){
				if (r.style[withAttr])
					tagRules.push(r);
			} else
				tagRules.push(r);
		}
	}
	return tagRules;
}

/* Image related */

function applyIePngAlpha(images){
	if (!iePngSupported)
		return;

	function propChange(e) {
		if (!iePngSupported)
			return;

		var pName = e.propertyName;
		if (pName != "src")
			return;

		var img = getEventElement(e);
		if (!new RegExp(blankSrc).test(img.src))
			img.fixImage();
	}

	function fixImg(img){
		img.realSrc = null;
		img.initFilter = img.style.filter;

		img.fixImage =
			function () {
				var src = this.src;

				if (src == this.realSrc) {
					this.src = blankSrc;
					return;
				}

				if (this.realSrc == null || ! new RegExp(blankSrc).test(src)) {
					this.realSrc = src;
				}

				if ( /\.png$/.test(this.realSrc.toLowerCase())) {
					this.src = blankSrc;
					var sizingMethod = this.getAttribute("sizingMethod");
					this.runtimeStyle.filter = this.initFilter +
						" progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + src + "',sizingMethod='" +
						(sizingMethod ? sizingMethod : "scale") + "')";
				} else {
					this.runtimeStyle.filter = this.initFilter;
				}
			};

		pushHandler(img, "propertychange", propChange);

		img.fixImage();
	}

	if (typeof images != "object" || typeof images.length != "number"){
		images = document.images;
	}

	for(var i = 0; i < images.length; i++){
		fixImg(images[i]);
	}
}

function applyIePngAlphaBackground(el){
	if (!iePngSupported)
		return;
	var bgi = el.style.backgroundImage;
	if (!bgi)
		return;
	var mm = /url\(([^)]+)\)/.exec(bgi);
	if (mm && mm.length == 2 && /\.png$/.test(mm[1])){
		el.style.backgroundImage = "";
		el.runtimeStyle.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + mm[1] + "',sizingMethod='scale')";
	}
}

function preloadImages(){
    if (document.images){
        var preloadImg = function(src){
            if (src && typeof(preloadedImages[src]) == "undefined"){
                var img = new Image();
                img.src = src;
                preloadedImages[src] = img;
            }
        }
        for (key in iconInfos){
            preloadImg(iconInfos[key].src);
        }
        for (var i = 0; i < preloadedImageUrls.length; i++){
            preloadImg(preloadedImageUrls[i]);
        }
    }
}

/*  Form related */

function clearForm(theForm, exceptIds){
    for(var i = 0; i < theForm.elements.length; i++){
        var el = theForm.elements[i];
        if (exceptIds) {
            for (var i = 0; i < exceptIds.length; i++) {
                if (el.id == exceptIds[i]){
                    el = null;
                    break;
                }
            }
            if (!el)
                continue;
        }
        switch (el.tagName.toUpperCase()){
            case "INPUT":
                switch (el.type.toLowerCase()){
                    case "text":
                    case "file":
                    case "password":
                        el.value = "";
                        break;
                    case "radio":
                    case "checkbox":
                        el.checked = false;
                        break;
                }
                break;
            case "SELECT":
                el.selectedIndex = 0;
                break;
            case "TEXTAREA":
                el.value = "";
                break;
        }
    }
}

function initFormValues(form, formData){
    for(var key in formData){
        var el = form.elements[key];
        if (!el){
            el = getElementById(key);
            if (!el)
                continue;
        }
        var data = formData[key];
        if (typeof data == "string" || typeof data == "number")
            el.value = data;
        else
            el[data[0]] = data[1];
    }
}

function singleFormSubmit(theForm, submitBtnId){
    var btn = findElementById(theForm, submitBtnId);
    if (btn) {
        btn.disabled = true;
        var field = ce("INPUT", theForm);
        with (field) {
            type = "hidden";
            name = btn.name;
            value = btn.value;
        }
    }
}

function fixIeSelectLists(lists){
	if (!ie)
		return;

	var normStyle = {}
	var hoverStyle = {}

	function getStyle(){
		var found = false;
		var cnt = 0;
		function setB(o, i, str){
			//alert(t + " " + o + " " + i + " " + str)
			if (!o[i]){
				o[i] = str;
				if (str && o == normStyle) found = true;
			}
		}

		var rules = getCssRules("SELECT");
		for (var j = rules.length - 1; j >= 0; j--){
			var r = rules[j];
			var t = r.selectorText.toLowerCase();
			if (t == "select"){
				setB(normStyle, "borderTop", r.style.borderTop);
				setB(normStyle, "borderRight", r.style.borderRight);
				setB(normStyle, "borderBottom", r.style.borderBottom);
				setB(normStyle, "borderLeft", r.style.borderLeft);
				setB(normStyle, "backgroundColor", r.style.backgroundColor);
			} else if (t == "select:hover"){
				setB(hoverStyle, "borderTop", r.style.borderTop);
				setB(hoverStyle, "borderRight", r.style.borderRight);
				setB(hoverStyle, "borderBottom", r.style.borderBottom);
				setB(hoverStyle, "borderLeft", r.style.borderLeft);
				setB(hoverStyle, "backgroundColor", r.style.backgroundColor);
			}
		}
		return found;
	}

	function over(e){
		var lst = getEventElement(e);
		with(lst.span.style){
			borderTop = lst.hoverStyle.borderTop;
			borderRight = lst.hoverStyle.borderRight;
			borderBottom = lst.hoverStyle.borderBottom;
			borderLeft = lst.hoverStyle.borderLeft;
		}
		if (lst.hoverStyle.backgroundColor && lst.normStyle.backgroundColor)
			lst.style.backgroundColor = lst.hoverStyle.backgroundColor;
	}

	function out(e){
		var lst = getEventElement(e);
		with(lst.span.style){
			borderTop = lst.normStyle.borderTop;
			borderRight = lst.normStyle.borderRight;
			borderBottom = lst.normStyle.borderBottom;
			borderLeft = lst.normStyle.borderLeft;
		}
		if (lst.normStyle.backgroundColor)
			lst.style.backgroundColor = lst.normStyle.backgroundColor;
	}

	function fixList(lst){
		lst.normStyle = normStyle;
		lst.hoverStyle = hoverStyle;
		var span = lst.span = ce("span", lst.parentNode, lst);
		span.appendChild(lst);
		with (span.style){
			overflow = "hidden";
			display = "block";
			width = lst.clientWidth + "px";
			borderTop = normStyle.borderTop;
			borderRight = normStyle.borderRight;
			borderBottom = normStyle.borderBottom;
			borderLeft = normStyle.borderLeft;
		}

		with (lst.style){
			if (lst.multiple){
				width = lst.clientWidth + 4 + "px";
				margin = "-3px";
			} else {
				width = lst.clientWidth + 3 + "px";
				margin = "-2px";
			}
		}

		if (hoverStyle){
			lst.attachEvent("onmouseover", over);
			lst.attachEvent("onmouseout", out);
		}
	}

	if (typeof lists != "object")
		lists = document.getElementsByTagName("select");

	if (!lists.length || !getStyle())
		return;

	for(var i = 0; i < lists.length; i++){
		fixList(lists[i]);
	}
}

function fixIeInputs(boxes){
	if (!ie)
		return;

	var normStyle = {}
	var hoverStyle = {}

	function getStyle(){
		var found = false;
		var cnt = 0;
		function setB(o, i, str){
			if (!o[i]){
				o[i] = str;
				if (str && o == hoverStyle) found = true;
			}
		}

		var rules = getCssRules("INPUT");
		for (var j = rules.length - 1; j >= 0; j--){
			var r = rules[j];
			switch (r.selectorText.toLowerCase()){
				case "input":
				case "textarea":
					setB(normStyle, "borderTop", r.style.borderTop);
					setB(normStyle, "borderRight", r.style.borderRight);
					setB(normStyle, "borderBottom", r.style.borderBottom);
					setB(normStyle, "borderLeft", r.style.borderLeft);
					setB(normStyle, "backgroundColor", r.style.backgroundColor);
					break;
				case "input:hover":
				case "textarea:hover":
					setB(hoverStyle, "borderTop", r.style.borderTop);
					setB(hoverStyle, "borderRight", r.style.borderRight);
					setB(hoverStyle, "borderBottom", r.style.borderBottom);
					setB(hoverStyle, "borderLeft", r.style.borderLeft);
					setB(hoverStyle, "backgroundColor", r.style.backgroundColor);
					break;
			}
		}
		return found;
	}
	
	function over(e){
		var box = getEventElement(e);
		with(box.style){
			borderTop = box.hoverStyle.borderTop;
			borderRight = box.hoverStyle.borderRight;
			borderBottom = box.hoverStyle.borderBottom;
			borderLeft = box.hoverStyle.borderLeft;
		}
		if (box.hoverStyle.backgroundColor && box.normStyle.backgroundColor)
			box.style.backgroundColor = box.hoverStyle.backgroundColor;
	}
	
	function out(e){
		var box = getEventElement(e);
		with(box.style){
			borderTop = box.normStyle.borderTop;
			borderRight = box.normStyle.borderRight;
			borderBottom = box.normStyle.borderBottom;
			borderLeft = box.normStyle.borderLeft;
		}
		if (box.normStyle.backgroundColor)
			box.style.backgroundColor = box.normStyle.backgroundColor;
	}

	function fixBox(box){
		box.normStyle = normStyle;
		box.hoverStyle = hoverStyle;

		if (hoverStyle){
			box.attachEvent("onmouseover", over);
			box.attachEvent("onmouseout", out);
		}
	}

	if (typeof boxes != "object"){
		boxes = elTN("input");
		boxes.append(elTN("textarea"));
	}

	if (!boxes.length || !getStyle())
		return;

	for(var i = 0; i < boxes.length; i++){
		fixBox(boxes[i]);
	}
}

//  Modal dialog stuff

/*
call this function just work like window.open(url,name,feature);
however, for IE5.0+, it will open a showModelessDialog window;
and For Gecko(Mozilla or Netscape), the child window will stay on top focus untill user close it.
programmed by hedger (hedger@yahoo-inc.com)
*/

function dialog(url,name,feature,isModal,args){
    if(url==null)
        return false;
    url = url
    if(name==null)
        name="";
    if(feature==null)
        feature="";
    if(window.showModelessDialog) {
        var WindowFeature = new Object();
        WindowFeature["width"] = 400;
        WindowFeature["height"] = 400;
        WindowFeature["left"] = "";
        WindowFeature["top"] = "";
        WindowFeature["resizable"] = "";

        if(feature !=null && feature!=""){
			function isArg(s){
				return /^([a-zA-Z]){1,}=([0-9]){1,}$/.test(s);
			}
            feature = ( feature.toLowerCase()).split(",");
            for(var i=0;i< feature.length;i++){
                feature[i] = feature[i].trim();
                if(isArg(feature[i])){
                    var featureName = feature[i].split("=")[0];
                    var featureValue = feature[i].split("=")[1];
                    if(WindowFeature[featureName]!=null)
                        WindowFeature[featureName] = featureValue;
                }
            }
        }

        if(WindowFeature["resizable"]==1 || WindowFeature["resizable"]=="1" || WindowFeature["resizable"].toString().toLowerCase()=="yes")
            WindowFeature["resizable"] = "resizable:1;minimize:1;maximize:1;"
        if(WindowFeature["left"]!="")
            WindowFeature["left"] ="dialogLeft:" +  WindowFeature["left"] +"px;";
        if(WindowFeature["top"]!="")
            WindowFeature["top"] ="dialogTop:" +  WindowFeature["Top"] +"px;";
        if(window.ModelessDialog == null)
            window.ModelessDialog = new Object();
        if(name!=""){
            if(window.ModelessDialog[name] !=null && !window.ModelessDialog[name].closed){
                window.ModelessDialog[name].focus();
                return window.ModelessDialog[name];
            }
        }
        var F = WindowFeature["left"] + WindowFeature["top"] + "dialogWidth:"+WindowFeature["width"] +" px;dialogHeight:"+
            WindowFeature["height"]+"px;center:1;help:0;" + WindowFeature["resizable"] +"status:0;unadorned:0;edge: raised; ;border:thick;"
        if(isModal){
            return window.showModalDialog(url,args,F);
        } else {
            window.ModelessDialog[name] = window.showModelessDialog(url,args,F);
            return window.ModelessDialog[name];
        }
    } else {
        if(document.getBoxObjectFor){
            if(isModal){
                window.ModalWindow = window.open(url,name,"modal=1," + feature);
                window.ModalWindow.dialogArguments = args;
                window.ModalFocus = function(){
                    if (ModalWindow){
                        if(!ModalWindow.closed)
                            ModalWindow.focus();
                        else{
                            window.ModalWindow = undefined;
                            window.removeEventListener(window.ModalFocus, "focus", false);
                            window.ModalFocus = undefined;
                        };
                    }
                }
                window.addEventListener("focus", window.ModalFocus, false);
                return false;
            }else{
                var w = window.open(url,name,"modal=1," + feature);
                w.dialogArguments = args;
                return w;
            }
        } else {
            var w = window.open(url,name,feature);
            w.dialogArguments = args;
            return w;
        }
    }
    return null;
}

function modal(url,feature,args){
    return dialog(url,"",feature,true,args);
}

/* Debugging */

function getObjProps(o){
	var s = (typeof o).toLowerCase();
	switch(s){
		case "string":
		case "number":
		case "boolean":
			s += ": " + o;
			break;
		case "undefined":
			break;
		case "object":
			for(k in o)
				s += "\no." + k + " = " + o[k];
			break;
		case "function":
			s += ":\n" + o;
			break;
		default:
			s += ": unknown type";
			break;
	}
	return s;
}
