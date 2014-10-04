/****** Object:  StoredProcedure [dbo].[AddRole] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddRole]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddRole]
GO

CREATE PROCEDURE [dbo].[AddRole]
    @Name varchar(20),
    @RoleID uniqueidentifier,
    @Description nvarchar(100)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Role */
        INSERT INTO [dbo].[Role]
        (
            [Name],
            [RoleID],
            [Description]
        )
        VALUES
        (
            @Name,
            @RoleID,
            @Description
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateRole] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateRole]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateRole]
GO

CREATE PROCEDURE [dbo].[UpdateRole]
    @Name varchar(20),
    @RoleID uniqueidentifier,
    @Description nvarchar(100)
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [RoleID] FROM [dbo].[Role]
            WHERE
                [RoleID] = @RoleID
        )
        BEGIN
            RAISERROR ('''dbo.Role'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Role */
        UPDATE [dbo].[Role]
        SET
            [Name] = @Name,
            [Description] = @Description
        WHERE
            [RoleID] = @RoleID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteRole] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteRole]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteRole]
GO

CREATE PROCEDURE [dbo].[DeleteRole]
    @RoleID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [RoleID] FROM [dbo].[Role]
            WHERE
                [RoleID] = @RoleID
        )
        BEGIN
            RAISERROR ('''dbo.Role'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete child RolePermission from Permission */
        DELETE
            [dbo].[Permission]
        FROM [dbo].[Permission]
            INNER JOIN [dbo].[Role] ON [Permission].[RoleID] = [Role].[RoleID]
        WHERE
            [dbo].[Role].[RoleID] = @RoleID

        /* Delete child RoleUser from UserRole */
        DELETE
            [dbo].[UserRole]
        FROM [dbo].[UserRole]
            INNER JOIN [dbo].[Role] ON [UserRole].[RoleID] = [Role].[RoleID]
        WHERE
            [dbo].[Role].[RoleID] = @RoleID

        /* Delete Role object from Role */
        DELETE
        FROM [dbo].[Role]
        WHERE
            [dbo].[Role].[RoleID] = @RoleID

    END
GO
