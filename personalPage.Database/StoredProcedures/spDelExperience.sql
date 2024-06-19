CREATE PROCEDURE dbo.spDelExperience
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Experiences WHERE Id = @Id;
END
GO
