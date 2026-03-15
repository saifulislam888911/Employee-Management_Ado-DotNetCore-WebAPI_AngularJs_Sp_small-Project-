app.controller("addEmployeeController", function ($scope, employeeService) {
  $scope.employee = {
    employeeName: "",
    dateofBirth: "",
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
        console.log("Added employee:", response.data);

        $scope.employee = {
          employeeName: "",
          dateofBirth: "",
          joiningDate: "",
          mobileNumber: "",
          address: "",
          division: "",
          district: "",
          religion: "",
        };
      })
      .catch(function (error) {
        console.error("Error Adding Employee:", error);
        alert("Failed to add employee.");
      });
  };
});
