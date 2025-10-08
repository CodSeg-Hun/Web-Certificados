$(document).ready(function () {
//    debugger;
    // Expand Panel
    $("#open").click(function () {
        $("div#panel").slideDown("slow");
//        alert('open');
    });

    // Collapse Panel
    $("#close").click(function () {
        $("div#panel").slideUp("slow");
//        alert('close');
    });

    $("#close2").click(function () {
        //$("div#panel").slideUp("slow");
//        $("div#panel").slideDown("slow");
        $("div#panel").slideUp("slow");

        var frm_element = document.getElementById('close');
        frm_element.style.display = 'none';

        var frm_element2 = document.getElementById('open');
        frm_element2.style.display = '';

        
        //$("div#panel").slideDown("slow");
        //alert('close2t');
    });

    // Switch buttons from "Log In | Register" to "Close Panel" on click
    $("#toggle a").click(function () {
        $("#toggle a").toggle();
//        alert('toggle a');
    });

});