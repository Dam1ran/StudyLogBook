$(document).ready(function () {    
    
    send();
    
});


document.querySelector("#searchMsgInput").addEventListener("keyup", event => {
    if (event.key !== "Enter" && event.key !== "Backspace" && event.key !== "Delete") return;
    searchMessageClick();
    event.preventDefault();
});


function searchMessageClick() {

    let newPageNumber = $('input[name="pageNumber"]').val();

    if (newPageNumber == -1
        //|| $.trim($("#searchMsgInput").val()).trim().length < 2
        )
    {        
        if (newPageNumber!=-2)
        {
            return false;
        }
    }

    send('search');

}



function clickAll()
{

    if ($('.toDeleteCheckbox_biger:first').prop('checked')) {

        $('.toDeleteCheckbox').prop('checked', true);
    }
    else
    {
        $('.toDeleteCheckbox').prop('checked', false);
    }

}



function onDelete() {

    var msgsToDelete = [];

    let deleteCheckboxes = document.getElementsByClassName("toDeleteCheckbox");

    for (let i = 1; i < deleteCheckboxes.length; i++) {

        if (deleteCheckboxes[i].checked) {            

            msgsToDelete.push(deleteCheckboxes[i].value);            

        }
        
    }

    if (msgsToDelete.length > 0) {

        $('#partialMessages').html('<img src="/Site/images/arrowload.gif" alt="Loading" width="75" height="100" />');

        let token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: "/user/DeleteMessages",
            type: 'Delete',
            data: {
                __RequestVerificationToken: token,
                MessagesToDelete: msgsToDelete
            },
            success: onResponse
        }).fail(function () { console.log("Could Not Delete Messages"); });       

    }

    
}


function prevPage() {
    send('prev');
}

function nextPage() {
    send('next');
}

function lastPage() {
    send('last');
}

function firstPage() {
    send('first');
}

function send(aPageNav) {

    let pageNumber = parseInt(($('input[name="pageNumber"]').val()));

    $('#partialMessages').html('<img src="/Site/images/arrowload.gif" alt="Loading" width="75" height="100" />');

    let searchMsgInput = $.trim($("#searchMsgInput").val()).length < 2 ? "" : $.trim($("#searchMsgInput").val());
    if (searchMsgInput == "")
    {
        ($("#searchMsgInput").val(""));
    }

    if (aPageNav == "search")
    {
        pageNumber = 1;
    }



    let token = $('input[name="__RequestVerificationToken"]').val();

    $.ajax({
        url: "/user/ViewInboxList",
        type: 'POST',
        data: {
            __RequestVerificationToken: token,
            PageNumber: pageNumber,
            PageNav: aPageNav,
            SearchString: searchMsgInput
        },
        success: onResponse
    }).fail(function () { console.log("Cannot Load Messages"); });
}


function onResponse(data) {

    $('#partialMessages').html(data).hide();
    $('#partialMessages').html(data).slideDown(500);

    $('#messagesNumber').html($('input[name="numberOfMessages"]').val());

    let searchMsgInput = $.trim($("#searchMsgInput").val()).length < 2 ? "" : $.trim($("#searchMsgInput").val());

    if (searchMsgInput.length > 1) {


        Array.from(document.getElementsByClassName("td_string")).forEach(
            function (element, index, array) {

                let textToHighlight = element.innerHTML.trim();

                let filter = new RegExp(searchMsgInput, "ig");

                element.innerHTML = textToHighlight.replace(filter, '<span style="background-color:wheat; box-shadow: 0 0 3px black; border-radius:4px;">$&</span>');

            }
        );

    }

    let newPageNumber = $('input[name="pageNumber"]').val();

    if (newPageNumber <= -1) {
        disableFirstPrev();
        disableNextLast();
        $('.deleteBtn').prop('disabled', true);
        $('.toDeleteCheckbox_biger').prop('disabled', true);

        return;

    }

    document.getElementsByClassName('homePageLbl')[0].innerHTML = newPageNumber;

    if ($('input[name="isFirstPage"]').val() == "True") {
        disableFirstPrev();
    }
    else {
        enableFirstPrev();
    }


    if ($('input[name="isLastPage"]').val() == "True") {
        disableNextLast();
    }
    else {
        enableNextLast();
    }

}




function disableFirstPrev()
{
    document.getElementsByClassName('firstPageBtn')[0].disabled = true;
    document.getElementsByClassName('firstPageBtn')[0].style.cursor = "default";
    document.getElementsByClassName('prevPageBtn')[0].disabled = true;
    document.getElementsByClassName('prevPageBtn')[0].style.cursor = "default";
}

function disableNextLast() {
    document.getElementsByClassName('nextPageBtn')[0].disabled = true;
    document.getElementsByClassName('nextPageBtn')[0].style.cursor = "default";
    document.getElementsByClassName('lastPageBtn')[0].disabled = true;
    document.getElementsByClassName('lastPageBtn')[0].style.cursor = "default";
}

function enableFirstPrev() {
    document.getElementsByClassName('firstPageBtn')[0].disabled = false;
    document.getElementsByClassName('firstPageBtn')[0].style.cursor = "pointer";
    document.getElementsByClassName('prevPageBtn')[0].disabled = false;
    document.getElementsByClassName('prevPageBtn')[0].style.cursor = "pointer";
}

function enableNextLast() {
    document.getElementsByClassName('nextPageBtn')[0].disabled = false;
    document.getElementsByClassName('nextPageBtn')[0].style.cursor = "pointer";
    document.getElementsByClassName('lastPageBtn')[0].disabled = false;
    document.getElementsByClassName('lastPageBtn')[0].style.cursor = "pointer";
}