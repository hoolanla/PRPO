
function DatePicker(thisForm, thisControl) {
    var url = "../controls/Calendar.aspx?FormName=" + thisForm + "&ControlName=" + thisControl;
    //var w   = 500;
    //var h   = 500; 
    var w = 252;
    var h = 229;
    if (document.all) var xMax = screen.width, yMax = screen.height;
    else {
        if (document.layers) var xMax = window.outerWidth, yMax = window.outerHeight;
        else var xMax = 640, yMax = 480;
    }
    if (w > xMax) w = xMax * .9;
    if (h > yMax) h = yMax * .9;
    var l = (xMax - w) / 2, t = (yMax - h) / 2;
    handle_PUH = window.open(url, "DatePicker", 'screenX=' + l + ',left=' + l + ',screenY=' + t + ',top=' + t + ',toolbar=0,location=0,directories=0,status=1,menubar=0,scrollbars=0,resizable=0,fullscreen=0,width=' + w + ',height=' + h);
}

function getIEVersion()
    // Returns the version of Internet Explorer or a -1
    // (indicating the use of another browser).
{
    var rv = -1; // Return value assumes failure
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}

function IECorrectWindowWidth(sWidth) {
    if (getIEVersion() == 7) {
        // window.dialogHeight/Width gives the frame size in IE6. In IE7 it gives you the content area size.
        var estimated_total_chrome_elements_width = 4; // left and right frame edge
        sWidth = parseInt(sWidth) - estimated_total_chrome_elements_width;
    }

    return sWidth;
}
function IECorrectWindowHeight(sHeight) {
    if (getIEVersion() == 7) {
        // window.dialogHeight/Width gives the frame size in IE6. In IE7 it gives you the content area size.
        var estimated_total_chrome_elements_height = 52; // title + status bar
        sHeight = parseInt(sHeight) - estimated_total_chrome_elements_height;
    }

    return sHeight;
}

var __defaultFired = false;
function WebForm_FireDefaultButton(event, target) {
    if (!__defaultFired && event.keyCode == 13 && !(event.srcElement && (event.srcElement.tagName.toLowerCase() == "textarea"))) {
        var defaultButton;
        if (defaultButton && typeof (defaultButton.click) != "undefined") {
            __defaultFired = true;
            defaultButton.click();
            event.cancelBubble = true;
            if (event.stopPropagation) event.stopPropagation();
            return false;
        }
    }
    return true;
}

function DoModalDialog(sURL, sTitle, sWidth, sHeight) {

    var objArgs = new Object();

    // Correct window size for IE 7
    sWidth = IECorrectWindowWidth(sWidth);
    sHeight = IECorrectWindowHeight(sHeight);

    var sFeatures = "edge: raised;help: 0;dialogWidth: " + sWidth + "px;dialogHeight: " + sHeight + "px;scroll:yes;overflow:auto;status: true;";
    objArgs.URL = sURL;
    objArgs.title = sTitle;
    objArgs.opener = window;
    return window.showModalDialog("DialogPage.htm", objArgs, sFeatures);
}

function DoModalDialogRefresh(sURL, sTitle, sWidth, sHeight) {

    var objArgs = new Object();

    // Correct window size for IE 7
    sWidth = IECorrectWindowWidth(sWidth);
    sHeight = IECorrectWindowHeight(sHeight);

    var sFeatures = "edge: raised;help: 0;dialogWidth: " + sWidth + "px;dialogHeight: " + sHeight + "px;scroll:yes;overflow:auto;status: true;";
    objArgs.URL = sURL;
    objArgs.title = sTitle;
    objArgs.opener = window;
    var modal = window.showModalDialog("DialogPage.htm", objArgs, sFeatures);

    //var sFeature = "location=0,toolbar=0, scrollbars=0,status=0, resizable=0, width=" + sWidth + ", height=" + sHeight;

    //var modal = window.open(sURL, "_blank", sFeature, true);

    if (modal == "REGINS") {
        window.location = "url";
        window.location.reload();
    }
}

function DoModalDialogAutoRefresh(sURL, sTitle, sWidth, sHeight) {

    var objArgs = new Object();

    // Correct window size for IE 7
    sWidth = IECorrectWindowWidth(sWidth);
    sHeight = IECorrectWindowHeight(sHeight);

    var sFeatures = "edge: raised;help: 0;dialogWidth: " + sWidth + "px;dialogHeight: " + sHeight + "px;scroll:yes;overflow:auto;status: true;";
    objArgs.URL = sURL;
    objArgs.title = sTitle;
    objArgs.opener = window;
    var modal = window.showModalDialog("DialogPage.htm", objArgs, sFeatures);
    //alert(modal);
    //if(modal=="REGINS")
    //{
    //window.location = "url";
    window.location.reload();
    //}
}

function calendarPicker(strcalender) {
    var objArgs = new Object();
    var ReturnValue;
    var sURL, sHeight, sWidth;
    var sIFURL;
    var parameterstring;

    objArgs.title = "Calendar";
    objArgs.opener = window;
    parameterstring = "txtdatecontrol=" + strcalender;

    sURL = "popCalendar.aspx";
    sIFURL = "";
    sHeight = 335;
    sWidth = 320;

    DoModalDialog(sURL, sIFURL, objArgs, sHeight, sWidth, 0, 0);
}

// [dFilter] - A Numerical Input Mask for JavaScript
// Written By Dwayne Forehand - March 27th, 2003
// Please reuse & redistribute while keeping this notice.

var dFilterStep

function dFilterStrip(dFilterTemp, dFilterMask) {
    dFilterMask = replace(dFilterMask, '#', '');
    for (dFilterStep = 0; dFilterStep < dFilterMask.length++; dFilterStep++) {
        dFilterTemp = replace(dFilterTemp, dFilterMask.substring(dFilterStep, dFilterStep + 1), '');
    }
    return dFilterTemp;
}

function dFilterMax(dFilterMask) {
    dFilterTemp = dFilterMask;
    for (dFilterStep = 0; dFilterStep < (dFilterMask.length + 1) ; dFilterStep++) {
        if (dFilterMask.charAt(dFilterStep) != '#') {
            dFilterTemp = replace(dFilterTemp, dFilterMask.charAt(dFilterStep), '');
        }
    }
    return dFilterTemp.length;
}

function dFilter(key, textbox, dFilterMask) {
    dFilterNum = dFilterStrip(textbox.value, dFilterMask);

    if (key == 9) {
        return true;
    }
    else if (key == 8 && dFilterNum.length != 0) {
        dFilterNum = dFilterNum.substring(0, dFilterNum.length - 1);
    }
    else if (((key > 47 && key < 58) || (key > 95 && key < 106)) && dFilterNum.length < dFilterMax(dFilterMask)) {
        dFilterNum = dFilterNum + String.fromCharCode(key);
    }

    var dFilterFinal = '';
    for (dFilterStep = 0; dFilterStep < dFilterMask.length; dFilterStep++) {
        if (dFilterMask.charAt(dFilterStep) == '#') {
            if (dFilterNum.length != 0) {
                dFilterFinal = dFilterFinal + dFilterNum.charAt(0);
                dFilterNum = dFilterNum.substring(1, dFilterNum.length);
            }
            else {
                dFilterFinal = dFilterFinal + "";
            }
        }
        else if (dFilterMask.charAt(dFilterStep) != '#') {
            dFilterFinal = dFilterFinal + dFilterMask.charAt(dFilterStep);
        }
        //		    dFilterTemp = replace(dFilterTemp,dFilterMask.substring(dFilterStep,dFilterStep+1),'');
    }


    textbox.value = dFilterFinal;
    return false;
}

function replace(fullString, text, by) {
    // Replaces text with by in string
    var strLength = fullString.length, txtLength = text.length;
    if ((strLength == 0) || (txtLength == 0)) return fullString;

    var i = fullString.indexOf(text);
    if ((!i) && (text != fullString.substring(0, txtLength))) return fullString;
    if (i == -1) return fullString;

    var newstr = fullString.substring(0, i) + by;

    if (i + txtLength < strLength)
        newstr += replace(fullString.substring(i + txtLength, strLength), text, by);

    return newstr;
}



// simple RegEx patterns to make life easy.

var reOneOrMoreDigits = /[\d+]/;
var reNoDigits = /[^\d]/gi;
function doMask(textBox) {
    var keyCode = event.which ? event.which : event.keyCode;
    // enter, backspace, delete and tab keys are allowed thru
    if (keyCode == 13 || keyCode == 8 || keyCode == 9 || keyCode == 46)
        return true;
    // get character from keyCode....dealing with the "Numeric KeyPad" 
    // keyCodes so that it can be used
    var keyCharacter = cleanKeyCode(keyCode);
    // grab the textBox value and the mask
    var val = textBox.value;
    var mask = textBox.mask;
    // simple Regex to check if key is a digit
    if (reOneOrMoreDigits.test(keyCharacter) == false)
        return false;
    // get value minus any masking by removing all non-numerics
    val = val.replace(reNoDigits, '');
    // add current keystroke
    val += keyCharacter;
    // mask it...val holds the existing TextBox.value + the current keystroke
    textBox.value = val.maskValue(mask);
    setCaretAtEnd(textBox);

    return false;
}

// puts starting chars in field
function onFocusMask(textBox) {
    var val = textBox.value;
    var mask = textBox.mask;
    if (val.length == 0 || val == null) {
        var i = mask.indexOf('#');
        textBox.value = mask.substring(0, i);
    }

    setCaretAtEnd(textBox);
    // set just in case.
    textBox.maxlength = mask.length;
}
// blank field if no digits entered
function onBlurMask(textBox) {
    var val = textBox.value;
    // if no digits....nada entered.....blank it.
    if (reOneOrMoreDigits.test(val) == false) {
        textBox.value = '';
    }
}
String.prototype.maskValue = function (mask) {
    var retVal = mask;
    var val = this;
    //loop thru mask and replace #'s with current value one at a time
    // better way of doing this ???

    for (var i = 0; i < val.length; i++) {
        retVal = retVal.replace(/#/i, val.charAt(i));
    }

    // get rid of rest of #'s
    retVal = retVal.replace(/#/gi, "");
    return retVal;
}
// The Numeric KeyPad returns keyCodes that ain't all that workable.
//
// ie: KeyPad '1' returns keyCode 97 which String.fromCharCode converts to an 'a'.
//
// This cheesy way allows the Numeric KeyPad to be used
function cleanKeyCode(key) {
    switch (key) {
        case 96: return "0"; break;
        case 97: return "1"; break;
        case 98: return "2"; break;
        case 99: return "3"; break;
        case 100: return "4"; break;
        case 101: return "5"; break;
        case 102: return "6"; break;
        case 103: return "7"; break;
        case 104: return "8"; break;
        case 105: return "9"; break;
        default: return String.fromCharCode(key); break;
    }
}

// From:
// http://www.faqts.com/knowledge_base/view.phtml/aid/1159/fid/130
function setCaretAtEnd(field) {
    if (field.createTextRange) {
        var r = field.createTextRange();
        r.moveStart('character', field.value.length);
        r.collapse();
        r.select();
    }
}

function numbersonly() {
    var keyCode = event.which ? event.which : event.keyCode;
    // enter, backspace, delete and tab keys are allowed thru
    if (keyCode == 13 || keyCode == 8 || keyCode == 9 || keyCode == 46 || keyCode == 37 || keyCode == 39)
        return true;
    if (event.keyCode >= 48 && event.keyCode <= 57)
        return true;

    // simple Regex to check if key is a digit
    var keyCharacter = cleanKeyCode(keyCode);
    if (reOneOrMoreDigits.test(keyCharacter) == false) {
        return false;
    }
    else
        return true;
    return false;
}

function numbersandslash() {
    var keyCode = event.which ? event.which : event.keyCode;
    // enter, backspace, delete and tab keys are allowed thru
    if (keyCode == 13 || keyCode == 8 || keyCode == 9 || keyCode == 46 || keyCode == 191 || keyCode == 111 || keyCode == 37 || keyCode == 39)
        return true;
    if (event.keyCode >= 48 && event.keyCode <= 57)
        return true;

    // simple Regex to check if key is a digit
    var keyCharacter = cleanKeyCode(keyCode);
    if (reOneOrMoreDigits.test(keyCharacter) == false) {
        return false;
    }
    else {
        return true;
    }
    return false;
}
function phonenumber() {
    var keyCode = event.which ? event.which : event.keyCode;
    // enter, backspace, delete, '-','.' and tab keys are allowed thru
    if (keyCode == 16 || keyCode == 13 || keyCode == 8 || keyCode == 9 || keyCode == 46 || keyCode == 191 || keyCode == 111 || keyCode == 189 || keyCode == 190 || keyCode == 107 || keyCode == 187 || keyCode == 57 || keyCode == 48 || keyCode == 37 || keyCode == 39 || keyCode == 32)
        return true;
    if (event.keyCode >= 48 && event.keyCode <= 57)
        return true;

    // simple Regex to check if key is a digit
    var keyCharacter = cleanKeyCode(keyCode);
    if (reOneOrMoreDigits.test(keyCharacter) == false) {
        return false;
    }
    else {
        return true;
    }
    return false;
}
