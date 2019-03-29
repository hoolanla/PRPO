<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site.Master" AutoEventWireup="true" CodeBehind="FrmApplication.aspx.cs" Inherits="PR_PO.PROJECT.FrmApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <script type='text/javascript'>
            function getMouseXY(e) {
              //  document.getElementById('x').value = e.pageX || event.clientX;
                //document.getElementById('y').value = e.pageY || event.clientY;

                //  alert(e.pageX);

                //var d = document.getElementById('signature');
                //d.style.position = "absolute";
                //d.style.left = e.pageX - 220 + 'px';
                //d.style.top = e.pageY - 20 + 'px';

                var d = document.getElementById('signature');
                d.style.position = "absolute";
                d.style.left = e.pageX - 220 + 'px';
                d.style.top = e.pageY - 20 + 'px';



                return true;
            }
</script>


    <style>
p.serif {
    font-family:Tahoma;
}

p.sansserif {
    font-family:"PSL Chamnarn Pro";
}
    p.form-pdf {
        font-family:"PSL Chamnarn Pro";
        /*font-size: 13px;*/
        color: #333;
        width: 137px;
        
        position:absolute;
        left:600px;
        Top: 400px

   
    }

    p.form-pdf-checkbox {
        font-family: "PSL Chamnarn Pro";
        font-size: 18px;
        color: #333;
        font: bold 12px/30px Tahoma;
       
    }
      p.form-pdf-checkbox {
        font-family:"PSL Chamnarn Pro";
        font-size: 20px;
        color: #333;
        position:absolute;
       
    }
      .container{
text-align: center;

      }

}

   @page {
        size: A4;
        margin: 0;
    }
   @media print {
        html, body {
            width: 210mm;
            height: 297mm;
        }
    }

      row { margin-right: 0px!important;}
      .wrapper {
    background: none!important;
}

      body {
  background-color:#ffffff;

}

</style>


 <div class="container container-table" style="background-color: #ffffff" >

     <!-- PAGE 1 -->
 <div class="row vertical-center-row">
 <div class="text-center col-md-1 col-md-offset-1" >
 <%-- <img  src="ImagesDoc/resize.PNG" width="800" alt="background image" onmousedown='return  getMouseXY(event);'  />--%>
       <img  src="ImagesDoc/convertToZoom.PNG" width="900" alt="background image" onmousedown='return  getMouseXY(event);'  />

     

<%--           <%--<object width="780" height="1096" data="Images/test.pdf" onmousedown='return  getMouseXY(event);'>--%>


<%--     <hr />
  X:
  <input id='x' />
  Y:
  <input id='y' />--%>


 


<div  id="signature1"><p id="signature" class="form-pdf"><img src="Images/signature.png" width ="160" height="50" /></p></div>
 


</div>
</div>



    

    
 




  <div class="row vertical-center-row">
      <div class="text-center col-md-1 col-md-offset-1" >
              <asp:Button ID="btnSubmit" runat="server" Text="Go" OnClick="btnSubmit_Click" />
 </div>
       </div>
   </div>


</asp:Content>
