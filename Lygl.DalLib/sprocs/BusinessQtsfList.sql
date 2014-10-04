/****** Object:  StoredProcedure [dbo].[GetBusinessQtsfList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessQtsfList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessQtsfList]
GO

CREATE PROCEDURE [dbo].[GetBusinessQtsfList]
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessQtsf from table */
        SELECT
            [BusinessQtsf].[BusinessID],
            [BusinessQtsf].[BusinessName],
            [BusinessQtsf].[MxID],
            [BusinessQtsf].[Price],
            [BusinessQtsf].[OperateTime],
            [BusinessQtsf].[Drawee],
            [BusinessQtsf].[OperatorID],
            [BusinessQtsf].[PayFlag]
        FROM [dbo].[BusinessQtsf]

    END
GO

/****** Object:  StoredProcedure [dbo].[GetBusinessQtsfListByMxID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBusinessQtsfListByMxID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBusinessQtsfListByMxID]
GO

CREATE PROCEDURE [dbo].[GetBusinessQtsfListByMxID]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get BusinessQtsf from table */
        SELECT
            [BusinessQtsf].[BusinessID],
            [BusinessQtsf].[BusinessName],
            [BusinessQtsf].[MxID],
            [BusinessQtsf].[Price],
            [BusinessQtsf].[OperateTime],
            [BusinessQtsf].[Drawee],
            [BusinessQtsf].[OperatorID],
            [BusinessQtsf].[PayFlag]
        FROM [dbo].[BusinessQtsf]
        WHERE
            [BusinessQtsf].[MxID] = @MxID

    END
GO

