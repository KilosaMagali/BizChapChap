(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$rootScope'];

    function indexCtrl($scope, $rootScope)
    {
        console.log('Passed by indexCtrl : current user :' + $rootScope.loggedUser.username);
        $scope.User = 'Kilosa';
    }

})(angular.module('bizChapChap'));