USE [lygl]
GO

/****** Object:  StoredProcedure [dbo].[GetSeekGlfList]    Script Date: 04/02/2012 07:56:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* 
查询指定日期内管理费将到期的所有管理费项
*/
Alter PROCEDURE [dbo].[GetSeekGlfList]
    @StartDate datetime,
	@EndDate datetime
AS
    BEGIN

        SET NOCOUNT ON
	declare @MxID uniqueidentifier;
	declare @MxName nvarchar(50);
	declare @Price money;
	declare @LastStartDate datetime;
	declare @LastEndDate datetime ;
	declare @BusinessID uniqueidentifier;

create table #(
	[MxID] [uniqueidentifier] NOT NULL,
	[MxName] [Nvarchar](50) ,
	[Price] [money] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[BusinessID] [uniqueidentifier] not NULL
	)
	
	DECLARE MxID_Cursor CURSOR FOR
		select distinct  mxid from sz

OPEN MxID_Cursor;
FETCH NEXT FROM MxID_Cursor into @MxID;
WHILE @@FETCH_STATUS = 0
   BEGIN
		select top 1 @LastEndDate=EndDate  from BusinessGlf where MxID=@MxID  order by  EndDate desc
		--if (@LastEndDate<=@DqDate)
		if (@LastEndDate between @StartDate and @EndDate)
		begin
		   select @MxName= MxName from Mx where MxID= @MxID;
		   select top 1 @Price =Price,@LastStartDate= StartDate,@LastEndDate=EndDate ,@BusinessID=BusinessID  from BusinessGlf where MxID=@MxID  order by  EndDate desc
		   insert # (MxID,MxName,Price,StartDate,EndDate,BusinessID ) values(@MxID,@MxName,@Price,@LastStartDate,@LastEndDate,@BusinessID );
		end;
   

      FETCH NEXT FROM MxID_Cursor into @MxID;
   END;
   select * from #
   
CLOSE MxID_Cursor;
DEALLOCATE MxID_Cursor;
end;



GO


