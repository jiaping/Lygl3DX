/****** Object:  StoredProcedure [dbo].[GetQtsfItemList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetQtsfItemList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetQtsfItemList]
GO

CREATE PROCEDURE [dbo].[GetQtsfItemList]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get QtsfItem from table */
        SELECT
            [QtsfItem].[QtsfItemID],
            [QtsfItem].[UnitPrice],
            [QtsfItem].[Unit],
            [QtsfItem].[Quantity],
            [QtsfItem].[BusinessID],
            [QtsfItem].[SubTotal],
            [QtsfItem].[Name]
        FROM [dbo].[QtsfItem]

    END
GO

/****** Object:  StoredProcedure [dbo].[GetQtsfItemListByBusinessID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetQtsfItemListByBusinessID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetQtsfItemListByBusinessID]
GO

CREATE PROCEDURE [dbo].[GetQtsfItemListByBusinessID]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get QtsfItem from table */
        SELECT
            [QtsfItem].[QtsfItemID],
            [QtsfItem].[UnitPrice],
            [QtsfItem].[Unit],
            [QtsfItem].[Quantity],
            [QtsfItem].[BusinessID],
            [QtsfItem].[SubTotal],
            [QtsfItem].[Name]
        FROM [dbo].[QtsfItem]
        WHERE
            [QtsfItem].[BusinessID] = @BusinessID

    END
GO

