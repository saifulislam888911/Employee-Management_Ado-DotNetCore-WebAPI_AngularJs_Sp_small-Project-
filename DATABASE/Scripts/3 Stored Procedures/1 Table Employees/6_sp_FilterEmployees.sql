USE EmployeeManagementDB;
GO

CREATE OR ALTER PROCEDURE dbo.sp_FilterEmployees
(
    @Name NVARCHAR(150) = NULL,
    @Designation NVARCHAR(100) = NULL,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL,
    @Salary DECIMAL(18,2) = NULL,
    @MinSalary DECIMAL(18,2) = NULL,
    @MaxSalary DECIMAL(18,2) = NULL
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
    WHERE 
        (
            @Name IS NULL 
            OR 
            @Name = '' 
            OR 
            EmployeeName LIKE CONCAT('%', @Name, '%') 
        )
        AND
        (
            @Designation IS NULL 
            OR 
            @Designation = '' 
            OR 
            Designation = @Designation
        )
        AND
        (
            @FromDate IS NULL 
            OR 
            JoiningDate >= @FromDate
        )
        AND
        (
            @ToDate IS NULL 
            OR 
            JoiningDate <= @ToDate
        )
        AND
        (
            (
                @Salary IS NULL
                AND
                @MinSalary IS NULL
                AND
                @MaxSalary IS NULL
            )
            OR
            (
                @Salary IS NOT NULL
                AND
                Salary = @Salary
            )
            OR
            (
                (
                    @MinSalary IS NOT NULL 
                    OR 
                    @MaxSalary IS NOT NULL
                )
                AND
                (
                    @MinSalary IS NULL 
                    OR 
                    Salary >= @MinSalary
                )
                AND
                (
                    @MaxSalary IS NULL 
                    OR 
                    Salary <= @MaxSalary
                )
            )
        )           
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
WHERE name = 'sp_FilterEmployees';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees;
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @Name = 'C';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @Designation = 'Officer';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @FromDate = '2020-01-01';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @ToDate = '2026-10-10';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @Salary = 5000000;
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @MinSalary = 5000000,
    @MaxSalary = 10000000;
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @Salary = 5000000,
    @MinSalary = 1000000,
    @MaxSalary = 10000000;
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @Name = 'C',
    @Designation = 'Officer',
    @FromDate = '2020-01-01',
    @ToDate = '2026-10-10',
    @Salary = 5000000;
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_FilterEmployees
    @Name = 'B',
    @Designation = 'Officer',
    @FromDate = '2020-01-01',
    @ToDate = '2026-10-10',
    @MinSalary = 5000000,
    @MaxSalary = 10000000;
GO
*/