$(document).ready(function () {

    console.log("Pluralsight Dutch Treat");

    var buyButton = $("#buy-button");
    buyButton.on("click", function () {
        console.log("Buy button pressed");
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("Click on " + $(this).text());
    });

    var $loginToggle = $("#login-toggle");
    var $popupForm = $(".popup-form");

    $popupForm.hide();

    $loginToggle.on("click", function () {
        $popupForm.fadeToggle(100);
    });

});