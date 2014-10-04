/****** Object:  StoredProcedure [dbo].[GetMxTypeNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxTypeNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxTypeNVL]
GO

CREATE PROCEDURE [dbo].[GetMxTypeNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxTypeNVL from table */
        SELECT
            [MxType].[MxTypeID],
            [MxType].[MxTypeName]
        FROM [dbo].[MxType]

    END
GO

