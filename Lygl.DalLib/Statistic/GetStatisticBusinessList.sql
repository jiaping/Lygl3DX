USE [lygl]
GO

/****** Object:  StoredProcedure [dbo].[GetGlfListByDate]    Script Date: 04/02/2012 07:56:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* 
查询指定日期内管理费将到期的所有管理费项
*/
alter PROCEDURE [dbo].[GetStatisticBusinessList]
	@UserID nvarchar(36),
    @StartDate datetime,
	@EndDate datetime

AS
    BEGIN

        SET NOCOUNT ON
	--declare @MxID uniqueidentifier;
	--declare @MxName nvarchar(50);
	--declare @Price money;
	--declare @StartDate datetime;
	--declare @EndDate datetime ;

create table #(
	[BusinessID] [uniqueidentifier] NOT NULL,
	[BusinessName] [Nvarchar](20) ,
	[MxID] [uniqueidentifier] NOT NULL,
	[MxName] [Nvarchar](50)
	)
	if (@UserID='')
	begin
		insert # 
		   select BusinessID,BusinessName,BusinessYd.MxID,MxName from BusinessYd inner join Mx on Mx.MxID=BusinessYd.MxID where operateTime between @StartDate and @EndDate ;
		insert # 
		   select BusinessID,BusinessName,BusinessGm.MxID,MxName from BusinessGm inner join Mx on Mx.MxID=BusinessGm.MxID where operateTime between @StartDate and @EndDate ;
		insert # 
		   select BusinessID,BusinessName,BusinessGlf.MxID,MxName from BusinessGlf inner join Mx on Mx.MxID=BusinessGlf.MxID where operateTime between @StartDate and @EndDate ;

	end else
	begin 
		insert # 
		   select BusinessID,BusinessName,BusinessYd.MxID,MxName from BusinessYd inner join Mx on Mx.MxID=BusinessYd.MxID where operatorID=@UserID and ( operateTime between @StartDate and @EndDate) ;
		insert # 
		   select BusinessID,BusinessName,BusinessGm.MxID,MxName from BusinessGm inner join Mx on Mx.MxID=BusinessGm.MxID where operatorID=@UserID and ( operateTime between @StartDate and @EndDate) ;
		insert # 
		   select BusinessID,BusinessName,BusinessGlf.MxID,MxName from BusinessGlf inner join Mx on Mx.MxID=BusinessGlf.MxID where operatorID=@UserID and ( operateTime between @StartDate and @EndDate) ;

	end;

	select * from #

--	DECLARE MxID_Cursor CURSOR FOR
--		select distinct  mxid from sz

--OPEN MxID_Cursor;
--FETCH NEXT FROM MxID_Cursor into @MxID;
--WHILE @@FETCH_STATUS = 0
--   BEGIN
--		select top 1 @EndDate=EndDate  from BusinessGlf where MxID=@MxID  order by  EndDate desc
--		if (@EndDate<=@DqDate)
--		begin
--		   select @MxName= MxName from Mx where MxID= @MxID;
--		   select top 1 @Price =Price,@StartDate= StartDate,@EndDate=EndDate  from BusinessGlf where MxID=@MxID  order by  EndDate desc
--		   insert # (MxID,MxName,Price,StartDate,EndDate ) values(@MxID,@MxName,@Price,@StartDate,@EndDate );
--		end;
   

--      FETCH NEXT FROM MxID_Cursor into @MxID;
--   END;
--   select * from #
   
--CLOSE MxID_Cursor;
--DEALLOCATE MxID_Cursor;
end;



GO


