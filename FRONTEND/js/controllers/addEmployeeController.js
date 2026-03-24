app.controller(
  "addEmployeeController",
  function ($scope, $location, employeeService, viewModalService) {
    $scope.employee = {
      employeeName: "",
      dateOfBirth: "",
      joiningDate: "",
      mobileNumber: "",
      address: "",
      division: "",
      district: "",
      religion: "",
    };

    $scope.addEmployee = function () {
      employeeService
        .addEmployee($scope.employee)
        .then(function (response) {
          alert("Employee added successfully.");
          console.log("Added employee: ", response.data);

          var newEmployee = response.data;

          $scope.employee = {
            employeeName: "",
            dateOfBirth: "",
            joiningDate: "",
            mobileNumber: "",
            address: "",
            division: "",
            district: "",
            religion: "",
          };

          // $location.path("/employeeList");

          viewModalService.openAssignModal(newEmployee);
        })
        .catch(function (error) {
          console.error("Error Adding Employee: ", error);
          alert("Failed to add employee.");
        });
    };
  },
);
