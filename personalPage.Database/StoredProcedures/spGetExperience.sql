CREATE PROCEDURE dbo.spGetExperiences
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Experiences;
END