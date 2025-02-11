﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="DataDocument2.aspx.cs" Inherits="PR_PO.PROJECT.DataDocument2" %>

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
