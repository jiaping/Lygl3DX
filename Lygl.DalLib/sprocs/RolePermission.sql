/****** Object:  StoredProcedure [dbo].[AddRolePermission] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddRolePermission]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddRolePermission]
GO

CREATE PROCEDURE [dbo].[AddRolePermission]
    @RoleID uniqueidentifier,
    @Name varchar(30),
    @PermissionID uniqueidentifier,
    @Description nvarchar(100)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Permission */
        INSERT INTO [dbo].[Permission]
        (
            [RoleID],
            [Name],
            [PermissionID],
            [Description]
        )
        VALUES
        (
            @RoleID,
            @Name,
            @PermissionID,
            @Description
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateRolePermission] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateRolePermission]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateRolePermission]
GO

CREATE PROCEDURE [dbo].[UpdateRolePermission]
    @Name varchar(30),
    @PermissionID uniqueidentifier,
    @Description nvarchar(100)
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
            RAISERROR ('''dbo.RolePermission'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Permission */
        UPDATE [dbo].[Permission]
        SET
            [Name] = @Name,
            [Description] = @Description
        WHERE
            [PermissionID] = @PermissionID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteRolePermission] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteRolePermission]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteRolePermission]
GO

CREATE PROCEDURE [dbo].[DeleteRolePermission]
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
            RAISERROR ('''dbo.RolePermission'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete RolePermission object from Permission */
        DELETE
        FROM [dbo].[Permission]
        WHERE
            [dbo].[Permission].[PermissionID] = @PermissionID

    END
GO
