/****** Object:  StoredProcedure [dbo].[GetBusinessGm] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessGm]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessGm]
GO

CREATE PROCEDURE [dbo].[GetBusinessGm]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessGm from table */
        SELECT
            [BusinessGm].[BusinessName],
            [BusinessGm].[PaymentPay],
            [BusinessGm].[Price],
            [BusinessGm].[MxID],
            [BusinessGm].[Drawee],
            [BusinessGm].[OperatorID],
            [BusinessGm].[PayFlag],
            [BusinessGm].[BusinessID],
            [BusinessGm].[OperateTime],
            [BusinessGm].[GmDate]
        FROM [dbo].[BusinessGm]
        WHERE
            [BusinessGm].[BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetBusinessGmByMx] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessGmByMx]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessGmByMx]
GO

CREATE PROCEDURE [dbo].[GetBusinessGmByMx]
    @MxID uniqueidentifier,
    @MxIDFlag uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessGm from table */
        SELECT
            [BusinessGm].[BusinessName],
            [BusinessGm].[PaymentPay],
            [BusinessGm].[Price],
            [BusinessGm].[MxID],
            [BusinessGm].[Drawee],
            [BusinessGm].[OperatorID],
            [BusinessGm].[PayFlag],
            [BusinessGm].[BusinessID],
            [BusinessGm].[OperateTime],
            [BusinessGm].[GmDate]
        FROM [dbo].[BusinessGm]
        WHERE
            [BusinessGm].[MxID] = @MxID AND
            [BusinessGm].[MxID] = @MxIDFlag

    END
GO

/****** Object:  StoredProcedure [dbo].[AddBusinessGm] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBusinessGm]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddBusinessGm]
GO

CREATE PROCEDURE [dbo].[AddBusinessGm]
    @BusinessName nvarchar(20),
    @PaymentPay money,
    @Price money,
    @MxID uniqueidentifier,
    @Drawee nvarchar(20),
    @OperatorID uniqueidentifier,
    @PayFlag bit,
    @BusinessID uniqueidentifier,
    @OperateTime datetime,
    @GmDate datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.BusinessGm */
        INSERT INTO [dbo].[BusinessGm]
        (
            [BusinessName],
            [PaymentPay],
            [Price],
            [MxID],
            [Drawee],
            [OperatorID],
            [PayFlag],
            [BusinessID],
            [OperateTime],
            [GmDate]
        )
        VALUES
        (
            @BusinessName,
            @PaymentPay,
            @Price,
            @MxID,
            @Drawee,
            @OperatorID,
            @PayFlag,
            @BusinessID,
            @OperateTime,
            @GmDate
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateBusinessGm] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBusinessGm]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateBusinessGm]
GO

CREATE PROCEDURE [dbo].[UpdateBusinessGm]
    @BusinessName nvarchar(20),
    @PaymentPay money,
    @Price money,
    @MxID uniqueidentifier,
    @Drawee nvarchar(20),
    @OperatorID uniqueidentifier,
    @PayFlag bit,
    @BusinessID uniqueidentifier,
    @OperateTime datetime,
    @GmDate datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessGm]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessGm'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.BusinessGm */
        UPDATE [dbo].[BusinessGm]
        SET
            [BusinessName] = @BusinessName,
            [PaymentPay] = @PaymentPay,
            [Price] = @Price,
            [MxID] = @MxID,
            [Drawee] = @Drawee,
            [OperatorID] = @OperatorID,
            [PayFlag] = @PayFlag,
            [OperateTime] = @OperateTime,
            [GmDate] = @GmDate
        WHERE
            [BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteBusinessGm] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteBusinessGm]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteBusinessGm]
GO

CREATE PROCEDURE [dbo].[DeleteBusinessGm]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessGm]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessGm'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete BusinessGm object from BusinessGm */
        DELETE
        FROM [dbo].[BusinessGm]
        WHERE
            [dbo].[BusinessGm].[BusinessID] = @BusinessID

    END
GO
