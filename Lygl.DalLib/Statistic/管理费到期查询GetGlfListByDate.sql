USE [lygl]
GO

/****** Object:  StoredProcedure [dbo].[GetGlfListByDate]    Script Date: 04/02/2012 07:56:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* 
��ѯָ�������ڹ���ѽ����ڵ����й������
*/
ALTER PROCEDURE [dbo].[GetGlfListByDate]
    @DqDate datetime
AS
    BEGIN

        SET NOCOUNT ON
	declare @MxID uniqueidentifier;
	declare @MxName nvarchar(50);
	declare @Price money;
	declare @StartDate datetime;
	declare @EndDate datetime ;

create table #(
	[MxID] [uniqueidentifier] NOT NULL,
	[MxName] [Nvarchar](50) ,
	[Price] [money] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL
	)
	
	DECLARE MxID_Cursor CURSOR FOR
		select distinct  mxid from sz

OPEN MxID_Cursor;
FETCH NEXT FROM MxID_Cursor into @MxID;
WHILE @@FETCH_STATUS = 0
   BEGIN
		select top 1 @EndDate=EndDate  from BusinessGlf where MxID=@MxID  order by  EndDate desc
		if (@EndDate<=@DqDate)
		begin
		   select @MxName= MxName from Mx where MxID= @MxID;
		   select top 1 @Price =Price,@StartDate= StartDate,@EndDate=EndDate  from BusinessGlf where MxID=@MxID  order by  EndDate desc
		   insert # (MxID,MxName,Price,StartDate,EndDate ) values(@MxID,@MxName,@Price,@StartDate,@EndDate );
		end;
   

      FETCH NEXT FROM MxID_Cursor into @MxID;
   END;
   select * from #
   
CLOSE MxID_Cursor;
DEALLOCATE MxID_Cursor;
end;



GO


