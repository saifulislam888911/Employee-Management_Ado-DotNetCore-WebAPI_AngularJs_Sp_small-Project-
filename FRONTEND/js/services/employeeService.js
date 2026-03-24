app.service("employeeService", function ($http, API_BASE_URL) {
  this.addEmployee = function (employee) {
    return $http.post(API_BASE_URL + "/employees", employee);
  };

  this.getAllEmployees = function () {
    return $http.get(API_BASE_URL + "/employees");
  };

  this.getEmployeeByCode = function (employeeCode) {
    return $http.get(API_BASE_URL + "/employees/" + employeeCode);
  };

  this.updateEmployee = function (employeeCode, employee) {
    return $http.put(API_BASE_URL + "/employees/" + employeeCode, employee);
  };

  this.deleteEmployee = function (employeeCode) {
    return $http.delete(API_BASE_URL + "/employees/" + employeeCode);
  };

  this.filterEmployees = function (filters) {
    return $http.get(API_BASE_URL + "/employees/filter", { params: filters });
  };
});
