USE EmployeeManagementDB;
GO

CREATE OR ALTER PROCEDURE dbo.sp_UpdateEmployee
(
    @EmployeeCode INT,
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

    UPDATE dbo.Employees
    SET
        EmployeeName = @EmployeeName,
        DateOfBirth = @DateOfBirth,
        JoiningDate = @JoiningDate,
        MobileNumber = @MobileNumber,
        [Address] = @Address,
        Division = @Division,
        District = @District,
        Religion = @Religion,
        Designation = @Designation,
        Salary = @Salary
    WHERE EmployeeCode = @EmployeeCode;  

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
WHERE name = 'sp_UpdateEmployee';
GO
*/

/*
USE EmployeeManagementDB;
GO

EXEC dbo.sp_UpdateEmployee
    @EmployeeCode = 5,
    @EmployeeName = 'Update Name',
    @DateOfBirth = '1990-01-01',
    @JoiningDate = '2020-01-01',
    @MobileNumber = '1234567890',
    @Address = '123 Update Street',
    @Division = 'Dhaka',
    @District = 'Dhaka',
    @Religion = 'Islam',
    @Designation = 'Officer',
    @Salary = 50000
GO
*/