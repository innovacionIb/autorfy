// SPDX-License-Identifier: Apache-2.0

'use strict';

var app = angular.module('application', []);

// Angular Controller
app.controller('appController', function($scope, appFactory){

	$("#success_holder").hide();
	$("#success_create").hide();
	$("#error_holder").hide();
	$("#error_query").hide();
	
	$scope.queryAllObra = function(){

		appFactory.queryAllObra(function(data){
			var array = [];
			for (var i = 0; i < data.length; i++){
				parseInt(data[i].Key);
				data[i].Record.Key = parseInt(data[i].Key);
				array.push(data[i].Record);
			}
			array.sort(function(a, b) {
			    return parseFloat(a.Key) - parseFloat(b.Key);
			});
			$scope.all_Obra = array;
		});
	}

	$scope.queryObra = function(){

		var id = $scope.Obra_id;

		appFactory.queryObra(id, function(data){
			$scope.query_Obra = data;

			if ($scope.query_Obra == "Could not locate Obra"){
				console.log()
				$("#error_query").show();
			} else{
				$("#error_query").hide();
			}
		});
	}

	$scope.recordObra = function(){

		appFactory.recordObra($scope.Obra, function(data){
			$scope.create_Obra = data;
			$("#success_create").show();
		});
	}

	$scope.changeHolder = function(){

		appFactory.changeHolder($scope.holder, function(data){
			$scope.change_holder = data;
			if ($scope.change_holder == "Error: no Obra catch found"){
				$("#error_holder").show();
				$("#success_holder").hide();
			} else{
				$("#success_holder").show();
				$("#error_holder").hide();
			}
		});
	}

});

// Angular Factory
// SPDX-License-Identifier: Apache-2.0

'use strict';

var app = angular.module('application', []);

// Angular Controller
app.controller('appController', function($scope, appFactory){

	$("#success_holder").hide();
	$("#success_create").hide();
	$("#error_holder").hide();
	$("#error_query").hide();
	
	$scope.queryAllObra = function(){

		appFactory.queryAllObra(function(data){
			var array = [];
			for (var i = 0; i < data.length; i++){
				parseInt(data[i].Key);
				data[i].Record.Key = parseInt(data[i].Key);
				array.push(data[i].Record);
			}
			array.sort(function(a, b) {
			    return parseFloat(a.Key) - parseFloat(b.Key);
			});
			$scope.all_obra = array;
		});
	}

	$scope.queryObra = function(){

		var id = $scope.obra_id;

		appFactory.queryObra(id, function(data){
			$scope.query_obra = data;

			if ($scope.query_obra == "Could not locate obra"){
				console.log()
				$("#error_query").show();
			} else{
				$("#error_query").hide();
			}
		});
	}

	$scope.recordObra = function(){

		appFactory.recordObra($scope.obra, function(data){
			$scope.create_obra = data;
			$("#success_create").show();
		});
	}

	$scope.changeHolder = function(){

		appFactory.changeHolder($scope.holder, function(data){
			$scope.change_holder = data;
			if ($scope.change_holder == "Error: no obra catch found"){
				$("#error_holder").show();
				$("#success_holder").hide();
			} else{
				$("#success_holder").show();
				$("#error_holder").hide();
			}
		});
	}

});

// Angular Factory
app.factory('appFactory', function($http){
	
	var factory = {};

    factory.queryAllObra = function(callback){

    	$http.get('/get_all_obra/').success(function(output){
			callback(output)
		});
	}

	factory.queryObra = function(id, callback){
    	$http.get('/get_obra/'+id).success(function(output){
			callback(output)
		});
	}

	factory.recordObra = function(data, callback){

		data.location = data.longitude + ", "+ data.latitude;

		var obra = data.id + "-" + data.location + "-" + data.timestamp + "-" + data.holder + "-" + data.vessel;

    	$http.get('/add_obra/'+obra).success(function(output){
			callback(output)
		});
	}

	

	return factory;
});


