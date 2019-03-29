
var getEmailUrl = "./SendMailApprove.aspx/getEmail";
var dlemail = $("#dlemail");
var dlemailCC = $("#dlemailCC");
var btnUpload = $("#btnUpload");
var HidEmail = $("#HidEmail");

function validateform() {
    var ret = true;
    $(".has-error , .has-success").removeClass("has-error has-success");

    if (dlemail.val() == -1) {
     //   dlemail.parent().addClass("has-error");
        dlemail.addClass("has-error");
        ret = false;
    }

    return ret;
}

function getEmail() {

        dlemail.find("option").remove();
        dlemail.append($('<option></option>').val(-1).html('<-- เลือกอีเมล์ -->').prop('selected', true));
    return $.ajax({
        type: "POST",
        url: "./SendMailApprove.aspx/getEmail",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {
            if (data.d) {
                    $.each(data.d, function (index, item) {
                        dlemail.append($('<option></option>').val(item.Email).html(item.Email));
                    
                    });
            }
        }
    });
}



//function getEmailCC() {
  
//    dlemailCC.find("option").remove();
//    dlemailCC.append($('<option></option>').val(-1).html('<-- เลือกอีเมล์ -->').prop('selected', true));
//    return $.ajax({
//        type: "POST",
//        url: "./Upload.aspx/getEmailCC",
//        data: "{}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
        
//            if (data.d) {
//                $.each(data.d, function (index, item) {
//                    dlemailCC.append($('option></option>').val(item.Email).html(item.Email));

//                    //var listBox = $('[id*=dlemailCC]');
//                    //$.map(data, function (item) {
//                    //    var option = document.createElement('option');
//                    //    option.text = item.Email;
//                    //    listBox[0].add(option);
//                    //});

//                });
//            }
//        }
//    });
//}




$(document).ready(function () {

   getEmail();


 //   getEmailCC();
 

    btnUpload.click(function (evt) {
        alert('evt');

        if (validateform()) {
            //  ServerValidation();
        }


        evt.preventDefault();

    });

});


// Event here 



