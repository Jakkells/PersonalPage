CREATE PROCEDURE dbo.spGetAllSkills
AS
BEGIN
    SELECT Id, Skill, Qualification, Description
    FROM dbo.Skills;
END
GO
