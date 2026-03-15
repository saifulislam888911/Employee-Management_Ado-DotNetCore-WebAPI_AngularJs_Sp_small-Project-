USE EmployeeManagementDB;
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