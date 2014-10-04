/****** Object:  StoredProcedure [dbo].[AddInvoice] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddInvoice]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddInvoice]
GO

CREATE PROCEDURE [dbo].[AddInvoice]
    @InvoiceID uniqueidentifier,
    @InvoiceAccount money,
    @MxID uniqueidentifier,
    @InvoiceTime datetime,
    @Drawee nvarchar(10),
    @InvoiceNumber nvarchar(22),
    @OperatorID uniqueidentifier,
    @IsPrinted bit
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Invoice */
        INSERT INTO [dbo].[Invoice]
        (
            [InvoiceID],
            [InvoiceAccount],
            [MxID],
            [InvoiceTime],
            [Drawee],
            [InvoiceNumber],
            [OperatorID],
            [IsPrinted]
        )
        VALUES
        (
            @InvoiceID,
            @InvoiceAccount,
            @MxID,
            @InvoiceTime,
            @Drawee,
            @InvoiceNumber,
            @OperatorID,
            @IsPrinted
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateInvoice] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateInvoice]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateInvoice]
GO

CREATE PROCEDURE [dbo].[UpdateInvoice]
    @InvoiceID uniqueidentifier,
    @InvoiceAccount money,
    @MxID uniqueidentifier,
    @InvoiceTime datetime,
    @Drawee nvarchar(10),
    @InvoiceNumber nvarchar(22),
    @OperatorID uniqueidentifier,
    @IsPrinted bit
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [InvoiceID] FROM [dbo].[Invoice]
            WHERE
                [InvoiceID] = @InvoiceID
        )
        BEGIN
            RAISERROR ('''dbo.Invoice'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Invoice */
        UPDATE [dbo].[Invoice]
        SET
            [InvoiceAccount] = @InvoiceAccount,
            [MxID] = @MxID,
            [InvoiceTime] = @InvoiceTime,
            [Drawee] = @Drawee,
            [InvoiceNumber] = @InvoiceNumber,
            [OperatorID] = @OperatorID,
            [IsPrinted] = @IsPrinted
        WHERE
            [InvoiceID] = @InvoiceID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteInvoice] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteInvoice]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteInvoice]
GO

CREATE PROCEDURE [dbo].[DeleteInvoice]
    @InvoiceID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [InvoiceID] FROM [dbo].[Invoice]
            WHERE
                [InvoiceID] = @InvoiceID
        )
        BEGIN
            RAISERROR ('''dbo.Invoice'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete Invoice object from Invoice */
        DELETE
        FROM [dbo].[Invoice]
        WHERE
            [dbo].[Invoice].[InvoiceID] = @InvoiceID

    END
GO
