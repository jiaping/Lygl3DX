/****** Object:  StoredProcedure [dbo].[GetLogROL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLogROL]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetLogROL]
GO

CREATE PROCEDURE [dbo].[GetLogROL]
    @Date datetime
AS
    BEGIN

        SET NOCOUNT ON

        /* Get LogRO from table */
        SELECT
            [Log].[Id],
            [Log].[Date],
            [Log].[Thread],
            [Log].[Level],
            [Log].[Logger],
            [Log].[Message],
            [Log].[Exception]
        FROM [dbo].[Log]
        WHERE
            [Log].[Date] = @Date

    END
GO

