/****** Object:  StoredProcedure [dbo].[GetPathROL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPathROL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetPathROL]
GO

CREATE PROCEDURE [dbo].[GetPathROL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get PathRO from table */
        SELECT
            [Path].[PathID],
            [Path].[Name],
            [Path].[GeometryText]
        FROM [dbo].[Path]

    END
GO

