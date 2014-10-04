/****** Object:  StoredProcedure [dbo].[GetLbBxNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLbBxNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetLbBxNVL]
GO

CREATE PROCEDURE [dbo].[GetLbBxNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get LbBxNVL from table */
        SELECT
            [LbBx].[Name],
            [LbBx].[Value]
        FROM [dbo].[LbBx]

    END
GO

