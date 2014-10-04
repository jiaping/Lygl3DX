/****** Object:  StoredProcedure [dbo].[AddBusinessQtsf] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBusinessQtsf]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddBusinessQtsf]
GO

CREATE PROCEDURE [dbo].[AddBusinessQtsf]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @MxID uniqueidentifier,
    @Price money,
    @OperateTime datetime,
    @Drawee nvarchar(20),
    @OperatorID uniqueidentifier,
    @PayFlag bit
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.BusinessQtsf */
        INSERT INTO [dbo].[BusinessQtsf]
        (
            [BusinessID],
            [BusinessName],
            [MxID],
            [Price],
            [OperateTime],
            [Drawee],
            [OperatorID],
            [PayFlag]
        )
        VALUES
        (
            @BusinessID,
            @BusinessName,
            @MxID,
            @Price,
            @OperateTime,
            @Drawee,
            @OperatorID,
            @PayFlag
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateBusinessQtsf] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBusinessQtsf]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateBusinessQtsf]
GO

CREATE PROCEDURE [dbo].[UpdateBusinessQtsf]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @MxID uniqueidentifier,
    @Price money,
    @OperateTime datetime,
    @Drawee nvarchar(20),
    @OperatorID uniqueidentifier,
    @PayFlag bit
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessQtsf]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessQtsf'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.BusinessQtsf */
        UPDATE [dbo].[BusinessQtsf]
        SET
            [BusinessName] = @BusinessName,
            [MxID] = @MxID,
            [Price] = @Price,
            [OperateTime] = @OperateTime,
            [Drawee] = @Drawee,
            [OperatorID] = @OperatorID,
            [PayFlag] = @PayFlag
        WHERE
            [BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteBusinessQtsf] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteBusinessQtsf]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteBusinessQtsf]
GO

CREATE PROCEDURE [dbo].[DeleteBusinessQtsf]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessQtsf]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessQtsf'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete child QtsfItem from QtsfItem */
        DELETE
            [dbo].[QtsfItem]
        FROM [dbo].[QtsfItem]
            INNER JOIN [dbo].[BusinessQtsf] ON [QtsfItem].[BusinessID] = [BusinessQtsf].[BusinessID]
        WHERE
            [dbo].[BusinessQtsf].[BusinessID] = @BusinessID

        /* Delete BusinessQtsf object from BusinessQtsf */
        DELETE
        FROM [dbo].[BusinessQtsf]
        WHERE
            [dbo].[BusinessQtsf].[BusinessID] = @BusinessID

    END
GO
