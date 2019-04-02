<%@ Page Title="Sendmail Approve" Language="C#" MasterPageFile="~/Masterpage/Site.Master" AutoEventWireup="true" CodeBehind="SendMailApprove.aspx.cs" Inherits="PR_PO.PROJECT.SendMailApprove"  EnableEventValidation="FALSE" %>





<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    

        <asp:ScriptManager  runat="server">
            <Scripts>
              
                <%--Framework Scripts--%>
               <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />

                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
         </Scripts>
        </asp:ScriptManager>

   <br />
    <br />
      <div class="panel panel-default">
    <div class="panel-body"><h2>PR: Sendmail Approve</h2></div>
  </div>

   
            <br />
    <hr />
    <br />


    <script src="Scripts/jquery.min.js"></script>
    

<link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>

<link href="CSS/bootstrap_multiselect.css" rel="stylesheet" type="text/css" />
<script src="Scripts/bootstrap-multiselect.js" type="text/javascript"></script>
<script type="text/javascript">
   


    $(function () {
        $('[id*=dlemailCC]').multiselect({
            includeSelectAllOption: true
        });
    });
</script>






<div class="form-group">

                    <label class="white col-sm-3 control-label " id="lblcountry">Send to :</label>
                    <div class="col-sm-9">
                        <div class="row">
                            <div class="col-md-4 gap-text">
                                <select class="form-control ft20" id="dlemail" tabindex ="1" name="dlemail">     
                                      <option value="THAILAND" selected ="selected"></option>
                      
                                </select>
                            </div>
             
                        </div>
                    </div>
                </div>     

<br />
    <br />

    <div class="form-group">

                    <label class="white col-sm-3 control-label " id="lblCC">CC :</label>
                    <div class="col-sm-9">
                        <div class="row">
                            <div class="col-md-4 gap-text">
                              <%--  <select class="form-control ft20" id="dlemailCC" tabindex ="2" name="dlemailCC" multiple="multiple">     
                                      <option value="THAILAND" selected ="selected"></option>
                                </select>--%>

                          <asp:Listbox ID="dlemailCC" runat="server" SelectionMode="Multiple">

 
</asp:Listbox>

                            </div>
             
                        </div>
                    </div>
                </div> 



        <asp:HiddenField ID="doc_id" runat="server"  />
        <asp:HiddenField ID="email" runat="server" />
        <asp:HiddenField ID="content" runat="server" />



        <div class="row">
        <div class="col-md-4">
        
        </div>
        <div class="col-md-4">
 
            <br />
            <br />


                    <asp:Button ID="btnUpload" runat="server" Text="SEND MAIL" CssClass="btn btn-default"
onclick="btnUpload_Click" />


         
        </div>
        <%--<div class="col-md-4">--%>
     
        <%--</div>--%>
    </div>    
         

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CotentScript" runat="server">
  
    <script src="<%=ResolveClientUrl("~/Scripts/pageScript/SendMailApprove.js")%>"></script>
     <input runat="server" type="hidden" value="" id="HidEmail"  />
</asp:Content>





