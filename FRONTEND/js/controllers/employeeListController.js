app.controller("employeeListController", function ($scope, employeeService) {
  $scope.employees = [];
  $scope.loading = false;

  $scope.designations = [
    "Officer",
    "Senior Officer",
    "Principal Officer",
    "Senior Principal Officer",
    "Director",
    "Managing Director",
    "Chairman",
    "CEO",
  ];

  $scope.filters = {
    name: "",
    designation: "",
    fromDate: "",
    toDate: "",
    salary: null,
    minSalary: null,
    maxSalary: null,
  };

  // -- Get All Employees --
  $scope.loadEmployees = function () {
    $scope.loading = true;

    employeeService
      .getAllEmployees()
      .then(function (response) {
        $scope.employees = response.data;
      })
      .catch(function (error) {
        console.error("Error fetching Employees: ", error);
        alert("Failed to Get Employee List.");
      })
      .finally(function () {
        $scope.loading = false;
      });
  };

  // -- Filter: Get All Employees --
  $scope.applyFilter = function () {
    $scope.loading = true;

    var filterParams = {
      name: $scope.filters.name || null,
      designation: $scope.filters.designation || null,
      fromDate: $scope.filters.fromDate || null,
      toDate: $scope.filters.toDate || null,
      salary: $scope.filters.salary || null,
      minSalary: $scope.filters.minSalary || null,
      maxSalary: $scope.filters.maxSalary || null,
    };

    employeeService
      .filterEmployees(filterParams)
      .then(function (response) {
        $scope.employees = response.data;
      })
      .catch(function (error) {
        console.error("Error fetching Filtered Employees: ", error);
        alert("Failed to Get Filtered Employee List.");
      })
      .finally(function () {
        $scope.loading = false;
      });
  };

  // -- Filter: Reset --
  $scope.resetFilter = function () {
    $scope.filters = {
      name: "",
      designation: "",
      fromDate: "",
      toDate: "",
      salary: null,
      minSalary: null,
      maxSalary: null,
    };

    $scope.loadEmployees();
  };

  // -- Delete Employee --
  $scope.deleteEmployee = function (employeeCode) {
    var isConfirmed = confirm(
      "Want to Delete Employee with Code " + employeeCode + "?",
    );

    if (!isConfirmed) {
      return;
    }

    employeeService
      .deleteEmployee(employeeCode)
      .then(function () {
        alert("Successful: EmployeeCode " + employeeCode + " Deleted.");

        $scope.loadEmployees();
      })
      .catch(function (error) {
        console.error("Error Deleting Employee: ", error);
        alert("Failed to Delete Employee.");
      });
  };

  $scope.loadEmployees();
});
