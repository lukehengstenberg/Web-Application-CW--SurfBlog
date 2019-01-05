// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var slideIndex = 0;
carousel();

function carousel() {
    var i;
    var x = document.getElementsByClassName("banner");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > x.length) { slideIndex = 1 }
    x[slideIndex - 1].style.display = "block";
    setTimeout(carousel, 4000);
}

$(function () {
    $(document).ajaxError(function (xhr, props) {
        if (props.status == 403) {
            window.location = "https://localhost:44392/Account/AccessDenied";
        } else if (props.status == 401) {
            window.location = "https://localhost:44392/Account/Login";
        }
    });
});