/****** Object:  StoredProcedure [dbo].[UpdateMxEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateAllMxStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].UpdateAllMxStatus
GO

CREATE PROCEDURE [dbo].UpdateAllMxStatus
AS
    BEGIN

        SET NOCOUNT ON
	--
	declare @MxID uniqueidentifier;
	declare @MxStatusID int;
	declare @MxTypeID int;
	declare @MaxMxs int;  --最大墓穴数
	declare @Azwdq int;  --表示未到期的安葬
	declare @Lbwdq int;  --表示未到期的立碑
	declare @Szs int; --表示安葬逝者数
	declare @Ylb bit;	 
	declare @newStatusID int; 
	--declare @today date;
	--set @today = GETDATE();
	declare @today datetime;
	set @today =left(CONVERT(varchar,GETDATE(),21),10);
	

	DECLARE Mx_Cursor CURSOR FOR
		SELECT MxID,MxStatusID,MxTypeID 
		FROM [dbo].[Mx] 
		WHERE  [Mx].[MxStatusID]=3 or [Mx].[MxStatusID]=4 or [Mx].[MxStatusID]=5;

OPEN Mx_Cursor;
FETCH NEXT FROM Mx_Cursor into @MxID,@mxStatusID,@mxTypeID;
WHILE @@FETCH_STATUS = 0
   BEGIN
        select @Azwdq=count(*) from Sz where [MxID]=@MxID and [RmDate]>@today;
	    select @Lbwdq=count(*) from BusinessLb where [MxID]=@MxID and [LbDate]>@today;
      if @Azwdq=0 and @Lbwdq=0
      begin
		select @Szs =COUNT(*) from Sz where [MxID]=@MxID;
		select @MaxMxs =MxXs  from MxType where MxTypeID  =@MxTypeID
		 IF NOT EXISTS
        (
            SELECT BusinessID FROM [dbo].[BusinessLb]
            WHERE  [MxID] = @MxID
        ) set @Ylb=0 else set @Ylb=1;
        if @Ylb =1
        begin
           if @Szs = @MaxMxs set @newStatusID=49 else
           begin
		     if @Szs=0 set @newStatusID=40;
             if @Szs= 1 set @newStatusID = 41;
             if @Szs= 2 set @newStatusID = 42;
             if @Szs= 3 set @newStatusID =43;
           end;
        end
        else
        begin
			if @Szs = @MaxMxs set @newStatusID=39 else
           begin
             if @Szs= 1 set @newStatusID = 31;
             if @Szs= 2 set @newStatusID = 32;
             if @Szs= 3 set @newStatusID =33;
           end;
        End;
        
      end  else
	  begin
		if @Azwdq>0 set @newStatusID=3;
		if @Lbwdq>0 set @newStatusID=4;
		if @Azwdq>0 and @Lbwdq>0 set @newStatusID=5
	  end;
        Update Mx set MxStatusID = @newStatusID where MxID=@MxID

      FETCH NEXT FROM Mx_Cursor into @MxID,@mxStatusID,@mxTypeID;
   END;
   
CLOSE Mx_Cursor;
DEALLOCATE Mx_Cursor;
end;
GO