app.controller("UserController",
function(restEndPoint, $scope, $http) {
	$scope.currentPage = 1;
	$scope.pageSize = 3;

	$scope.user = {};

	getData();

	$scope.CreateUser = function(btn) {
		var button = angular.element(btn.getElementsByTagName("button"));

		$scope.DisableButton(button[0], true);

		$http.post(restEndPoint + "api/user", $scope.user)
			.then(function (){
				$scope.user = {};
				getData(button[0]);

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


	$scope.DeleteUser = function(btn, id) {

		$scope.DisableButton(btn, true);

		$http.delete(restEndPoint + "api/user", { params: { id: id } })
			.then(function() {
				getData(btn);
			});
	}

	function getData(btn) {
		$http.get(restEndPoint + "api/user?pageNumber=" + $scope.currentPage + "&pageSize=" + $scope.pageSize)
			.then(function(response) {
				if (btn != null) {
					$scope.DisableButton(btn, false);
				}
				$scope.deleteShow = false;
				$scope.totalItems = response.data.TotalItems;
				$scope.items = response.data.Items;
			});
	}

	$scope.pageChanged = function() {
		getData();
	};
});