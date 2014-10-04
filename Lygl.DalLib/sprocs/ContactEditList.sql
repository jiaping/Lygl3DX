/****** Object:  StoredProcedure [dbo].[GetContactEditListByMxID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetContactEditListByMxID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetContactEditListByMxID]
GO

CREATE PROCEDURE [dbo].[GetContactEditListByMxID]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get ContactEdit from table */
        SELECT
            [Contact].[ContactID],
            RTRIM([Contact].[Name]) AS [Name],
            [Contact].[Dw],
            RTRIM([Contact].[Address]) AS [Address],
            RTRIM([Contact].[Phone]) AS [Phone],
            RTRIM([Contact].[Mobile]) AS [Mobile],
            RTRIM([Contact].[SfzID]) AS [SfzID],
            RTRIM([Contact].[Yszgx]) AS [Yszgx],
            [Contact].[MxID]
        FROM [dbo].[Contact]
        WHERE
            [Contact].[MxID] = @MxID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetContactEditListByName] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetContactEditListByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetContactEditListByName]
GO

CREATE PROCEDURE [dbo].[GetContactEditListByName]
    @Name nchar(10)
AS
    BEGIN

        SET NOCOUNT ON

        /* Search Variables */
        IF (@Name <> '')
            SET @Name = RTRIM(@Name) + '%'
        ELSE
            SET @Name = '%'

        /* Get ContactEdit from table */
        SELECT
            [Contact].[ContactID],
            RTRIM([Contact].[Name]) AS [Name],
            [Contact].[Dw],
            RTRIM([Contact].[Address]) AS [Address],
            RTRIM([Contact].[Phone]) AS [Phone],
            RTRIM([Contact].[Mobile]) AS [Mobile],
            RTRIM([Contact].[SfzID]) AS [SfzID],
            RTRIM([Contact].[Yszgx]) AS [Yszgx],
            [Contact].[MxID]
        FROM [dbo].[Contact]
        WHERE
            [Contact].[Name] = @Name

    END
GO

