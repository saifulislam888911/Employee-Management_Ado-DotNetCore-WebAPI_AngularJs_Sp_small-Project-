USE master;
GO

IF DB_ID('EmployeeManagementDB') IS NULL
BEGIN
    CREATE DATABASE EmployeeManagementDB;
END
GO



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



IF NOT EXISTS (SELECT 1 FROM dbo.Employees)
BEGIN
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
        'Mr. AAA',
        '1998-01-01',
        '2026-02-02',
        '01712345678',
        'Dhaka, Bangladesh',
        'Dhaka',
        'Dhaka',
        'Islam',
        'Chairman',
        10000000018.12
    ),
    (
        'Mr. BBB',
        '1998-01-01',
        '2026-02-02',
        '01812345678',
        'Gazipur, Bangladesh',
        'Dhaka',
        'Gazipur',
        'Islam',
        'Managing Director',
        10000000018.12
    ),
    (
        'Mr. CCC',
        '1998-01-01',
        '2026-02-02',
        '01912345678',
        'Chattogram, Bangladesh',
        'Chattogram',
        'Chattogram',
        'Islam',
        'Director',
        10000000018.12
    );
END
GO