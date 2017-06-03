app.controller("PetController",
function (restEndPoint, $scope, $http, $routeParams)
{
	$scope.currentPage = 1;
	$scope.pageSize = 3;

	$scope.pet = {};

	$scope.$on("$routeChangeSuccess", function () {
		var id = $routeParams["id"];
		if (id !== "undefined") {
			$scope.userId = id;
			getData(id);
		}
	});

	$scope.CreatePet = function(btn) {
		var button = angular.element(btn.getElementsByTagName("button"));

		$scope.DisableButton(button[0], true);

		$scope.pet.userId = $scope.userId;

		$http.post(restEndPoint + "api/pet", $scope.pet)
			.then(function () {
				$scope.pet = {};
				getData($scope.userId,button[0]);
			});
	};

	$scope.DisableButton = function(btn, isDisable) {
		btn.disabled = isDisable;
		var spanButton = angular.element(btn.getElementsByTagName("span"));
		if (isDisable) {
			spanButton[0].style.display = "";
		} else {
			spanButton[0].style.display = "none";
		}
	}


	$scope.DeletePet = function(btn, id) {

		$scope.DisableButton(btn, true);

		$http.delete(restEndPoint + "api/pet", { params: { id: id } })
			.then(function() {
				getData($scope.userId, btn);
			});
	}

	function getData(userId, btn) {
		$http.get(restEndPoint + "api/pet?pageNumber=" + $scope.currentPage + "&pageSize=" + $scope.pageSize + "&userId=" + userId)
			.then(function(response) {
				if (btn != null) {
					$scope.DisableButton(btn, false);
				}
				$scope.deleteShow = false;
				$scope.totalItems = response.data.TotalItems;
				$scope.items = response.data.Items;
				$scope.UserName = response.data.UserName;
			});
	}

	$scope.pageChanged = function() {
		getData($scope.userId);
	};
});