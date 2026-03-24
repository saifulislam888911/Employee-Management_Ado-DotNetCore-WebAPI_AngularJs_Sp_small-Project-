app.controller(
  "editEmployeeController",
  function ($scope, $routeParams, $location, employeeService) {
    $scope.employeeCode = $routeParams.employeeCode;
    $scope.loading = false;

    $scope.designations = [
      "Officer",
      "Senior Officer",
      "Principal Officer",
      "Senior Principal Officer",
    ];

    $scope.employee = {
      employeeName: "",
      dateOfBirth: "",
      joiningDate: "",
      mobileNumber: "",
      address: "",
      division: "",
      district: "",
      religion: "",
      designation: "",
      salary: null,
    };

    // -- Get Employee by Code(ID) --
    $scope.loadEmployeeByCode = function () {
      $scope.loading = true;

      employeeService
        .getEmployeeByCode($scope.employeeCode)
        .then(function (response) {
          $scope.employee = response.data;

          if ($scope.employee.dateOfBirth) {
            $scope.employee.dateOfBirth = new Date($scope.employee.dateOfBirth);
          }

          if ($scope.employee.joiningDate) {
            $scope.employee.joiningDate = new Date($scope.employee.joiningDate);
          }
        })
        .catch(function (error) {
          console.error("Error Loading Employee: ", error);
          alert("Failed to Load Employee");
        })
        .finally(function () {
          $scope.loading = false;
        });
    };

    // -- Update Employee By Code(Id) --
    $scope.updateEmployee = function () {
      $scope.loading = true;

      employeeService
        .updateEmployee($scope.employeeCode, $scope.employee)
        .then(function (response) {
          alert("Employee Updated Successfully.");
          console.log("Updated Employee: ", response.data);

          $location.path("/employeeList");
        })
        .catch(function (error) {
          console.error("Error Upating Employee: ", error);
          alert("Failed to Update Employee");
        })
        .finally(function () {
          $scope.loading = false;
        });
    };

    $scope.loadEmployeeByCode();
  },
);
