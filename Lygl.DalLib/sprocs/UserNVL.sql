/****** Object:  StoredProcedure [dbo].[GetUserNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetUserNVL]
GO

CREATE PROCEDURE [dbo].[GetUserNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get UserNVL from table */
        SELECT
            RTRIM([User].[Code]) AS [Code],
            [User].[UserID]
        FROM [dbo].[User]

    END
GO

