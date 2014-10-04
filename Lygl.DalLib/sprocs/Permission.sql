/****** Object:  StoredProcedure [dbo].[AddPermission] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPermission]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddPermission]
GO

CREATE PROCEDURE [dbo].[AddPermission]
    @Name varchar(30),
    @PermissionID uniqueidentifier,
    @Description nvarchar(100),
    @RoleID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Permission */
        INSERT INTO [dbo].[Permission]
        (
            [Name],
            [PermissionID],
            [Description],
            [RoleID]
        )
        VALUES
        (
            @Name,
            @PermissionID,
            @Description,
            @RoleID
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdatePermission] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatePermission]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdatePermission]
GO

CREATE PROCEDURE [dbo].[UpdatePermission]
    @Name varchar(30),
    @PermissionID uniqueidentifier,
    @Description nvarchar(100),
    @RoleID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [PermissionID] FROM [dbo].[Permission]
            WHERE
                [PermissionID] = @PermissionID
        )
        BEGIN
            RAISERROR ('''dbo.Permission'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Permission */
        UPDATE [dbo].[Permission]
        SET
            [Name] = @Name,
            [Description] = @Description,
            [RoleID] = @RoleID
        WHERE
            [PermissionID] = @PermissionID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeletePermission] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeletePermission]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeletePermission]
GO

CREATE PROCEDURE [dbo].[DeletePermission]
    @PermissionID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [PermissionID] FROM [dbo].[Permission]
            WHERE
                [PermissionID] = @PermissionID
        )
        BEGIN
            RAISERROR ('''dbo.Permission'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete Permission object from Permission */
        DELETE
        FROM [dbo].[Permission]
        WHERE
            [dbo].[Permission].[PermissionID] = @PermissionID

    END
GO
