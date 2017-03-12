(function (app) {
    'use strict';

    app.controller('listingAddCtrl', listingAddCtrl);

    listingAddCtrl.$inject = ['$scope', '$rootScope', 'apiService'];

    function listingAddCtrl($scope, $rootScope, apiService) {
        $scope.listing = { CategoryRefId: 0, SubCategoryRefId: 0, Date: new Date() };
        $scope.addListing = addListing;
        $scope.loadSubCategories = loadSubCategories;
        $scope.categoryChanged = categoryChanged;
        $scope.showImagePreview = showImagePreview;
        $scope.FilesToUpload = [];
        $scope.categories = [];
        $scope.subcategories = [];
        $scope.isCategorySelected = false;

        //Upload thumbnails
        $scope.ImagePreviewSrc1 = "/Content/img/CameraIcon.png"
        $scope.ImagePreviewSrc2 = "/Content/img/CameraIcon.png"
        $scope.ImagePreviewSrc3 = "/Content/img/CameraIcon.png"
        $scope.ImagePreviewSrc4 = "/Content/img/CameraIcon.png"
        $scope.ImagePreviewSrc5 = "/Content/img/CameraIcon.png"
        $scope.ImagePreviewSrc6 = "/Content/img/CameraIcon.png"


        var listingImages = [];



        function addListing() {
            apiService.post('/api/listings/add', $scope.listing,
            addListingSucceded,
            addListingFailed);
        }

        function showImagePreview(input) {
            console.log("Previewing " + input.length);

            if (input && input.length > 0) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $scope.ImagePreviewSrc1 = '';
                    $scope.ImagePreviewSrc1 = e.target.result;
                }

                reader.readAsDataURL(input[0]);
            }
        }


        function addListingSucceded(response) {
            console.log($scope.listing.Title + ' has been submitted');
            $scope.listing = response.data;

            //Upload images for the listing
            if (listingImages.length > 0) {
                //fileUploadService.uploadImage(Image, $scope.listing.Id, redirectToEdit);
            }
        }

        function addListingFailed(response) {
            console.log(response);
            console.log(response.statusText);
        }

        function categoryChanged() {
            if ($scope.listing.CategoryRefId > 0) {
                loadSubCategories();
                $scope.isCategorySelected = true;
            } else {
                $scope.subcategories = [];
            }
        }

        function loadCategories() {
            apiService.get('/api/categories/', null,
            categoriesLoadCompleted,
            categoriesLoadFailed);
        }

        function loadSubCategories() {
            apiService.get('/api/categories/subcategory/' + $scope.listing.CategoryRefId, null,
            subCategoriesLoadCompleted,
            subCategoriesLoadFailed);
        }

        function categoriesLoadCompleted(response) {
            $scope.categories = response.data;
        }

        function categoriesLoadFailed(response) {
            console.log(response.data);
        }

        function subCategoriesLoadCompleted(response) {
            $scope.subcategories = response.data;
        }

        function subCategoriesLoadFailed(response) {
            console.log(response.data);
        }

        loadCategories();
    }



})(angular.module('bizChapChap'));;