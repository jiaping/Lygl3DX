/****** Object:  StoredProcedure [dbo].[GetInvoiceItemEditList] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceItemEditList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetInvoiceItemEditList]
GO

CREATE PROCEDURE [dbo].[GetInvoiceItemEditList]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get InvoiceItemEdit from table */
        SELECT
            [InvoiceItem].[InvoiceItemID],
            [InvoiceItem].[InvoiceID],
            [InvoiceItem].[Price],
            [InvoiceItem].[Quantity],
            [InvoiceItem].[ItemTypeID],
            [InvoiceItem].[BusinessID],
            [InvoiceItem].[PayFlag],
            [InvoiceItem].[MxID],
            [InvoiceItem].[BusinessName]
        FROM [dbo].[InvoiceItem]
        WHERE
            [InvoiceItem].[MxID] = @MxID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetInvoiceItemEditListByMxInvoiceTypeBusinessID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceItemEditListByMxInvoiceTypeBusinessID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetInvoiceItemEditListByMxInvoiceTypeBusinessID]
GO

CREATE PROCEDURE [dbo].[GetInvoiceItemEditListByMxInvoiceTypeBusinessID]
    @MxID uniqueidentifier,
    @ItemTypeID int,
    @BusinessID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get InvoiceItemEdit from table */
        SELECT
            [InvoiceItem].[InvoiceItemID],
            [InvoiceItem].[InvoiceID],
            [InvoiceItem].[Price],
            [InvoiceItem].[Quantity],
            [InvoiceItem].[ItemTypeID],
            [InvoiceItem].[BusinessID],
            [InvoiceItem].[PayFlag],
            [InvoiceItem].[MxID],
            [InvoiceItem].[BusinessName]
        FROM [dbo].[InvoiceItem]
        WHERE
            [InvoiceItem].[MxID] = @MxID AND
            [InvoiceItem].[ItemTypeID] = @ItemTypeID AND
            [InvoiceItem].[BusinessID] = @BusinessID

    END
GO

/****** Object:  StoredProcedure [dbo].[GetInvoiceItemEditListbyPayFlag] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceItemEditListbyPayFlag]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetInvoiceItemEditListbyPayFlag]
GO

CREATE PROCEDURE [dbo].[GetInvoiceItemEditListbyPayFlag]
    @MxID uniqueidentifier,
    @PayFlag bit
AS
    BEGIN

        SET NOCOUNT ON

        /* Get InvoiceItemEdit from table */
        SELECT
            [InvoiceItem].[InvoiceItemID],
            [InvoiceItem].[InvoiceID],
            [InvoiceItem].[Price],
            [InvoiceItem].[Quantity],
            [InvoiceItem].[ItemTypeID],
            [InvoiceItem].[BusinessID],
            [InvoiceItem].[PayFlag],
            [InvoiceItem].[MxID],
            [InvoiceItem].[BusinessName]
        FROM [dbo].[InvoiceItem]
        WHERE
            [InvoiceItem].[MxID] = @MxID AND
            [InvoiceItem].[PayFlag] = @PayFlag

    END
GO

/****** Object:  StoredProcedure [dbo].[GetInvoiceItemEditListByMxInvoice] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceItemEditListByMxInvoice]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetInvoiceItemEditListByMxInvoice]
GO

CREATE PROCEDURE [dbo].[GetInvoiceItemEditListByMxInvoice]
    @InvoiceID uniqueidentifier,
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get InvoiceItemEdit from table */
        SELECT
            [InvoiceItem].[InvoiceItemID],
            [InvoiceItem].[InvoiceID],
            [InvoiceItem].[Price],
            [InvoiceItem].[Quantity],
            [InvoiceItem].[ItemTypeID],
            [InvoiceItem].[BusinessID],
            [InvoiceItem].[PayFlag],
            [InvoiceItem].[MxID],
            [InvoiceItem].[BusinessName]
        FROM [dbo].[InvoiceItem]
        WHERE
            [InvoiceItem].[InvoiceID] = @InvoiceID AND
            [InvoiceItem].[MxID] = @MxID

    END
GO

