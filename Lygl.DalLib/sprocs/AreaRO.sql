/****** Object:  StoredProcedure [dbo].[GetAreaRO] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAreaRO]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetAreaRO]
GO

CREATE PROCEDURE [dbo].[GetAreaRO]
    @AreaID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get AreaRO from table */
        SELECT
            [Area].[AreaID],
            [Area].[Name],
            [Area].[GeometryText],
            [Area].[Angle]
        FROM [dbo].[Area]
        WHERE
            [Area].[AreaID] = @AreaID

    END
GO

