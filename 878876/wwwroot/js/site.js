
// Script to animate banner.
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
    setTimeout(carousel, 3000);
};

// Script to display error screen when unauthorized.
$(function () {
    $(document).ajaxError(function (xhr, props) {
        if (props.status == 403) {
            window.location = "https://localhost:44392/Account/AccessDenied";
        } else if (props.status == 401) {
            window.location = "https://localhost:44392/Account/Login";
        }
    });
});

// Script to toggle post content.
$('.toggleButton').click(function () {
    $(this).closest('tr').next('tr').find('.postContent').toggle();
});