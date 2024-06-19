CREATE PROCEDURE dbo.spGetExperienceById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Company, Title, Description, StartDate, EndDate
    FROM dbo.Experiences
    WHERE Id = @Id;
END
