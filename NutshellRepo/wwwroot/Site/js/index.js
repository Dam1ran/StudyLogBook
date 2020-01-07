function closeWelcomeDiv() {
    document.getElementById("welcomeDiv").style.display = "none";

}



jQuery(document).ready(function () {

    //document.getElementById("welcomeDiv").style.display = "flex";

    //var headerContainer = document.getElementById("headerContainer");
    //headerContainer.insertAdjacentHTML("afterend", "<div style='background-color:red;'>KIKAT<div/>");    
    //headerContainer.insertAdjacentHTML("afterend","<div class='container'><img src='/Site/images/Index/bigLogo.svg' alt='nutshell_logo' height='250' width='250'/></div>");
       
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