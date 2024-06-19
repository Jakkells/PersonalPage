CREATE PROCEDURE dbo.spDelEducation
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Education WHERE Id = @Id;
END
GO