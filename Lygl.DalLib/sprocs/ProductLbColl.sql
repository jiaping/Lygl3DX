/****** Object:  StoredProcedure [dbo].[GetProductLbColl] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProductLbColl]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetProductLbColl]
GO

CREATE PROCEDURE [dbo].[GetProductLbColl]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get ProductLb from table */
        SELECT
            RTRIM([ProductLB].[ItemID]) AS [ItemID],
            RTRIM([ProductLB].[Name]) AS [Name],
            [ProductLB].[UnitPrice],
            [ProductLB].[Unit]
        FROM [dbo].[ProductLB]

    END
GO

