/****** Object:  StoredProcedure [dbo].[GetMxXsNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxXsNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxXsNVL]
GO

CREATE PROCEDURE [dbo].[GetMxXsNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxXsNVL from table */
        SELECT
            [MxType].[MxTypeID],
            [MxType].[MxXs]
        FROM [dbo].[MxType]

    END
GO

