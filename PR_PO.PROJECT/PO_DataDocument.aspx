<%@ Page Title="PO#" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="PO_DataDocument.aspx.cs" Inherits="PR_PO.PROJECT.PO_DataDocument" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
 



    <br />
    <br />


 <div class="panel panel-default">
    <div class="panel-body"><h2>PO</h2></div>
  </div>

    <table border="0"><tr><td class="auto-style1"></td></tr></table>


    <table style="width:100%" border="0"><tr><td>
        <div  style="width:100%" >

      <telerik:RadScriptManager ID="mgr1" runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="ajxMgr" runat="server"></telerik:RadAjaxManager>

                 <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1"  runat="server" LoadingPanelID="RadAjaxLoadingPanel1">



            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false"    AllowPaging="true" PageSize="10"   onitemdatabound="RadGrid1_ItemDataBound" OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnPageSizeChanged="RadGrid1_PageSizeChanged">
          
                 <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True">
    <Columns>

             <telerik:GridBoundColumn HeaderText="Document id" DataField="doc_id" UniqueName="doc_id" AllowFiltering="True"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="80px">
      </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn HeaderText="Doc Name" DataField="doc_name" UniqueName="doc_name" Display="false">
      </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn HeaderText="PR#" DataField="content" UniqueName="content" AllowFiltering="True"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="80px" >
      </telerik:GridBoundColumn>

        <telerik:GridBoundColumn HeaderText="Supplier Name" DataField="supplier_name" UniqueName="supplier_name" AllowFiltering="True"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="80px">
      </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn HeaderText="Create by" DataField="create_by" UniqueName="create_by" Display="false">
      </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn HeaderText="Create Date" DataField="Create_Date" UniqueName="Create_Date" Display="false">
      </telerik:GridBoundColumn>



            <telerik:GridTemplateColumn HeaderText="PO File" AllowFiltering="false">
        <ItemTemplate>

                      <asp:LinkButton   ID="PO_PDF" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PO_PDF"> <asp:Image ID="img_po"  runat="server" ImageUrl="Images/pdf.png" Height="20" Width="20"  /></asp:LinkButton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>

    
            <telerik:GridTemplateColumn HeaderText="PR File" AllowFiltering="false">
        <ItemTemplate>
          
                      <asp:LinkButton   ID="PDF_Approve" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PDF_Approve" > <asp:Image ID="img_pr"  runat="server" ImageUrl="Images/pdf.png" Height="20" Width="20"  /></asp:LinkButton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


      <telerik:GridTemplateColumn HeaderText="Attach Files" AllowFiltering="false">
        <ItemTemplate>

                      <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="cmd"> <asp:Image ID="img_zip"  runat="server" ImageUrl="Images/zip.png" Height="20" Width="20"  /></asp:LinkButton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>

                      
             <telerik:GridBoundColumn HeaderText="Review By" DataField="secure_prepare" UniqueName="secure_prepare" AllowFiltering="True"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="80px" >
      </telerik:GridBoundColumn>

                  <telerik:GridTemplateColumn HeaderText="Request" AllowFiltering="false">
        <ItemTemplate>
                  <asp:imagebutton  ID="step2" runat="server" CommandName="step2" 
                    PostBackUrl='<%# "PO_SendMailRequest.aspx?doc_id=" + Eval("doc_id") + "&email=" + Session["EMAIL"].ToString() + "&content=" + Eval("content") %>' 
                       CommandArgument='<%# Eval("step2") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


                       <telerik:GridTemplateColumn HeaderText="Review" AllowFiltering="false">
        <ItemTemplate>
                  <asp:imagebutton  ID="step3" runat="server" CommandName="step3" 
                        PostBackUrl='<%# "PO_SendMailReview.aspx?doc_id=" + Eval("doc_id") + "&email=" + Session["EMAIL"].ToString() + "&content=" + Eval("content") %>' 
                       CommandArgument='<%# Eval("step3") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


        <telerik:GridTemplateColumn HeaderText="Approve" AllowFiltering="false">
        <ItemTemplate>
                  <asp:imagebutton  ID="step4" runat="server" CommandName="step4" 
                       PostBackUrl='<%# "PO_SendMailApprove.aspx?doc_id=" + Eval("doc_id") + "&email=" + Session["EMAIL"].ToString() + "&content=" + Eval("content") %>' 
                       CommandArgument='<%# Eval("step4") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>




                                 <telerik:GridBoundColumn HeaderText="Approve by" DataField="secure_approve" UniqueName="secure_approve" AllowFiltering="True"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="80px" >
      </telerik:GridBoundColumn>

  

            <telerik:GridTemplateColumn HeaderText="Approve view" AllowFiltering="false">
        <ItemTemplate>
  
                      <asp:LinkButton ID="PO_PDF_APROVE" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PO_PDF_APPROVE"> <asp:Image ID="img_complete"  runat="server" ImageUrl="Images/pdf.png" Height="20" Width="20"  /></asp:LinkButton>
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
