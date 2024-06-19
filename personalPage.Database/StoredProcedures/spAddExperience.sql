CREATE PROCEDURE spAddExperience
    @Company NVARCHAR(100),
    @Title NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @StartDate DATE,
    @EndDate DATE NULL
AS
BEGIN
    INSERT INTO Experiences (Company, Title, Description, StartDate, EndDate)
    VALUES (@Company, @Title, @Description, @StartDate, @EndDate);
END
