/****** Object:  StoredProcedure [dbo].[GetInvoiceItemRO] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceItemRO]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetInvoiceItemRO]
GO

CREATE PROCEDURE [dbo].[GetInvoiceItemRO]
    @InvoiceItemID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get InvoiceItemRO from table */
        SELECT
            [InvoiceItem].[InvoiceItemID],
            [InvoiceItem].[InvoiceID],
            [InvoiceItem].[Price],
            [InvoiceItem].[Quantity],
            [InvoiceItem].[BusinessID],
            [InvoiceItem].[BusinessName],
            [InvoiceItem].[PayFlag],
            [InvoiceItem].[MxID],
            [InvoiceItem].[ItemTypeID]
        FROM [dbo].[InvoiceItem]
        WHERE
            [InvoiceItem].[InvoiceItemID] = @InvoiceItemID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetInvoiceItemROByMxIDBusinessID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceItemROByMxIDBusinessID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetInvoiceItemROByMxIDBusinessID]
GO

CREATE PROCEDURE [dbo].[GetInvoiceItemROByMxIDBusinessID]
    @MxID uniqueidentifier,
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get InvoiceItemRO from table */
        SELECT
            [InvoiceItem].[InvoiceItemID],
            [InvoiceItem].[InvoiceID],
            [InvoiceItem].[Price],
            [InvoiceItem].[Quantity],
            [InvoiceItem].[BusinessID],
            [InvoiceItem].[BusinessName],
            [InvoiceItem].[PayFlag],
            [InvoiceItem].[MxID],
            [InvoiceItem].[ItemTypeID]
        FROM [dbo].[InvoiceItem]
        WHERE
            [InvoiceItem].[MxID] = @MxID AND
            [InvoiceItem].[BusinessID] = @BusinessID

    END
GO

