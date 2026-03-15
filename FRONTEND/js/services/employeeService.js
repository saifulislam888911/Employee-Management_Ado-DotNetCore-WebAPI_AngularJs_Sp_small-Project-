app.service("employeeService", function ($http, API_BASE_URL) {
  this.getAllEmployees = function () {
    return $http.get(API_BASE_URL + "/employees");
  };

  this.addEmployee = function (employee) {
    return $http.post(API_BASE_URL + "/employees", employee);
  };
});
