
var _lang = "";
var _ID = "21";
var _ATID = "";
var _SUT_ID = "9";
var _subChanel = "";
var _profile = "";


//--------------------------- Label -----------------------------//
//var lblTitle = $("[id*=lblTitle]");
//var lblHeaderContent = $("[id*=lblHeaderContent]");

//var lblFirstName = $("[#lblFirstName]");
//var lblLastname = $("[id*=lblLastname]");
//var lblCardId = $("[id*=lblCardId]");
//var lblPhoneNo = $("[id*=lblPhoneNo]");
//var lblEmail = $("[id*=lblEmail]");

//var lblLivingInthailand = $("[id*=lblLivingInthailand]");
//var livingth = $('.livingth');

//var lblchkYes = $("[id*=lblchkYes]");
//var lblchkNo = $("[id*=lblchkNo]");


function BindEvent() {

    if (_ID != '') {
        $.when(initData()).done(function () {

        });
    }
    if (_SUT_ID != '') {
        $.when(ReadSuitData()).done(function () {

        });
    }
};

function initData() {

    var obj = new Object();
    obj.ID = _ID;

    if (_ID == '') {
        return false;
    }

    return $.ajax({
        type: "POST",
        url: './FormApplication.aspx/OnlineCleintID_Read',
        data: JSON.stringify({ "criteria": obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // error: function (XMLHttpRequest, textStatus, errorThrown) {
        // alert("OnlineCleintID_Read some error");
        //},
        success: function (data) {
            if (data.d) {
                //$loading.show();
                console.log(data);
                var d = data.d[0];
                $(document).ready(function () {
                    $("#MktName").text(d.MarketingName);
                    $("#MktCode").text(d.MktCode);
                    $("#1_1_NameTh").text(d.FNameTH);
                    $("#1_1_LastnameTH").text(d.LNameTH);
                    $("#1_1_NameEN").text(d.FNameEN);
                    $("#1_1_LastnameEN").text(d.LNameEN);
                    $("#1_2_PersonalID").text(d.PersonalID);
                    $("#1_2_Mobile").text(d.Mobile);
                    initTitle(d);
                    ShowCurAddress(d);
                    IsSpouse(d);
                    initSex(d);
                    OccID(d);
                    chkTypeOfBusinessID(d);
                    chkSourceOfIncomeID(d);
                    RelatedWithStaffID(d);
                    IsPoliticalPosition(d);
                    IsConvictedMoneyLaundering(d);
                    chkSpouse(d);
                    IsUltimateBenificiary(d);
                    IsUltimateControllingPerson(d);
                    IsSpousePoliticalPosition(d);
                    IsUSCitizen(d);
                    IsBornInUSA(d);
                    HasPOBoxInUS(d);
                    HasUSTelephoneNumber(d);
                    HasPayBenefitToUSAccount(d);
                    HasGrantedToUSNationalityPerson(d);
                    IsW8BEN(d);
                });
            }
        }
    });
}

function ReadSuitData() {

    var obj = new Object();

    obj.SUD_SUT_ID = _SUT_ID;
    if (_SUT_ID == '') {
        return false;
    }

    return $.ajax({
        type: "POST",
        url: './FormApplication.aspx/T_SuitabilityDetail_Read',
        data: JSON.stringify({ "criteria": obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("some error");
        },
        success: function (data) {
            if (data.d.length > 0) {
                var d = data.d[0];
                choice1(data.d[0]);
                choice2(data.d[1]);
                choice3(data.d[2]);
                choice4(data.d[3]);
                choice5(data.d[4]);
                choice6(data.d[5]);
                choice7(data.d[6]);
                choice8(data.d[7]);
                choice9(data.d[8]);
                choice10(data.d[9]);
                totalSuit();
            }
        }
    });

}

function initSex(data) {
    $("#1_2_Male").hide();
    $("#1_2_FeMale").hide();
    if (data.TitleTH == 'นาย') {
        $("#1_2_Male").show();
    } else {
        $("#1_2_FeMale").show();
    }
}

function initTitle(data) {
    $("#1_1_TitleName_1").hide();
    $("#1_1_TitleName_2").hide();
    $("#1_1_TitleName_3").hide();
    if (data.TitleTH == 'นาย') {
        $("#1_1_TitleName_1").show();
    }
    if (data.TitleTH == 'นางสาว') {
        $("#1_1_TitleName_2").show();
    }
    if (data.TitleTH == 'นาง') {
        $("#1_1_TitleName_3").show();
    }
}

//1
function ShowCurAddress(data) {
    $("#1_3_Cur_Add1").text(data.Cur_Add1);
    $("#1_3_Cur_Add2").text(data.Cur_Add2);
    $("#1_3_Cur_Add3").text(data.Cur_Add3);
    $("#1_3_Cur_Add4").text(data.Cur_Add4);
    $("#1_3_Cur_SubDistrict").text(data.Cur_SubDistrict);
    $("#1_3_Cur_District").text(data.Cur_District);
    $("#1_3_Cur_Province").text(data.Cur_Province);
    $("#1_3_Cur_ZipCode").text(data.Cur_ZipCode);
    $("#1_3_Cur_Phone").text(data.Cur_Phone);
    $("#1_3_Cur_Country").text(data.Cur_Country);
}

function calAge() {

    //dob = new Date(dob);
    //var today = new Date();
    //var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));
    //$('#age').html(age + ' years old');

}

// 2 

function OccID(data) {
    $("#2_1_OccID_K207_Other").text(data.OccDesc);
    $("#2_1_OccID_K204").hide();
    $("#2_1_OccID_K311").hide();
    $("#2_1_OccID_K208").hide();
    $("#2_1_OccID_K115").hide();
    $("#2_1_OccID_K116").hide();
    $("#2_1_OccID_K203").hide();
    $("#2_1_OccID_K201").hide();
    $("#2_1_OccID_K206").hide();
    $("#2_1_OccID_K202").hide();
    $("#2_1_OccID_K205").hide();
    $("#2_1_OccID_K207").hide();
    $("#2_1_OccID_K207_Other").hide();
    if (data.OccID == 'K201' || data.OccID == 'K202' || data.OccID == 'K205' || data.OccID == 'K206' || data.OccID == 'K207') {
        showWorkPlace('0', data);
    } else {
        showWorkPlace('1', data);
    }
    if (data.OccID == 'K207') {
        $("#2_1_OccID_K207").show();
        $("#2_1_OccID_K207_Other").show();
    } else {
        $("#2_1_OccID_" + data.OccID).show();
    }
}

function showWorkPlace(flag, data) {

    $("#2_3_TypeOfBusinessDepartment").text(data.TypeOfBusinessDepartment);
    $("#2_3_TypeOfBusinessPosition").text(data.TypeOfBusinessPosition);
    $("#2_4_CompanyName").text(data.CompanyName);
    $("#2_5_Off_Add1").text(data.Off_Add1);
    $("#2_5_Off_Add2").text(data.Off_Add2);
    $("#2_5_Off_Add3").text(data.Off_Add3);
    $("#2_5_Off_Add4").text(data.Off_Add4);
    $("#2_5_Off_SubDistrict").text(data.Off_SubDistrict);
    $("#2_5_Off_District").text(data.Off_District);
    $("#2_5_Off_Province").text(data.Off_Province);
    $("#2_5_Off_Phone").text(data.Off_Phone);
    $("#2_5_Off_Country").text(data.Off_Country);
    $("#2_5_Off_ZipCode").text(data.Off_Zipcode);

    if (flag = '1') {
        $("#2_3_TypeOfBusinessDepartment").show();
        $("#2_3_TypeOfBusinessPosition").show();
        $("#2_4_CompanyName").show();
        $("#2_5_Off_Add1").show();
        $("#2_5_Off_Add2").show();
        $("#2_5_Off_Add3").show();
        $("#2_5_Off_Add4").show();
        $("#2_5_Off_SubDistrict").show();
        $("#2_5_Off_District").show();
        $("#2_5_Off_Province").show();
        $("#2_5_Off_Phone").show();
        $("#2_5_Off_Country").show();
        $("#2_5_Off_ZipCode").show();
    } else {
        $("#2_3_TypeOfBusinessDepartment").hide();
        $("#2_3_TypeOfBusinessPosition").hide();
        $("#2_4_CompanyName").hide();
        $("#2_5_Off_Add1").hide();
        $("#2_5_Off_Add2").hide();
        $("#2_5_Off_Add3").hide();
        $("#2_5_Off_Add4").hide();
        $("#2_5_Off_SubDistrict").hide();
        $("#2_5_Off_District").hide();
        $("#2_5_Off_Province").hide();
        $("#2_5_Off_Phone").hide();
        $("#2_5_Off_Country").hide();
        $("#2_5_Off_ZipCode").hide();
    }
}

function chkTypeOfBusinessID(data) {
    $("#2_2_BusinessID_001").hide();
    $("#2_2_BusinessID_002").hide();
    $("#2_2_BusinessID_003").hide();
    $("#2_2_BusinessID_004").hide();
    $("#2_2_BusinessID_005").hide();
    $("#2_2_BusinessID_006").hide();
    $("#2_2_BusinessID_007").hide();
    $("#2_2_BusinessID_008").hide();
    $("#2_2_BusinessID_009").hide();
    $("#2_2_BusinessID_010").hide();
    $("#2_2_BusinessID_011").hide();
    $("#2_2_BusinessID_012").hide();
    $("#2_2_BusinessID_013").hide();
    $("#2_2_BusinessID_014").hide();
    $("#2_2_BusinessID_015").hide();
    $("#2_2_BusinessID_016").hide();
    $("#2_2_BusinessID_017").hide();
    $("#2_2_BusinessID_018").hide();
    $("#2_2_BusinessID_" + data.TypeOfBusinessID).show();
}

function chkSourceOfIncomeID(data) {

    $("#3_1_sourceOfIncomeDesc").text(data.SourceOfIncomeDesc);
    $("#3_1_sourceOfIncomeDesc").hide();
    $("#3_1_sourceOfIncomeID_001").hide();
    $("#3_1_sourceOfIncomeID_002").hide();
    $("#3_1_sourceOfIncomeID_003").hide();
    $("#3_1_sourceOfIncomeID_004").hide();
    $("#3_1_sourceOfIncomeID_005").hide();
    $("#3_1_sourceOfIncomeID_006").hide();
    $("#3_1_sourceOfIncomeID_099").hide();
    $("#3_1_sourceOfIncomeID_999").hide();
    $("#3_1_sourceOfIncomeID_" + data.SourceOfIncomeID).show();

    if (data.SourceOfIncomeID == '099') {
        $("#3_1_sourceOfIncomeDesc").show();
    }

}
// 7
function chkSpouse(data) {

    $("#7_2_Spouse_Name").text(data.FirstNameTHSpouse);
    $("#7_2_Spouse_LName").text(data.LastNameTHSpouse);

    $("#7_2_Spouse_NO").hide();
    $("#7_2_Spouse_YES").hide();
    $("#7_2_Spouse_Name").hide();
    $("#7_2_Spouse_LName").hide();
    if (data.IsSpouse == true) {
        $("#7_2_Spouse_YES").show();
        $("#7_2_Spouse_Name").show();
        $("#7_2_Spouse_LName").show();
    }
}

function RelatedWithStaffID(data) {
    $("#7_3_Related_Name").text(data.RelatedWithStaffFName);
    $("#7_3_Related_LastName").text(data.RelatedWithStaffLName);
    $("#7_3_Related_Status").text(data.RelatedWithStaffType);
    $("#7_3_Related_Name").hide();
    $("#7_3_Related_LastName").hide();
    $("#7_3_Related_Status").hide();
    $("#7_3_RelatedWithStaffID_NO").hide();
    $("#7_3_RelatedWithStaffID_YES").hide();

    if (data.RelatedWithStaffID == 'true') {
        $("#7_3_RelatedWithStaffID_YES").show();
        $("#7_3_Related_Name").show();
        $("#7_3_Related_LastName").show();
        $("#7_3_Related_Status").show();
    } else {
        $("#7_3_RelatedWithStaffID_NO").show();
    }
}

function IsUltimateBenificiary(data) {
    $("#7_4_UltimateBenificiaryFName").text(data.UltimateBenificiaryFName);
    $("#7_4_UltimateBenificiaryLName").text(data.UltimateBenificiaryLName);
    $("#7_4_UltimateBenificiaryRelationship").text(data.UltimateBenificiaryRelationship);
    $("#7_4_UltimateBenificiary_NO").hide();
    $("#7_4_UltimateBenificiary_YES").hide();
    $("#7_4_UltimateBenificiaryFName").hide();
    $("#7_4_UltimateBenificiaryLName").hide();
    $("#7_4_UltimateBenificiaryRelationship").hide();
    if (data.UltimateBenificiaryID != null) {
        if (data.UltimateBenificiaryID != 'false') {
            $("#7_4_UltimateBenificiary_YES").show();
            $("#7_4_UltimateBenificiaryFName").show();
            $("#7_4_UltimateBenificiaryLName").show();
            $("#7_4_UltimateBenificiaryRelationship").show();
        } else {
            $("#7_4_UltimateBenificiary_NO").show();
        }
    }
}

function IsUltimateControllingPerson(data) {
    $("#7_5_UltimateControllingPersonFName").text(data.UltimateControllingPersonFName);
    $("#7_5_UltimateControllingPersonLName").text(data.UltimateControllingPersonLName);
    $("#7_5_UltimateControllingPersonRelationship").text(data.UltimateControllingPersonRelationship);
    $("#7_5_UltimateControllingPerson_NO").hide();
    $("#7_5_UltimateControllingPerson_YES").hide();
    if (data.UltimateControllingPersonID != null) {
        if (data.UltimateControllingPersonID != 'false') {
            $("#7_5_UltimateControllingPerson_YES").show();
            $("#7_5_UltimateControllingPersonFName").show();
            $("#7_5_UltimateControllingPersonLName").show();
            $("#7_5_UltimateControllingPersonRelationship").show();
        } else {
            $("#7_5_UltimateControllingPerson_NO").show();
        }
    }
}

// 9

function IsSpouse(data) {
    $("#1_2_IsSpouseNO").hide();
    $("#1_2_IsSpouseYES").hide();
    $("#1_2_IsSpouseOther").hide();
    if (data.IsSpouse == true) {
        $("#1_2_IsSpouseYES").show();
    }
}

function IsPoliticalPosition(data) {
    $("#9_1_PoliticalPositionName").text(data.PoliticalPositionName);
    $("#9_1_PoliticalPositionYear").text(data.PoliticalPositionYear);
    $("#9_1_IsPoliticalPosition_NO").hide();
    $("#9_1_IsPoliticalPosition_YES").hide();
    $("#9_1_PoliticalPositionName").hide();
    $("#9_1_PoliticalPositionYear").hide();
    if (data.IsPoliticalPosition == true) {
        $("#9_1_IsPoliticalPosition_YES").show();
        $("#9_1_PoliticalPositionName").show();
        $("#9_1_PoliticalPositionYear").show();
    } else {
        $("#9_1_IsPoliticalPosition_NO").show();
    }
}

function IsConvictedMoneyLaundering(data) {
    $("#9_2_ConvictedMoneyLaunderingName").text(data.ConvictedMoneyLaunderingName);
    $("#9_2_ConvictedMoneyLaunderingYear").text(data.ConvictedMoneyLaunderingYear);

    $("#9_2_IsConvictedMoneyLaundering_NO").hide();
    $("#9_2_IsConvictedMoneyLaundering_YES").hide();
    $("#9_2_ConvictedMoneyLaunderingName").hide();
    $("#9_2_ConvictedMoneyLaunderingYear").hide();
    if (data.IsConvictedMoneyLaundering == true) {
        $("#9_2_IsConvictedMoneyLaundering_YES").show();
        $("#9_2_ConvictedMoneyLaunderingName").show();
        $("#9_2_ConvictedMoneyLaunderingYear").show();
    } else {
        $("#9_2_IsConvictedMoneyLaundering_NO").show();
    }

}

function IsSpousePoliticalPosition(data) {

    $("#9_3_SpousePoliticalPositionYear").text(data.SpousePoliticalPositionYear);
    $("#9_3_SpousePoliticalPositionName").text(data.SpousePoliticalPositionName);
    $("#9_3_SpousePoliticalPositionYear").hide();
    $("#9_3_SpousePoliticalPositionName").hide();
    $("#9_3_IsSpousePoliticalPosition_NO").hide();
    $("#9_3_IsSpousePoliticalPosition_YES").hide();
    if (data.IsSpousePoliticalPosition != false) {
        $("#9_3_SpousePoliticalPositionYear").show();
        $("#9_3_SpousePoliticalPositionName").show();
        $("#9_3_IsSpousePoliticalPosition_YES").show();
    } else {
        $("#9_3_IsSpousePoliticalPosition_NO").show();
    }
}

// 11 FACTA
function IsUSCitizen(data) {
    $("#11_1_IsUSCitizen_NO").hide();
    $("#11_1_IsUSCitizen_YES").hide();

    $("#11_1_USCitizen").hide();
    $("#11_1_HasUSPassport").hide();
    $("#11_1_HasUSGreencard").hide();
    $("#11_1_IsTAXResident").hide();

    if (data.IsUSCitizen == true) {
        $("#11_1_IsUSCitizen_YES").show();
        if (data.USCitizen == true) {
            $("#11_1_USCitizen").show();
        }
        if (data.HasUSPassport == true) {
            $("#11_1_HasUSPassport").show();
        }
        if (data.HasUSGreencard == true) {
            $("#11_1_HasUSGreencard").show();
        }
        if (data.IsTAXResident) {
            $("#11_1_IsTAXResident").show();
        }

    } else {
        $("#11_1_IsUSCitizen_NO").show();
    }
}

function IsBornInUSA(data) {
    $("#11_2_IsBornInUSA_NO").hide();
    $("#11_2_IsBornInUSA_YES").hide();
    if (data.IsBornInUSA == true) {
        $("#11_2_IsBornInUSA_YES").show();
    } else {
        $("#11_2_IsBornInUSA_NO").show();
    }
}

function HasPOBoxInUS(data) {
    $("#11_3_POBoxInUSAddress").text(data.POBoxInUSAddress);
    $("#11_3_HasPOBoxInUS_NO").hide();
    $("#11_3_HasPOBoxInUS_YES").hide();
    $("#11_3_POBoxInUSAddress").hide();
    if (data.HasPOBoxInUS == true) {
        $("#11_3_HasPOBoxInUS_YES").show();
        $("#11_3_POBoxInUSAddress").show();
    } else {
        $("#11_3_HasPOBoxInUS_NO").show();
    }
}

function HasUSTelephoneNumber(data) {
    $("#11_4_USTelephoneNumber").text(data.USTelephoneNumber);
    $("#11_4_USMobileNumber").text(data.USMobileNumber);
    $("#11_4_HasUSTelephoneNumber_NO").hide();
    $("#11_4_HasUSTelephoneNumber_YES").hide();
    $("#11_4_USTelephoneNumber").hide();
    $("#11_4_USMobileNumber").hide();
    if (data.HasUSTelephoneNumber == true) {
        $("#11_4_HasUSTelephoneNumber_YES").show();
        $("#11_4_USTelephoneNumber").show();
        $("#11_4_USMobileNumber").show();
    } else {
        $("#11_4_HasUSTelephoneNumber_NO").show();
    }
}

function HasPayBenefitToUSAccount(data) {
    $("#11_5_PayBenefitToUSAccountBankName").text(data.PayBenefitToUSAccountBankName);
    $("#11_5_PayBenefitToUSAccountBankAccNo").text(data.PayBenefitToUSAccountBankAccNo);
    $("#11_5_HasPayBenefitToUSAccount_NO").hide();
    $("#11_5_HasPayBenefitToUSAccount_YES").hide();
    $("#11_5_PayBenefitToUSAccountBankName").hide();
    $("#11_5_PayBenefitToUSAccountBankAccNo").hide();
    if (data.HasPayBenefitToUSAccount == true) {
        $("#11_5_HasPayBenefitToUSAccount_YES").show();
        $("#11_5_PayBenefitToUSAccountBankName").show();
        $("#11_5_PayBenefitToUSAccountBankAccNo").show();
    } else {
        $("#11_5_HasPayBenefitToUSAccount_NO").show();
    }
}

function HasGrantedToUSNationalityPerson(data) {
    $("#11_6_GrantedToUSNationalityPersonName").text(data.GrantedToUSNationalityPersonName);
    $("#11_6_GrantedToUSNationalityPersonRelationship").text(data.GrantedToUSNationalityPersonRelationship);
    $("#11_6_HasGrantedToUSNationalityPerson_NO").hide();
    $("#11_6_HasGrantedToUSNationalityPerson_YES").hide();
    $("#11_6_GrantedToUSNationalityPersonName").hide();
    $("#11_6_GrantedToUSNationalityPersonRelationship").hide();
    if (data.HasGrantedToUSNationalityPerson == true) {
        $("#11_6_HasGrantedToUSNationalityPerson_YES").show();
        $("#11_6_GrantedToUSNationalityPersonName").show();
        $("#11_6_GrantedToUSNationalityPersonRelationship").show();
    } else {
        $("#11_6_HasGrantedToUSNationalityPerson_NO").show();
    }
}

function IsW8BEN(data) {
    $("#11_7_IsNotW8BEN").hide();
    $("#11_7_IsW9").hide();
    $("#11_7_IsW8BEN").hide();
    if (data.IsW8BEN == true) {
        $("#11_7_IsW8BEN").show();
    }
    if (data.IsW9 == true) {
        $("#11_7_IsW9").show();
    }
    if (data.IsNotW8BEN == true) {
        $("#11_7_IsNotW8BEN").show();
    }
}

function choice1(data) {
    $("#1_SUD_Answer1_Checked").hide();
    $("#1_SUD_Answer2_Checked").hide();
    $("#1_SUD_Answer3_Checked").hide();
    $("#1_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#1_SUD_Answer1_Checked").show();
        $("#1_SUD_Point").text(data.SUD_Answer1_Point);
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#1_SUD_Answer2_Checked").show();
        $("#1_SUD_Point").text(data.SUD_Answer2_Point);
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#1_SUD_Answer3_Checked").show();
        $("#1_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#1_SUD_Answer4_Checked").show();
        $("#1_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function choice2(data) {
    $("#2_SUD_Answer1_Checked").hide();
    $("#2_SUD_Answer2_Checked").hide();
    $("#2_SUD_Answer3_Checked").hide();
    $("#2_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#2_SUD_Answer1_Checked").show();
        $("#2_SUD_Point").text(data.SUD_Answer1_Point);
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#2_SUD_Answer2_Checked").show();
        $("#2_SUD_Point").text(data.SUD_Answer2_Point);
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#2_SUD_Answer3_Checked").show();
        $("#2_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#2_SUD_Answer4_Checked").show();
        $("#2_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function choice3(data) {
    $("#3_SUD_Answer1_Checked").hide();
    $("#3_SUD_Answer2_Checked").hide();
    $("#3_SUD_Answer3_Checked").hide();
    $("#3_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#3_SUD_Answer1_Checked").show();
        $("#3_SUD_Point").text(data.SUD_Answer1_Point);
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#3_SUD_Answer2_Checked").show();
        $("#3_SUD_Point").text(data.SUD_Answer2_Point);
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#3_SUD_Answer3_Checked").show();
        $("#3_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#3_SUD_Answer4_Checked").show();
        $("#3_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function choice4(data) {
    $("#4_SUD_Answer1_Checked").hide();
    $("#4_SUD_Answer2_Checked").hide();
    $("#4_SUD_Answer3_Checked").hide();
    $("#4_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#4_SUD_Answer1_Checked").show();
        $("#4_SUD_Point").text(data.SUD_Answer1_Point);
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#4_SUD_Answer2_Checked").show();
        $("#4_SUD_Point").text(data.SUD_Answer2_Point);
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#4_SUD_Answer3_Checked").show();
        $("#4_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#4_SUD_Answer4_Checked").show();
        $("#4_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function choice5(data) {
    $("#5_SUD_Answer1_Checked").hide();
    $("#5_SUD_Answer2_Checked").hide();
    $("#5_SUD_Answer3_Checked").hide();
    $("#5_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#5_SUD_Answer1_Checked").show();
        $("#5_SUD_Point").text(data.SUD_Answer1_Point);
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#5_SUD_Answer2_Checked").show();
        $("#5_SUD_Point").text(data.SUD_Answer2_Point);
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#5_SUD_Answer3_Checked").show();
        $("#5_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#5_SUD_Answer4_Checked").show();
        $("#5_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function choice6(data) {
    $("#6_SUD_Answer1_Checked").hide();
    $("#6_SUD_Answer2_Checked").hide();
    $("#6_SUD_Answer3_Checked").hide();
    $("#6_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#6_SUD_Answer1_Checked").show();
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#6_SUD_Answer2_Checked").show();
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#6_SUD_Answer3_Checked").show();
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#6_SUD_Answer4_Checked").show();
    }
}

function choice7(data) {
    $("#7_SUD_Answer1_Checked").hide();
    $("#7_SUD_Answer2_Checked").hide();
    $("#7_SUD_Answer3_Checked").hide();
    $("#7_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#7_SUD_Answer1_Checked").show();
        $("#7_SUD_Point").text(data.SUD_Answer1_Point);
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#7_SUD_Answer2_Checked").show();
        $("#7_SUD_Point").text(data.SUD_Answer2_Point);
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#7_SUD_Answer3_Checked").show();
        $("#7_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#7_SUD_Answer4_Checked").show();
        $("#7_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function choice8(data) {
    $("#8_SUD_Answer1_Checked").hide();
    $("#8_SUD_Answer2_Checked").hide();
    $("#8_SUD_Answer3_Checked").hide();
    $("#8_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#8_SUD_Answer1_Checked").show();
        $("#8_SUD_Point").text(data.SUD_Answer1_Point);
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#8_SUD_Answer2_Checked").show();
        $("#8_SUD_Point").text(data.SUD_Answer2_Point);
    }

    if (data.SUD_Answer3_Checked == true) {
        $("#8_SUD_Answer3_Checked").show();
        $("#8_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#8_SUD_Answer4_Checked").show();
        $("#8_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function choice9(data) {
    $("#9_SUD_Answer1_Checked").hide();
    $("#9_SUD_Answer2_Checked").hide();
    $("#9_SUD_Answer3_Checked").hide();
    $("#9_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#9_SUD_Answer1_Checked").show();
        $("#9_SUD_Point").text(data.SUD_Answer1_Point);
    }

    if (data.SUD_Answer2_Checked == true) {
        $("#9_SUD_Answer2_Checked").show();
        $("#9_SUD_Point").text(data.SUD_Answer2_Point);
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#9_SUD_Answer3_Checked").show();
        $("#9_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#9_SUD_Answer4_Checked").show();
        $("#9_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function choice10(data) {
    $("#10_SUD_Answer1_Checked").hide();
    $("#10_SUD_Answer2_Checked").hide();
    $("#10_SUD_Answer3_Checked").hide();
    $("#10_SUD_Answer4_Checked").hide();

    if (data.SUD_Answer1_Checked == true) {
        $("#10_SUD_Answer1_Checked").show();
        $("#10_SUD_Point").text(data.SUD_Answer1_Point);
    }
    if (data.SUD_Answer2_Checked == true) {
        $("#10_SUD_Answer2_Checked").show();
        $("#10_SUD_Point").text(data.SUD_Answer2_Point);
    }
    if (data.SUD_Answer3_Checked == true) {
        $("#10_SUD_Answer3_Checked").show();
        $("#10_SUD_Point").text(data.SUD_Answer3_Point);
    }
    if (data.SUD_Answer4_Checked == true) {
        $("#10_SUD_Answer4_Checked").show();
        $("#10_SUD_Point").text(data.SUD_Answer4_Point);
    }
}

function totalSuit() {
    var c1 = parseInt($("#1_SUD_Point").text());
    var c2 = parseInt($("#2_SUD_Point").text());
    var c3 = parseInt($("#3_SUD_Point").text());
    var c4 = parseInt($("#4_SUD_Point").text());
    var c5 = parseInt($("#5_SUD_Point").text());
    var c6 = parseInt($("#6_SUD_Point").text());
    var c7 = parseInt($("#7_SUD_Point").text());
    var c8 = parseInt($("#8_SUD_Point").text());
    var c9 = parseInt($("#9_SUD_Point").text());
    var c10 = parseInt($("#10_SUD_Point").text());

    $("#Total_SUD_Point").text(c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9 + c10);
}


