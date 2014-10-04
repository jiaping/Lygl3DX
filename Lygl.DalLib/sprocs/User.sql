/****** Object:  StoredProcedure [dbo].[AddUser] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddUser]
GO

CREATE PROCEDURE [dbo].[AddUser]
    @Code char(4),
    @Name varchar(20),
    @Password varchar(50),
    @LastLoginTime datetime,
    @LoginCount int,
    @MaxLoginCount int,
    @UserID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.User */
        INSERT INTO [dbo].[User]
        (
            [Code],
            [Name],
            [Password],
            [LastLoginTime],
            [LoginCount],
            [MaxLoginCount],
            [UserID]
        )
        VALUES
        (
            @Code,
            @Name,
            @Password,
            @LastLoginTime,
            @LoginCount,
            @MaxLoginCount,
            @UserID
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateUser] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateUser]
GO

CREATE PROCEDURE [dbo].[UpdateUser]
    @Code char(4),
    @Name varchar(20),
    @Password varchar(50),
    @LastLoginTime datetime,
    @LoginCount int,
    @MaxLoginCount int,
    @UserID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [UserID] FROM [dbo].[User]
            WHERE
                [UserID] = @UserID
        )
        BEGIN
            RAISERROR ('''dbo.User'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.User */
        UPDATE [dbo].[User]
        SET
            [Code] = @Code,
            [Name] = @Name,
            [Password] = @Password,
            [LastLoginTime] = @LastLoginTime,
            [LoginCount] = @LoginCount,
            [MaxLoginCount] = @MaxLoginCount
        WHERE
            [UserID] = @UserID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteUser] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteUser]
GO

CREATE PROCEDURE [dbo].[DeleteUser]
    @UserID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [UserID] FROM [dbo].[User]
            WHERE
                [UserID] = @UserID
        )
        BEGIN
            RAISERROR ('''dbo.User'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete child UserRole from UserRole */
        DELETE
            [dbo].[UserRole]
        FROM [dbo].[UserRole]
            INNER JOIN [dbo].[User] ON [UserRole].[UserID] = [User].[UserID]
        WHERE
            [dbo].[User].[UserID] = @UserID

        /* Delete User object from User */
        DELETE
        FROM [dbo].[User]
        WHERE
            [dbo].[User].[UserID] = @UserID

    END
GO
