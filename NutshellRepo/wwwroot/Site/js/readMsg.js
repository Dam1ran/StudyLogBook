$(document).ready(function () {    
    
   
    
});



function popUpConfirmation() {
    var popup = document.getElementById("confirmDeleteDiv");
    popup.style.display = "flex";
}

function ClosePopUpConfirmation() {
    var popup = document.getElementById("confirmDeleteDiv");
    popup.style.display = "none";

}


function deleteMessage()
{
    let token = $('input[name="__RequestVerificationToken"]').val();

    let MsgId = $('input[name="MsgId"]').val();

    $.ajax({
        type: 'Delete',
        url: "/user/DeleteReadMessage",
        data: {
            __RequestVerificationToken: token,
            MessageIdToDelete: MsgId
        },
        success: () =>
        {
            document.getElementById("back_to_inbox").click();
        }
    }).fail(function () {
        alert("Error");
        console.log("Could Not Delete The Message");
    });  
}


function markAsUnreadMessage() {

    let token = $('input[name="__RequestVerificationToken"]').val();

    let MsgId = $('input[name="MsgId"]').val();

    $.ajax({
        type: "Put",
        url: "/user/MarkAsUnread",
        data: {
            __RequestVerificationToken: token,
            MessageIdToMarkAsUnread: MsgId
        },
        success: function() {
            document.getElementById("back_to_inbox").click();
        }
    }).fail(function () {
        alert("Error");
        console.log("Could Not Mark The Message");
    });
}