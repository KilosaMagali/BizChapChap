var app = angular.module("bizChapChap", ['ngRoute', 'ngFileUpload', 'angularCSS']);

app.config(
    function ($routeProvider, $locationProvider) {
        //url prefix
        $locationProvider.hashPrefix('');
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                controller: "indexCtrl",
                css: "scripts/spa/home/home.css"
            })
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl",
                css: "scripts/spa/account/account.css"
            })
            .when("/fileupload", {
                templateUrl: "scripts/spa/upload/fileUpload.html",
                controller: "fileUploadCtrl",
                css: "scripts/spa/upload/fileUpload.css"
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl",
                css: "scripts/spa/account/account.css"
            })
            .when("/listingadd", {
                templateUrl: "scripts/spa/listings/add.html",
                controller: "listingAddCtrl",
                css: "scripts/spa/listings/listingadd.css"
            })
            .otherwise({ redirectTo: "/" });

        
    }
);

