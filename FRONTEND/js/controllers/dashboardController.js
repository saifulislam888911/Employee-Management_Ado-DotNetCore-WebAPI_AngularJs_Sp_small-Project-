app.controller(
  "dashboardController",
  function ($scope, $rootScope, employeeService, viewModalService) {
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

    $scope.viewEmployeeByCode = function (employeeCode) {
      employeeService
        .getEmployeeByCode(employeeCode)
        .then(function (response) {
          viewModalService.openViewModal(response.data);
        })
        .catch(function (error) {
          console.error("Error loading employee:", error);
          alert("Failed to load employee.");
        });
    };

    $scope.$on("employeeDataUpdated", function () {
      $scope.loadEmployees();
    });

    $scope.loadEmployees();
  },
);
