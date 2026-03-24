app.controller(
  "viewModalController",
  function ($scope, $location, $rootScope, employeeService, viewModalService) {
    $scope.viewModalState = viewModalService.state;

    $scope.designations = [
      "Officer",
      "Senior Officer",
      "Principal Officer",
      "Senior Principal Officer",
    ];

    // Newly Added Employee will be shown
    $scope.onEmployeeViewModal = function () {
      if (!$scope.viewModalState.employeeCode) {
        return;
      }

      employeeService
        .getEmployeeByCode($scope.viewModalState.employeeCode)
        .then(function (response) {
          $scope.viewModalState.employee = response.data;
        })
        .catch(function (error) {
          console.error("Error Loading Employee in Modal: ", error);
        });
    };

    // Newly Added Employee's Designation and Salary Adding
    $scope.updateEmployeeViewModal = function () {
      var employeeCode = $scope.viewModalState.employeeCode;

      var emplyDsgntnAndSlry = {
        designation: $scope.viewModalState.employee.designation,
        salary: $scope.viewModalState.employee.salary,
      };

      employeeService
        .updateEmployee(employeeCode, emplyDsgntnAndSlry)
        .then(function () {
          alert("Designation and salary saved successfully.");

          $rootScope.$broadcast("employeeDataUpdated");

          viewModalService.closeModal();

          $location.path("/dashboard");
        })
        .catch(function (error) {
          console.error(
            "Error Saving Designation and Salary from Modal:",
            error,
          );
          alert("Failed to Save Designation and Salary.");
        });
    };

    // Dashboard Employee View
    $scope.editEmployeeViewModal = function () {
      var employeeCode = $scope.viewModalState.employeeCode;

      viewModalService.closeModal();

      $location.path("/editEmployee/" + employeeCode);
    };

    // Close The Modal
    $scope.closeModal = function () {
      viewModalService.closeModal();

      $location.path("/dashboard");
    };
  },
);
