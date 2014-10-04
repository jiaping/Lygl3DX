/****** Object:  StoredProcedure [dbo].[AddUserRole] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUserRole]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddUserRole]
GO

CREATE PROCEDURE [dbo].[AddUserRole]
    @UserID uniqueidentifier,
    @RoleID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.UserRole */
        INSERT INTO [dbo].[UserRole]
        (
            [UserID],
            [RoleID]
        )
        VALUES
        (
            @UserID,
            @RoleID
        )

    END
GO


/****** Object:  StoredProcedure [dbo].[DeleteUserRole] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUserRole]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteUserRole]
GO

CREATE PROCEDURE [dbo].[DeleteUserRole]
    @RoleID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [RoleID] FROM [dbo].[UserRole]
            WHERE
                [RoleID] = @RoleID
        )
        BEGIN
            RAISERROR ('''dbo.UserRole'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete UserRole object from UserRole */
        DELETE
        FROM [dbo].[UserRole]
        WHERE
            [dbo].[UserRole].[RoleID] = @RoleID

    END
GO
