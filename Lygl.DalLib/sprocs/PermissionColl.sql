/****** Object:  StoredProcedure [dbo].[GetPermissionColl] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPermissionColl]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetPermissionColl]
GO

CREATE PROCEDURE [dbo].[GetPermissionColl]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get Permission from table */
        SELECT
            [Permission].[Name],
            [Permission].[PermissionID],
            [Permission].[Description],
            [Permission].[RoleID]
        FROM [dbo].[Permission]

    END
GO

