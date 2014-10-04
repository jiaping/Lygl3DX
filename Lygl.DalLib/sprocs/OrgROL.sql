/****** Object:  StoredProcedure [dbo].[GetOrgChild] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrgChild]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetOrgChild]
GO

CREATE PROCEDURE [dbo].[GetOrgChild]
    @PID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get OrgNodeInfo from table */
        SELECT
            [Org].[Code],
            [Org].[Name],
            [Org].[OrderNo],
            [Org].[OrgID],
            [Org].[PID]
        FROM [dbo].[Org]
        WHERE
            [Org].[PID] = @PID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetOrgROL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrgROL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetOrgROL]
GO

CREATE PROCEDURE [dbo].[GetOrgROL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get OrgNodeInfo from table */
        SELECT
            [Org].[Code],
            [Org].[Name],
            [Org].[OrderNo],
            [Org].[OrgID],
            [Org].[PID]
        FROM [dbo].[Org]

    END
GO

