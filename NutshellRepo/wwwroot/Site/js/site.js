$(document).ready(function () {

    document.getElementById("loginA").href = "javascript:void(0)";

    if (document.getElementById("userNameString") != null) {

        $.ajax({

            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: document.getElementById("userNameString").innerHTML,
            url: "/user/MessagesCount",
            success: function (data)
            {
                let result = jQuery.parseJSON(data.result);                              

                if (result > 0) {
                    let envelope = document.getElementById("envelope");

                    envelope.title = `${result} New Message${(result > 1) ? "s" : ""}`;
                    envelope.style.color = "wheat";
                }
                else
                {
                    envelope.title = "No New Messages";
                    envelope.style.color = "rgb(160,160,170)";
                }
                
            }        

        });
        
    }
   


    //alert(document.getElementById("userNameString").innerHTML);
});






//$(document).ready(function () {          


//    document.getElementById("loginA").setAttribute('href','javascript:void(0)');

//    //if (document.getElementById("userNameString").val() != null)
//    //{
//    //}

//    alert(document.getElementById("userNameString").val());

//    alert("sukanahui");
//    //$.ajax({
//    //    url: @Url.Action("MessagesCount","account");

//    //});
    
//});




function showLogin() {

    document.getElementById("modalTintDiv").style.display = "block";
    $('div.form-group > input:first').focus();

}

function closeLoginModal(){
    
    document.getElementById("modalTintDiv").style.display = "none";

}

function validateLogIn() {

    var emailString = document.forms["loginDivModal"]["Email"].value;

    var emailRegEx = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    var emailState = emailRegEx.test(emailString);

    var result = true;

    if (document.forms["loginDivModal"]["Password"].value.length < 6)
    {
        document.getElementById("pwdFeedback").style.display = "block";

        result = false;
    }
    else
    {
        document.getElementById("pwdFeedback").style.display = "none";
    }

    if (emailState == false)
    {            
        document.getElementById("emailFeedback").style.display = "block";
        
        result = false;
    }
    else {

        document.getElementById("emailFeedback").style.display = "none";
    }

    return result;

}





window.onclick = function (event) {

    if (event.target != document.getElementById('burgerMenu')
        //&& event.target != document.getElementsByClassName('barMenuContainer')[0]
        && event.target != document.getElementsByClassName('bar')[0]
        && event.target != document.getElementsByClassName('bar')[1]
        && event.target != document.getElementsByClassName('bar')[2]
        && event.target != document.getElementsByClassName('sidenav')[0])
    {
        closeNav();
    }

    if (event.target == document.getElementById("modalTintDiv")) {
        document.getElementById("modalTintDiv").style.display = "none";
    }

}


function openNav() {        

    if (document.getElementById("mySidenav").style.width == 0 || document.getElementById("mySidenav").style.width == "0px")
    {

        document.getElementById("mySidenav").style.width = "150px";
    }
    else
    {
        closeNav();
    }
   
}
    
function closeNav()
{
    document.getElementById("mySidenav").style.width = "0";        
} 

function empty() {

    if ($.trim($("#searchField").val()) === "" || $.trim($("#searchField").val()).length < 2) {
        $("#searchField").attr("placeholder", "Enter Subject");
         setTimeout(function () { $("#searchField").attr("placeholder", "Find a Nutshell"); }, 2000);
        $("#searchField").val("");
        return false;
    };
}




/*

$(window).scroll(function(){

    if ($(window).scrollTop() == $(document).height() - $(window).height()){

        
            //$(window).scrollTop() == $(document).height() - $(window).height()-100


        //getresult('getresult.php?page='+pagenum);

        
        //document.getElementById("temp").innerHTML = curentPageNumber;
        //document.body.innerHTML += '<div class="grid-container"> <div class="grid-item" id="likes">Likes</div><div class="grid-item" id="author">Author</div><div class="grid-item" id="subject">Subjects</div><div class="grid-item" id="compartment">Compartment</div></div>';

        //var c = document.getElementById("nutshell_1").children[1].innerHTML=curentPageNumber;

        // alert(c[0].value);

/*
        alert("scroll top: "+$(window).scrollTop());
        alert("document height: "+$(document).height());
        alert("window height: "+$(window).height());

*/



        // loadMore();
       

/*
        
        
    }
});


var curentPageNumber = 1;

function loadMore(){    

    //request to load first 10 entries of nutshells



    for(var i = 0;i<10;i++) {
    curentPageNumber++;

    if(curentPageNumber%2==0) 
    {
        document.getElementById('nutDivs').innerHTML+=`<div class="grid-container-nutshell" id="nutshell_1" style="background-color:  rgb(100, 100, 110);"><div class="grid-item-nutshell-likes" id="">${curentPageNumber}</div><div class="grid-item-nutshell-author" id="">Damiran</div><div class="grid-item-nutshell-subject" id="">WEB shitt</div><div class="grid-item-nutshell-compartment" id="">HTML + CSS</div></div>`;
        
    }
    else
    {
        document.getElementById('nutDivs').innerHTML+=`<div class="grid-container-nutshell" id="nutshell_1"><div class="grid-item-nutshell-likes" id="">${curentPageNumber}</div><div class="grid-item-nutshell-author" id="">Damiran</div><div class="grid-item-nutshell-subject" id="">WEB shitt</div><div class="grid-item-nutshell-compartment" id="">HTML + CSS</div></div>`;
    }
}
    

}


function getresult(url) {
    $.ajax({
    url: url,
    type: "GET",
    data:  {rowcount:$("#rowcount").val()},
    beforeSend: function(){
    $('#loader-icon').show();
    },
    complete: function(){
    $('#loader-icon').hide();
    },
    success: function(data){
    $("#faq-result").append(data);
    },
    error: function(){} 	        
    });
}

*/







//(function () {'use strict';
//    window.addEventListener('load', function () {



//        if (!$group.data('validate')) {
//            state = $(this).val() ? true : false;
//        } else if ($group.data('validate') == "email") {
//            state = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test($(this).val())
//        }

//        if (state) {
//            form.classList.add('was-validated');
//        }

//        //// Fetch all the forms we want to apply custom Bootstrap validation styles to
//        //var forms = document.getElementsByClassName('needValidate');
//        //// Loop over them and prevent submission
//        //var validation = Array.prototype.filter.call(forms, function (form) {
//        //    form.addEventListener('submit', function (event) {
//        //        if (form.checkValidity() === false) {
//        //            event.preventDefault();
//        //            event.stopPropagation();
//        //        }
//        //        form.classList.add('was-validated');
//        //    }, false);
//        //});
//    }, false);
//})();