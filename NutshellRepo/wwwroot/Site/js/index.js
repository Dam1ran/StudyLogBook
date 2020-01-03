function closeWelcomeDiv() {
    document.getElementById("welcomeDiv").style.display = "none";

}



jQuery(document).ready(function () {

    highlight();


});




function highlight()
{    
    setTimeout(function() {
        document.getElementById("learn").style.color = "whitesmoke";
    }, 1000);
    setTimeout(function() {        
        document.getElementById("concise").style.color = "whitesmoke";
    }, 1500);
    setTimeout(function () {        
        document.getElementById("revise").style.color = "whitesmoke";
    }, 2000);
    setTimeout(function () {            
        document.getElementById("siteNameString").style.color = "wheat";
    }, 2500);


}