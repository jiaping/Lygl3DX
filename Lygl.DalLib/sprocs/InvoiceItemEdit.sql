/****** Object:  StoredProcedure [dbo].[AddInvoiceItemEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddInvoiceItemEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddInvoiceItemEdit]
GO

CREATE PROCEDURE [dbo].[AddInvoiceItemEdit]
    @InvoiceItemID uniqueidentifier,
    @InvoiceID uniqueidentifier,
    @Price money,
    @Quantity int,
    @ItemTypeID int,
    @BusinessID uniqueidentifier,
    @PayFlag bit,
    @MxID uniqueidentifier,
    @BusinessName nvarchar(20)
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.InvoiceItem */
        INSERT INTO [dbo].[InvoiceItem]
        (
            [InvoiceItemID],
            [InvoiceID],
            [Price],
            [Quantity],
            [ItemTypeID],
            [BusinessID],
            [PayFlag],
            [MxID],
            [BusinessName]
        )
        VALUES
        (
            @InvoiceItemID,
            @InvoiceID,
            @Price,
            @Quantity,
            @ItemTypeID,
            @BusinessID,
            @PayFlag,
            @MxID,
            @BusinessName
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateInvoiceItemEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateInvoiceItemEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateInvoiceItemEdit]
GO

CREATE PROCEDURE [dbo].[UpdateInvoiceItemEdit]
    @InvoiceItemID uniqueidentifier,
    @InvoiceID uniqueidentifier,
    @Price money,
    @Quantity int,
    @ItemTypeID int,
    @BusinessID uniqueidentifier,
    @PayFlag bit,
    @MxID uniqueidentifier,
    @BusinessName nvarchar(20)
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [InvoiceItemID] FROM [dbo].[InvoiceItem]
            WHERE
                [InvoiceItemID] = @InvoiceItemID
        )
        BEGIN
            RAISERROR ('''dbo.InvoiceItemEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.InvoiceItem */
        UPDATE [dbo].[InvoiceItem]
        SET
            [InvoiceID] = @InvoiceID,
            [Price] = @Price,
            [Quantity] = @Quantity,
            [ItemTypeID] = @ItemTypeID,
            [BusinessID] = @BusinessID,
            [PayFlag] = @PayFlag,
            [MxID] = @MxID,
            [BusinessName] = @BusinessName
        WHERE
            [InvoiceItemID] = @InvoiceItemID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteInvoiceItemEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteInvoiceItemEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteInvoiceItemEdit]
GO

CREATE PROCEDURE [dbo].[DeleteInvoiceItemEdit]
    @InvoiceItemID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [InvoiceItemID] FROM [dbo].[InvoiceItem]
            WHERE
                [InvoiceItemID] = @InvoiceItemID
        )
        BEGIN
            RAISERROR ('''dbo.InvoiceItemEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete InvoiceItemEdit object from InvoiceItem */
        DELETE
        FROM [dbo].[InvoiceItem]
        WHERE
            [dbo].[InvoiceItem].[InvoiceItemID] = @InvoiceItemID

    END
GO
