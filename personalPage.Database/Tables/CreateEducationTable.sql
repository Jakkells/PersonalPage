CREATE TABLE Education (
    Id INT PRIMARY KEY IDENTITY,
    University NVARCHAR(100) NULL,
    Qualification NVARCHAR(100) NULL,
    Description NVARCHAR(MAX) NULL,
    StartDate DATETIME NULL,
    EndDate DATETIME NULL
);