USE EmployeeManagementDB;
GO

IF OBJECT_ID('dbo.Employees', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Employees
    (
        EmployeeCode INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeName NVARCHAR(150) NOT NULL,
        DateOfBirth DATE NOT NULL,
        JoiningDate DATE NOT NULL,
        MobileNumber NVARCHAR(20) NOT NULL,
        [Address] NVARCHAR(500) NOT NULL,
        Division NVARCHAR(100) NOT NULL,
        District NVARCHAR(100) NOT NULL,
        Religion NVARCHAR(50) NOT NULL,
        Designation NVARCHAR(100) NOT NULL,
        Salary DECIMAL(18,2) NOT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy NVARCHAR(150) NOT NULL DEFAULT 'ADMIN-SAIF'
    );
END
GO