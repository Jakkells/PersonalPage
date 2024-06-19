CREATE PROCEDURE spAddSkill
    @Skill NVARCHAR(100),
    @Qualification NVARCHAR(100),
    @Description NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Skills (Skill, Qualification, Description)
    VALUES (@Skill, @Qualification, @Description);
END
GO
