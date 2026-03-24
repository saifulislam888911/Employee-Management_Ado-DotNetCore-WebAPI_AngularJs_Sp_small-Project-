USE EmployeeManagementDB;
GO

CREATE OR ALTER PROCEDURE dbo.sp_GetEmployeeByCode
(
    @EmployeeCode INT
)
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
    WHERE EmployeeCode = @EmployeeCode;
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
WHERE name = 'sp_GetEmployeeByCode';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_GetEmployeeByCode
    @EmployeeCode = 1;
GO
*/