<%@ Page Title="Upload" Language="C#" MasterPageFile="~/Masterpage/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="PR_PO.PROJECT.Upload"  EnableEventValidation="FALSE" %>




<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  



        <telerik:RadScriptManager ID="mgr1" runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="ajxMgr" runat="server"></telerik:RadAjaxManager>



    <h2><%: Title %>
     
        </h2>

   


    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
        <script src="jquery.multifile.js"></script>


<br />
    <br />   

    <div class="custom-control custom-radio">
           <asp:RadioButton  class="custom-control-input"  id="PR_Check" GroupName="radio1" runat="server" Checked="true" />
  <label class="custom-control-label" for="PR_Check">PR</label>
</div>

    <div class="custom-control custom-radio">
           <asp:RadioButton  class="custom-control-input" id="PO_Check" runat="server" GroupName ="radio1"/>
  <label class="custom-control-label" for="PO_Check">PO</label>
</div>

        <br />
    <hr />
    <br />

    <div class="form-group">

                    <label class="white col-sm-3 control-label " id="lblCC">Content :</label>
                    <div class="col-sm-9">
                        <div class="row">
                            <div class="col-md-4 gap-text">
                              <%--  <select class="form-control ft20" id="dlemailCC" tabindex ="2" name="dlemailCC" multiple="multiple">     
                                      <option value="THAILAND" selected ="selected"></option>
                      
                                </select>--%>

                           <asp:TextBox runat="server" ID="Content" CssClass="form-control" OnTextChanged="Content_TextChanged" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Content"
                                CssClass="text-danger" ErrorMessage="The content field is required." />

                            </div>
             
                        </div>
                    </div>
                </div> 



    
    <div class="form-group">
  <label for="ddlCustomer">Customer:</label><telerik:RadComboBox ID="ddlCustomer" Runat="server"   EnableLoadOnDemand="true" DataTextField="cust_name" DataValueField="cust_id" ItemsPerRequest="10" EnableVirtualScrolling="true" Width="300px" AutoPostBack="False">
        </telerik:RadComboBox>
<asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCustomer"
          CssClass="text-danger" ErrorMessage="Please select one item." />

<%--             <telerik:RadButton RenderMode="Lightweight" runat="server" ID="Button1" Text="Submit" OnClick="Button1_Click" CssClass="submit" data-toggle="collapse" data-target="#demo"  />--%>

</div>




    <h3>File main [PDF]</h3>
        <asp:FileUpload ID="FileUpload1" runat="server" />
<%--    <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile" />--%>
        <br />
<asp:Label ID="lblMessage" ForeColor="Green" runat="server" />
    <br />
    <hr />
    <br />
    <h2>Attach file</h2>
<input id="file_input" type="file" name="files[]" class="multi" >

<script type="text/javascript">
    jQuery(function ($) {
        $('#file_input').multifile();
    });
  </script>
  
    <script type="text/javascript">
        $(function () {
            $('#<%=FileUpload1.ClientID %>').change(
            function () {
                var fileExtension = ['jpeg', 'pdf'];
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                     alert("Only '.pdf' formats are allowed.");
                     $('#<%=FileUpload1.ClientID %>').val('');
                }
              
            })
    })
</script>


<%--        <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=form1.ClientID%>").validate({
                errorElement: "em",
                errorPlacement: function (error, element) {
                    error.insertAfter(element.parent());
                },
                errorClass: "hasError"
            });
            //Add rules for File upload control
            $("#<%=FileUpload1.ClientID%>").rules('add', {
                required: true,
                accept: "image/jpeg,image/jpg",
                fileSize: true,
                messages: {
                    accept: "Invalid file format"
                }
            });
            //custom validation method for file size
            $.validator.addMethod("fileSize", function (value, element) {
                files = element.files;
                size = files[0].size;
                if (size > 71680) {
                    return false;
                }
                return true;
            }, 'file should be upto 70 kb');
            $(document).on("change", function () {
                $("#<%=form1.ClientID%>").valid();
            });
        });

    </script>--%>

        <div class="row">
        <div class="col-md-4">
        
        </div>
        <div class="col-md-4">
 

                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-default"
onclick="btnUpload_Click" />


         
        </div>
        <%--<div class="col-md-4">--%>
     
        <%--</div>--%>
    </div>    
         

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CotentScript" runat="server">
  
    <script src="<%=ResolveClientUrl("~/Scripts/pageScript/Upload.js")%>"></script>
     <input runat="server" type="hidden" value="" id="HidEmail"  />
</asp:Content>





