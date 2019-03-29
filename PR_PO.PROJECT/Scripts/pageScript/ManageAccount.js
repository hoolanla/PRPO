
/*--------------------------- Permission ----------------------------------*/
var _readPermission = false;
var _createPermission = false;
var _updatePermission = false;
var _deletePermission = false;
var _reportPermission = false;
/*-------------------------------------------------------------------------*/

/*--------------------------- Variable ------------------------------------*/
var frmSaveAction = null;

var JqGridParameter = {
    "Rows": null,
    "Page": null,
    "Sidx": null,
    "Sord": null
}

var Department_SearchCriteria = {
    "DeptID": null,
    "DeptCode": null,
    "DeptName": null,

    "CreatedBy": null,

    "DateStart": null,
    "DateEnd": null,

    "rdViewBy": null,
    "JqGridParameter": JqGridParameter
};

/*----------------------------- Button in Main Page -------------------------------------*/
var btnSearch = $("[id*=btnSearch]");
var btnCancel = $("[id*=btnCancel]");
var btnAdd = $("[id*=btnAdd]");

/*-----------------------------Layout +  Grid -------------------------------------*/
var frmActionTitle = $("#frmActionTitle");

var dvViewByDepartmentNo = $("[id*=dvViewByDepartmentNo]");

var grdDepartment_SearchByDepartmentNo = $("#grdDepartment_SearchByDepartmentNo");
var grdDepartment_SearchByDepartmentNoPager = $("#grdDepartment_SearchByDepartmentNoPager");


/*--------------------------- Text Box -----------------------------------*/
var txtDeptID = $("input[id*=txtDeptID]");
var txtDeptCode = $("input[id*=txtDeptCode]");
var txtDeptName = $("input[id*=txtDeptName]");
var txtCreatedBy = $("[id*=txtCreatedBy]");

var txtDateStart = $("input[id*=txtDateStart]");
var txtDateEnd = $("input[id*=txtDateEnd]");


/*--------------------------- Dropdown Box -----------------------------------*/

/*--------------------------- Check Box -----------------------------------*/


/*--------------------------- Radio Box -----------------------------------*/
var rdViewBy = $("input[name*=rdViewBy]");

/*------------------------*/


/*----------------------------- All Controls in Popup Form -------------------------------------*/
/*--- Button ---*/
var btnfrmSave = $("[id*=btnfrmSave]");
var btnfrmClose = $("[id*=btnfrmClose]");

/*--- Text Box ---*/
var txtfrm_DeptID = $("[id*=txtfrm_DeptID]");
var txtfrm_DeptCode = $("[id*=txtfrm_DeptCode]");
var txtfrm_DeptName = $("[id*=txtfrm_DeptName]");

/*--- Hidden */
var hdfrm_DeptID = $("input[id*=hdfrm_DeptID]");
/*-------------------------------------------------------------------------------------------------*/



/*-------- Criteria -----*/

function bindEvent() {

    btnSearch.click(function (e) {


        dvViewByDepartmentNo.show();

        iniDepartment_SearchByDepartmentNo();

        e.preventDefault();
        return true;
    });


    //btnCancel.click(function (e) {
    //    //txtCustID.val("");

    //    e.preventDefault();
    //    location.reload();
    //});

    //inProvID.change(function (e) {

    //    var sCateValue = inProvID.val();

    //    txtCateValue.val(sCateValue);

    //    var arry1 = sCateValue.split(":");
    //    txtProvCode.val($.trim(arry1[1]));

    //    var sCateName = inProvID.find('option:selected').text();

    //    var arry2 = sCateName.split("-");
    //    //txtProvCode.val($.trim(arry2[0]));
    //    txtCateName.val($.trim(arry2[1]));
    //});


    ////--- Clear Province Data ----------
    //txtProvCode.val("");
    //txtCateName.val("");

    btnAdd.click(function (e) {

        //-- Clear Popup Form Data ---------
        clearPopupFormData();

        frmActionTitle.html("สร้างใหม่");

        frmSaveAction = "addnew";

        setTimeout("btnfrmSave.focus();", 1000);

    });

    btnfrmSave.click(function (e) {

        if (frmSaveAction != null) {
            if (frmValidateSave() == true) {
                var sAction = "";

                if (frmSaveAction == "addnew") {
                    sAction = "AddNew";
                }
                else if (frmSaveAction == "update") {
                    sAction = "Update";
                }

                if (sAction == "") {
                    alert("Unknown Saved Action ! \n\nPlease close this form and open this form to save data again.");
                    return false;
                }

                //if (confirm("Do you want to " + sAction + " data?") == true) {
                Department_Save();
                //}
            }
            else {
                e.preventDefault();
                return false;
            }
        }
        else {
            alert("Saved-Action not define !\n\nPlease close and try to open Add/Edit form again.");
        }

        e.preventDefault();
        return true;
    });

    btnfrmClose.click(function (e) {
        document.location = '#';
    });

    //--- Initial Max Length -----------------------------
    txtDeptID.attr("maxlength", 6);

    txtfrm_DeptID.attr("maxlength", 6);
    txtfrm_DeptCode.attr("maxlength", 20);
    txtfrm_DeptName.attr("maxlength", 50);


    txtDeptID.numeric({ decimal: false, negative: false }, function (e) { alert("Department ID is Positive integers only"); this.value = ""; this.focus(); });
    txtfrm_DeptID.numeric({ decimal: false, negative: false }, function (e) { alert("Department ID is Positive integers only"); this.value = ""; this.focus(); });


    //--- Load data to grid ---------
    btnSearch.trigger("click");


    frmSaveAction = null;
    frmSaveAction = "addnew";
    frmActionTitle.html("สร้างใหม่");

    //--- Set Event Handler For Criteria Controls To Search Data --------------------------
    btnSearch.focus();
    set_event_control_to_search(txtDeptID);
    set_event_control_to_search(txtDeptCode);
    set_event_control_to_search(txtDeptName);
    set_event_control_to_search(txtCreatedBy);

    //--- Set Event Handler For Popup Form Controls To Save Data ---------------------------
    set_event_frm_control_to_save(txtfrm_DeptID);
    set_event_frm_control_to_save(txtfrm_DeptCode);
    set_event_frm_control_to_save(txtfrm_DeptName);
}

function set_event_control_to_search(obj) {

    obj.keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            //alert('You pressed enter!' + $(this).val());
            //if ($.trim($(this).val()) != "") {
            //    btnSearch.trigger("click");
            //}
            //else {
            //    $(this).val("");
            //    $(this).focus();
            //}
            btnSearch.focus();
            btnSearch.trigger("click");
        }
    });

    obj.on("keydown", function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            //alert("KeyDown Entered!");
            btnSearch.focus();
        }
    });
}

function set_event_frm_control_to_save(obj) {

    obj.keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            btnfrmSave.focus();
            btnfrmSave.trigger("click");
        }
    });

    obj.on("keydown", function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            btnfrmSave.focus();
        }
    });
}

function clearPopupFormData() {
    $("[id*=txtfrm]").val("");
}

function frmValidateSave() {

    if ($.trim(txtfrm_DeptID.val()) == "") {
        alert("Please type Department ID !");
        txtfrm_DeptID.val("");
        txtfrm_DeptID.focus();
        return false;
    }

    if ($.trim(txtfrm_DeptCode.val()) == "") {
        alert("Please type Department Code !");
        txtfrm_DeptCode.val("");
        txtfrm_DeptCode.focus();
        return false;
    }

    if ($.trim(txtfrm_DeptName.val()) == "") {
        alert("Please type Department Name !");
        txtfrm_DeptName.val("");
        txtfrm_DeptName.focus();
        return false;
    }

    return true;
}

function Department_Save() {

    /*
        var postDepartment_Save = {
     
            "SaveAction": null,

            "DeptID": null,

            "Name": null,
            "ContactName": null,
            "Email": null,

            "Telephone": null,
            "Mobile": null,
            "Fax": null,

            "Address": null,
            "District": null,
            "AmphurID": null,
            "ProvID": null,
            "PostalCode": null,
     
            "CreatedDate": null,
            "CreatedBy": null,
     
            "UpdatedDate": null,
            "UpdatedBy": null,
        }
    */

    var postDepartment_Save = new Object();
    postDepartment_Save.SaveAction = frmSaveAction;

    if (frmSaveAction == "addnew") {
        postDepartment_Save.DeptID = $.trim(txtfrm_DeptID.val()) == "" ? null : txtfrm_DeptID.val();
    } else if (frmSaveAction == "update") {
        postDepartment_Save.DeptID = $.trim(hdfrm_DeptID.val()) == "" ? null : hdfrm_DeptID.val();
        postDepartment_Save.txtDeptID = $.trim(txtfrm_DeptID.val()) == "" ? null : txtfrm_DeptID.val();
    }

    postDepartment_Save.DeptCode = $.trim(txtfrm_DeptCode.val()) == "" ? null : txtfrm_DeptCode.val();
    postDepartment_Save.DeptName = $.trim(txtfrm_DeptName.val()) == "" ? null : txtfrm_DeptName.val();

    $.ajax({
        type: "POST",
        url: "MDepartment.aspx/Department_Save",
        data: JSON.stringify({ "criteria": postDepartment_Save }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d) {

                if ($.isEmptyObject(data.d[0])
                    || $.isEmptyObject(data.d[0].StatusCode)) {

                    alert("No Response Data");
                    //e.preventDefault();
                    return false;
                }

                if (data.d[0].StatusCode.trim() == "0") {   //0 : Error

                    alert("Error Info: " + data.d[0].StatusDesc + "\n\n" + data.d[0].result);
                    //e.preventDefault();
                    return false;
                }
                else if (data.d[0].StatusCode.trim() == "1") {  //1 : Complete

                    //alert("Save Completed. \n\n" + data.d[0].result);

                    //if (frmSaveAction == "addnew") {
                    //    //-- Set New Code No. ------
                    //    txtfrm_DepartmentCode.val(data.d[0].result);
                    //}

                }
                else {
                    alert("Not found this result status in system: " + data.d[0].StatusCode);
                    //e.preventDefault();
                    //return false;
                }

                btnfrmClose.trigger("click");

                //ReLoad Again
                btnSearch.trigger("click");

            }
        }
    });
}

function ClearAllControls() {
    $("[id*=txt]").val("");
}

function setParamDepartment_SearchCriteria() {

    Department_SearchCriteria.DeptID = $.trim(txtDeptID.val()) == "" ? null : txtDeptID.val();
    Department_SearchCriteria.DeptCode = $.trim(txtDeptCode.val()) == "" ? null : txtDeptCode.val();
    Department_SearchCriteria.DeptName = $.trim(txtDeptName.val()) == "" ? null : txtDeptName.val();
    Department_SearchCriteria.CreatedBy = $.trim(txtCreatedBy.val()) == "" ? null : txtCreatedBy.val();

    Department_SearchCriteria.rdViewBy = $.trim(rdViewBy.filter(':checked').val()) == "" ? null : rdViewBy.filter(':checked').val();
}

function CustomColumn_Edit(columnName, subgridTableId, w, _updatePermission) {

    var tmpBool = false;
    if (_updatePermission == "true") {
        tmpBool = false;
    }
    else {
        tmpBool = true;
    }

    return {
        name: columnName, index: columnName, align: 'center', hidden: tmpBool, width: w, sortable: false, formatter: function (cellValue, options, rowValue) {

            return '<a id="' + subgridTableId + '_btnEdit" href="#modal" rid="' + cellValue + '"><img  src="../Images/edit_action.png" style=" width: 20px" /></a>';
        }
    };
}

function CustomColumn_Delete(columnName, subgridTableId, w, _deletePermission) {

    var tmpBool = false;
    if (_deletePermission == "true") {
        tmpBool = false;
    }
    else {
        tmpBool = true;
    }

    return {
        name: columnName, index: columnName, align: 'center', hidden: tmpBool, width: w, sortable: false, formatter: function (cellValue, options, rowValue) {

            return '<span class="grid-button-delete" id="' + subgridTableId + '_btnDelete" rid="' + cellValue + '" RowID="' + options.rowId + '"><img  src="../Images/Delete1.png" style=" width: 20px" /></span>';
        }
    };
}

function HiddenBtnAdd(_createPermission) {
    var x = document.getElementById('btnAdd');
    if (_createPermission == "true") {
        x.style.display = 'block';
    } else {
        x.style.display = 'none';
    }
}

function HiddenBtnSearch(_readPermission) {
    var x = document.getElementById('btnSearch');
    if (_readPermission == "true") {
        x.style.display = 'block';
    } else {
        x.style.display = 'none';
    }
}


function iniDepartment_SearchByDepartmentNo() {
    debugger;
    setParamDepartment_SearchCriteria();

    grdDepartment_SearchByDepartmentNo.setGridParam({ page: 1 }).trigger("reloadGrid");



    grdDepartment_SearchByDepartmentNo.jqGrid({
        url: 'MDepartment.aspx/getDepartment_Search_L1',
        serializeGridData: function (postData) {

            Department_SearchCriteria.JqGridParameter.Rows = postData.rows;
            Department_SearchCriteria.JqGridParameter.Page = postData.page;
            Department_SearchCriteria.JqGridParameter.Sidx = postData.sidx;
            Department_SearchCriteria.JqGridParameter.Sord = postData.sord;

            return JSON.stringify({ "criteria": Department_SearchCriteria, "gridParam": postData });
        },
        datatype: 'json',
        mtype: 'POST',
        ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
        jsonReader: objRender,

        autowidth: true,
        height: '100%',
        shrinkToFit: true,
        //width: "1245",
        //height: '100%',
        //shrinkToFit: false,
        altRows: true,
        altclass: "myAltRowClassOdd",
        rowNum: 20,
        viewrecords: false,
        gridview: false,
        pager: grdDepartment_SearchByDepartmentNoPager,
        sortname: 'Seq',
        sortorder: 'asc',
        subGrid: false,
        colNames: [
            "Dept ID",
            "Dept Code",
            "Dept Name",
            "Created Date",
            "Edit",
            "Delete"
        ],
        colModel: [
            { name: "DeptID", index: "DeptID", align: 'center', width: "70" },
            { name: "DeptCode", index: "DeptCode", align: 'left', width: "100" },
            { name: "DeptName", index: "DeptName", align: 'left', width: "250" },
            { name: "CreatedDate", index: "CreatedDate", align: 'center', width: "90", formatter: "date", formatoptions: { srcformat: "m/d/Y", newformat: "d/m/Y" } },

            CustomColumn_Edit("DeptID", "Grid1_Department", 60),

            CustomColumn_Delete("DeptID", "Grid1_Department", 60)


        ],
        loadComplete: function (result) {

            HiddenBtnAdd(_createPermission);
            HiddenBtnSearch(_readPermission);

            $("[id*='" + "Grid1_Department" + "_btnEdit']").click(function (e) {

                frmActionTitle.html("แก้ไข");

                frmSaveAction = "update";

                //-- Clear Popup Form Data ---------
                clearPopupFormData();

                var DeptID = $(this).attr("rid");

                //--- Load Data to Edit ---------------
                frmEditLoad_Department(DeptID);

                setTimeout("btnfrmSave.focus();", 2000);

                //e.preventDefault();
                return true;
            });

            $("[id*='" + "Grid1_Department" + "_btnDelete']").click(function (e) {

                var sDeptID = grdDepartment_SearchByDepartmentNo.jqGrid('getCell', $(this).attr("RowID"), 'DeptID');
                var sName = grdDepartment_SearchByDepartmentNo.jqGrid('getCell', $(this).attr("RowID"), 'DeptName');

                if (confirm("Do you want to Delete this data? \n\nDepartment ID : " + sDeptID + "\n\nDepartment Name : " + sName) == true) {

                    Delete_Department($(this).attr("rid"), sName);
                }

                e.preventDefault();
                return true;
            });

        },
        subGridRowExpanded: function (subgridId, id) {
            var grid = $(this);

            ////// iniDepartment_SearchByDepartmentNoLv2(grid, subgridId, id);

        },
        beforeSelectRow: function (rowid, e) {
            return false;
        }
    });

}

function frmEditLoad_Department(DeptID) {

    var postLoadEdit_Department = new Object();
    postLoadEdit_Department.DeptID = DeptID;

    $.ajax({
        type: "POST",
        url: "MDepartment.aspx/LoadEdit_Department",
        data: JSON.stringify({ "criteria": postLoadEdit_Department }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d) {

                if ($.isEmptyObject(data.d[0])
                    //|| $.isEmptyObject(data.d[0].DeptID)
                    ) {

                    alert("No Response Data");
                    return false;
                }

                //Load Department Header Data -------------------------

                hdfrm_DeptID.val(data.d[0].DeptID);

                txtfrm_DeptID.val(data.d[0].DeptID);
                txtfrm_DeptCode.val(data.d[0].DeptCode);
                txtfrm_DeptName.val(data.d[0].DeptName);

                txtfrm_DeptName.focus();
            }
        }
    });

}

function Delete_Department(DeptID, DeptName) {

    var postDelete_Department = new Object();
    postDelete_Department.DeptID = DeptID;
    postDelete_Department.DeptName = DeptName;

    $.ajax({
        type: "POST",
        url: "MDepartment.aspx/Department_Delete",
        data: JSON.stringify({ "criteria": postDelete_Department }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d) {

                if ($.isEmptyObject(data.d[0])
                   || $.isEmptyObject(data.d[0].StatusCode)) {

                    alert("No Response Data");
                    //e.preventDefault();
                    return false;
                }

                if (data.d[0].StatusCode.trim() == "0") {   //0 : Error

                    alert("Error Info: " + data.d[0].StatusDesc + "\n\n" + data.d[0].result);
                    //e.preventDefault();
                    //return false;
                }
                else if (data.d[0].StatusCode.trim() == "1") {  //1 : Complete

                    if (data.d[0].StatusDesc == "UNSUCCESS") {
                        alert("Cannot delete this Department due to the data already has used in relate system. \n\n" + data.d[0].result);
                    }
                    else {
                        alert("Delete Completed. \n\n"
                            //+ data.d[0].result
                        );
                    }
                }
                else {
                    alert("Not found this result status in system: " + data.d[0].StatusCode);
                    //e.preventDefault();
                    //return false;
                }

                //ReLoad Again
                btnSearch.trigger("click");

            }
        }
    });
}