// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.addEventListener("load", function () {
    resize();
});

window.addEventListener("resize", function(e) {
    resize();
});

function resize() {
    let w = window.innerWidth;
    let h = window.innerHeight;
    $(".tData").css("width", (w - 25)/7);
    $(".tData").css("height", (h - 225)/6);
    
}