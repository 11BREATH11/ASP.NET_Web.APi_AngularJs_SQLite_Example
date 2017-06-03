var app = angular.module("OwnersApp", ["ui.bootstrap", "ngRoute"])
.config(function ($routeProvider,$locationProvider)
	{
		$routeProvider.when("/pets/:id",
        {
        	templateUrl: "views/pets.html",
        	controller: "UserController"
        });

		$routeProvider.when("/users",
        {
        	templateUrl: "views/users.html",
        	controller: "UserController"
        });

		$routeProvider.otherwise({ redirectTo: "/users" });

		$locationProvider.html5Mode(true);

}).constant("restEndPoint", "http://localhost:52027/");;
