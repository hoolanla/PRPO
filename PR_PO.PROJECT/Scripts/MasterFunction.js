var prmAccID = null, inputObjectAccID, funcObjectAccID, _GlobalAccIdValue, _GlobalAccNameValue;
var prmCustID = null, inputObjectCustID, funcObjectCustID, _GlobalCustIdValue, _GlobalCustNameENValue, _GlobalCustNameTHValue, _GlobalCusCIFIDValue;
var prmStockID = null, inputObjectStockID, funcObjectStockID, _GlobalStockIdValue, _GlobalStockNameValue;
var prmCIFID = null, inputObjectCIFID, funcObjectCIFID, _GlobalAuthorizeCIFIDValue, _GlobalAuthorizeNameValue;

var prmEmail = null, inputObjectEmail, funcObjectEmail, _GlobalEmailIDValue, _GlobalEmailNameValue;

var prmCustID_PrintDepositCard = null
    , inputObject_PrintDepositCard_CustID
    , funcObject_PrintDepositCard_CustID
    , _Global_PrintDepositCard_ClientIDValue
    , _Global_PrintDepositCard_CustNameENValue
    , _Global_PrintDepositCard_CustNameTHValue
    , _Global_PrintDepositCard_ALL_AccIDValue;

var prmEmpID = null, inputObjectEmpID, funcObjectEmpID, _GlobalEmpIdValue, _GlobalEmpNameENValue, _GlobalEmpNameTHValue, _GlobalEmpBranchValue, _GlobalEmpDepartmentValue, _GlobalEmpPositionENValue, _GlobalEmpPositionTHValue;
var prmDeptID = null, inputObjectDeptID, funcObjectDeptID, _GlobalDeptIdValue, _GlobalDeptCodeValue, _GlobalDeptNameValue;



/*---------------------------------- Loading Spin -------------------------------------------*/

var opts = {
    lines: 13, // The number of lines to draw
    length: 30, // The length of each line
    width: 30, // The line thickness
    radius: 15, // The radius of the inner circle
    corners: 1, // Corner roundness (0..1)
    rotate: 35, // The rotation offset
    direction: 1, // 1: clockwise, -1: counterclockwise
    color: '#70cde3', // #rgb or #rrggbb or array of colors
    speed: 0.8, // Rounds per second
    trail: 75, // Afterglow percentage
    shadow: false, // Whether to render a shadow
    hwaccel: false, // Whether to use hardware acceleration
    className: 'spinner', // The CSS class to assign to the spinner
    //zIndex: 2e9, // The z-index (defaults to 2000000000)
    zIndex: 2000000000, // The z-index (defaults to 2000000000)
    top: '50%', // Top position relative to parent
    left: '50%' // Left position relative to parent
};


//var spinner;

function startLoading() {
    var loading = $("#loading");
    var intervalValue = setInterval(function () {
        loading.fadeIn();
        clearInterval(intervalValue);
    }, 0);

    //spinner = new Spinner(opts).spin(loading[0]);
}

function stopLoading() {

    var loading = $("#loading");
    var intervalValue = setInterval(function () {
        //spinner.stop();
        loading.fadeOut();
        clearInterval(intervalValue);
    }, 2000);

}

/*-------------------------------------------------------------------------------------------*/

/*-------------------------------- JqGrid Function ------------------------------------------*/

var objRender = {
    root: function (obj) { return obj.d.data; },
    page: function (obj) { return obj.d.page; },
    total: function (obj) { return obj.d.total; },
    records: function (obj) { return obj.d.records; },
    repeatitems: false,
};

function JqGridGroupHeader(object, groupOption, colStyle, addMore) {

    if (!addMore || addMore == null || addMore == "") {
        object.jqGrid('destroyGroupHeader');
    }

    if (!colStyle || colStyle == null || colStyle == "") {
        colStyle = false;
    }
    else {
        colStyle = true;
    }

    object.jqGrid('setGroupHeaders', {
        useColSpanStyle: colStyle,
        groupHeaders: groupOption
    });
}

function reDefineColWidth(grid, columnWidths) {

    var widthsArr = columnWidths.split('|');

    for (var j = 0; j < grid.find("tr:eq(0) td").length ; j++) {
        grid.closest(".ui-jqgrid-view").find('.ui-jqgrid-labels > th:eq(' + j + ')').css('width', widthsArr[j]); // will set the column header widths
        grid.find("tr").find('td:eq(' + j + ')').each(function () { $(this).css('width', widthsArr[j]); }) // will set the column widths
    }
}

/*-------------------------------------------------------------------------------------------*/


/*------------------------------ Select 2 By ACC ID ------------------------------------------*/

function SetSelect2AccID(inputObject, funcObject) {

    inputObjectAccID = inputObject;
    funcObjectAccID = funcObject;

    AddEndRequest();

    var _url = 'WebMethodFunction.aspx/GetAutoCompleteAccount';

    $(inputObjectAccID).select2({
        placeholder: "Please fill account id or account name."
        , width: "100%"
        , minimumInputLength: 3
        , allowClear: true
        , ajax: {
            transport: function (options) {
                return $.ajax($.extend({}, options, { global: false }));
            },
            type: "POST",
            url: _url,
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            data: function (term, page) {
                return JSON.stringify({
                    keySearch: term
                    , page_limit: 10,
                    page: page
                });
            },
            quietMillis: 100,
            results: function (data, page) {
                return { results: data.d };
            }
        }
        , formatResult: FormatResultAccID
        , formatSelection: FormatSelectionAccID
        , id: function (object) {
            return object.AccID;
        }
        , initSelection: function (element, callback) {
            var data = { AccID: element.val(), AccName: element.val() };
            callback(data);
        }

    });

}

function FormatResultAccID(data) {
    var markup = "<table style='font-size:12px; color:#222222; background: transparent;' cellspacing='1' cellpadding='2' border='0' width='100%' ><tr>";
    markup += "<td width='80px'>AccId:</td>";
    markup += "<td>" + data.AccID + "</td></tr><tr>";
    markup += "<td width='80px'>AccName:</td>";
    markup += "<td>" + data.AccName + "</td></tr></table><hr style='background-color: #a6c9e2; height: 1px; border: 0' />";
    return markup;
}

function FormatSelectionAccID(data) {

    _GlobalAccIdValue = data.AccID;
    _GlobalAccNameValue = data.AccName;

    if (funcObjectAccID != null || funcObjectAccID != undefined) {
        funcObjectAccID(data.AccID, data.AccName);
    }

    return data.AccID;
}

/*-------------------------------------------------------------------------------------------*/


/*------------------------------ Select 2 By Cust ID -----------------------------------------*/

function SetSelect2CustID(inputObject, funcObject, CustomFormatSelectionCustID) {

    inputObjectCustID = inputObject;
    funcObjectCustID = funcObject;

    AddEndRequest();

    var _url = 'WebMethodFunction.aspx/GetAutoCompleteCustomer';

    $(inputObjectCustID).select2({
        placeholder: "Search for a customer, please type Customer ID, CIFID or customer name."
        , width: "100%"
        , minimumInputLength: 3
        , allowClear: true
        , ajax: {
            transport: function (options) {
                return $.ajax($.extend({}, options, { global: false }));
            },
            type: "POST",
            url: _url,
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            quietMillis: 100,
            data: function (term, page) {
                return JSON.stringify({
                    keySearch: term
                    , page_limit: 10,
                    page: page
                });
            },
            results: function (data, page) {
                return { results: data.d };
            }
        }
        , formatResult: FormatResultCustID
        , formatSelection: $.isEmptyObject(CustomFormatSelectionCustID) ? FormatSelectionCustID : CustomFormatSelectionCustID,
        id: function (object) {

            return object.CustID;;
        }
        , initSelection: function (element, callback) {
            var obj = jQuery.parseJSON($(inputObjectCustID).val());
            var data = { CustID: obj.CustID, CIFID: obj.CIFID, CustNameEN: obj.CustNameEN };

            callback(data);
        }
    });

}

function FormatResultCustID(data) {
    var markup = "<table cellspacing='1' cellpadding='1' border='0' width='500px' ><tr>";
    markup += "<td width='60px'><strong>CustID: </strong></td>";

    markup += "<td width='200px'>" + data.CustID + "</td>";
    markup += "<td width='60px' align='right'><strong>CIFID: </strong></td>"
    markup += "<td>" + data.CIFID + "</td></tr>";
    markup += "<td><strong>Name: </strong></td>";
    markup += "<td colspan=3>" + data.CustNameEN + "</td></tr></table><hr style='background-color: #a6c9e2; height: 1px; border: 0' />";
    return markup;
}

function FormatSelectionCustID(data) {

    _GlobalCustIdValue = data.CustID;
    _GlobalCusCIFIDValue = data.CIFID;
    _GlobalCustNameENValue = data.CustNameEN;
    _GlobalCustNameTHValue = data.CustNameTH;

    if (funcObjectCustID != null || funcObjectCustID != undefined) {
        funcObjectCustID(data.CustID, data.CustNameEN, data.CustNameTH, data.CIFID);
    }

    return "(" + data.CustID + ") " + data.CustNameEN;

}

/*-------------------------------------------------------------------------------------------*/

/*------------------------------ Select 2 By Cust ID >>> Print Deposit Card -----------------------------------------*/

function SetSelect2CustID_PrintDepositCard(inputObject, funcObject) {

    inputObject_PrintDepositCard_CustID = inputObject;
    funcObject_PrintDepositCard_CustID = funcObject;

    AddEndRequest();

    var _url = 'WebMethodFunction.aspx/GetAutoCompleteCustomer_PrintDepositCard';

    $(inputObject_PrintDepositCard_CustID).select2({
        placeholder: "Search for a customer, please type Customer ID or customer name or Account ID or CIFID."
        , width: "100%"
        , minimumInputLength: 3
        , allowClear: true
        , ajax: {
            transport: function (options) {
                return $.ajax($.extend({}, options, { global: false }));
            },
            type: "POST",
            url: _url,
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            quietMillis: 100,
            data: function (term, page) {
                return JSON.stringify({
                    keySearch: term
                    , page_limit: 10,
                    page: page
                });
            },
            results: function (data, page) {
                return { results: data.d };
            }
        }
        , formatResult: FormatResultCustID_PrintDepositCard
        , formatSelection: FormatSelectionCustID_PrintDepositCard,
        id: function (object) {

            return object.ClientID;
        }
        , initSelection: function (element, callback) {
            var obj = jQuery.parseJSON($(inputObject_PrintDepositCard_CustID).val());
            var data = { ClientID: obj.ClientID, ALL_AccID: obj.ALL_AccID, CIFID: obj.CIFID, CustNameEN: obj.CustNameEN, CustNameTH: obj.CustNameTH };

            callback(data);
        }
    });

}

function FormatResultCustID_PrintDepositCard(data) {
    var markup = "<table cellspacing='1' cellpadding='1' border='0' width='500px' ><tr>";
    markup += "<td width='60px'><strong>CustID: </strong></td>";
    markup += "<td width='200px'>" + data.ClientID + "</td>";

    markup += "<td width='60px' align='right'><strong>AccID: </strong></td>"
    markup += "<td width='200px'>" + data.ALL_AccID + "</td>";

    markup += "<td width='60px' align='right'><strong>CIFID: </strong></td>"
    markup += "<td>" + data.CIFID + "</td></tr>";

    markup += "<tr><td><strong>Name: </strong></td>";
    markup += "<td colspan=5>" + data.CustNameEN + "</td></tr></table><hr style='background-color: #a6c9e2; height: 1px; border: 0' />";
    return markup;
}

function FormatSelectionCustID_PrintDepositCard(data) {

    _Global_PrintDepositCard_ClientIDValue = data.ClientID;
    _Global_PrintDepositCard_ALL_AccIDValue = data.ALL_AccID;
    _Global_PrintDepositCard_CustNameENValue = data.CustNameEN;
    _Global_PrintDepositCard_CustNameTHValue = data.CustNameTH;

    if (funcObject_PrintDepositCard_CustID != null || funcObject_PrintDepositCard_CustID != undefined) {
        //funcObject_PrintDepositCard_CustID(data.ClientID, data.CustNameEN, data.CustNameTH, data.ALL_AccID);
        funcObject_PrintDepositCard_CustID(data);
    }

    //return "(" + data.ClientID + ") " + data.CustNameEN + " : " + data.CustNameTH;
    return data.ClientID;

}

/*-------------------------------------------------------------------------------------------*/


/*------------------------------ Select 2 By Stock ID ---------------------------------------*/

function SetSelect2StockID(inputObject, funcObject) {

    inputObjectStockID = inputObject;
    funcObjectStockID = funcObject;

    AddEndRequest();

    var _url = 'WebMethodFunction.aspx/GetAutoCompleteStock';

    $(inputObjectStockID).select2({
        placeholder: "Please fill stock name."
        , width: "100%"
        , minimumInputLength: 2
        , allowClear: true
        , ajax: {
            transport: function (options) {
                return $.ajax($.extend({}, options, { global: false }));
            },
            type: "POST",
            url: _url,
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            data: function (term, page) {
                return JSON.stringify({
                    keySearch: term
                    , page_limit: 10,
                    page: page
                });
            },
            quietMillis: 100,
            results: function (data, page) {
                return { results: data.d };
            }
        }
        , formatResult: FormatResultStockID
        , formatSelection: FormatSelectionStockID
        , id: function (object) {
            return object.SecCode;
        }
        , initSelection: function (element, callback) {
            var data = { SecCode: element.val(), SecName: element.val() };
            callback(data);
        }

    });

}

function FormatResultStockID(data) {
    var markup = "<table style='font-size:12px; color:#222222; background: transparent;' cellspacing='1' cellpadding='2' border='0' width='100%' ><tr>";
    markup += "<td width='80px'>Security Code:</td>";
    markup += "<td>" + data.SecCode + "</td></tr><tr>";
    markup += "<td width='80px'>Security Name:</td>";
    markup += "<td>" + data.SecName + "</td></tr></table><hr style='background-color: #a6c9e2; height: 1px; border: 0' />";
    return markup;
}

function FormatSelectionStockID(data) {

    _GlobalStockIdValue = data.SecCode;
    _GlobalStockNameValue = data.SecName;

    if (funcObjectStockID != null || funcObjectStockID != undefined) {
        funcObjectStockID(data.SecCode);
    }

    return data.SecCode;

}

/*-------------------------------------------------------------------------------------------*/

/*------------------------------ Select 2 By CIF ID ---------------------------------------*/


function SetSelect2CIFID(inputObject, funcObject) {

    inputObjectCIFID = inputObject;
    funcObjectCIFID = funcObject;

    AddEndRequest();


    var _url = 'WebMethodFunction.aspx/GetAutoCompleteCIFID';

    $(inputObjectCIFID).select2({
        placeholder: "Please fill CIFID."
        , width: "100%"
        , minimumInputLength: 3
        , allowClear: true
        , ajax: {
            transport: function (options) {
                return $.ajax($.extend({}, options, { global: false }));
            },
            type: "POST",
            url: _url,
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            data: function (term, page) {
                return JSON.stringify({
                    keySearch: term
                    , page_limit: 10,
                    page: page
                });
            },
            quietMillis: 100,
            results: function (data, page) {
                return { results: data.d };
            }
        }
        , formatResult: FormatResultCIFID
        , formatSelection: FormatSelectionCIFID
        , id: function (object) {
            return object.AuthorizeCIFID;
        }
        , initSelection: function (element, callback) {
            var data = { AuthorizeCIFID: element.val(), AuthorizeName: element.val() };
            callback(data);
        }

    });

}

function FormatResultCIFID(data) {

    // var markup = "<span style= 'padding-right:5px;width:100px;text-align: right;'>" + "Personal ID : " + "</span>";

    var markup = "<span style= 'padding-right:5px;width:100px;text-align: right;'>" + "Personal ID6 : " + "</span>";
    markup += "<span style= 'width:200px;text-align: left;'>" + data.AuthorizeCIFID + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
    markup += "<span style= 'padding-right:5px;width:50px;text-align: right;'>" + "Tel : " + "</span>";
    markup += "<span style= 'width:150px;text-align: left;'>" + data.AuthorizeTel + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
    markup += "<span style= 'padding-right:5px;width:50px;text-align: right;'>" + "Email : " + "</span>";
    markup += "<span style= 'width:150px;text-align: left;'>" + data.Email + "</span>";
    markup += "<br><span style= 'padding-right:5px;width:100px;text-align: right;'>" + "Authorize Name : " + "</span>";
    markup += "<span style= 'width:250px;text-align: left;'>" + data.AuthorizeName

    var padLength = 50 - data.AuthorizeName.toString().length;

    var padSpace = padString(padLength, "&nbsp;");

    //+ "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"


    markup += padSpace + "</span>";
    markup += "<span style= 'width:250px;text-align: left;'>" + data.AuthorizeNameTH + "</span>";

    markup += "<hr style='background-color: #a6c9e2; height: 1px; border: 0' />";





    //var markup = "<table style='font-size:12px; color:#222222; background: transparent;' cellspacing='0' cellpadding='0' border='1' width='100%' >";
    //markup += "<tr><td>Personal ID8 :&nbsp;aabb</td><td>Tel6 :&nbsp;" + data.AuthorizeTel + "</td><td>Email6 :&nbsp;&nbsp;" + data.Email + "</td></tr>";

    //markup += "</table><hr style='background-color: #a6c9e2; height: 1px; border: 0' />";


    return markup;
}


function FormatSelectionCIFID(data) {

    _GlobalAuthorizeCIFIDValue = data.AuthorizeCIFID;
    _GlobalAuthorizeNameValue = data.AuthorizeName;

    if (funcObjectCIFID != null || funcObjectCIFID != undefined) {
        funcObjectCIFID(data.AuthorizeCIFID, data.AuthorizeName);
    }

    return data.AuthorizeCIFID;
}


/*------------------------------ Select 2 By Email ---------------------------------------*/


function SetSelect2Email(inputObject, funcObject) {


    inputObjectEmail = inputObject;
    funcObjectEmail = funcObject;

    AddEndRequest();

    //inputObjectEmail, funcObjectEmail, _GlobalEmailValue


    var _url = 'WebMethodFunction.aspx/GetAutoCompleteEmail';

    $(inputObjectEmail).select2({
        placeholder: "Please fill Email."
        , width: "100%"
        , minimumInputLength: 3
        , allowClear: true
        , ajax: {
            transport: function (options) {
                return $.ajax($.extend({}, options, { global: false }));
            },
            type: "POST",
            url: _url,
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            data: function (term, page) {
                return JSON.stringify({
                    keySearch: term
                    , page_limit: 10,
                    page: page
                });
            },
            quietMillis: 100,
            results: function (data, page) {
                return { results: data.d };
            }
        }
        , formatResult: FormatResultEmail
        , formatSelection: FormatSelectionEmail
        , id: function (object) {
            return object.Email;
        }
        , initSelection: function (element, callback) {
            var data = { Email: element.val() };

            callback(data);
        }

    });

}

function FormatResultEmail(data) {

    var markup = "<span style= 'padding-right:5px;width:50px;text-align: right;'>" + "Email : " + "</span>";
    markup += "<span style= 'width:150px;text-align: left;'>" + data.Email + "</span>";

    markup += "<hr style='background-color: #a6c9e2; height: 1px; border: 0' />";

    return markup;
}


function FormatSelectionEmail(data) {

    _GlobalEmailIDValue = data.Email;
    _GlobalEmailNameValue = data.Email;

    if (funcObjectEmail != null || funcObjectEmail != undefined) {
        funcObjectEmail(data.Email, data.Email);
    }

    return data.Email;
}



/*------------------------------ Select 2 By Employee ID -----------------------------------------*/

function SetSelect2EmpID(inputObject, funcObject, CustomFormatSelectionEmpID) {

    inputObjectEmpID = inputObject;
    funcObjectEmpID = funcObject;

    AddEndRequest();

    var _url = '../FrontEnd/Helper/WebMethodFunction.aspx/GetAutoCompleteEmployee';

    $(inputObjectEmpID).select2({
        placeholder: "Search for a employee, please type Employee ID or employee name."
        , width: "100%"
        , minimumInputLength: 3
        , allowClear: true
        , ajax: {
            transport: function (options) {
                return $.ajax($.extend({}, options, { global: false }));
            },
            type: "POST",
            url: _url,
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            quietMillis: 100,
            data: function (term, page) {
                return JSON.stringify({
                    keySearch: term
                    , page_limit: 10,
                    page: page
                });
            },
            results: function (data, page) {
                return { results: data.d };
            }
        }
        , formatResult: FormatResultEmpID
        , formatSelection: $.isEmptyObject(CustomFormatSelectionEmpID) ? FormatSelectionEmpID : CustomFormatSelectionEmpID
        , id: function (object) {

            return object.EmpID;;
        }
        , initSelection: function (element, callback) {
            var obj = jQuery.parseJSON($(inputObjectEmpID).val());
            var data = { EmpID: obj.EmpID, EmpNameEN: obj.EmpNameEN, EmpNameTH: obj.EmpNameTH };

            callback(data);
        }
    });

}

function FormatResultEmpID(data) {
    var markup = "<table cellspacing='1' cellpadding='1' border='0' width='500px' ><tr>";
    markup += "<td width='20px'><strong>EmpID: </strong></td>";
    markup += "<td width='200px'>" + data.EmpID + "</td>";
    //markup += "<td width='60px' align='right'><strong>Branch: </strong></td>"
    //markup += "<td>" + data.EmpBranch + "</td>";
    markup += "</tr>";
    markup += "<tr><td><strong>Name: </strong></td>";
    markup += "<td colspan=3>" + data.EmpNameEN + "</td></tr></table><hr style='background-color: #a6c9e2; height: 1px; border: 0; padding:0px; margin:0px' />";
    return markup;
}

function FormatSelectionEmpID(data) {

    _GlobalEmpIdValue = data.EmpID;
    _GlobalEmpNameENValue = data.EmpNameEN;
    _GlobalEmpNameTHValue = data.EmpNameTH;
    _GlobalEmpBranchValue = data.EmpBranch;
    _GlobalEmpDepartmentValue = data.EmpDepartment;
    _GlobalEmpPositionENValue = data.EmpPositionEN;
    _GlobalEmpPositionTHValue = data.EmpPositionTH;

    if (funcObjectEmpID != null || funcObjectEmpID != undefined) {
        funcObjectEmpID(data.EmpID, data.EmpNameEN, data.EmpNameTH, data.EmpBranch, data.EmpDepartment, data.EmpPositionEN, data.EmpPositionTH);
    }

    return "(" + data.EmpID + ") " + data.EmpNameEN;

}

/*-------------------------------------------------------------------------------------------*/

/*------------------------------ Select 2 By Department ID -----------------------------------------*/

function SetSelect2DeptID(inputObject, funcObject, CustomFormatSelectionDeptID) {

    inputObjectDeptID = inputObject;
    funcObjectDeptID = funcObject;

    AddEndRequest();

    var _url = '../FrontEnd/Helper/WebMethodFunction.aspx/GetAutoCompleteDepartment';

    $(inputObjectDeptID).select2({
        placeholder: "Search for a Department, please type Department ID or Department Code or Department name."
            , width: "100%"
            , minimumInputLength: 3
            , allowClear: true
            , ajax: {
                transport: function (options) {
                    return $.ajax($.extend({}, options, { global: false }));
                },
                type: "POST",
                url: _url,
                params: {
                    contentType: 'application/json; charset=utf-8'
                },
                dataType: 'json',
                quietMillis: 100,
                data: function (term, page) {
                    return JSON.stringify({
                        keySearch: term
                        , page_limit: 10,
                        page: page
                    });
                },
                results: function (data, page) {
                    return { results: data.d };
                }
            }
            , formatResult: FormatResultDeptID
            , formatSelection: $.isEmptyObject(CustomFormatSelectionDeptID) ? FormatSelectionDeptID : CustomFormatSelectionDeptID
            , id: function (object) {

                return object.DeptID;
            }
            , initSelection: function (element, callback) {
                var obj = jQuery.parseJSON($(inputObjectDeptID).val());
                var data = { DeptID: obj.DeptID, DeptCode: obj.DeptCode, DeptName: obj.DeptName };

                callback(data);
            }
    });

}

function FormatResultDeptID(data) {
    var markup = "<table cellspacing='1' cellpadding='1' border='0' width='500px' ><tr>";
    markup += "<td width='150px'><strong>Department ID: </strong></td>";
    markup += "<td width='200px'>" + data.DeptID + "</td>";
    markup += "<td width='60px' align='right'><strong>Code: </strong></td>"
    markup += "<td>" + data.DeptCode + "</td>";
    markup += "</tr>";
    markup += "<tr><td><strong>Department Name: </strong></td>";
    markup += "<td colspan=3>" + data.DeptName + "</td></tr></table><hr style='background-color: #a6c9e2; height: 1px; border: 0; padding:0px; margin:0px' />";
    return markup;
}

function FormatSelectionDeptID(data) {

    _GlobalDeptIdValue = data.DeptID;
    _GlobalDeptCodeValue = data.DeptCode;
    _GlobalDeptNameValue = data.DeptName;

    if (funcObjectDeptID != null || funcObjectDeptID != undefined) {
        funcObjectDeptID(data.DeptID, data.DeptCode, data.DeptName);
    }

    return "(" + data.DeptID + ") " + data.DeptName;

}

/*-------------------------------------------------------------------------------------------*/







/*-------------------------------------------------------------------------------------------*/

/*--------------------------------- Other Function ------------------------------------------*/

$.wait = function (ms) {
    var defer = $.Deferred();
    setTimeout(function () { defer.resolve(); }, ms);
    return defer;
};

function AddEndRequest() {

    if (prmAccID != null) {
        prmAccID.add_endRequest(function (s, e) {

            var selectVal = $(inputObjectAccID).val();

            $(inputObjectAccID).select2("destroy");

            SetSelect2AccID(inputObjectAccID, funcObjectAccID);

            $(inputObjectAccID).select2("val", selectVal);

        });
    }

    if (prmCustID != null) {

        prmCustID.add_endRequest(function (s, e) {

            $(inputObjectCustID).select2("destroy");

            SetSelect2CustID(inputObjectCustID, funcObjectCustID);

            $(inputObjectCustID).select2("val", "");

        });
    }

    if (prmCustID_PrintDepositCard != null) {

        prmCustID_PrintDepositCard.add_endRequest(function (s, e) {

            $(inputObject_PrintDepositCard_CustID).select2("destroy");

            SetSelect2CustID_PrintDepositCard(inputObject_PrintDepositCard_CustID, funcObject_PrintDepositCard_CustID);

            $(inputObject_PrintDepositCard_CustID).select2("val", "");

        });
    }


    if (prmStockID != null) {

        prmStockID.add_endRequest(function (s, e) {

            $(inputObjectStockID).select2("destroy");

            SetSelect2StockID(inputObjectStockID, funcObjectStockID);

            $(inputObjectStockID).select2("val", "");

        });
    }

    if (prmCIFID != null) {

        prmCIFID.add_endRequest(function (s, e) {

            $(inputObjectCIFID).select2("destroy");

            SetSelect2CIFID(inputObjectCIFID, funcObjectCIFID);

            $(inputObjectCIFID).select2("val", "");

        });
    }

    if (prmEmpID != null) {

        prmEmpID.add_endRequest(function (s, e) {

            $(inputObjectEmpID).select2("destroy");

            SetSelect2EmpID(inputObjectEmpID, funcObjectEmpID);

            $(inputObjectEmpID).select2("val", "");

        });
    }

    if (prmDeptID != null) {

        prmDeptID.add_endRequest(function (s, e) {

            $(inputObjectDeptID).select2("destroy");

            SetSelect2DeptID(inputObjectDeptID, funcObjectDeptID);

            $(inputObjectDeptID).select2("val", "");

        });
    }
}

$(window).load(function () {

    setInterval("displaytime()", 1000);

    startLoading();

});

$(document).ready(function () {

    stopLoading();

    iniPage();

});

function iniPage() {

    var divInformationSupport = $("#information-support");
    var groupDatePicker = $(".date-container .input-group.date");

    if (divInformationSupport) {

        divInformationSupport.popover({
            trigger: "hover",
            placement: "bottom",
            html: true,
            container: 'body',
            title: "<span class='glyphicon glyphicon-info-sign'></span> Information Suppport",
            content: "<span>สอบถามการใช้งาน ติดต่อศูนย์ลูกค้าสัมพันธ์ โทรศัพท์ 02-860-9797-8</span><br /><span>พบปัญหาการใช้งาน ติดต่อ IT Helpdesk โทรศัพท์ 02-862-9938</span>"
        });
    }

    if (groupDatePicker) {
//setDefaultDatePicker(groupDatePicker);
    }

    $(".validateInt").keypress(function (e) {
        validateInt(e);
    });

    $(".validateDecimal").keypress(function (e) {
        validateDecimal(e);
    });

    $.fn.modal.Constructor.prototype.enforceFocus = function () { };

}

function setStartDateEndDate(objStartDate, objEndDate) {

    if (objStartDate && objEndDate) {

        objStartDate.change({ obj: objEndDate }, function (evt) {
            if ($(this).val() == "") {
//setDefaultDatePicker($(evt.data.obj).parent(".input-group.date"));
            }
            else {
                var startDateObj = $(this).parent(".input-group.date");
                var endDateObj = $(evt.data.obj).parent(".input-group.date");
                var dateSelect = startDateObj.datepicker("getDate");

                endDateObj.datepicker("setStartDate", dateSelect);

                if (startDateObj.attr("data-max-year")) {
                    var year = startDateObj.data("maxYear");
                    var maxYear = new Date(dateSelect.getFullYear() + year, dateSelect.getMonth(), dateSelect.getDate());
                    endDateObj.datepicker("setEndDate", maxYear);
                }
                else if (startDateObj.attr("data-max-month")) {
                    var month = startDateObj.data("maxMonth");
                    var maxMonth = new Date(dateSelect.getFullYear(), dateSelect.getMonth() + month, dateSelect.getDate());
                    endDateObj.datepicker("setEndDate", maxMonth);
                }
            }
        });

        objEndDate.change({ obj: objStartDate }, function (evt) {
            if ($(this).val() == "") {
             //   setDefaultDatePicker($(evt.data.obj).parent(".input-group.date"));
            }
            else {
                var startDateObj = $(evt.data.obj).parent(".input-group.date");
                var endDateObj = $(this).parent(".input-group.date")
                var dateSelect = endDateObj.datepicker("getDate");

                startDateObj.datepicker("setEndDate", dateSelect);

                if (endDateObj.attr("data-min-year")) {
                    var year = endDateObj.data("minYear");
                    var minYear = new Date(dateSelect.getFullYear() - year, dateSelect.getMonth(), dateSelect.getDate());
                    startDateObj.datepicker("setStartDate", minYear);
                }
                else if (endDateObj.attr("data-min-month")) {
                    var month = endDateObj.data("minMonth");
                    var minMonth = new Date(dateSelect.getFullYear(), dateSelect.getMonth() - month, dateSelect.getDate());
                    startDateObj.datepicker("setStartDate", minMonth);
                }
            }
        });

    }

}

//function setDefaultDatePicker(object) {
//    if (object) {
//        object.datepicker("remove");
//        object.datepicker({
//            format: "dd/mm/yyyy",
//            todayHighlight: true,
//            orientation: "top left",
//            autoclose: true,
//            todayBtn: true
//        });
//    }
//}

function removeStartDateEndDate(object) {
    if (object) {
        object.parent(".input-group.date").datepicker("setStartDate", null);
        object.parent(".input-group.date").datepicker("setEndDate", null);
    }
}

function setCurrentDatePicker(object) {

    var currentDate = new Date();

    if (object) {
        object.parent(".input-group.date").datepicker("update", currentDate);
    }

    return currentDate;

}

function setValueDatePicker(object, date) {

    var dateSplit = date.split("/");

    var value = new Date(dateSplit[2], dateSplit[1] - 1, dateSplit[0]);

    if (object) {
        object.parent(".input-group.date").datepicker("setDate", value);
    }

    return value;
}

function colModel(colName, colAlign, colWidth) {
    return colModel(colName, colAlign, colWidth, false);
}

function colModel(colName, colAlign, colWidth, colHidden) {
    return { name: colName, index: colName, align: colAlign, width: colWidth, hidden: colHidden };
}

function colAmount(colName, colAlign, colWidth, colDecimalPlaces) {
    return { name: colName, index: colName, align: colAlign, width: colWidth, formatter: 'currency', formatoptions: { decimalPlaces: colDecimalPlaces, defaultValue: 0 } };
}

function checkAccountNoFormat(value) {

    var regex = /^\d{6}-+\d{1}/;

    if (!regex.test(value)) {
        alert("Acccount No. is worng format, Please recheck.");

        return false;
    }

    return true;

}

//Extend grid load complete event in jqgrid for set real width on bootstrap class row
var emptyMsgDiv =
    $('<div class="col-xs-12"><div class="emptyMsgDiv">'
    + '<span class="glyphicon glyphicon-exclamation-sign icon-sms" aria-hidden="true"></span>'
    + '<span class="sr-only">Error:</span>&nbsp;'
    + 'Record not found'
    + '</div></div>');

$.extend($.jgrid.defaults, {
    gridComplete: function () {

        if ($(this).find("tbody tr:not(.jqgfirstrow)").length > 0) {
            emptyMsgDiv.hide();
        }
        else {
            emptyMsgDiv.show();
            emptyMsgDiv.insertAfter($(this).parent());
        }

        if ($(this).closest(".subgrid-data").length == 0) {

            var rowWidth = $(this).closest(".row").width();

            if (rowWidth > 0) {
                $(this).setGridWidth(rowWidth);
            }
        }
    }
});

function validateInt(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]/;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

function validateDecimal(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    //var regex = /^\d+(.\d+)?$/;
    var regex = /^\d+|.\d{1,2}/;

    var regex_int = /[0-9]|\./;
    if (!regex_int.test(key)) {
        theEvent.returnValue = false;
        theEvent.preventDefault();
    }

    if (($(evt.target).val() + key).split('.').length - 1 > 1) {
        theEvent.returnValue = false;
        theEvent.preventDefault();
    }
    else {
        if (!regex.test($(evt.target).val() + key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
        }
    }
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

function divexpandcollapse(divHis, divRelatedAcc, divTradingRecord) {
    var vdivHis = document.getElementById(divHis);
    var vdivRelatedAcc = document.getElementById(divRelatedAcc);
    var vdivTradingRecord = document.getElementById(divTradingRecord);

    //var img = document.getElementById('img' + divHis);

    if (vdivHis.style.display == "none") {
        vdivHis.style.display = "inline";

        //img.src = "../Images/arrow-01.png";
    } else {
        vdivHis.style.display = "none";
        img.src = "../Images/arrow-02.png";


    }

    vdivRelatedAcc.style.display = "none";
    vdivTradingRecord.style.display = "none";

}

function divTextexpandcollapse(divHis, divRelatedAcc, divTradingRecord) {

    var vdivHis = document.getElementById(divHis);
    var vdivRelatedAcc = document.getElementById(divRelatedAcc);
    var vdivTradingRecord = document.getElementById(divTradingRecord);
    var img = document.getElementById('img' + divHis);

    if (vdivRelatedAcc.style.display == "none") {
        vdivRelatedAcc.style.display = "inline";

        img.src = "../Images/arrow-01.png";
    } else {
        vdivRelatedAcc.style.display = "none";
        img.src = "../Images/arrow-02.png";
    }
    vdivHis.style.display = "none";
    vdivTradingRecord.style.display = "none";
}

function divText2Expandcollapse(divHis, divRelatedAcc, divTradingRecord) {

    var vdivHis = document.getElementById(divHis);
    var vdivRelatedAcc = document.getElementById(divRelatedAcc);
    var vdivTradingRecord = document.getElementById(divTradingRecord);

    if (vdivTradingRecord.style.display == "none") {
        vdivTradingRecord.style.display = "inline";
    } else {
        vdivTradingRecord.style.display = "none";
        img.src = "../Images/arrow-02.png";
    }
    vdivHis.style.display = "none";
    vdivRelatedAcc.style.display = "none";
}

function Concenexpandcollapse(divname) {
    var div = document.getElementById(divname);
    //  var divConcentration = document.getElementById("divConcentration");
    var img = document.getElementById('img' + divname);

    if (div.style.display == "none") {
        // divConcentration.style.height = "900px";           
        div.style.display = "inline";
        div.style.paddingBottom = "10px";

        img.src = "../Images/arrow-01.png";
    } else {
        div.style.display = "none";
        div.style.padding = "0px 15px 0px 15px";


        //   divConcentration.style.height = "380px";
        img.src = "../Images/arrow-02.png";
    }
}

/*-------------------------------------------------------------------------------------------*/

/*------------------------------- Date Time Header Function ---------------------------------*/

var currenttime = Date();
var montharray = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
var serverdate = new Date(currenttime);

function padlength(what) {
    var output = (what.toString().length == 1) ? "0" + what : what;
    return output;
}

function padString(n, str) {
    var strPad = "";

    for (i = 0; i < n - 1; i++) {
        strPad += str;
    }

    return strPad;

}

function displaytime() {

    serverdate.setSeconds(serverdate.getSeconds() + 1);

    var datestring = padlength(montharray[serverdate.getMonth()]) + " " + serverdate.getDate() + ", " + serverdate.getFullYear();
    var timestring = padlength(serverdate.getHours()) + ":" + padlength(serverdate.getMinutes()) + ":" + padlength(serverdate.getSeconds());

    var lblServertime = $("#servertime");

    if (lblServertime.length > 0) {
        lblServertime.text(datestring + " | " + timestring);
    }
}

/*-------------------------------------------------------------------------------------------*/

function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?[\w-]{1,4}$/i;
    return pattern.test(emailAddress);
}

//function isValidEmailAddress(v) {
//    var r = new RegExp("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
//    return (v.match(r) == null) ? false : true;
//}

//function isValidEmailAddress(emailAddress) {
//    var pattern = new RegExp(/^([\w-\.\+]+@([\w-]+\.)+[\w-]{2,4})?$/);
//    return pattern.test(emailAddress);
//}

function ReplaceNumberWithCommas(yourNumber) {
    //Seperates the components of the number
    var n = yourNumber.toString().split(".");
    //Comma-fies the first part
    n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //Combines the two sections
    return n.join(".");
}

function RemoveNumberWithCommas(yourNumber) {
    return yourNumber.replace(/,/g, "");
}

function ValidNumberFormat(object, toFixed) {
    if ($(object).val() != "") {
        var number = parseFloat($(object).val()).toFixed(toFixed);
        var text = ReplaceNumberWithCommas(number);
        $(object).val(text);
    }
}

function MaxLength(evt, length) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);

    if (window.getSelection().toString() == "") {
        if (($(theEvent.target).val() + key).length > length) {
            theEvent.returnValue = false;
            theEvent.preventDefault();
        }
    }
    else {
        if ($(evt.target).parent().find(".validateDecimal").length > 0) {
            var regex = /^\d+|.\d{1,2}/;
            if (!regex.test(key)) {
                theEvent.returnValue = false;
                theEvent.preventDefault();
            }
        } else {
            var regex = /[0-9]/;
            if (!regex.test(key)) {
                theEvent.returnValue = false;
                theEvent.preventDefault();
            }
        }
    }
}