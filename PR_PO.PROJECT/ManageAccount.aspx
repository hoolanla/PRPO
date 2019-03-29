<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="ManageAccount.aspx.cs" Inherits="PR_PO.PROJECT.ManageAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <style>
        .help-block { 
            display:none 
        }

        select{
            font-family:"Courier New";/*, Courier, monospace*/
            font-size: 16px !important;  
            font-weight: bold;
        }
    </style>


	<div id="modal" class="MDepartment">
		<div class="modal-content" >
			<div class="header">
                <button id="btnfrmClose" class="btn red">X</button>
				<h2>Department / จัดการข้อมูลแผนก [<span id="frmActionTitle">สร้างใหม่ - แก้ไข</span>]</h2>
			</div>
			<div class="copy">				
            
                <input type="hidden" id="hdfrm_DeptID" />
            
                <div class="form-horizontal" style="width:95%">
                    <fieldset>

                        <!-- Form Name -->
                        <!--<legend>Department / จัดการข้อมูลแผนก</legend>-->

                        <!-- Text input-->
                        <div class="form-group required">
                          <label class="col-md-2 control-label" for="textinput">Dept ID :</label>  
                          <div class="col-md-3">
                          <input id="txtfrm_DeptID" name="txtfrm_DeptID" type="text" placeholder="Department ID" class="form-control input-md" />
                          <span class="help-block">help</span>  
                          </div>
                        </div>

                        <!-- Text input-->
                        <div class="form-group required">
                          <label class="col-md-2 control-label" for="textinput">Dept Code :</label>  
                          <div class="col-md-6">
                          <input id="txtfrm_DeptCode" name="txtfrm_DeptCode" type="text" placeholder="Department Code" class="form-control input-md" />
                          <span class="help-block">help</span>  
                          </div>
                        </div>

                        <!-- Text input-->
                        <div class="form-group required">
                          <label class="col-md-2 control-label" for="textinput">Dept Name :</label>  
                          <div class="col-md-10">
                          <input id="txtfrm_DeptName" name="txtfrm_DeptName" type="text" placeholder="Department Name" class="form-control input-md" />
                          <span class="help-block">help</span>  
                          </div>
                        </div>


                    </fieldset>
                </div>

			</div>

			<div class="cf footer">
				<button id="btnfrmSave" class="btn">Save</button>
			</div>
		</div>
		<div class="overlay"></div>
	</div>

    
    <!--#########################################################################################Criteria: Start################-->
    <table>
        <tr>
            <td>
                <div class="row criteria MDepartment" style="padding:10px 0px">

                    <div class="col-xs-10 nopadding" style="width:85%">

                        <div class="row">

                            <div class="col-xs-4 nopadding col1" style="width:255px; margin-left:30px">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="width:100px">Dept ID:</label>
                                    <div class="col-xs-8 nopadding" style="width:150px">
                                        <input type="text" id="txtDeptID" class="form-control mycustom" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-4 nopadding col1" style="width:290px; margin-left:30px">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="width:100px">Dept Code:</label>
                                    <div class="col-xs-8 nopadding" style="width:160px">
                                        <input type="text" id="txtDeptCode" class="form-control mycustom" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-4 nopadding col1" style="width: 420px; margin-left:10px">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="width:110px">Dept Name:</label>
                                    <div class="col-xs-8 nopadding" style="width:305px">
                                        <input type="text" id="txtDeptName" class="form-control mycustom" runat="server" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        
                        <div class="row" style="margin-top: 10px; display:none">

                            <div class="col-xs-4 nopadding" style="width: 346px; margin-left:20px; display:none">
                                <label class="control-label col-xs-4" style="width:178px">Approved Date &nbsp;&nbsp;&nbsp;&nbsp;From:</label>
                                <div class="col-xs-8 nopadding" style="width:161px">
                                    <div class='input-group date' id='dpdStartDate'>
                                        <input id='txtDateStart' type='text' class="form-control mycustom" runat="server" />
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-4 nopadding" style="width: 200px; margin-left:0px; display:none">
                                <label class="control-label col-xs-4" style="width:50px">To:</label>
                                <div class="col-xs-8 nopadding" style="width:140px">
                                    <div class='input-group date' id='dpdEndDate'>
                                        <input id='txtDateEnd' type='text' class="form-control mycustom" runat="server" />
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-4 nopadding" style="width: 280px; margin-left:30px; display:none">
                                <label class="control-label col-xs-4" style="width:100px">Create By:</label>
                                <div class="col-xs-8 nopadding" style="width:150px">
                                    <input type="text" id="txtCreatedBy" class="form-control mycustom" runat="server" />
                                </div>
                            </div>

                        </div>
                          
                    </div>

                    <div class="col-xs-2 nopadding" style="width:15%">
                        <div class="col-xs-6 nopadding" style="width:100px"><input type="button" id="btnSearch" value="Search" class="btn" /></div>
                        <div class="col-xs-6 nopadding" style="width:50px; text-align:right"><a id="btnAdd" href="#modal"><img src="../Images/Add.png" width="40" alt="เพิ่มข้อมูล" /></a></div>
                    </div>

                </div>

                
            </td>
        </tr>
    </table>
    <!--#########################################################################################Criteria: End################-->
     
    <br />   


    <div style="text-align:center" class="ft16 b"><b><asp:label runat="server" ID ="lblcaption">Department / จัดการข้อมูลแผนก</asp:label></b><br /><br /></div>


    <div id="dvViewByDepartmentNo" style="display:none">
        
      <div class="row">    
      
        <table id="grdDepartment_SearchByDepartmentNo"></table>
        <div id="grdDepartment_SearchByDepartmentNoPager"></div>

      </div>
    </div>  



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CotentScript" runat="server">

    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {


            //prmCustID = Sys.WebForms.PageRequestManager.getInstance();

            $.getScript("Scripts/pageScript/ManageAccount.js", function () {

            

                bindEvent();


                //SetSelect2CustID(txtCustID, MyCustom_AfterSelectedCustID, MyCustom_FormatSelectionCustID);
                //SetSelect2CustID(txtCustID, MyCustom_AfterSelectedCustID, null);



            });

        });

    </script>

</asp:Content>
