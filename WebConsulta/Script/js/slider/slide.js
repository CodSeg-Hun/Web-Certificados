$(document).ready(function () {
    $("#open").click(function () {
        $("div#panel").slideDown("slow");
    });

    // Collapse Panel
    $("#close").click(function () {
        $("div#panel").slideUp("slow");
    });

    $("#close2").click(function () {
        $("div#panel").slideUp("slow");

        var frm_element = document.getElementById('close');
        frm_element.style.display = 'none';

        var frm_element2 = document.getElementById('open');
        frm_element2.style.display = '';
    });

    // Switch buttons from "Log In | Register" to "Close Panel" on click
    $("#toggle a").click(function () {
        $("#toggle a").toggle();
    });

});