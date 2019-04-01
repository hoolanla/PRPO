<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="PO_DataDocument.aspx.cs" Inherits="PR_PO.PROJECT.PO_DataDocument" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <style>
body {
    background-color: linen;
}

h1 {
    color: maroon;
    margin-left: 40px;


} 

.hiddencol { display: none; }



    .rounded_corners
    {
        border: 1px solid #A1DCF2;
        -webkit-border-radius: 8px;
        -moz-border-radius: 8px;
        border-radius: 8px;
        overflow: hidden;
    }
    .rounded_corners td, .rounded_corners th
    {
        border: 1px solid #A1DCF2;
        font-family: Arial;
        font-size: 10pt;
        text-align: center;
    }
    .rounded_corners table table td
    {
        border-style: none;
    }

    table{
    width:100%;
}
        .auto-style1 {
            height: 22px;
        }
    </style>


  

    <table border="0"><tr><td class="auto-style1"></td></tr></table>


    <table style="width:100%" border="1"><tr><td>
        <div class="rounded_corners" style="width:100%" >

      <telerik:RadScriptManager ID="mgr1" runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="ajxMgr" runat="server"></telerik:RadAjaxManager>

                 <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1"  runat="server" LoadingPanelID="RadAjaxLoadingPanel1">



            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false"    AllowPaging="true" PageSize="2"   onitemdatabound="RadGrid1_ItemDataBound" OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnPageSizeChanged="RadGrid1_PageSizeChanged">
          
                 <MasterTableView >
    <Columns>

             <telerik:GridBoundColumn HeaderText="Document id" DataField="doc_id" UniqueName="doc_id">
      </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn HeaderText="Doc Name" DataField="doc_name" UniqueName="doc_name" Display="false">
      </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn HeaderText="PR#" DataField="content" UniqueName="content" >
      </telerik:GridBoundColumn>

        <telerik:GridBoundColumn HeaderText="Supplier Name" DataField="supplier_name" UniqueName="supplier_name" >
      </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn HeaderText="Create by" DataField="create_by" UniqueName="create_by" Display="false">
      </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn HeaderText="Create Date" DataField="Create_Date" UniqueName="Create_Date" Display="false">
      </telerik:GridBoundColumn>



            <telerik:GridTemplateColumn HeaderText="PO File">
        <ItemTemplate>
              <asp:ImageButton ID="ImagePDF1" runat="server" CausesValidation="false" CommandName="" ImageUrl="~/Images/pdf.png" Height="20" Width="20" Text='<%# Eval("doc_id") %>' />
                      <asp:LinkButton   ID="PO_PDF" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PO_PDF">Download</asp:LinkButton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>

    
            <telerik:GridTemplateColumn HeaderText="PR File">
        <ItemTemplate>
              <asp:ImageButton ID="ImagePDF2" runat="server" CausesValidation="false" CommandName="" ImageUrl="~/Images/pdf.png" Height="20" Width="20" Text='<%# Eval("doc_id") %>' />
                      <asp:LinkButton   ID="PDF_Approve" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PDF_Approve" >Download</asp:LinkButton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


      <telerik:GridTemplateColumn HeaderText="Attach Files">
        <ItemTemplate>
       <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="" ImageUrl="~/Images/zip.png" Height="20" Width="20" Text='<%# Eval("doc_id") %>' />
                      <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="cmd">Download</asp:LinkButton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>

                      
             <telerik:GridBoundColumn HeaderText="Review By" DataField="secure_prepare" UniqueName="secure_prepare" >
      </telerik:GridBoundColumn>

                  <telerik:GridTemplateColumn HeaderText="Request">
        <ItemTemplate>
                  <asp:imagebutton  ID="step2" runat="server" CommandName="step2"  CommandArgument='<%# Eval("step2") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


                       <telerik:GridTemplateColumn HeaderText="Review">
        <ItemTemplate>
                  <asp:imagebutton  ID="step3" runat="server" CommandName="step3"  CommandArgument='<%# Eval("step3") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


        <telerik:GridTemplateColumn HeaderText="Approve">
        <ItemTemplate>
                  <asp:imagebutton  ID="step4" runat="server" CommandName="step4"  CommandArgument='<%# Eval("step4") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>




                                 <telerik:GridBoundColumn HeaderText="Approve by" DataField="secure_approve" UniqueName="secure_approve" >
      </telerik:GridBoundColumn>

  

            <telerik:GridTemplateColumn HeaderText="Approve view">
        <ItemTemplate>
     <asp:ImageButton ID="ImagePDF" runat="server" CausesValidation="false" CommandName="" ImageUrl="~/Images/pdf.png" Height="20" Width="20" Text='<%# Eval("doc_id") %>' />
                      <asp:LinkButton ID="PDF_APROVE" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PDF_APPROVE">Download</asp:LinkButton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


    </Columns>
  </MasterTableView>
                
                
                
         </telerik:RadGrid>

       </telerik:RadAjaxPanel>




            </div>


        </td></tr></table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CotentScript" runat="server">
</asp:Content>
