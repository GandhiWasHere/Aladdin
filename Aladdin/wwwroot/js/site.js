// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.search-submit__icon').click(function () {
    var search = $('.search-bar').val();
    window.location = '/Products/index?searchString=' + search;
});