CREATE PROCEDURE spUpdateExperience
    @Id INT,
    @Company NVARCHAR(100),
    @Title NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @StartDate DATE,
    @EndDate DATE NULL
AS
BEGIN
    UPDATE Experiences
    SET Company = @Company,
        Title = @Title,
        Description = @Description,
        StartDate = @StartDate,
        EndDate = @EndDate
    WHERE Id = @Id;
END
