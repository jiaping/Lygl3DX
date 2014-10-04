USE [lygl]
GO

/****** Object:  StoredProcedure [dbo].[UpdateGlf]    Script Date: 04/02/2012 04:48:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateGlf]
AS
    BEGIN

        SET NOCOUNT ON
	declare @MxID uniqueidentifier;
	declare @MxStatusID int;
	declare @MxTypeID int;
	declare @Rmdate datetime;
	declare @OperatorID uniqueidentifier;
	declare @OperateTime datetime;
	declare @Drawee nvarchar(10);
	--declare @today date;
	--set @today = GETDATE();
	declare @today datetime;
	--set @today =left(CONVERT(varchar,GETDATE(),21),10);	

	DECLARE Mx_Cursor CURSOR FOR
		SELECT MxID,MxStatusID,MxTypeID
		FROM [dbo].[Mx] 
		WHERE exists(
		select top 1 RmDate,OperatorID,OperateTime  from Sz where MxID=Mx.Mxid 
		) 

OPEN Mx_Cursor;
FETCH NEXT FROM Mx_Cursor into @MxID,@mxStatusID,@mxTypeID;
WHILE @@FETCH_STATUS = 0
   BEGIN
		
		select top 1 @Rmdate=RmDate,@OperatorID =OperatorID,@OperateTime= OperateTime  from Sz where MxID=@MxID order by  RmDate asc
		select top 1 @Drawee = name from Contact where MxID=@MxID;
		if not exists(
		   select * from BusinessGlf where MxID=@MxID
		) 
		begin
		   insert BusinessGlf (BusinessID,BusinessName,MxID,Price,StartDate,EndDate,Drawee,OperatorID,PayFlag,OperateTime) 
		      values( NEWID(),'π‹¿Ì∑—',@MxID,0,@Rmdate,dateadd(year,10,@Rmdate),@Drawee,@OperatorID,1,@OperateTime)
		end;
   

      FETCH NEXT FROM Mx_Cursor into @MxID,@mxStatusID,@mxTypeID;
   END;
   
   
CLOSE Mx_Cursor;
DEALLOCATE Mx_Cursor;
end;


GO


