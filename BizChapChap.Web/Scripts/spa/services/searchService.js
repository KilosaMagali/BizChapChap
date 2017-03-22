(function (app) {
    'use strict';
    app.factory('searchService', searchService);
    searchService.$inject = ['$rootScope', 'apiService'];
    function searchService($rootScope, apiService) {
        

        var service = {
            searchListings: searchListings
        }

        function searchListings(criteria, searchSuccess, searchFailure) {
            var config = {
                params: {
                    page: criteria.page,
                    pageSize: criteria.pageSize,
                    subCategoryId: criteria.subCategoryId,
                    regionId: criteria.regionId,
                    searchPhrase: criteria.searchPhrase

                }
            };

            apiService.get("/api/listings", config,
                searchSuccess,
                searchFailure);
        }
        
        return service;
    }

})(angular.module('bizChapChap'));