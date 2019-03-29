<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="PO.aspx.cs" Inherits="PR_PO.PROJECT.PO" %>

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

                  <telerik:GridBoundColumn HeaderText="Create by" DataField="create_by" UniqueName="create_by" Display="false">
      </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn HeaderText="Create Date" DataField="Create_Date" UniqueName="Create_Date" Display="false">
      </telerik:GridBoundColumn>



            <telerik:GridTemplateColumn HeaderText="PR File">
        <ItemTemplate>
              <asp:ImageButton ID="ImagePDF_Prepare2" runat="server" CausesValidation="false" CommandName="" ImageUrl="~/Images/pdf.png" Height="20" Width="20" Text='<%# Eval("doc_id") %>' />
                      <asp:LinkButton   ID="PDF_PREPARE2" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PDF_PREPARE" OnClick="lnkDownload_Click">Download</asp:LinkButton>
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

                  <telerik:GridTemplateColumn HeaderText="Sign prepare">
        <ItemTemplate>
                  <asp:imagebutton  ID="step2" runat="server" CommandName="step2"  CommandArgument='<%# Eval("step2") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


                       <telerik:GridTemplateColumn HeaderText="Send mail" Display="false">
        <ItemTemplate>
                  <asp:imagebutton  ID="step3" runat="server" CommandName="step3"  CommandArgument='<%# Eval("step3") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>


                                 <telerik:GridBoundColumn HeaderText="Approve by" DataField="secure_approve" UniqueName="secure_approve" >
      </telerik:GridBoundColumn>

                       <telerik:GridTemplateColumn HeaderText="Result">
        <ItemTemplate>
                  <asp:imagebutton  ID="step4" runat="server" CommandName="step4"  CommandArgument='<%# Eval("step4") %>' ></asp:imagebutton>
     </ItemTemplate>
       </telerik:GridTemplateColumn>




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



<asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="grid_RowDataBound" DataKeyNames="doc_id" OnRowCommand="grid_RowCommand"  HeaderStyle-BackColor="#3AC0F2"
        HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"
        RowStyle-ForeColor="#3A3A3A"  AllowPaging="True"
         OnPageIndexChanged="grid_PageIndexChanged" OnPageIndexChanging="grid_PageIndexChanging" OnRowDeleted="grid_RowDeleted" OnRowDeleting="grid_RowDeleting">
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <Columns>
            <asp:BoundField DataField="doc_id" HeaderText="Document ID" />
            <asp:BoundField DataField="doc_name" HeaderText="Document Name" />
            <asp:BoundField DataField="create_by" HeaderText="Create By" />
            <asp:BoundField DataField="create_date" HeaderText="Create Date" />
            <asp:BoundField DataField="secure_prepare"  ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"   >
<HeaderStyle CssClass="hiddencol"></HeaderStyle>

<ItemStyle CssClass="hiddencol"></ItemStyle>
            </asp:BoundField>
            <asp:ButtonField ButtonType="Image" ControlStyle-Width="20px" ControlStyle-Height="20px" ImageUrl="~/Images/pdf.png" Text="PDF" DataTextField="doc_id" HeaderText="Sign prepare" CommandName="PDF">
<ControlStyle Height="20px" Width="20px"></ControlStyle>
                <HeaderStyle CssClass="hiddencol"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="hiddencol" />
            </asp:ButtonField>
            <asp:TemplateField HeaderText="Attach Files" ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="" ImageUrl="~/Images/zip.png" Text='<%# Eval("doc_id") %>' />
                      <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="cmd">Download</asp:LinkButton>
                </ItemTemplate>
                <ControlStyle Height="20px" Width="20px" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Image" DataTextField="step3" HeaderText="Send mail" ImageUrl="~/Images/email.png" Text="EMAIL" CommandName="EMAIL">
            <ControlStyle Height="20px" Width="20px" />
                <HeaderStyle CssClass="hiddencol"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="hiddencol" />
            </asp:ButtonField>
            <asp:TemplateField HeaderText="Sign prepare">
              
                <ItemTemplate>
                    <asp:imagebutton  ID="step2" runat="server" CommandName="step2"  CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" ></asp:imagebutton>
                </ItemTemplate>
                <ControlStyle Height="20px" Width="20px" />
                <HeaderStyle Height="20px" Width="20px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>



            <asp:TemplateField HeaderText="Send mail">
             
                <ItemTemplate>
                    <asp:imagebutton ID="step3" runat="server" CommandName="step3" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"  ></asp:imagebutton>
                </ItemTemplate>
                <ControlStyle Height="20px" Width="20px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Approve">
             
                <ItemTemplate>
                    <asp:imagebutton ID="step4" runat="server" CommandName="step4"   CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" ></asp:imagebutton>
                </ItemTemplate>
                <ControlStyle Height="20px" Width="20px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
<asp:BoundField DataField="secure_approve">
<HeaderStyle CssClass="hiddencol"></HeaderStyle>

<ItemStyle CssClass="hiddencol"></ItemStyle>
</asp:BoundField>
            <asp:TemplateField HeaderText="Prepare View">
                
                    <ItemTemplate>
                                   <asp:ImageButton ID="ImagePDF_Prepare" runat="server" CausesValidation="false" CommandName="" ImageUrl="~/Images/pdf.png" Text='<%# Eval("doc_id") %>' />
                      <asp:LinkButton ID="PDF_PREPARE" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PDF_PREPARE">Download</asp:LinkButton>
                            </ItemTemplate>
                   </asp:TemplateField>
                  <asp:TemplateField HeaderText="Approve View">


                    <ItemTemplate>
                                   <asp:ImageButton ID="ImagePDF" runat="server" CausesValidation="false" CommandName="" ImageUrl="~/Images/pdf.png" Text='<%# Eval("doc_id") %>' />
                      <asp:LinkButton ID="PDF_APROVE" runat="server" CommandArgument='<%# Eval("doc_id") %>' CommandName="PDF_APPROVE">Download</asp:LinkButton>
                            </ItemTemplate>

            </asp:TemplateField>


     
            <asp:BoundField DataField="secure_approve"    HeaderText="Approve Name">
            </asp:BoundField>
            <asp:BoundField DataField="approve_problem" HeaderText="Not Approve" />
            <asp:TemplateField HeaderText="Delete">

                   <ItemTemplate>
                                
                      <asp:ImageButton ID="DELETE" runat="server" OnClientClick="return confirm('Are you sure you want to delete this event?');" CommandName="DELETE" ImageUrl="~/Images/DeleteHS.png" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                            </ItemTemplate>
            </asp:TemplateField>
        </Columns>

<HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>

    <PagerSettings Mode="NumericFirstLast" />

<RowStyle BackColor="#A1DCF2" ForeColor="#3A3A3A"></RowStyle>
    </asp:GridView>
            </div>


        </td></tr></table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CotentScript" runat="server">
</asp:Content>
