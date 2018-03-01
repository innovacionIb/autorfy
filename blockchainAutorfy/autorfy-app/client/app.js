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
        $http.get('/add_User/' + data.email).success(function (output) {
            callback(output)
        });
    }
    factory.getUser = function (data, callback) {
        $http.get('/get_user/' + data.email).success(function (output) {
            callback(output)
        });
    }
    factory.recordObra = function (data, callback) {
        $http.get('/get_all_obra/').success(function (output1) {
            var obra = (output1.length + 1) + "-" + file.name + "-" + data.location + "-" + data.fechacreacion.replaceAll("/", "_") + "-" + Math.round(+new Date() / 1000).toString() + "-" + data.descripcion + "-" + localStorage.getItem("UEmail")+ "-" + data.tag ;
            $http.get('/add_obra/' + obra).success(function (output) {
                callback(output)
            });
        });
    }

    return factory;
});

app.controller('LoginController', function ($scope, appFactory) {
    $scope.Login = function () {
        appFactory.getUser($scope.User, function (data) {
            if (data.enrollment.signingIdentity == User.password && data.name == User.email)
            {
                localStorage.setItem("IDAutorFY", data.enrollment.signingIdentity);
                localStorage.setItem("UEmail", data.name);
                window.location.href = './Main.html';
            }
        });
    }
    $scope.getLocalProfile = function (callback) {
        var profileImgSrc = localStorage.getItem("PROFILE_IMG_SRC");
        var profileName = localStorage.getItem("PROFILE_NAME");
        var profileReAuthEmail = localStorage.getItem("PROFILE_REAUTH_EMAIL");

        if (profileName !== null
            && profileReAuthEmail !== null
            && profileImgSrc !== null) {
            callback(profileImgSrc, profileName, profileReAuthEmail);
        }
    }

    /**
     * Main function that load the profile if exists
     * in localstorage
     */
    $scope.loadProfile= function() {
        if (!$scope.supportsHTML5Storage()) { return false; }
        // we have to provide to the callback the basic   information to set the profile
        $scope.getLocalProfile(function (profileImgSrc, profileName, profileReAuthEmail) {
            //changes in the UI
            $("#profile-img").attr("src", profileImgSrc);
            $("#profile-name").html(profileName);
            $("#reauth-email").html(profileReAuthEmail);
            $("#inputEmail").hide();
            $("#remember").hide();
        });
    }

    /**
     * function that checks if the browser supports HTML5
     * local storage
     */
    $scope.supportsHTML5Storage= function() {
        try {
            return 'localStorage' in window && window['localStorage'] !== null;
        } catch (e) {
            return false;
        }
    }

    /**
     * Test data. This data will be safe by the web app in the first successful login of a auth user. To Test the scripts, delete the localstorage data and comment this call.
     */
    $scope.testLocalStorageData= function() {
        if (!$scope.supportsHTML5Storage()) { return false; }
        localStorage.setItem("PROFILE_IMG_SRC", "//lh3.googleusercontent.com/-6V8xOA6M7BA/AAAAAAAAAAI/AAAAAAAAAAA/rzlHcD0KYwo/photo.jpg?sz=120");
        localStorage.setItem("PROFILE_NAME", "César Izquierdo Tello");
        localStorage.setItem("PROFILE_REAUTH_EMAIL", "oneaccount@gmail.com");
    }

    $scope.loadProfile();
});

// Angular Controller
app.controller('appController', function ($scope, appFactory) {

    $("#success_holder").hide();
    $("#success_create").hide();
    $("#error_holder").hide();
    $("#error_query").hide();
    if (!(localStorage.getItem("IDAutorFY") != null && localStorage.getItem("IDAutorFY") != "")) {
        window.location.href = './';
    }
    $scope.queryAllObra = function () {

        appFactory.queryAllObra(function (data) {
            var array = [];
            for (var i = 0; i < data.length; i++) {
                parseInt(data[i].Key);
                data[i].Record.Key = parseInt(data[i].Key);
                if (data[i].Record.usuario == localStorage.getItem("UEmail")) {
                    array.push(data[i].Record);
                }
            }
            array.sort(function (a, b) {
                return parseFloat(a.Key) - parseFloat(b.Key);
            });
            $scope.all_Obra = array;
        });
    }

    //$scope.queryObra = function () {

    //    var id = $scope.Obra_id;

    //    appFactory.queryObra(id, function (data) {
    //        $scope.query_Obra = data;

    //        if ($scope.query_Obra == "Could not locate Obra") {
    //            console.log()
    //            $("#error_query").show();
    //        } else {
    //            $("#error_query").hide();
    //        }
    //    });
    //}

    $scope.queryObraTag = function (tag) {

        appFactory.queryAllObra(function (data) {
            var array = [];
            for (var i = 0; i < data.length; i++) {
                parseInt(data[i].Key);
                data[i].Record.Key = parseInt(data[i].Key);
                if (data[i].Record.tag.includes(tag) && data[i].Record.usuario == localStorage.getItem("UEmail")) { array.push(data[i].Record); }
            }
            array.sort(function (a, b) {
                return parseFloat(a.Key) - parseFloat(b.Key);
            });
            $scope.all_Obra = array;
        });
    }
});
app.controller('AddWorkController', function ($scope, appFactory)
{
    if (!(localStorage.getItem("IDAutorFY") != null && localStorage.getItem("IDAutorFY") != "")) {
        window.location.href = './';
    }
    $scope.usuario = localStorage.getItem("UEmail");
    $("#inputUsuario").val($scope.usuario);
    $scope.recordObra = function () {
        $scope.Obra.location = $("#hdlat").val() + "," + $("#hdlong").val();
        appFactory.recordObra($scope.Obra, function (data) {
            $scope.create_Obra = data;
            $('#myModal').modal('show');
        });
    }
});

app.controller('RegisterController', function ($scope, appFactory) {
    $scope.addUser = function () {
        appFactory.addUser($scope.User, function (data) {
            $scope.NewUser = { Id: data.enrollment.signingIdentity, Email: data.name };
            $('#myModal').modal('show');
            localStorage.setItem("IDAutorFY", data.enrollment.signingIdentity);
            localStorage.setItem("UEmail", data.name );
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

