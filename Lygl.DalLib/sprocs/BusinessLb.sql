/****** Object:  StoredProcedure [dbo].[GetBusinessLbByMxID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessLbByMxID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessLbByMxID]
GO

CREATE PROCEDURE [dbo].[GetBusinessLbByMxID]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessLb from table */
        SELECT
            [BusinessLb].[BusinessID],
            [BusinessLb].[BusinessName],
            [BusinessLb].[MxID],
            [BusinessLb].[OperatorID],
            [BusinessLb].[Drawee],
            [BusinessLb].[Price],
            [BusinessLb].[PayFlag],
            [BusinessLb].[OperateTime],
            [BusinessLb].[LbrText],
            [BusinessLb].[LbsjText],
            [BusinessLb].[Bx],
            [BusinessLb].[LbDate],
            [BusinessLb].[KzSj],
            [BusinessLb].[SgSj],
            [BusinessLb].[Bw],
            [BusinessLb].[Kzg],
            [BusinessLb].[Sgy]
        FROM [dbo].[BusinessLb]
        WHERE
            [BusinessLb].[MxID] = @MxID

        /* Get LbItem from table */
        SELECT
            [LbItem].[LbItemID],
            [LbItem].[Name],
            [LbItem].[UnitPrice],
            [LbItem].[Unit],
            [LbItem].[SubTotal],
            [LbItem].[Quantity]
        FROM [dbo].[LbItem]
            INNER JOIN [dbo].[BusinessLb] ON [LbItem].[BusinessID] = [BusinessLb].[BusinessID]
        WHERE
            [BusinessLb].[MxID] = @MxID

        /* Get BwSz from table */
        SELECT
            [BwSz].[BwSzID],
            [BwSz].[Ch],
            [BwSz].[Sheng],
            [BwSz].[Gu]
        FROM [dbo].[BwSz]
            INNER JOIN [dbo].[BusinessLb] ON [BwSz].[BusinessID] = [BusinessLb].[BusinessID]
        WHERE
            [BusinessLb].[MxID] = @MxID

    END
GO

/****** Object:  StoredProcedure [dbo].[AddBusinessLb] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBusinessLb]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddBusinessLb]
GO

CREATE PROCEDURE [dbo].[AddBusinessLb]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @MxID uniqueidentifier,
    @OperatorID uniqueidentifier,
    @Drawee nvarchar(20),
    @Price money,
    @PayFlag bit,
    @OperateTime datetime,
    @LbrText nvarchar(50),
    @LbsjText nvarchar(50),
    @Bx nvarchar(10),
    @LbDate datetime,
    @KzSj datetime,
    @SgSj datetime,
    @Bw binary(50),
    @Kzg nvarchar(20),
    @Sgy nvarchar(20)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.BusinessLb */
        INSERT INTO [dbo].[BusinessLb]
        (
            [BusinessID],
            [BusinessName],
            [MxID],
            [OperatorID],
            [Drawee],
            [Price],
            [PayFlag],
            [OperateTime],
            [LbrText],
            [LbsjText],
            [Bx],
            [LbDate],
            [KzSj],
            [SgSj],
            [Bw],
            [Kzg],
            [Sgy]
        )
        VALUES
        (
            @BusinessID,
            @BusinessName,
            @MxID,
            @OperatorID,
            @Drawee,
            @Price,
            @PayFlag,
            @OperateTime,
            @LbrText,
            @LbsjText,
            @Bx,
            @LbDate,
            @KzSj,
            @SgSj,
            @Bw,
            @Kzg,
            @Sgy
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateBusinessLb] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBusinessLb]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateBusinessLb]
GO

CREATE PROCEDURE [dbo].[UpdateBusinessLb]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @MxID uniqueidentifier,
    @OperatorID uniqueidentifier,
    @Drawee nvarchar(20),
    @Price money,
    @PayFlag bit,
    @OperateTime datetime,
    @LbrText nvarchar(50),
    @LbsjText nvarchar(50),
    @Bx nvarchar(10),
    @LbDate datetime,
    @KzSj datetime,
    @SgSj datetime,
    @Bw binary(50),
    @Kzg nvarchar(20),
    @Sgy nvarchar(20)
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessLb]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessLb'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.BusinessLb */
        UPDATE [dbo].[BusinessLb]
        SET
            [BusinessName] = @BusinessName,
            [MxID] = @MxID,
            [OperatorID] = @OperatorID,
            [Drawee] = @Drawee,
            [Price] = @Price,
            [PayFlag] = @PayFlag,
            [OperateTime] = @OperateTime,
            [LbrText] = @LbrText,
            [LbsjText] = @LbsjText,
            [Bx] = @Bx,
            [LbDate] = @LbDate,
            [KzSj] = @KzSj,
            [SgSj] = @SgSj,
            [Bw] = @Bw,
            [Kzg] = @Kzg,
            [Sgy] = @Sgy
        WHERE
            [BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteBusinessLb] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteBusinessLb]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteBusinessLb]
GO

CREATE PROCEDURE [dbo].[DeleteBusinessLb]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessLb]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessLb'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete child BwSz from BwSz */
        DELETE
            [dbo].[BwSz]
        FROM [dbo].[BwSz]
            INNER JOIN [dbo].[BusinessLb] ON [BwSz].[BusinessID] = [BusinessLb].[BusinessID]
        WHERE
            [dbo].[BusinessLb].[BusinessID] = @BusinessID

        /* Delete child LbItem from LbItem */
        DELETE
            [dbo].[LbItem]
        FROM [dbo].[LbItem]
            INNER JOIN [dbo].[BusinessLb] ON [LbItem].[BusinessID] = [BusinessLb].[BusinessID]
        WHERE
            [dbo].[BusinessLb].[BusinessID] = @BusinessID

        /* Delete BusinessLb object from BusinessLb */
        DELETE
        FROM [dbo].[BusinessLb]
        WHERE
            [dbo].[BusinessLb].[BusinessID] = @BusinessID

    END
GO
