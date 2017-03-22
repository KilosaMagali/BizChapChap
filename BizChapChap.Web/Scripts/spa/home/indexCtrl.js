(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl)
        .directive("owlCarousel", function () {
            return {
                restrict: 'E',
                transclude: false,
                link: function (scope) {
                    scope.initCarousel = function (element) {
                        // provide any default options you want
                        var defaultOptions = {
                        };
                        var customOptions = scope.$eval($(element).attr('data-options'));
                        // combine the two options objects
                        for (var key in customOptions) {
                            defaultOptions[key] = customOptions[key];
                        }
                        // init carousel
                        $(element).owlCarousel(defaultOptions);
                    };
                }
            };
        })
        .directive('owlCarouselItem', [function () {
            return {
                restrict: 'A',
                transclude: false,
                link: function (scope, element) {
                    // wait for the last item in the ng-repeat then call init
                    if (scope.$last) {
                        scope.initCarousel(element.parent());
                    }
                }
            };
        }]);

    indexCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'searchService','$filter'];

    function indexCtrl($scope, $rootScope, apiService, searchService, $filter)
    {
        //console.log('Passed by indexCtrl : current user :' + $rootScope.loggedUser.username);
        $scope.User = 'Kilosa';

        $scope.searchItem = {};

        $scope.categories = [];

        $scope.regions = [];

        $scope.featuredListings = [];

        $scope.formatDateListing = formatDateListing;

        $scope.search = search;

        var criterias = {
             page: 0,
             pageSize: 6,
            //subCategoryId: ,
            //regionId: ,
            //searchPhrase: 
        };


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

        function search() {
            criterias.subCategoryId = $scope.searchItem.subCategoryId;
            criterias.regionId = $scope.searchItem.regionId;
            criterias.searchPhrase = $scope.searchItem.searchPhrase;
            searchService.searchListings(criterias, searchCompleted, searchFailed);
        }

        function searchCompleted(response) {
            console.log(response.data);
        }

        function searchFailed(response) {
            console.log(response.data);
        }

        function formatDateListing(date)
        {
            if (nbDaysFromDate(date) <= 0)
                return "Today ";// + $filter('date')(date, 'h:mm a');
            else if (nbDaysFromDate(date) == 1)
                return "1 day ago";
            else if (nbDaysFromDate(date) < 4)
                return nbDaysFromDate(date) + " days ago";
            else
                return $filter('date')(date, "dd/MM");
        }

        
        function nbDaysFromDate(date) {
            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
            var today = new Date();

           return Math.round(Math.abs((today - new Date(date)) / (oneDay)));
        }

        loadCategories();
        loadRegions();
        loadFeaturedListings();
    }

})(angular.module('bizChapChap'));