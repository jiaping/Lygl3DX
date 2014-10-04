USE [lygl]
GO

/****** Object:  StoredProcedure [dbo].[GetStatisticSrList]    Script Date: 04/02/2012 07:56:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* 
查询指定日期内管理费将到期的所有管理费项
*/
alter PROCEDURE [dbo].GetStatisticSrList
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
	[Name] [Nvarchar](50),
	[SubTotal] [decimal],
	[Num] [int],
	)
	if (@UserID='')
	begin
		insert # 
		   SELECT	[BusinessName] as Name
					,sum([price]*[Quantity]) as subtotal
					,SUM([quantity]) as Num      
				FROM [lygl].[dbo].[InvoiceItem] 
				where InvoiceID in 
						(SELECT  [InvoiceID]  FROM [lygl].[dbo].[Invoice] where  InvoiceTime>= @StartDate and InvoiceTime<@EndDate)
				group by [BusinessName]
	end else
	begin 
		insert # 
			SELECT	[BusinessName] as Name
					,sum([price]*[Quantity]) as suttotal
					,SUM([quantity]) as Num      
				FROM [lygl].[dbo].[InvoiceItem] 
				where InvoiceID in 
						(SELECT  [InvoiceID]  FROM [lygl].[dbo].[Invoice] where operatorID=@UserID and InvoiceTime>= @StartDate and InvoiceTime<@EndDate)
				group by [BusinessName]
	end;

	select * from #

end;



GO


