/****** Object:  StoredProcedure [dbo].[GetMxStatusNVL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxStatusNVL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxStatusNVL]
GO

CREATE PROCEDURE [dbo].[GetMxStatusNVL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxStatusNVL from table */
        SELECT
            [MxStatus].[Name],
            [MxStatus].[Value]
        FROM [dbo].[MxStatus]

    END
GO

