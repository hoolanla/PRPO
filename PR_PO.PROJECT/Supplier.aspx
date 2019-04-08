<%@ Page  Title="PR" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Supplier.aspx.cs" Inherits="PR_PO.PROJECT.Supplier" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  

    <script type="text/javascript">
        function callBackFn(arg) {
            alert("this is the client-side callback function. The RadAlert returned: " + arg);
        }
</script>

    <br />
    <br />


 <div class="panel panel-default">
    <div class="panel-body"><h2>PR</h2></div>
  </div>


    <table border="0"><tr><td class="auto-style1"></td></tr></table>


    <table style="width:100%" border="0"><tr><td>
        <div  style="width:100%" >

      <telerik:RadScriptManager ID="mgr1" runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="ajxMgr" runat="server"></telerik:RadAjaxManager>
               <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                     </telerik:RadWindowManager>
                 <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                  
                  
        </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1"  runat="server" LoadingPanelID="RadAjaxLoadingPanel1">



            <telerik:RadGrid ID="RadGrid1"  runat="server" AutoGenerateColumns="false"    AllowPaging="true" PageSize="10"   onitemdatabound="RadGrid1_ItemDataBound" 
                 OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource"   
       
                OnPageIndexChanged="RadGrid1_PageIndexChanged" OnPageSizeChanged="RadGrid1_PageSizeChanged">
          
                 <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="True" ShowFooter="True">
    <Columns>

    

                  <telerik:GridBoundColumn HeaderText="Supp company" DataField="supp_company" UniqueName="supp_company" Display="true" ShowFilterIcon="false"                 
                       AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" AllowFiltering="True" FilterControlWidth="80px">
      </telerik:GridBoundColumn>

                          <telerik:GridBoundColumn HeaderText="Supp code"  DataField="supp_code"   AllowFiltering="True"  UniqueName="supp_code"   AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="80px">
      </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn HeaderText="Supplier Name" DataField="supp_short_name" UniqueName="supp_short_name" 
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false"  AllowFiltering="true" FilterControlWidth="80px">
      </telerik:GridBoundColumn>

                                  <telerik:GridBoundColumn HeaderText="Address" DataField="supp_address_1" UniqueName="supp_address_1" 
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false"  AllowFiltering="true" FilterControlWidth="80px">
      </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn HeaderText="Contact person" DataField="supp_contact_person" UniqueName="supp_contact_person" 
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false"  AllowFiltering="true" FilterControlWidth="80px">
      </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn HeaderText="Contact position" DataField="supp_contact_position" UniqueName="supp_contact_position" 
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false"  AllowFiltering="true" FilterControlWidth="80px">
      </telerik:GridBoundColumn>


    </Columns>
  </MasterTableView>
                
                
                
         </telerik:RadGrid>

       </telerik:RadAjaxPanel>



            </div>


        </td></tr></table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CotentScript" runat="server">
</asp:Content>
