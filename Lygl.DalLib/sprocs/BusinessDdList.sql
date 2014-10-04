/****** Object:  StoredProcedure [dbo].[GetBusinessDdList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessDdList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessDdList]
GO

CREATE PROCEDURE [dbo].[GetBusinessDdList]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessDd from table */
        SELECT
            [BusinessDd].[BusinessID],
            [BusinessDd].[BusinessName],
            [BusinessDd].[StartDate],
            [BusinessDd].[EndDate],
            [BusinessDd].[MxID],
            [BusinessDd].[OperatorID],
            [BusinessDd].[Drawee],
            [BusinessDd].[Price],
            [BusinessDd].[PayFlag],
            [BusinessDd].[OperateTime]
        FROM [dbo].[BusinessDd]

    END
GO

/****** Object:  StoredProcedure [dbo].[GetBusinessDdListByMxID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessDdListByMxID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessDdListByMxID]
GO

CREATE PROCEDURE [dbo].[GetBusinessDdListByMxID]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessDd from table */
        SELECT
            [BusinessDd].[BusinessID],
            [BusinessDd].[BusinessName],
            [BusinessDd].[StartDate],
            [BusinessDd].[EndDate],
            [BusinessDd].[MxID],
            [BusinessDd].[OperatorID],
            [BusinessDd].[Drawee],
            [BusinessDd].[Price],
            [BusinessDd].[PayFlag],
            [BusinessDd].[OperateTime]
        FROM [dbo].[BusinessDd]
        WHERE
            [BusinessDd].[MxID] = @MxID

    END
GO

