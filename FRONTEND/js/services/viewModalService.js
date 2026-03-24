app.factory("viewModalService", function (employeeService) {
  var state = {
    isOpen: false,
    mode: "view",
    title: "",

    employeeCode: null,
    employee: {},
    employeeDropdown: [],
  };

  function resetState() {
    state.isOpen = false;
    state.mode = "view";
    state.title = "";

    state.employeeCode = null;
    state.employee = {};
    state.employeeDropdown = [];
  }

  function loadEmployeeDropdown() {
    return employeeService.getAllEmployees().then(function (response) {
      state.employeeDropdown = response.data;
    });
  }

  return {
    state: state,

    openAssignModal: function (newEmployee) {
      state.isOpen = true;
      state.mode = "assign";
      state.title = "Assign Designation and Salary";

      state.employeeCode = newEmployee.employeeCode;
      state.employee = angular.copy(newEmployee);

      return loadEmployeeDropdown();
    },

    openViewModal: function (employee) {
      state.isOpen = true;
      state.mode = "view";
      state.title = "Employee Details";

      state.employeeCode = employee.employeeCode;
      state.employee = angular.copy(employee);

      return loadEmployeeDropdown();
    },

    closeModal: function () {
      resetState();
    },
  };
});
