/****** Object:  StoredProcedure [dbo].[GetMxStyleNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxStyleNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxStyleNVL]
GO

CREATE PROCEDURE [dbo].[GetMxStyleNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxStyleNVL from table */
        SELECT
            [MxStyle].[MxStyleID],
            [MxStyle].[MxStyleName]
        FROM [dbo].[MxStyle]

    END
GO

