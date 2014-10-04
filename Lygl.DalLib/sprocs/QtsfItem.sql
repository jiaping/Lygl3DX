/****** Object:  StoredProcedure [dbo].[AddQtsfItem] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddQtsfItem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddQtsfItem]
GO

CREATE PROCEDURE [dbo].[AddQtsfItem]
    @QtsfItemID uniqueidentifier,
    @UnitPrice money,
    @Unit nvarchar(4),
    @Quantity int,
    @BusinessID uniqueidentifier,
    @SubTotal money,
    @Name nvarchar(10)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.QtsfItem */
        INSERT INTO [dbo].[QtsfItem]
        (
            [QtsfItemID],
            [UnitPrice],
            [Unit],
            [Quantity],
            [BusinessID],
            [SubTotal],
            [Name]
        )
        VALUES
        (
            @QtsfItemID,
            @UnitPrice,
            @Unit,
            @Quantity,
            @BusinessID,
            @SubTotal,
            @Name
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateQtsfItem] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateQtsfItem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateQtsfItem]
GO

CREATE PROCEDURE [dbo].[UpdateQtsfItem]
    @QtsfItemID uniqueidentifier,
    @UnitPrice money,
    @Unit nvarchar(4),
    @Quantity int,
    @BusinessID uniqueidentifier,
    @SubTotal money,
    @Name nvarchar(10)
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [QtsfItemID] FROM [dbo].[QtsfItem]
            WHERE
                [QtsfItemID] = @QtsfItemID
        )
        BEGIN
            RAISERROR ('''dbo.QtsfItem'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.QtsfItem */
        UPDATE [dbo].[QtsfItem]
        SET
            [UnitPrice] = @UnitPrice,
            [Unit] = @Unit,
            [Quantity] = @Quantity,
            [BusinessID] = @BusinessID,
            [SubTotal] = @SubTotal,
            [Name] = @Name
        WHERE
            [QtsfItemID] = @QtsfItemID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteQtsfItem] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteQtsfItem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteQtsfItem]
GO

CREATE PROCEDURE [dbo].[DeleteQtsfItem]
    @QtsfItemID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [QtsfItemID] FROM [dbo].[QtsfItem]
            WHERE
                [QtsfItemID] = @QtsfItemID
        )
        BEGIN
            RAISERROR ('''dbo.QtsfItem'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete QtsfItem object from QtsfItem */
        DELETE
        FROM [dbo].[QtsfItem]
        WHERE
            [dbo].[QtsfItem].[QtsfItemID] = @QtsfItemID

    END
GO
