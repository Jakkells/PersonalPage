CREATE PROCEDURE spAddEducation
    @University NVARCHAR(100),
    @Qualification NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    INSERT INTO Education (University, Qualification, Description, StartDate, EndDate)
    VALUES (@University, @Qualification, @Description, @StartDate, @EndDate);
END
