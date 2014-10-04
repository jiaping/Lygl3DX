/****** Object:  StoredProcedure [dbo].[GetUserROL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserROL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetUserROL]
GO

CREATE PROCEDURE [dbo].[GetUserROL]
    @OrgID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get UserNodeInfo from table */
        SELECT
            RTRIM([User].[Code]) AS [Code],
            [User].[Name],
            [User].[UserID],
            [User].[OrgID]
        FROM [dbo].[User]
        WHERE
            [User].[OrgID] = @OrgID

    END
GO

