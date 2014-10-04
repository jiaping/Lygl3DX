/****** Object:  StoredProcedure [dbo].[GetUserNameNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserNameNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetUserNameNVL]
GO

CREATE PROCEDURE [dbo].[GetUserNameNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get UserNameNVL from table */
        SELECT
            [User].[Name],
            [User].[UserID]
        FROM [dbo].[User]

    END
GO

