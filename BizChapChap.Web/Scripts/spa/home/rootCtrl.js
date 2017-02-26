(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$rootScope'];

    function rootCtrl($scope, $rootScope) {
        console.log('Inside rootCtrl');
        function isUserValid() {
            console.log('User Validity : ' + $rootScope.loggedUser.tokenValid);
            return $rootScope.loggedUser.tokenValid;
        }
    }

})(angular.module('bizChapChap'));