/****** Object:  StoredProcedure [dbo].[AddBwSz] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBwSz]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddBwSz]
GO

CREATE PROCEDURE [dbo].[AddBwSz]
    @BusinessID uniqueidentifier,
    @BwSzID uniqueidentifier,
    @Ch nvarchar(20),
    @Sheng nvarchar(20),
    @Gu nvarchar(20)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.BwSz */
        INSERT INTO [dbo].[BwSz]
        (
            [BusinessID],
            [BwSzID],
            [Ch],
            [Sheng],
            [Gu]
        )
        VALUES
        (
            @BusinessID,
            @BwSzID,
            @Ch,
            @Sheng,
            @Gu
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateBwSz] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBwSz]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateBwSz]
GO

CREATE PROCEDURE [dbo].[UpdateBwSz]
    @BwSzID uniqueidentifier,
    @Ch nvarchar(20),
    @Sheng nvarchar(20),
    @Gu nvarchar(20)
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BwSzID] FROM [dbo].[BwSz]
            WHERE
                [BwSzID] = @BwSzID
        )
        BEGIN
            RAISERROR ('''dbo.BwSz'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.BwSz */
        UPDATE [dbo].[BwSz]
        SET
            [Ch] = @Ch,
            [Sheng] = @Sheng,
            [Gu] = @Gu
        WHERE
            [BwSzID] = @BwSzID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteBwSz] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteBwSz]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteBwSz]
GO

CREATE PROCEDURE [dbo].[DeleteBwSz]
    @BwSzID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [BwSzID] FROM [dbo].[BwSz]
            WHERE
                [BwSzID] = @BwSzID
        )
        BEGIN
            RAISERROR ('''dbo.BwSz'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete BwSz object from BwSz */
        DELETE
        FROM [dbo].[BwSz]
        WHERE
            [dbo].[BwSz].[BwSzID] = @BwSzID

    END
GO
