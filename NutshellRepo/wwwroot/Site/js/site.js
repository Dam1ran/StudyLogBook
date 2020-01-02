jQuery(document).ready(function(){          

   
    
    var headText = "\"Learn, Concise, Revise!\"";
    var headTextLenght = headText.length;
    var headTextIndex  = 0;
    var headTextAppend = "";  
    delay = 50;
   
    for (let i = 0; i < headTextLenght; i++) {
       setTimeout( textAppender, delay * i);
    }
       
    function textAppender(){

        headTextAppend+=headText[headTextIndex];
        headTextIndex++;
        $(".bigText").text(headTextAppend);
        
    }

    //setTimeout(closeWelcome,10000);


   
    
    //document.body.innerHTML += '<div class="grid-container"> <div class="grid-item" id="likes">Likes</div><div class="grid-item" id="author">Author</div><div class="grid-item" id="subject">Subjects</div><div class="grid-item" id="compartment">Compartment</div></div>';

    // document.getElementById("Nauthor").innerHTML = "My new text!";


    //loadMore();



    
});





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
    
function closeNav() {
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