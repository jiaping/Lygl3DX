/****** Object:  StoredProcedure [dbo].[GetOrgNodeInfo] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrgNodeInfo]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetOrgNodeInfo]
GO

CREATE PROCEDURE [dbo].[GetOrgNodeInfo]
    @OrgID uniqueidentifier
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
            [Org].[OrgID] = @OrgID

    END
GO

