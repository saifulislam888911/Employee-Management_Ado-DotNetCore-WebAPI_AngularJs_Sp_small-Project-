var app = angular.module("employeeApp", ["ngRoute"]);

app.constant("API_BASE_URL", "https://localhost:7049/api");

app.config(function ($routeProvider) {
  $routeProvider
    .when("/dashboard", {
      templateUrl: "views/dashboard.html",
      controller: "dashboardController",
    })
    .when("/addEmployee", {
      templateUrl: "views/addEmployee.html",
      controller: "addEmployeeController",
    })
    .when("/employeeList", {
      templateUrl: "views/employeeList.html",
      controller: "employeeListController",
    })
    .when("/editEmployee/:employeeCode", {
      templateUrl: "views/editEmployee.html",
      controller: "editEmployeeController",
    })
    .otherwise({
      redirectTo: "/dashboard",
    });
});
