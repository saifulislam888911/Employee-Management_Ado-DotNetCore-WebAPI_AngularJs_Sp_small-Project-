USE EmployeeManagementDB;
GO

CREATE OR ALTER PROCEDURE dbo.sp_GetAllEmployees
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        EmployeeCode,
        EmployeeName,
        DateOfBirth,
        JoiningDate,
        MobileNumber,
        [Address],
        Division,
        District,
        Religion,
        Designation,
        Salary,
        CreatedAt,
        CreatedBy
    FROM dbo.Employees
    ORDER BY EmployeeCode DESC;
END
GO

/*
---------- ---------- ----------
*/

/*
USE EmployeeManagementDB;
GO

SELECT name
FROM sys.procedures
WHERE name = 'sp_GetAllEmployees';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_GetAllEmployees;
GO
*/