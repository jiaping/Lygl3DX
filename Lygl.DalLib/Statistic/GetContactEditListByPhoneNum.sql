/****** Object:  StoredProcedure [dbo].[GetContactEditListByPhoneNum] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetContactEditListByPhoneNum]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetContactEditListByPhoneNum]
GO

CREATE PROCEDURE [dbo].[GetContactEditListByPhoneNum]
    @PhoneNum nvarchar(11)
AS
    BEGIN

        SET NOCOUNT ON

        /* Search Variables */
        IF (@PhoneNum <> '')
            SET @PhoneNum = RTRIM(@PhoneNum) + '%'
        ELSE
            SET @PhoneNum = '%'

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
             (ISNULL([Contact].[phone], '') LIKE RTRIM(@PhoneNum)) or (ISNULL([Contact].[mobile], '') LIKE RTRIM(@PhoneNum))

    END
GO
