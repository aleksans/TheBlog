﻿$(function() {
    $(".dropdown-toggle").dropdown();

    $(".dropdown input, .dropdown label").click(function(e) {
        e.stopPropagation();
    });
})