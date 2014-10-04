/****** Object:  StoredProcedure [dbo].[GetRoleListByRoleID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRoleListByRoleID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetRoleListByRoleID]
GO

CREATE PROCEDURE [dbo].[GetRoleListByRoleID]
    @RoleID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get Role from table */
        SELECT
            [Role].[Name],
            [Role].[RoleID],
            [Role].[Description]
        FROM [dbo].[Role]
        WHERE
            [Role].[RoleID] = @RoleID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetRoleList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRoleList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetRoleList]
GO

CREATE PROCEDURE [dbo].[GetRoleList]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get Role from table */
        SELECT
            [Role].[Name],
            [Role].[RoleID],
            [Role].[Description]
        FROM [dbo].[Role]

    END
GO

