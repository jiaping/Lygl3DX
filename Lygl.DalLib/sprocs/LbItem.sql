/****** Object:  StoredProcedure [dbo].[AddLbItem] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddLbItem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddLbItem]
GO

CREATE PROCEDURE [dbo].[AddLbItem]
    @BusinessID uniqueidentifier,
    @LbItemID uniqueidentifier,
    @Name nvarchar(10),
    @UnitPrice money,
    @Unit nvarchar(4),
    @SubTotal money,
    @Quantity int
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.LbItem */
        INSERT INTO [dbo].[LbItem]
        (
            [BusinessID],
            [LbItemID],
            [Name],
            [UnitPrice],
            [Unit],
            [SubTotal],
            [Quantity]
        )
        VALUES
        (
            @BusinessID,
            @LbItemID,
            @Name,
            @UnitPrice,
            @Unit,
            @SubTotal,
            @Quantity
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateLbItem] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateLbItem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateLbItem]
GO

CREATE PROCEDURE [dbo].[UpdateLbItem]
    @LbItemID uniqueidentifier,
    @Name nvarchar(10),
    @UnitPrice money,
    @Unit nvarchar(4),
    @SubTotal money,
    @Quantity int
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [LbItemID] FROM [dbo].[LbItem]
            WHERE
                [LbItemID] = @LbItemID
        )
        BEGIN
            RAISERROR ('''dbo.LbItem'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.LbItem */
        UPDATE [dbo].[LbItem]
        SET
            [Name] = @Name,
            [UnitPrice] = @UnitPrice,
            [Unit] = @Unit,
            [SubTotal] = @SubTotal,
            [Quantity] = @Quantity
        WHERE
            [LbItemID] = @LbItemID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteLbItem] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteLbItem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteLbItem]
GO

CREATE PROCEDURE [dbo].[DeleteLbItem]
    @LbItemID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [LbItemID] FROM [dbo].[LbItem]
            WHERE
                [LbItemID] = @LbItemID
        )
        BEGIN
            RAISERROR ('''dbo.LbItem'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete LbItem object from LbItem */
        DELETE
        FROM [dbo].[LbItem]
        WHERE
            [dbo].[LbItem].[LbItemID] = @LbItemID

    END
GO
