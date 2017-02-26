(function (app) {
    'use strict';
    app.controller('fileUploadCtrl',fileUploadCtrl);
    fileUploadCtrl.$inject = ['$scope', '$rootScope', 'Upload', 'apiService'];
    function fileUploadCtrl($scope, $rootScope, Upload, apiService)
    {
        $scope.upload = [];
        $scope.UploadedFiles = [];
        $scope.FilesToUpload = [];
        $scope.ImagePreviewSrc = 'http://farm4.static.flickr.com/3316/3546531954_eef60a3d37.jpg';
        $scope.Images = [];
        $scope.ImageToFetch = '';

        console.log("In Upload Ctrl");
        $scope.startUploading = function ($files) {
            console.log("In Uploading " + $files.length + " files");

            //$files: an array of files selected
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    var ImageVM = {
                        Caption: "Here is a caption",
                        UploadedImage:$file
                    };
                    $scope.upload[index] = apiService.postFile("api/upload/postImage", ImageVM, uploadSuccess, uploadFailure, uploadProgress);
                })(i);
            }
        }

        function uploadSuccess(data, status, headers, config) {
            $scope.UploadedFiles.push({ FileName: data.FileName, FilePath: data.LocalFilePath, FileLength: data.FileLength, Caption: data.Caption });
        }

        function uploadFailure(data, status, headers, config) {
            console.log(data);
        }

        function uploadProgress(progress) {
            console.log(progress);
        }

        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        }

        $scope.showImagePreview = function (input) {
            console.log("Previewing " + input.length);
            
            if (input && input.length > 0) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $scope.ImagePreviewSrc = '';
                    $scope.ImagePreviewSrc = e.target.result;
                }

                reader.readAsDataURL(input[0]);
            }
        }

        $scope.getImages = function () {
            if ($scope.ImageToFetch)
            {
                $scope.getImage($scope.ImageToFetch);
                return;
            }
            var config = {};
            apiService.get("api/upload/getimages", config, getImagesSuccess, getImagesFailure);
        }

        $scope.getImage = function (imageName) {
            var config = {
                params: {
                    imageName: imageName
                }
            };

            apiService.get("api/upload/getimage", config, getImagesSuccess, getImagesFailure);
        }

        function getImagesFailure(error) {
            console.log(error.status);
        }

        function getImagesSuccess(result) {
            console.log("Success => images fetch")
            if (Array.isArray(result.data)) 
            {
                $scope.Images = result.data;
            }
            else
            {
                $scope.Images = [];
                $scope.Images.push(result.data);
            }
                
        }


       
}

})(angular.module('bizChapChap'));