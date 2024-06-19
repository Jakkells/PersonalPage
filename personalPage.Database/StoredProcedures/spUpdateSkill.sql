CREATE PROCEDURE spUpdateSkill
    @Id INT,
    @Skill NVARCHAR(100),
    @Qualification NVARCHAR(100),
    @Description NVARCHAR(MAX)
AS
BEGIN
    UPDATE Skills
    SET Skill = @Skill,
        Qualification = @Qualification,
        Description = @Description
    WHERE Id = @Id;
END
GO
