SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:              Vitaly Vengrov
-- Create date: 2007/08/08
-- Description: Copyes Document and his related Participants and Tracts
-- =============================================
ALTER PROCEDURE sp_CopyDocument
    @docId int, @userId int
AS
BEGIN
        SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRAN

        declare @copiedDocId int, @tractId int, @newTractId int

        UPDATE DOCUMENT 
            SET IsActive = 0 
         WHERE docId = @docId

        INSERT INTO [Document]
                   ([IsPublic],[DocTypeId],[Volume],[Page],[DocumentNo],[County],[State],[DateFiledYear]
                   ,[DateFiledMonth],[DateFiledDay],[DateSignedYear],[DateSignedMonth],[DateSignedDay]
                   ,[ResearchNote],[ImageLink],[CreatedBy],[DateModified],[IsActive],[DocBranchUid])
        SELECT [IsPublic],[DocTypeId],[Volume],[Page],[DocumentNo],[County],[State],[DateFiledYear]
                   ,[DateFiledMonth],[DateFiledDay],[DateSignedYear],[DateSignedMonth],[DateSignedDay]
                   ,[ResearchNote],[ImageLink],[CreatedBy], GetDate(), 1, [DocBranchUid]
          FROM [Document]
         WHERE [DocId] = @docId

        SELECT @copiedDocId = @@IDENTITY

        INSERT INTO [Participant]
                   ([DocID],[AsNamed],[FirstName],[MiddleName],[LastName],[IsSeller])
        SELECT @copiedDocId,[AsNamed],[FirstName],[MiddleName],[LastName],[IsSeller]
          FROM [Participant]
         WHERE [DocId] = @docId

        DECLARE tract_cursor cursor FORWARD_ONLY FOR (
            SELECT [TractId]
              FROM [Tract]
             WHERE [DocId] = @docId
        )

        OPEN tract_cursor
        FETCH NEXT FROM tract_cursor into @tractId

        WHILE @@FETCH_STATUS = 0
        BEGIN

            INSERT INTO [Tract] ([Easting],[Northing],[RefName],[CreatedBy],[IsDeleted],[DocID],[CalledAC],[UnitId])
            SELECT [Easting],[Northing],[RefName],[CreatedBy],[IsDeleted],@copiedDocId,[CalledAC],[UnitId]
              FROM [Tract]
             WHERE TractId = @tractId

            SET @newTractId = @@IDENTITY

            INSERT INTO [TractCalls] ([TractId], CallType, CallDBValue, CallOrder, CreatedByMouse)
            SELECT @newTractId, CallType, CallDBValue, CallOrder, CreatedByMouse
              FROM [TractCalls] where TractId = @tractId

            INSERT INTO [TractTextObjects] ([TractId], [Text], [Easting], [Northing], [Rotation])
            SELECT @newTractId, [Text], [Easting], [Northing], [Rotation]
              FROM [TractTextObjects] where TractId = @tractId

            FETCH NEXT FROM tract_cursor into @TractId
        END

        CLOSE tract_cursor
        DEALLOCATE tract_cursor

        COMMIT TRAN
        RETURN @copiedDocId
    END TRY
    BEGIN CATCH

        -- Print error information. 
        PRINT 'Error ' + CONVERT(varchar(50), ERROR_NUMBER()) +
              ', Severity ' + CONVERT(varchar(5), ERROR_SEVERITY()) +
              ', State ' + CONVERT(varchar(5), ERROR_STATE()) + 
              ', Procedure ' + ISNULL(ERROR_PROCEDURE(), '-') + 
              ', Line ' + CONVERT(varchar(5), ERROR_LINE());
        PRINT ERROR_MESSAGE();

        IF XACT_STATE() <> 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

    END CATCH

END
GO
