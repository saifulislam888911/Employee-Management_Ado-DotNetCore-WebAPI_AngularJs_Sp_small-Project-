app.controller("dashboardController", function ($scope, employeeService) {
  $scope.employees = [];
  $scope.loading = false;

  $scope.loadEmployees = function () {
    $scope.loading = true;

    employeeService
      .getAllEmployees()
      .then(function (response) {
        $scope.employees = response.data;
      })
      .catch(function (error) {
        console.error("Error fetching employees:", error);
        alert("Failed to Get Employee List.");
      })
      .finally(function () {
        $scope.loading = false;
      });
  };

  $scope.loadEmployees();
});
