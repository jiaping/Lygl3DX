/****** Object:  StoredProcedure [dbo].[AddBusinessGlf] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBusinessGlf]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddBusinessGlf]
GO

CREATE PROCEDURE [dbo].[AddBusinessGlf]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @MxID uniqueidentifier,
    @Price money,
    @StartDate datetime,
    @EndDate datetime,
    @Drawee nvarchar(20),
    @OperatorID uniqueidentifier,
    @PayFlag bit,
    @OperateTime datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.BusinessGlf */
        INSERT INTO [dbo].[BusinessGlf]
        (
            [BusinessID],
            [BusinessName],
            [MxID],
            [Price],
            [StartDate],
            [EndDate],
            [Drawee],
            [OperatorID],
            [PayFlag],
            [OperateTime]
        )
        VALUES
        (
            @BusinessID,
            @BusinessName,
            @MxID,
            @Price,
            @StartDate,
            @EndDate,
            @Drawee,
            @OperatorID,
            @PayFlag,
            @OperateTime
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateBusinessGlf] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBusinessGlf]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateBusinessGlf]
GO

CREATE PROCEDURE [dbo].[UpdateBusinessGlf]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @MxID uniqueidentifier,
    @Price money,
    @StartDate datetime,
    @EndDate datetime,
    @Drawee nvarchar(20),
    @OperatorID uniqueidentifier,
    @PayFlag bit,
    @OperateTime datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessGlf]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessGlf'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.BusinessGlf */
        UPDATE [dbo].[BusinessGlf]
        SET
            [BusinessName] = @BusinessName,
            [MxID] = @MxID,
            [Price] = @Price,
            [StartDate] = @StartDate,
            [EndDate] = @EndDate,
            [Drawee] = @Drawee,
            [OperatorID] = @OperatorID,
            [PayFlag] = @PayFlag,
            [OperateTime] = @OperateTime
        WHERE
            [BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteBusinessGlf] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteBusinessGlf]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteBusinessGlf]
GO

CREATE PROCEDURE [dbo].[DeleteBusinessGlf]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessGlf]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessGlf'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete BusinessGlf object from BusinessGlf */
        DELETE
        FROM [dbo].[BusinessGlf]
        WHERE
            [dbo].[BusinessGlf].[BusinessID] = @BusinessID

    END
GO
