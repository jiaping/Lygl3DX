/****** Object:  StoredProcedure [dbo].[GetUserRoleList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserRoleList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetUserRoleList]
GO

CREATE PROCEDURE [dbo].[GetUserRoleList]
    @UserID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get UserRole from table */
        SELECT
            [UserRole].[RoleID]
        FROM [dbo].[UserRole]
        WHERE
            [UserRole].[UserID] = @UserID

    END
GO

