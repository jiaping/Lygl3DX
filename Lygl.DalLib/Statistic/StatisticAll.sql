/****** Object:  StoredProcedure [dbo].[GetMxROL] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLyInfo]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].GetLyInfo
GO

CREATE PROCEDURE [dbo].GetLyInfo
		@TotalMqNum int Output,
		@TotalMxNum int Output	
AS
    BEGIN

        SET NOCOUNT ON

        /* Get 墓区总数 from table */
        set @TotalMqNum = (SELECT count(*) as TotalMqNum
        FROM [dbo].[Area])

		/* Get 墓穴总数 from table */
		set @TotalMxNum = (SELECT count(*) as TotalMxNum
        FROM [dbo].[Mx])
		
		select @TotalMqNum as TotalMqNum ,@TotalMxNum as TotalMxNum

    END
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMqInfo]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].GetMqInfo
GO
CREATE PROCEDURE [dbo].GetMqInfo
		@MqID uniqueidentifier,
		@MqMxNum int Output,
		@MqDsMxNum int output,
		@MqYdMxNum int output,
		@MqYsMxNum int output
AS
    BEGIN

        SET NOCOUNT ON

        /* Get 墓穴总数 from table */
        set @MqMxNum = (SELECT count(*) as MqMxNum
        FROM [dbo].[Mx] where AreaID=@MqID)

		set @MqDsMxNum = (SELECT count(*) as MqDsNum
        FROM [dbo].[Mx] where AreaID=@MqID and mxstatusid=0)
		set @MqYdMxNum = (SELECT count(*) as MqYdNum
        FROM [dbo].[Mx] where AreaID=@MqID and mxstatusid=1)
		set @MqYsMxNum = (SELECT count(*) as MqYsNum
        FROM [dbo].[Mx] where AreaID=@MqID and mxstatusid>1)

		/* Get 墓穴总数 from table 
		set @TotalMxNum = (SELECT count(*) as TotalMxNum
        FROM [dbo].[Mx])
		
		select @TotalMqNum as TotalMqNum ,@TotalMxNum as TotalMxNum*/

    END
GO




