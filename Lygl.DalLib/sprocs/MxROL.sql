/****** Object:  StoredProcedure [dbo].[GetMxROL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxROL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxROL]
GO

CREATE PROCEDURE [dbo].[GetMxROL]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxRO from table */
        SELECT
            [Mx].[MxID],
            [Mx].[MxName],
            [Mx].[Pos],
            [Mx].[MxStatusID],
            [Mx].[MxTypeID],
            [Mx].[AreaID],
            [Mx].[Price],
            [Mx].[Rmsj],
            [Mx].[ManageFeeEndTime],
            RTRIM([Mx].[KbrName]) AS [KbrName],
            [Mx].[Bwjf],
            RTRIM([Mx].[Mz]) AS [Mz],
            [Mx].[MxStyleID],
            [Mx].[SzName],
            [Mx].[Angle]
        FROM [dbo].[Mx]

    END
GO

/****** Object:  StoredProcedure [dbo].[GetMxROLByAreaID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxROLByAreaID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxROLByAreaID]
GO

CREATE PROCEDURE [dbo].[GetMxROLByAreaID]
    @AreaID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxRO from table */
        SELECT
            [Mx].[MxID],
            [Mx].[MxName],
            [Mx].[Pos],
            [Mx].[MxStatusID],
            [Mx].[MxTypeID],
            [Mx].[AreaID],
            [Mx].[Price],
            [Mx].[Rmsj],
            [Mx].[ManageFeeEndTime],
            RTRIM([Mx].[KbrName]) AS [KbrName],
            [Mx].[Bwjf],
            RTRIM([Mx].[Mz]) AS [Mz],
            [Mx].[MxStyleID],
            [Mx].[SzName],
            [Mx].[Angle]
        FROM [dbo].[Mx]
        WHERE
            [Mx].[AreaID] = @AreaID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetMxROLItem] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxROLItem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxROLItem]
GO

CREATE PROCEDURE [dbo].[GetMxROLItem]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxRO from table */
        SELECT
            [Mx].[MxID],
            [Mx].[MxName],
            [Mx].[Pos],
            [Mx].[MxStatusID],
            [Mx].[MxTypeID],
            [Mx].[AreaID],
            [Mx].[Price],
            [Mx].[Rmsj],
            [Mx].[ManageFeeEndTime],
            RTRIM([Mx].[KbrName]) AS [KbrName],
            [Mx].[Bwjf],
            RTRIM([Mx].[Mz]) AS [Mz],
            [Mx].[MxStyleID],
            [Mx].[SzName],
            [Mx].[Angle]
        FROM [dbo].[Mx]
        WHERE
            [Mx].[MxID] = @MxID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetMxROLByMxName] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxROLByMxName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxROLByMxName]
GO

CREATE PROCEDURE [dbo].[GetMxROLByMxName]
    @MxName varchar(50)
AS
    BEGIN

        SET NOCOUNT ON

        /* Search Variables */
        IF (@MxName <> '')
            SET @MxName = @MxName + '%'
        ELSE
            SET @MxName = '%'

        /* Get MxRO from table */
        SELECT
            [Mx].[MxID],
            [Mx].[MxName],
            [Mx].[Pos],
            [Mx].[MxStatusID],
            [Mx].[MxTypeID],
            [Mx].[AreaID],
            [Mx].[Price],
            [Mx].[Rmsj],
            [Mx].[ManageFeeEndTime],
            RTRIM([Mx].[KbrName]) AS [KbrName],
            [Mx].[Bwjf],
            RTRIM([Mx].[Mz]) AS [Mz],
            [Mx].[MxStyleID],
            [Mx].[SzName],
            [Mx].[Angle]
        FROM [dbo].[Mx]
        WHERE
            [Mx].[MxName] = @MxName

    END
GO

