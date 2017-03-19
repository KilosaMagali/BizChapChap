(function (app) {
    'use strict';

    app.controller('listingAddCtrl', listingAddCtrl);

    listingAddCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'fileUploadService'];

    function listingAddCtrl($scope, $rootScope, apiService, fileUploadService) {
        $scope.listing = { CategoryRefId: 0, SubCategoryRefId: 0, Date: new Date(), CurrencyRefId:1 };
        $scope.addListing = addListing;
        $scope.loadSubCategories = loadSubCategories;
        $scope.categoryChanged = categoryChanged;
        $scope.isValidSource = isValidSource;
        $scope.showImagePreview = showImagePreview;
        $scope.FilesToUpload = [];
        $scope.categories = [];
        $scope.subcategories = [];
        $scope.isCategorySelected = false;

        //Upload thumbnails
        $scope.ImagePreviewSrc1 = "/Content/img/CameraIcon.png";
        $scope.ImagePreviewSrc2 = "/Content/img/CameraIcon.png";
        $scope.ImagePreviewSrc3 = "/Content/img/CameraIcon.png";
        $scope.ImagePreviewSrc4 = "/Content/img/CameraIcon.png";
        $scope.ImagePreviewSrc5 = "/Content/img/CameraIcon.png";
        $scope.ImagePreviewSrc6 = "/Content/img/CameraIcon.png";


        var listingImages = [];



        function addListing() {
            /*var j = 0;
            for (var i = 0; i < $scope.FilesToUpload.length; i++)
                if (isValidSource($scope.FilesToUpload[i])) {
                    listingImages[j] = $scope.FilesToUpload[i];
                    j++;
                }*/
                    

            console.log("Length to upload " + listingImages.length);
            
            apiService.post('/api/listings/add', $scope.listing,
            addListingSucceded,
            addListingFailed);
        }

        //Show a browsed photo
        function showImagePreview(input, position) {
            console.log("Previewing " + input + " " + position);

            if (input) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $scope.FilesToUpload[position] = '';
                    $scope.FilesToUpload[position] = e.target.result;
                }

                listingImages[position] = input;
                reader.readAsDataURL(input);
            }
        }

        function isValidSource(source)
        {
            return source && source != '';
        }

        function addListingSucceded(response) {
            console.log($scope.listing.Title + ' has been submitted');
            $scope.listing = response.data;

            //Upload images for the listing
            if (listingImages.length > 0) {
                fileUploadService.uploadImage(listingImages, $scope.listing.Id, uploadSuccess,uploadFailure,uploadProgress);
            }
            else
            {
                console.log("Listings with images gets more visits");
            }
        }

        function uploadSuccess(data, status, headers, config) {
            console.log($scope.listing.Title + 'images  have been submitted');
            $scope.UploadedFiles.push({ FileName: data.FileName, FilePath: data.LocalFilePath, FileLength: data.FileLength, Caption: data.Caption });
        }

        function uploadFailure(data, status, headers, config) {
            console.log(data);
        }

        function uploadProgress(progress) {
            console.log(progress);
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