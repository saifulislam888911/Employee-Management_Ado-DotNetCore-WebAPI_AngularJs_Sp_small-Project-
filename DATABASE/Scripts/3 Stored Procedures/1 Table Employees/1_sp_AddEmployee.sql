USE EmployeeManagementDB;
GO

CREATE OR ALTER PROCEDURE dbo.sp_AddEmployee
(
    @EmployeeName NVARCHAR(150),
    @DateOfBirth DATE,
    @JoiningDate DATE,
    @MobileNumber NVARCHAR(20),
    @Address NVARCHAR(500),
    @Division NVARCHAR(100),
    @District NVARCHAR(100),
    @Religion NVARCHAR(50),
    @Designation NVARCHAR(100),
    @Salary DECIMAL(18,2)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Employees
    (
        EmployeeName,
        DateOfBirth,
        JoiningDate,
        MobileNumber,
        [Address],
        Division,
        District,
        Religion,
        Designation,
        Salary
    )
    VALUES
    (
        @EmployeeName,
        @DateOfBirth,
        @JoiningDate,
        @MobileNumber,
        @Address,
        @Division,
        @District,
        @Religion,
        @Designation,
        @Salary
    );

    SELECT *
    FROM dbo.Employees
    WHERE EmployeeCode = SCOPE_IDENTITY();
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
WHERE name = 'sp_AddEmployee';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_AddEmployee
    @EmployeeName = 'Saiful Islam',
    @DateOfBirth = '1998-01-01',
    @JoiningDate = '2026-01-01',
    @MobileNumber = '01712345678',
    @Address = 'House No-1/A, Khilgaon, Dhaka-1000, Bangladesh',
    @Division = 'Dhaka',
    @District = 'Dhaka',
    @Religion = 'Islam',
    @Designation = 'CEO',
    @Salary = 100000000000000018.12;
GO
*/