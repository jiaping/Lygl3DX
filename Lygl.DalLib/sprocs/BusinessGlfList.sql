/****** Object:  StoredProcedure [dbo].[GetBusinessGlfList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessGlfList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessGlfList]
GO

CREATE PROCEDURE [dbo].[GetBusinessGlfList]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessGlf from table */
        SELECT
            [BusinessGlf].[BusinessID],
            [BusinessGlf].[BusinessName],
            [BusinessGlf].[MxID],
            [BusinessGlf].[Price],
            [BusinessGlf].[StartDate],
            [BusinessGlf].[EndDate],
            [BusinessGlf].[Drawee],
            [BusinessGlf].[OperatorID],
            [BusinessGlf].[PayFlag],
            [BusinessGlf].[OperateTime]
        FROM [dbo].[BusinessGlf]

    END
GO

/****** Object:  StoredProcedure [dbo].[GetBusinessGlfListGetByMx] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessGlfListGetByMx]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessGlfListGetByMx]
GO

CREATE PROCEDURE [dbo].[GetBusinessGlfListGetByMx]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessGlf from table */
        SELECT
            [BusinessGlf].[BusinessID],
            [BusinessGlf].[BusinessName],
            [BusinessGlf].[MxID],
            [BusinessGlf].[Price],
            [BusinessGlf].[StartDate],
            [BusinessGlf].[EndDate],
            [BusinessGlf].[Drawee],
            [BusinessGlf].[OperatorID],
            [BusinessGlf].[PayFlag],
            [BusinessGlf].[OperateTime]
        FROM [dbo].[BusinessGlf]
        WHERE
            [BusinessGlf].[MxID] = @MxID

    END
GO

