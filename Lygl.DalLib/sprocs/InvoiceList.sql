/****** Object:  StoredProcedure [dbo].[GetInvoiceListByMxID] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceListByMxID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetInvoiceListByMxID]
GO

CREATE PROCEDURE [dbo].[GetInvoiceListByMxID]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get Invoice from table */
        SELECT
            [Invoice].[InvoiceID],
            [Invoice].[InvoiceAccount],
            [Invoice].[MxID],
            [Invoice].[InvoiceTime],
            [Invoice].[Drawee],
            [Invoice].[InvoiceNumber],
            [Invoice].[OperatorID],
            [Invoice].[IsPrinted]
        FROM [dbo].[Invoice]
        WHERE
            [Invoice].[MxID] = @MxID

    END
GO

