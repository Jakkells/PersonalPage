CREATE PROCEDURE spUpdateEducation
    @Id            INT,
    @University    NVARCHAR(100),
    @Qualification NVARCHAR(100),
    @Description   NVARCHAR(MAX),
    @StartDate     DATETIME,
    @EndDate       DATETIME
AS
BEGIN
    UPDATE Education
    SET University    = @University,
        Qualification = @Qualification,
        Description   = @Description,
        StartDate     = @StartDate,
        EndDate       = @EndDate
    WHERE Id          = @Id;
END