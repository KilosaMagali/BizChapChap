(function (app) {
    'use strict';

    app.factory('apiService', apiService);

    apiService.$inject = ['$http', '$rootScope', 'Upload'];

    function apiService($http, $rootScope, Upload) {
        var service = {
            get: get,
            post: post,
            postUrlEncoded: postUrlEncoded,
            postFile: uploadFile
        };

        function get(url, config, success, failure) {
            return $http.get(url, config)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        failure(error);
                    });
        }

        function post(url, data, success, failure) {
            return $http.post(url, data)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        failure(error);
                    });
        }

        function uploadFile(webapiurl, $file, success, failure, progress) {
            return Upload.upload({
                url: webapiurl, 
                method: "POST",
                file: $file
            })
             .progress(function (evt) {
                progress(evt);
            })
             .success(function (data, status, headers, config) {
                success(data, status, headers, config);
             })
             .error(function (data, status, headers, config) {
                failure(data, status, headers, config);
            });
        }

        function postUrlEncoded(url,dataIn,success, failure) {
            $http({
                method: 'POST',
                url: url,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: dataIn
            }).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }

        return service;
    }

})(angular.module('bizChapChap'));