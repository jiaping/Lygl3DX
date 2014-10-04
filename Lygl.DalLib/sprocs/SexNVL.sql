/****** Object:  StoredProcedure [dbo].[GetSexNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSexNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetSexNVL]
GO

CREATE PROCEDURE [dbo].[GetSexNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get SexNVL from table */
        SELECT
            [Sex].[SexID],
            [Sex].[SexName]
        FROM [dbo].[Sex]

    END
GO

