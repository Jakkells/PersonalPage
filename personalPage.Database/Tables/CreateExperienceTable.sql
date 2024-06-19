CREATE TABLE Experiences (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Company NVARCHAR(100),
    Title NVARCHAR(100),
    Description NVARCHAR(MAX),
    StartDate DATE,
    EndDate DATE NULL
);