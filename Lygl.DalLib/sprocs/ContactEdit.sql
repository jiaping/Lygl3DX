/****** Object:  StoredProcedure [dbo].[AddContactEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddContactEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddContactEdit]
GO

CREATE PROCEDURE [dbo].[AddContactEdit]
    @ContactID uniqueidentifier,
    @Name nchar(10),
    @Dw nvarchar(50),
    @Address nchar(255),
    @Phone nchar(11),
    @Mobile nchar(11),
    @SfzID nchar(18),
    @Yszgx nchar(10),
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Contact */
        INSERT INTO [dbo].[Contact]
        (
            [ContactID],
            [Name],
            [Dw],
            [Address],
            [Phone],
            [Mobile],
            [SfzID],
            [Yszgx],
            [MxID]
        )
        VALUES
        (
            @ContactID,
            @Name,
            @Dw,
            @Address,
            @Phone,
            @Mobile,
            @SfzID,
            @Yszgx,
            @MxID
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateContactEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateContactEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateContactEdit]
GO

CREATE PROCEDURE [dbo].[UpdateContactEdit]
    @ContactID uniqueidentifier,
    @Name nchar(10),
    @Dw nvarchar(50),
    @Address nchar(255),
    @Phone nchar(11),
    @Mobile nchar(11),
    @SfzID nchar(18),
    @Yszgx nchar(10),
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [ContactID] FROM [dbo].[Contact]
            WHERE
                [ContactID] = @ContactID
        )
        BEGIN
            RAISERROR ('''dbo.ContactEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Contact */
        UPDATE [dbo].[Contact]
        SET
            [Name] = @Name,
            [Dw] = @Dw,
            [Address] = @Address,
            [Phone] = @Phone,
            [Mobile] = @Mobile,
            [SfzID] = @SfzID,
            [Yszgx] = @Yszgx,
            [MxID] = @MxID
        WHERE
            [ContactID] = @ContactID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteContactEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteContactEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteContactEdit]
GO

CREATE PROCEDURE [dbo].[DeleteContactEdit]
    @ContactID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [ContactID] FROM [dbo].[Contact]
            WHERE
                [ContactID] = @ContactID
        )
        BEGIN
            RAISERROR ('''dbo.ContactEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete ContactEdit object from Contact */
        DELETE
        FROM [dbo].[Contact]
        WHERE
            [dbo].[Contact].[ContactID] = @ContactID

    END
GO
