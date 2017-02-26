(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope','apiService','$rootScope'];

    function loginCtrl($scope, apiService, $rootScope) {
        console.log('Passed by loginCtrl');
        $scope.login = login;
        $scope.user = {};
        $scope.user.grant_type = 'password';

        function login() {
            console.log('User data ' + $scope.user.username + ' ' + $scope.user.password + '  ' + $scope.user.grant_type);
            apiService.postUrlEncoded('/token', $scope.user, loginSuccess, loginFailure);
        }

        function loginSuccess(response) {
            console.log("login success " + response.data.access_token + ' res: ' + response.data.userName);
            $rootScope.loggedUser = {
                username: response.data.userName,
                accessToken: response.data.access_token,
                tokenValid: true
            };
              
                 
        }

        function loginFailure(response) {
            console.log("login failure " + response.status);
        }
    }

})(angular.module('bizChapChap'));