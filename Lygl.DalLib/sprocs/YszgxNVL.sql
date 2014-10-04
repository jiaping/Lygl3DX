/****** Object:  StoredProcedure [dbo].[GetYszgxNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetYszgxNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetYszgxNVL]
GO

CREATE PROCEDURE [dbo].[GetYszgxNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get YszgxNVL from table */
        SELECT
            [Yszgx].[Name],
            [Yszgx].[Value]
        FROM [dbo].[Yszgx]

    END
GO

