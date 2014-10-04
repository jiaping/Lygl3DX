/****** Object:  StoredProcedure [dbo].[GetProductQtsfColl] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProductQtsfColl]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetProductQtsfColl]
GO

CREATE PROCEDURE [dbo].[GetProductQtsfColl]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get productQtsf from table */
        SELECT
            RTRIM([ProductQtsf].[ItemID]) AS [ItemID],
            [ProductQtsf].[Name],
            [ProductQtsf].[UnitPrice],
            [ProductQtsf].[Unit]
        FROM [dbo].[ProductQtsf]

    END
GO

