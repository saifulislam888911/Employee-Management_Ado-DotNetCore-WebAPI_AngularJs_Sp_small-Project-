USE EmployeeManagementDB;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DeleteEmployee
(
    @EmployeeCode INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE
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
WHERE name = 'sp_DeleteEmployee';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_DeleteEmployee
    @EmployeeCode = 6;
GO
*/