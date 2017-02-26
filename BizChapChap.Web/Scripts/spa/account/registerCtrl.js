(function (app) {
    'use strict';

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope','apiService'];

    function registerCtrl($scope, apiService) {
        console.log('Passed by registerCtrl');
        $scope.register = register;
        $scope.user = {};

        function register() {
            console.log('User data ' + $scope.user.email + ' ' + $scope.user.password + ' ' + $scope.user.confirmPassword);
            apiService.post('/api/account/register', $scope.user, registerSuccess, registerFailure);
        }

        function registerSuccess(result) {
            console.log("register success " + result);
        }

        function registerFailure(response) {
            console.log("register failure " + response);
        }
    }

})(angular.module('bizChapChap'));