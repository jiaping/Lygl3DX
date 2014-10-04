/****** Object:  StoredProcedure [dbo].[GetBusinessYd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessYd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessYd]
GO

CREATE PROCEDURE [dbo].[GetBusinessYd]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessYd from table */
        SELECT
            [BusinessYd].[BusinessID],
            [BusinessYd].[BusinessName],
            [BusinessYd].[DownPayment],
            [BusinessYd].[Price],
            [BusinessYd].[MxID],
            [BusinessYd].[Drawee],
            [BusinessYd].[OperatorID],
            [BusinessYd].[PayFlag],
            [BusinessYd].[OperateTime],
            [BusinessYd].[YDDate],
            [BusinessYd].[Syz]
        FROM [dbo].[BusinessYd]
        WHERE
            [BusinessYd].[BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetBusinessYdByMxID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessYdByMxID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessYdByMxID]
GO

CREATE PROCEDURE [dbo].[GetBusinessYdByMxID]
    @MxID uniqueidentifier,
    @MxIDFlag uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessYd from table */
        SELECT
            [BusinessYd].[BusinessID],
            [BusinessYd].[BusinessName],
            [BusinessYd].[DownPayment],
            [BusinessYd].[Price],
            [BusinessYd].[MxID],
            [BusinessYd].[Drawee],
            [BusinessYd].[OperatorID],
            [BusinessYd].[PayFlag],
            [BusinessYd].[OperateTime],
            [BusinessYd].[YDDate],
            [BusinessYd].[Syz]
        FROM [dbo].[BusinessYd]
        WHERE
            [BusinessYd].[MxID] = @MxID AND
            [BusinessYd].[MxID] = @MxIDFlag

    END
GO

/****** Object:  StoredProcedure [dbo].[AddBusinessYd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBusinessYd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddBusinessYd]
GO

CREATE PROCEDURE [dbo].[AddBusinessYd]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @DownPayment money,
    @Price money,
    @MxID uniqueidentifier,
    @Drawee nvarchar(20),
    @OperatorID uniqueidentifier,
    @PayFlag bit,
    @OperateTime datetime,
    @YDDate datetime,
    @Syz nvarchar(20)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.BusinessYd */
        INSERT INTO [dbo].[BusinessYd]
        (
            [BusinessID],
            [BusinessName],
            [DownPayment],
            [Price],
            [MxID],
            [Drawee],
            [OperatorID],
            [PayFlag],
            [OperateTime],
            [YDDate],
            [Syz]
        )
        VALUES
        (
            @BusinessID,
            @BusinessName,
            @DownPayment,
            @Price,
            @MxID,
            @Drawee,
            @OperatorID,
            @PayFlag,
            @OperateTime,
            @YDDate,
            @Syz
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateBusinessYd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBusinessYd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateBusinessYd]
GO

CREATE PROCEDURE [dbo].[UpdateBusinessYd]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @DownPayment money,
    @Price money,
    @MxID uniqueidentifier,
    @Drawee nvarchar(20),
    @OperatorID uniqueidentifier,
    @PayFlag bit,
    @OperateTime datetime,
    @YDDate datetime,
    @Syz nvarchar(20)
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessYd]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessYd'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.BusinessYd */
        UPDATE [dbo].[BusinessYd]
        SET
            [BusinessName] = @BusinessName,
            [DownPayment] = @DownPayment,
            [Price] = @Price,
            [MxID] = @MxID,
            [Drawee] = @Drawee,
            [OperatorID] = @OperatorID,
            [PayFlag] = @PayFlag,
            [OperateTime] = @OperateTime,
            [YDDate] = @YDDate,
            [Syz] = @Syz
        WHERE
            [BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteBusinessYd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteBusinessYd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteBusinessYd]
GO

CREATE PROCEDURE [dbo].[DeleteBusinessYd]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessYd]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessYd'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete BusinessYd object from BusinessYd */
        DELETE
        FROM [dbo].[BusinessYd]
        WHERE
            [dbo].[BusinessYd].[BusinessID] = @BusinessID

    END
GO
