/****** Object:  StoredProcedure [dbo].[GetPathEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPathEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetPathEdit]
GO

CREATE PROCEDURE [dbo].[GetPathEdit]
    @PathID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get PathEdit from table */
        SELECT
            [Path].[PathID],
            [Path].[Name],
            [Path].[GeometryText]
        FROM [dbo].[Path]
        WHERE
            [Path].[PathID] = @PathID

    END
GO

/****** Object:  StoredProcedure [dbo].[AddPathEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPathEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddPathEdit]
GO

CREATE PROCEDURE [dbo].[AddPathEdit]
    @PathID uniqueidentifier,
    @Name varchar(20),
    @GeometryText varchar(MAX)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Path */
        INSERT INTO [dbo].[Path]
        (
            [PathID],
            [Name],
            [GeometryText]
        )
        VALUES
        (
            @PathID,
            @Name,
            @GeometryText
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdatePathEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatePathEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdatePathEdit]
GO

CREATE PROCEDURE [dbo].[UpdatePathEdit]
    @PathID uniqueidentifier,
    @Name varchar(20),
    @GeometryText varchar(MAX)
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [PathID] FROM [dbo].[Path]
            WHERE
                [PathID] = @PathID
        )
        BEGIN
            RAISERROR ('''dbo.PathEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Path */
        UPDATE [dbo].[Path]
        SET
            [Name] = @Name,
            [GeometryText] = @GeometryText
        WHERE
            [PathID] = @PathID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeletePathEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeletePathEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeletePathEdit]
GO

CREATE PROCEDURE [dbo].[DeletePathEdit]
    @PathID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [PathID] FROM [dbo].[Path]
            WHERE
                [PathID] = @PathID
        )
        BEGIN
            RAISERROR ('''dbo.PathEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete PathEdit object from Path */
        DELETE
        FROM [dbo].[Path]
        WHERE
            [dbo].[Path].[PathID] = @PathID

    END
GO
