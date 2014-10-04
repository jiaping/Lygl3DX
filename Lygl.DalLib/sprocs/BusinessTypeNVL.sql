/****** Object:  StoredProcedure [dbo].[GetBusinessTypeNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessTypeNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessTypeNVL]
GO

CREATE PROCEDURE [dbo].[GetBusinessTypeNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessTypeNVL from table */
        SELECT
            [BusinessType].[BusinessTypeID],
            [BusinessType].[BusinessTypeName]
        FROM [dbo].[BusinessType]

    END
GO

