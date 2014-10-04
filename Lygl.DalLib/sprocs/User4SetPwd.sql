/****** Object:  StoredProcedure [dbo].[GetUser4SetPwd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUser4SetPwd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetUser4SetPwd]
GO

CREATE PROCEDURE [dbo].[GetUser4SetPwd]
    @UserID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get User4SetPwd from table */
        SELECT
            [User].[UserID],
            [User].[Password]
        FROM [dbo].[User]
        WHERE
            [User].[UserID] = @UserID

    END
GO

/****** Object:  StoredProcedure [dbo].[AddUser4SetPwd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUser4SetPwd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddUser4SetPwd]
GO

CREATE PROCEDURE [dbo].[AddUser4SetPwd]
    @UserID uniqueidentifier,
    @Password varchar(50)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.User */
        INSERT INTO [dbo].[User]
        (
            [UserID],
            [Password]
        )
        VALUES
        (
            @UserID,
            @Password
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateUser4SetPwd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateUser4SetPwd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateUser4SetPwd]
GO

CREATE PROCEDURE [dbo].[UpdateUser4SetPwd]
    @UserID uniqueidentifier,
    @Password varchar(50)
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
            RAISERROR ('''dbo.User4SetPwd'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.User */
        UPDATE [dbo].[User]
        SET
            [Password] = @Password
        WHERE
            [UserID] = @UserID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteUser4SetPwd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUser4SetPwd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteUser4SetPwd]
GO

CREATE PROCEDURE [dbo].[DeleteUser4SetPwd]
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
            RAISERROR ('''dbo.User4SetPwd'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete User4SetPwd object from User */
        DELETE
        FROM [dbo].[User]
        WHERE
            [dbo].[User].[UserID] = @UserID

    END
GO
