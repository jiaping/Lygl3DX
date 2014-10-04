/****** Object:  StoredProcedure [dbo].[GetRolePermissionListByRoleID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRolePermissionListByRoleID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetRolePermissionListByRoleID]
GO

CREATE PROCEDURE [dbo].[GetRolePermissionListByRoleID]
    @RoleID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get RolePermission from table */
        SELECT
            [Permission].[Name],
            [Permission].[PermissionID],
            [Permission].[Description]
        FROM [dbo].[Permission]
        WHERE
            [Permission].[RoleID] = @RoleID

    END
GO

