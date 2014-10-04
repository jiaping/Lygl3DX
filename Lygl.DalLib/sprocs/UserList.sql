/****** Object:  StoredProcedure [dbo].[GetUserList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetUserList]
GO

CREATE PROCEDURE [dbo].[GetUserList]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get User from table */
        SELECT
            RTRIM([User].[Code]) AS [Code],
            [User].[Name],
            [User].[Password],
            [User].[LastLoginTime],
            [User].[LoginCount],
            [User].[MaxLoginCount],
            [User].[UserID]
        FROM [dbo].[User]

    END
GO

/****** Object:  StoredProcedure [dbo].[GetUserListByUserID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserListByUserID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetUserListByUserID]
GO

CREATE PROCEDURE [dbo].[GetUserListByUserID]
    @UserID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get User from table */
        SELECT
            RTRIM([User].[Code]) AS [Code],
            [User].[Name],
            [User].[Password],
            [User].[LastLoginTime],
            [User].[LoginCount],
            [User].[MaxLoginCount],
            [User].[UserID]
        FROM [dbo].[User]
        WHERE
            [User].[UserID] = @UserID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetUserListByUserCodePwd] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserListByUserCodePwd]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetUserListByUserCodePwd]
GO

CREATE PROCEDURE [dbo].[GetUserListByUserCodePwd]
    @Code char(4),
    @Password varchar(50)
AS
    BEGIN

        SET NOCOUNT ON

        /* Search Variables */
        IF (@Code <> '')
            SET @Code = RTRIM(@Code) + '%'
        ELSE
            SET @Code = '%'
        IF (@Password <> '')
            SET @Password = @Password + '%'
        ELSE
            SET @Password = '%'

        /* Get User from table */
        SELECT
            RTRIM([User].[Code]) AS [Code],
            [User].[Name],
            [User].[Password],
            [User].[LastLoginTime],
            [User].[LoginCount],
            [User].[MaxLoginCount],
            [User].[UserID]
        FROM [dbo].[User]
        WHERE
            [User].[Code] = @Code AND
            [User].[Password] = @Password

    END
GO

