CREATE PROCEDURE dbo.spGetAllEducation
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Education;
END