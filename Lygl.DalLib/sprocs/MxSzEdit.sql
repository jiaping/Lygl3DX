/****** Object:  StoredProcedure [dbo].[AddMxSzEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddMxSzEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddMxSzEdit]
GO

CREATE PROCEDURE [dbo].[AddMxSzEdit]
    @SzID uniqueidentifier,
    @Name nvarchar(10),
    @Sex nvarchar(10),
    @MxID uniqueidentifier,
    @RmDate datetime,
    @Age int,
    @OperatorID uniqueidentifier,
    @OperateTime datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Sz */
        INSERT INTO [dbo].[Sz]
        (
            [SzID],
            [Name],
            [Sex],
            [MxID],
            [RmDate],
            [Age],
            [OperatorID],
            [OperateTime]
        )
        VALUES
        (
            @SzID,
            @Name,
            @Sex,
            @MxID,
            @RmDate,
            @Age,
            @OperatorID,
            @OperateTime
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateMxSzEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateMxSzEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateMxSzEdit]
GO

CREATE PROCEDURE [dbo].[UpdateMxSzEdit]
    @SzID uniqueidentifier,
    @Name nvarchar(10),
    @Sex nvarchar(10),
    @MxID uniqueidentifier,
    @RmDate datetime,
    @Age int,
    @OperatorID uniqueidentifier,
    @OperateTime datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [SzID] FROM [dbo].[Sz]
            WHERE
                [SzID] = @SzID
        )
        BEGIN
            RAISERROR ('''dbo.MxSzEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Sz */
        UPDATE [dbo].[Sz]
        SET
            [Name] = @Name,
            [Sex] = @Sex,
            [MxID] = @MxID,
            [RmDate] = @RmDate,
            [Age] = @Age,
            [OperatorID] = @OperatorID,
            [OperateTime] = @OperateTime
        WHERE
            [SzID] = @SzID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteMxSzEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteMxSzEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteMxSzEdit]
GO

CREATE PROCEDURE [dbo].[DeleteMxSzEdit]
    @SzID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [SzID] FROM [dbo].[Sz]
            WHERE
                [SzID] = @SzID
        )
        BEGIN
            RAISERROR ('''dbo.MxSzEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete MxSzEdit object from Sz */
        DELETE
        FROM [dbo].[Sz]
        WHERE
            [dbo].[Sz].[SzID] = @SzID

    END
GO
