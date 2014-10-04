/****** Object:  StoredProcedure [dbo].[GetMxSzEditListByMxID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxSzEditListByMxID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxSzEditListByMxID]
GO

CREATE PROCEDURE [dbo].[GetMxSzEditListByMxID]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxSzEdit from table */
        SELECT
            [Sz].[SzID],
            [Sz].[Name],
            [Sz].[Sex],
            [Sz].[MxID],
            [Sz].[RmDate],
            [Sz].[Age],
            [Sz].[OperatorID],
            [Sz].[OperateTime]
        FROM [dbo].[Sz]
        WHERE
            [Sz].[MxID] = @MxID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetMxSzEditListBySzName] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxSzEditListBySzName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxSzEditListBySzName]
GO

CREATE PROCEDURE [dbo].[GetMxSzEditListBySzName]
    @Name nvarchar(10)
AS
    BEGIN

        SET NOCOUNT ON

        /* Search Variables */
        IF (@Name <> '')
            SET @Name = @Name + '%'
        ELSE
            SET @Name = '%'

        /* Get MxSzEdit from table */
        SELECT
            [Sz].[SzID],
            [Sz].[Name],
            [Sz].[Sex],
            [Sz].[MxID],
            [Sz].[RmDate],
            [Sz].[Age],
            [Sz].[OperatorID],
            [Sz].[OperateTime]
        FROM [dbo].[Sz]
        WHERE
            [Sz].[Name] = @Name

    END
GO

