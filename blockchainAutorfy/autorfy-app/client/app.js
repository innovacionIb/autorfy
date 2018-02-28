// SPDX-License-Identifier: Apache-2.0

'use strict';

var app = angular.module('application', []);
var file;
// Angular Factory
app.factory('appFactory', function ($http) {

    var factory = {};

    factory.queryAllObra = function (callback) {

        $http.get('/get_all_obra/').success(function (output) {
            callback(output)
        });
    }

    factory.queryObra = function (id, callback) {
        $http.get('/get_obra/' + id).success(function (output) {
            callback(output)
        });
    }
    factory.addUser = function (data, callback) {
        $http.get('/add_User/' + data.name).success(function (output) {
            callback(output)
        });
    }
    factory.recordObra = function (data, callback) {
        $http.get('/get_all_obra/').success(function (output1) {
            var obra = (output1.length + 1) + "-" +  file.name + "-" + data.location + "-" + data.fechacreacion.replaceAll("/", "_")   + "-" +  Math.round(+new Date() / 1000).toString()+ "-" +data.descripcion + "-" +data.usuario+ "-" + data.tag ;
            $http.get('/add_obra/' + obra).success(function (output) {
                window.location.href = './Main.html';
                callback(output)
            });
        });
    }

    return factory;
});



// Angular Controller
app.controller('appController', function ($scope, appFactory) {

    $("#success_holder").hide();
    $("#success_create").hide();
    $("#error_holder").hide();
    $("#error_query").hide();

    $scope.queryAllObra = function () {

        appFactory.queryAllObra(function (data) {
            var array = [];
            for (var i = 0; i < data.length; i++) {
                parseInt(data[i].Key);
                data[i].Record.Key = parseInt(data[i].Key);
                array.push(data[i].Record);
            }
            array.sort(function (a, b) {
                return parseFloat(a.Key) - parseFloat(b.Key);
            });
            $scope.all_Obra = array;
        });
    }

    $scope.queryObra = function () {

        var id = $scope.Obra_id;

        appFactory.queryObra(id, function (data) {
            $scope.query_Obra = data;

            if ($scope.query_Obra == "Could not locate Obra") {
                console.log()
                $("#error_query").show();
            } else {
                $("#error_query").hide();
            }
        });
    }

    $scope.recordObra = function () {
        $scope.Obra.location = $("#hdlat").val() + "," + $("#hdlong").val();
        appFactory.recordObra($scope.Obra, function (data) {
            $scope.create_Obra = data;
            $("#success_create").show();
        });
    }

    $scope.addUser = function () {
        appFactory.addUser($scope.User, function (data) {
            $("#success_create").show();
            window.location.href = './Main.html';
        });
    }

    $scope.queryObraTag = function (tag) {

        appFactory.queryAllObra(function (data) {
            var array = [];
            for (var i = 0; i < data.length; i++) {
                parseInt(data[i].Key);
                data[i].Record.Key = parseInt(data[i].Key);
                if (data[i].Record.tag.includes(tag)) { array.push(data[i].Record); }
            }
            array.sort(function (a, b) {
                return parseFloat(a.Key) - parseFloat(b.Key);
            });
            $scope.all_Obra = array;
        });
    }
});

angular.module("application").directive("filesInput", function () {
    return {
        require: "ngModel",
        link: function postLink(scope, elem, attrs, ngModel) {
            elem.on("change", function (e) {
                file = elem[0].files[0];
                var file_data = file;
                var form_data = new FormData();
                form_data.append('file', file_data);
                $.ajax({
                    url: 'http://ibinnovation9734.cloudapp.net/ImgAutorFY/uploadID.php', // point to server-side PHP script 
                    dataType: 'text',  // what to expect back from the PHP script, if anything
                    cache: false,
                    contentType: false,
                    processData: false,
                    data: form_data,
                    type: 'post',
                    success: function (php_script_response) {
                    }
                });
            })
        }
    }
});


String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

