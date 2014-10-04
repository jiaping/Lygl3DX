/****** Object:  StoredProcedure [dbo].[GetAreaEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAreaEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetAreaEdit]
GO

CREATE PROCEDURE [dbo].[GetAreaEdit]
    @AreaID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get AreaEdit from table */
        SELECT
            [Area].[AreaID],
            [Area].[Name],
            [Area].[GeometryText],
            [Area].[Angle]
        FROM [dbo].[Area]
        WHERE
            [Area].[AreaID] = @AreaID

    END
GO

/****** Object:  StoredProcedure [dbo].[AddAreaEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddAreaEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddAreaEdit]
GO

CREATE PROCEDURE [dbo].[AddAreaEdit]
    @AreaID uniqueidentifier,
    @Name varchar(20),
    @GeometryText varchar(MAX),
    @Angle int
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Area */
        INSERT INTO [dbo].[Area]
        (
            [AreaID],
            [Name],
            [GeometryText],
            [Angle]
        )
        VALUES
        (
            @AreaID,
            @Name,
            @GeometryText,
            @Angle
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateAreaEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateAreaEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateAreaEdit]
GO

CREATE PROCEDURE [dbo].[UpdateAreaEdit]
    @AreaID uniqueidentifier,
    @Name varchar(20),
    @GeometryText varchar(MAX),
    @Angle int
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [AreaID] FROM [dbo].[Area]
            WHERE
                [AreaID] = @AreaID
        )
        BEGIN
            RAISERROR ('''dbo.AreaEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Area */
        UPDATE [dbo].[Area]
        SET
            [Name] = @Name,
            [GeometryText] = @GeometryText,
            [Angle] = @Angle
        WHERE
            [AreaID] = @AreaID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteAreaEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteAreaEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteAreaEdit]
GO

CREATE PROCEDURE [dbo].[DeleteAreaEdit]
    @AreaID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [AreaID] FROM [dbo].[Area]
            WHERE
                [AreaID] = @AreaID
        )
        BEGIN
            RAISERROR ('''dbo.AreaEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete AreaEdit object from Area */
        DELETE
        FROM [dbo].[Area]
        WHERE
            [dbo].[Area].[AreaID] = @AreaID

    END
GO
