/****** Object:  StoredProcedure [dbo].[AddBusinessDd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBusinessDd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddBusinessDd]
GO

CREATE PROCEDURE [dbo].[AddBusinessDd]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @StartDate datetime,
    @EndDate datetime,
    @MxID uniqueidentifier,
    @OperatorID uniqueidentifier,
    @Drawee nvarchar(20),
    @Price money,
    @PayFlag bit,
    @OperateTime datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.BusinessDd */
        INSERT INTO [dbo].[BusinessDd]
        (
            [BusinessID],
            [BusinessName],
            [StartDate],
            [EndDate],
            [MxID],
            [OperatorID],
            [Drawee],
            [Price],
            [PayFlag],
            [OperateTime]
        )
        VALUES
        (
            @BusinessID,
            @BusinessName,
            @StartDate,
            @EndDate,
            @MxID,
            @OperatorID,
            @Drawee,
            @Price,
            @PayFlag,
            @OperateTime
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateBusinessDd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBusinessDd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateBusinessDd]
GO

CREATE PROCEDURE [dbo].[UpdateBusinessDd]
    @BusinessID uniqueidentifier,
    @BusinessName nvarchar(20),
    @StartDate datetime,
    @EndDate datetime,
    @MxID uniqueidentifier,
    @OperatorID uniqueidentifier,
    @Drawee nvarchar(20),
    @Price money,
    @PayFlag bit,
    @OperateTime datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessDd]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessDd'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.BusinessDd */
        UPDATE [dbo].[BusinessDd]
        SET
            [BusinessName] = @BusinessName,
            [StartDate] = @StartDate,
            [EndDate] = @EndDate,
            [MxID] = @MxID,
            [OperatorID] = @OperatorID,
            [Drawee] = @Drawee,
            [Price] = @Price,
            [PayFlag] = @PayFlag,
            [OperateTime] = @OperateTime
        WHERE
            [BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteBusinessDd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteBusinessDd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteBusinessDd]
GO

CREATE PROCEDURE [dbo].[DeleteBusinessDd]
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BusinessID] FROM [dbo].[BusinessDd]
            WHERE
                [BusinessID] = @BusinessID
        )
        BEGIN
            RAISERROR ('''dbo.BusinessDd'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete BusinessDd object from BusinessDd */
        DELETE
        FROM [dbo].[BusinessDd]
        WHERE
            [dbo].[BusinessDd].[BusinessID] = @BusinessID

    END
GO
