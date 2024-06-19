CREATE PROCEDURE dbo.spGetEducationById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, University, Qualification, Description, StartDate, EndDate
    FROM dbo.Education
    WHERE Id = @Id;
END