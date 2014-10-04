/****** Object:  StoredProcedure [dbo].[AddRoleUser] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddRoleUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddRoleUser]
GO

CREATE PROCEDURE [dbo].[AddRoleUser]
    @RoleID uniqueidentifier,
    @UserID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.UserRole */
        INSERT INTO [dbo].[UserRole]
        (
            [RoleID],
            [UserID]
        )
        VALUES
        (
            @RoleID,
            @UserID
        )

    END
GO


/****** Object:  StoredProcedure [dbo].[DeleteRoleUser] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteRoleUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteRoleUser]
GO

CREATE PROCEDURE [dbo].[DeleteRoleUser]
    @UserID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [UserID] FROM [dbo].[UserRole]
            WHERE
                [UserID] = @UserID
        )
        BEGIN
            RAISERROR ('''dbo.RoleUser'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete RoleUser object from UserRole */
        DELETE
        FROM [dbo].[UserRole]
        WHERE
            [dbo].[UserRole].[UserID] = @UserID

    END
GO
