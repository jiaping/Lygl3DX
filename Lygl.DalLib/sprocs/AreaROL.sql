/****** Object:  StoredProcedure [dbo].[GetAreaROL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAreaROL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetAreaROL]
GO

CREATE PROCEDURE [dbo].[GetAreaROL]
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

    END
GO

/****** Object:  StoredProcedure [dbo].[GetAreaROLByID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAreaROLByID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetAreaROLByID]
GO

CREATE PROCEDURE [dbo].[GetAreaROLByID]
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

