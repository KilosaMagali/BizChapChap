(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$rootScope','apiService'];

    function indexCtrl($scope, $rootScope, apiService)
    {
        //console.log('Passed by indexCtrl : current user :' + $rootScope.loggedUser.username);
        $scope.User = 'Kilosa';

        $scope.searchItem = {};

        $scope.categories = [];

        $scope.regions = [];

        $scope.featuredListings = [];


        function loadCategories() {
            apiService.get('/api/categories/', null,
                categoriesLoadCompleted,
                categoriesLoadFailed);
        }

        function categoriesLoadCompleted(response) {
            $scope.categories = response.data;
        }

        function categoriesLoadFailed(response) {
            console.log(response.data);
        }

        function loadRegions() {
            apiService.get('/api/regions/', null,
                regionsLoadCompleted,
                regionsLoadFailed);
        }

        function regionsLoadCompleted(response) {
            $scope.regions = response.data;
        }

        function regionsLoadFailed(response) {
            console.log(response.data);
        }

        function loadFeaturedListings() {
            apiService.get('/api/listings/featured', null,
                listingsLoadCompleted,
                listingsLoadFailed);
        }

        function listingsLoadCompleted(response) {
            $scope.featuredListings = response.data;
        }

        function listingsLoadFailed(response) {
            console.log(response.data);
        }


        loadCategories();
        loadRegions();
        loadFeaturedListings();
    }

})(angular.module('bizChapChap'));