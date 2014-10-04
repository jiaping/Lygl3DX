/****** Object:  StoredProcedure [dbo].[GetRoleUserList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRoleUserList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetRoleUserList]
GO

CREATE PROCEDURE [dbo].[GetRoleUserList]
    @RoleID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get RoleUser from table */
        SELECT
            [UserRole].[UserID]
        FROM [dbo].[UserRole]
        WHERE
            [UserRole].[RoleID] = @RoleID

    END
GO

