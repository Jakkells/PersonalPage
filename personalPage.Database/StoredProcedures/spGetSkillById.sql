CREATE PROCEDURE spGetSkillById
    @Id INT
AS
BEGIN
    SELECT Id, Skill, Qualification, Description
    FROM Skills
    WHERE Id = @Id;
END
GO
