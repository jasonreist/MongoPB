var app = angular.module("PaycheckBudget", ["ngRoute"]);

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "main.htm"
    })
    .when("/about", {
        templateUrl: "about.htm"
    })
    .when("/contact", {
        templateUrl: "contact.htm"
    })
    .when("/bills", {
        templateUrl: "bills.htm"
    })
    .when("/calendar", {
        templateUrl: "calendar.htm"
    })
    .when("/income", {
        templateUrl: "income.htm"
    })
    .when("/settings", {
        templateUrl: "settings.htm"
    });
});

app.directive('footer', function () {
    return {
        restrict: 'A', //This menas that it will be used as an attribute and NOT as an element.
        replace: false,
        templateUrl: "footer.htm",
    }
});