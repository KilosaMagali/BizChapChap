(function (app) {
    'use strict';
    app.factory('fileUploadService', fileUploadService);
    fileUploadService.$inject = ['$rootScope', 'Upload', 'apiService'];
    function fileUploadService($rootScope, Upload, apiService) {
        $rootScope.upload = [];
        $rootScope.UploadedFiles = [];
        $rootScope.Images = [];
        $rootScope.ImageToFetch = '';

        var service = {
            uploadImage: uploadImage,
            abortUpload: abortUpload,
            getImages: getImages,
            getImage: getImage
        }

        function uploadImage($files, listingId, uploadSuccess, uploadFailure, uploadProgress) {
            //$files: an array of files selected
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    var ImageVM = {
                        ListingId: listingId,
                        UploadedImage: $file
                    };
                    $rootScope.upload[index] = apiService.postFile("api/upload/postImage", ImageVM, uploadSuccess, uploadFailure, uploadProgress);
                })(i);
            }
        }

        function abortUpload(index) {
            $rootScope.upload[index].abort();
        }

        function getImages(getImagesSuccess, getImagesFailure) {
            var config = {};
            apiService.get("api/upload/getimages", config, getImagesSuccess, getImagesFailure);
        }

        function getImage(imageName, getImagesSuccess, getImagesFailure) {
            var config = {
                params: {
                    imageName: imageName
                }
            };

            apiService.get("api/upload/getimage", config, getImagesSuccess, getImagesFailure);
        }

        return service;
    }

})(angular.module('bizChapChap'));