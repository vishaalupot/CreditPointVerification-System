
async function jqMsgBoxMain(msg, title = "Message Box", reload = 'false') {

    return new Promise((resolve, reject) => {

        $("#lbdiagMsgMain").text(msg);

        $("#dialog-messageMain").attr("title", title);

        $("#dialog-messageMain").dialog({

            stack: 5,
            zIndex: 9999,
            dialogClass: "no-close",
            modal: true,

            buttons: {
                Ok: function () {
                    resolve(true);

                    $(this).dialog("close");

                    if (reload == 'true') {
                        window.location.reload();
                        
                    }
                    
                    return true;
                }

            }

        });

    });

};


async function jqMsgOkCancel(msg, title = "Message Box", reload = 'false') {

    return new Promise((resolve, reject) => {
        
        $("#lbdiagMsgMain").text(msg);
        $("#dialog-messageMain").attr("title", title);
        $("#dialog-messageMain").dialog({

            stack: 5,
            zIndex: 9999,
            dialogClass: "no-close",
            modal: true,
            buttons: {
                Ok: function () {
                    resolve(true);
                    $(this).dialog("close");

                    if (reload == 'true') {
                        window.location.reload();
                    }
                },

                Cancel: function () {
                    resolve(false);
                    $(this).dialog("close");

                }
            }

        });

    });

};


async function jqMsgInput(msg, elementID, title = "Message Box", reload = 'false') {  

    return new Promise((resolve, reject) => {
        let remarksInput = $("#" + elementID).siblings("input#attendance_Remarks");
        let StatusCode = $("#" + elementID).val();
       //console.log("statuscode: " + StatusCode); 
        
        //$("#lbdiagMsgInput").text(msg);
        $("#lbdiagMsgInput").html(msg);
        $("#dialog-msgInput").attr("title", title);
        $("#dialogInput").removeAttr("hidden");
        $("#dialog-msgInput").dialog({
            stack: 5,
            zIndex: 9999,
            dialogClass: "no-close",
            modal: true,
            closeOnEscape: false,
            buttons: {
                Ok: function () {   
                    var Remarks = $("#dialogInput").val().trim();
                    if (Remarks.length >= 8 || StatusCode == "WO") {
                        if (Remarks.includes(",")) {
                            alert("Do not include comma(,) in the Remarks!" )
                        }
                        else {
                            remarksInput.val($("#dialogInput").val().trim());
                            $(this).dialog("close");
                            resolve(true);
                        }

                    }
                    else {
                        alert("Invalid Remarks! \n It should be at least 8 charaters.");
                    }
             
                    return true;
                },

                Cancel: function () {
                  
                    $("#" + elementID).val($("#tempLeave").val());
                    $(this).dialog("close");
                    
                }

            }

        });

    });

};


async function jqMsgLeaveHeader(msg, elementID, element, title, reload = 'false') {

    
    return new Promise((resolve, reject) => {
        
        //$("#diagmsgHeader").attr("title", title);
        $("#DateSelected").text(title);
        $("#dialog-msgHeader").removeAttr("hidden");
        $("#dialog-msgHeader").dialog({
            stack: 5,
            zIndex: 9999,
            dialogClass: "no-close",
            modal: true,
            closeOnEscape: false,
            buttons: {
                Ok: function () {

                    let existingLeave = $("#ExistingLeave").val().trim();
                    let ReplaceLeave = $("#ReplaceLeave").val().trim();

                    if (existingLeave == '') {
                        existingLeave = '-';
                    }

                    if (ReplaceLeave == '') {
                        ReplaceLeave = '-';
                    }

                    element.each(function () {
                        var elementId = $(this).attr("id");
                        var remarksInput = $("#" + elementId).siblings("input#attendance_Remarks");
                        var value = $(this).val().trim();                        
                        //console.log(value);
                       
                        if (value === existingLeave) {
                            var valueExists = $(this).find('option').filter(function () {                                
                                return $(this).val().trim() === ReplaceLeave;
                            }).length > 0;

                            if (!valueExists) {
                                if ($(this).not('option[text="' + existingLeave +'"]')) {
                                    $(this).append($('<option>', {
                                        value: ReplaceLeave,
                                        text: ReplaceLeave
                                    }));
                                }
                            }
                            $(this).val(ReplaceLeave);
                            if ($("#dialogHeader").val().trim != ""){
                                remarksInput.val($("#dialogHeader").val().trim());
                            }
                            
                            originalBackgroundColor = $(this).css("background-color");
                            //$(this).css("background-color", "lightgrey");

                        }
                    });
                   
                    $(this).dialog("close");
                    return true;
                    
                },

                Cancel: function () {
                    // console.log("esc: " + $("#tempLeave").val());
                    $("#" + elementID).val($("#tempLeave").val());                   
                    $(this).dialog("close");
                    //$('#dialog-msgInput').dialog('close');
                }

            }

        });

    });

};


async function jqMsgInputPay(msg, msg2, title = "Message Box", reload = 'false') {

    return new Promise((resolve, reject) => {
        //let remarksInput = "";//$("#" + elementID).siblings("input#attendance_Remarks");
        //let StatusCode = "";//$("#" + elementID).val();

        
        $("#lbdiagMsgInputPay").html(msg);
        $("#lbMsgInstall").html(msg2);
        $("#dialog-msgInputPay").attr("title", title);        
        $("#dialogInputPay").removeAttr("hidden");        
        $("#tbMsgInstall").removeAttr("hidden");
        $("#dialog-msgInputPay").dialog({
            stack: 5,
            zIndex: 9999,
            dialogClass: "no-close",
            modal: true,
            closeOnEscape: false,
            buttons: {
                Ok: function () {
           
                    $(this).dialog("close");
                    resolve(true);
                    return true;
                },

                Cancel: function () {

                    /*  $("#" + elementID).val($("#tempLeave").val());*/
                    resolve(false);
                    $(this).dialog("close");

                }

            }

        });

    });

};


//$('#dialog-msgInput').on('keydown', function (event) {
   
//})
async function ConfirmCreate() {

    if (await jqMsgOkCancel('Create?')) {

        sessionStorage.setItem("textChange", "No");

        $('form').submit();

    }

}



async function ConfirmEdit() {

    if (await jqMsgOkCancel('Save?')) {

        sessionStorage.setItem("textChange", "No");

        $('form').submit();

    }

}



async function ConfirmDelete(delUrl, idNum, reload = 'false') {
 
    
    if (await jqMsgOkCancel('Confirm Delete?',null,reload)) {

        $.ajax({


            url: delUrl,

            data: { id: idNum },

            type: "Post",

            success: function (result) {

                jqMsgBoxMain(result, null, 'true');


            },

            error: function (xhr, status, error) {

                console.log("An error occurred while deleting the record: " + error);

            }

        })

    }

}



function deleteEmployee(id) {

    if (confirm("Are you sure you want to delete this employee?")) {

        $.ajax({

            url: '@Url.Action("DeleteConfirmed", "Employees", new { id = "__id__" })'.replace('__id__', id),

            type: 'POST',

            dataType: 'json',

            data: { '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },

            success: function (result) {

                if (result.success) {

                    alert('Employee deleted successfully.');

                    window.location.reload();

                } else {

                    alert('An error occurred while deleting the employee.');

                }

            },

            error: function (xhr, status, error) {

                alert('An error occurred while deleting the employee.');

                console.log(error);

            }

        });

    }

}


function jqMsgBoxTimeOut(msg, title = "Message Box", reload = 'false') {

    return new Promise((resolve, reject) => {

        $("#lbdiagMsgMain").text(msg);

        $("#dialog-messageMain").attr("title", title);

        $("#dialog-messageMain").dialog({

            stack: 5,
            zIndex: 9999,
            dialogClass: "no-close",
            modal: true,

            buttons: {
                Ok: function () {
                    
                    resolve(true);
                   
                    $(this).dialog("close");

                    if (reload == 'true') {
                        window.location.reload();

                    }
                    else {
                    
                    }
                    return true;
                }

            }

        });

    });

};


async function ConfirmDeleteLeaves(delUrl, sdate, edate, statuscode) {

    let msg2 = "";

    d1 = new Date(Date.parse(sdate));

    d2 = new Date(Date.parse(edate));

    df1 = new Date();

    var formatter = new Intl.DateTimeFormat('en-US', {

        day: '2-digit',

        month: 'short',

        year: 'numeric',

        weekday: 'short'

    });

    if (sdate == edate) {

        msg2 = formatter.format(d1);
    }

    else {        
        msg2 = formatter.format(d1) + " to " + formatter.format(d2);

    }

    if (await jqMsgOkCancel('Confirm Delete (' + statuscode + ') ' + msg2 + '?')) {

        $.ajax({

            url: delUrl,
            //data: { snr: idNum },
            type: "Post",

            success: function (result) {

                //window.location.href = '@Url.Action("Index","Employees")';

                alert("Leaves Deleted!");

                window.location.reload();

                //jqMsgBoxMain("Leaves deleted");
            },

            error: function (xhr, status, error) {

                jqMsgBoxMain("An error occurred while deleting the record: " + error);

            }

        })

    }


}


async function btnEditUser(url) {
 
    window.location.href = url;
}



//$(document).ready(function () {

//    var currentPage = window.location.href.split('/').pop();

//    console.log('@Url.Action("Index","Employees",new {id=321})');

//    console.log("currentpage: " +currentPage);

//});

async function AttendSheet(url) {    
    await showOverlay();
    window.location.href = url;
}


function showOverlay() {   
    $('#overlay').removeAttr('hidden');    
    $("#overlay").show();
    $("#progress-bar-container").show();
}

function hideOverlay() {
    $('#overlay').hide();
}

function hideProgressBar() {
    $("#overlay").hide();
    $("#progress-bar-container").hide();
}

function BackButton() {  
    history.back();
}



var logoutTimer;
var inactivityPeriod = 20 * 60 * 1000; // 20 minutes
var warningThreshold = 1 * 60 * 1000; // 1 minute

function startLogoutTimer() {
    clearTimeout(logoutTimer);
    logoutTimer = setTimeout(logoutUser, inactivityPeriod);
    var remainingTime = inactivityPeriod - warningThreshold;
   
        setTimeout(showLogoutWarning, remainingTime);  
}

function resetLogoutTimer() {
    clearTimeout(logoutTimer);    
        startLogoutTimer();    
}

function logoutUser() {
    // Perform the logout action
   // window.location.href = "/logout";
    $('#logoutForm').submit();
}

function showLogoutWarning() {
    var remainingSeconds = warningThreshold / 1000;
    
    //alert("Your session will expire in " + remainingSeconds + " seconds. Please save your work and refresh the page.");
    jqMsgBoxMain("Your session will expire in " + remainingSeconds + " seconds. Please save your work and refresh the page.","Session Expiry");
    
}

if (getcurrentPath() != "/Account/Login") {
// Attach event listeners to monitor user activity
window.addEventListener("click", resetLogoutTimer);
//window.addEventListener("mousemove", resetLogoutTimer);
//window.addEventListener("keypress", resetLogoutTimer);
}

function getcurrentPath() {
    var currentPath = window.location.pathname;

    // Remove the route variables from the path
    var pathSegments = currentPath.split('/');
    if (pathSegments[pathSegments.length - 1].includes('?')) {
        pathSegments.pop();
    }

    // Remove the leading slash if present
    if (pathSegments[0] === '') {
        pathSegments.shift();
    }

    // Reconstruct the URL without the server name and route variables
    var currentAddress = '/' + pathSegments.join('/');
    // Output the current address
    //console.log(currentAddress);
    return currentAddress;
}


if (getcurrentPath() != "/Account/Login") {
    startLogoutTimer();
}


async function updateColors() {
    $(".Leavedropdown").each(function () {
        LeaveBackgroundColor($(this));
    });
}

function LeaveBackgroundColor(ddleave) {
    //var value = ddleave.text().trim();
    var value = ddleave.val();
  
    switch (value) {
        case "A":
            ddleave.css("background-color", "RGB(192, 0, 0)");
            break;
        case "AL":
            ddleave.css("background-color", "RGB(155, 194, 230)");
            break;
        case "CL":
            ddleave.css("background-color", "RGB(198, 224, 180)");
            break;
        case "CO":
        case "HCO/P":
        case "HCO/HSL":
        case "HCO/HAL":
        case "HCO/HLOP":
            ddleave.css("background-color", "RGB(208, 206, 206)");
            break;
        case "H":
            ddleave.css("background-color", "RGB(142, 169, 219)");
            break;
        case "HAL":
            ddleave.css("background-color", "RGB(189, 215, 238)");
            break;
        case "HAL/HLOP":
            ddleave.css("background-color", "RGB(221, 235, 247)");
            break;
        case "HLOP":
            ddleave.css("background-color", "RGB(249, 62, 31)");
            break;
        case "HML/HLOP":
            ddleave.css("background-color", "RGB(252, 228, 214)");
            break;
        case "HSL":
            ddleave.css("background-color", "RGB(255, 230, 153)");
            break;
        case "HSL/HAL":
            ddleave.css("background-color", "RGB(255, 192, 0)");
            break;
        case "HSL/HLOP":
            ddleave.css("background-color", "RGB(255, 242, 204)");
            break;
        case "IR":
            ddleave.css("background-color", "RGB(123, 123, 123)");
            break;
        case "LOP":
            ddleave.css("background-color", "RGB(243, 58, 53)");
            break;
        case "ML":
        case "PL":
            ddleave.css("background-color", "RGB(248, 203, 173)");
            break;
        case "NA":
        case "PH":
        case "HPH":
            ddleave.css("background-color", "RGB(255, 255, 255)");
            break;
        case "NCNS":
            ddleave.css("background-color", "RGB(255, 0, 0)");
            break;
        case "P":
            ddleave.css("background-color", "RGB(0, 176, 80)");
            break;
        case "SL":
            ddleave.css("background-color", "RGB(255, 217, 102)");
            break;
        case "VE":
            ddleave.css("background-color", "RGB(0, 176, 240)");
            break;
        case "VR":
            ddleave.css("background-color", "RGB(201, 201, 201)");
            break;
        case "WFH":
            ddleave.css("background-color", "RGB(198, 224, 180)");
            break;
        case "WO":
            ddleave.css("background-color", "RGB(191, 191, 191)");
            break;
        case "-":
        case "":
            ddleave.css("background-color", "RGB(255, 255, 255)");
        default:
            //console.log(value);
            ddleave.css("background-color", "RGB(255, 255, 255)");              
            break;
    }

}


function ConvertStrtoDt(dateString) {
   
    var dateObject = new Date(dateString);
   
    if (!isNaN(dateObject.getTime())) {
        //console.log("Valid Date:", dateObject);
        return dateObject;
    } else {
        console.log("Invalid Date.  Check with Support Application Team.")
        return (new Date("1/1/0001"));
    }
}

function ConverDtddMMM(datestr) {
    var options = { day: '2-digit', month: 'short' };
    var formattedDate = datestr.toLocaleDateString('en-US', options);
    return formattedDate;
}

function ConverDtMMMdd(datestr) {
    var options = { day: '2-digit', month: 'short' };
    var formattedDate = datestr.toLocaleDateString('en-UK', options);
    return formattedDate;
}

function ConvertDtYYYYMMDD(dateObj) {
    var currentDate = dateObj;
    var year = currentDate.getFullYear();
    var month = String(currentDate.getMonth() + 1).padStart(2, '0'); // Months are 0-based.
    var day = String(currentDate.getDate()).padStart(2, '0');

    return year + '-' + month + '-' + day;
}


function convertDateyyyMMdd(dateString) {
    var parts = dateString.split('-');

    // Construct a new Date object using the parsed parts
    var date = new Date(parts[2], monthAbbreviationToNumber(parts[1]) - 1, parts[0]);

    // Format the date as "yyyy-MM-dd"
    var formattedDate = date.getFullYear() + '-' + padZero(date.getMonth() + 1) + '-' + padZero(date.getDate());

    return formattedDate;
}

function monthAbbreviationToNumber(monthAbbreviation) {
    var monthMap = {
        'Jan': 1, 'Feb': 2, 'Mar': 3, 'Apr': 4, 'May': 5, 'Jun': 6,
        'Jul': 7, 'Aug': 8, 'Sep': 9, 'Oct': 10, 'Nov': 11, 'Dec': 12
    };
    return monthMap[monthAbbreviation];
}

function numberToMonthAbbreviation(monthNumber) {
    var monthMap = {
        1: 'Jan', 2: 'Feb', 3: 'Mar', 4: 'Apr', 5: 'May', 6: 'Jun',
        7: 'Jul', 8: 'Aug', 9: 'Sep', 10: 'Oct', 11: 'Nov', 12: 'Dec'
    };
    return monthMap[monthNumber];
}

function padZero(number) {
    return (number < 10 ? '0' : '') + number;
}

function AttendancebeforeDOJ() {

    const yearSelected = $("#dropdownMonth").val().split('-')[1];
    const monthSelected = $("#dropdownMonth").val().split('-')[0];
    const startdt = ConvertStrtoDt("01 " + monthSelected + " " + yearSelected);

    $(".dojclass").each(function () {
        let day = 1;

        let strdoj = $(this).val().split(' ')[0];
        let dtdoj = ConvertStrtoDt(strdoj);
        let empcode = $(this).prev('input[type="hidden"]').val();

        let row = $(this).closest('tr');

        if (dtdoj > startdt) {

            $(".DateHeader").each(function () {
                let keyDate = ConvertStrtoDt(day + " " + monthSelected + " " + yearSelected);
                if (dtdoj > keyDate) {
                    //let keyDatestr = ConverDtddMMM(strdt);
                    let columnIndex = $('.DateHeader').index($(this)) + 3;
                    let cell = row.find('td').eq(columnIndex);
                    let selectLeave = cell.find('.Leavedropdown');

                    //console.log(selectLeave);

                    selectLeave.append($('<option>', {
                        value: "NA",
                        text: "NA"
                    }));
                    selectLeave.val('NA');
                    selectLeave.prop('disabled', true);

                }
                day = day + 1;

            })

        }

    })
}