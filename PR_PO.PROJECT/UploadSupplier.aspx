<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="UploadSupplier.aspx.cs" Inherits="PR_PO.PROJECT.UploadSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


      <h3>File main [XLSX]</h3>
        <asp:FileUpload ID="FileUpload1" runat="server" />

        <br />



        <script type="text/javascript">
            $(function () {
                $('#<%=FileUpload1.ClientID %>').change(
            function () {
                var fileExtension = ['xlsx'];
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    alert("Only '.xlsx' formats are allowed.");
                    $('#<%=FileUpload1.ClientID %>').val('');
                }

            })
            })
</script>





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
<asp:Content ID="Content2" ContentPlaceHolderID="CotentScript" runat="server">
</asp:Content>
