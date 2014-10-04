/****** Object:  StoredProcedure [dbo].[GetMxEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMxEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetMxEdit]
GO

CREATE PROCEDURE [dbo].[GetMxEdit]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Get MxEdit from table */
        SELECT
            [Mx].[MxID],
            [Mx].[AreaID],
            [Mx].[Price],
            [Mx].[Rmsj],
            [Mx].[ManageFeeEndTime],
            RTRIM([Mx].[KbrName]) AS [KbrName],
            [Mx].[Bwjf],
            RTRIM([Mx].[Mz]) AS [Mz],
            [Mx].[CreateDate],
            [Mx].[SzName],
            [Mx].[MxName],
            [Mx].[MxTypeID],
            [Mx].[MxStatusID],
            [Mx].[MxStyleID],
            [Mx].[Pos],
            [Mx].[Angle]
        FROM [dbo].[Mx]
        WHERE
            [Mx].[MxID] = @MxID

    END
GO

/****** Object:  StoredProcedure [dbo].[AddMxEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddMxEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[AddMxEdit]
GO

CREATE PROCEDURE [dbo].[AddMxEdit]
    @MxID uniqueidentifier,
    @AreaID uniqueidentifier,
    @Price money,
    @Rmsj datetime,
    @ManageFeeEndTime datetime,
    @KbrName nchar(10),
    @Bwjf bit,
    @Mz nchar(10),
    @CreateDate datetime,
    @SzName nvarchar(20),
    @MxName varchar(50),
    @MxTypeID int,
    @MxStatusID int,
    @MxStyleID int,
    @Pos varchar(MAX),
    @Angle int
AS
    BEGIN

        SET NOCOUNT ON

        /* Insert object into dbo.Mx */
        INSERT INTO [dbo].[Mx]
        (
            [MxID],
            [AreaID],
            [Price],
            [Rmsj],
            [ManageFeeEndTime],
            [KbrName],
            [Bwjf],
            [Mz],
            [CreateDate],
            [SzName],
            [MxName],
            [MxTypeID],
            [MxStatusID],
            [MxStyleID],
            [Pos],
            [Angle]
        )
        VALUES
        (
            @MxID,
            @AreaID,
            @Price,
            @Rmsj,
            @ManageFeeEndTime,
            @KbrName,
            @Bwjf,
            @Mz,
            @CreateDate,
            @SzName,
            @MxName,
            @MxTypeID,
            @MxStatusID,
            @MxStyleID,
            @Pos,
            @Angle
        )

    END
GO

/****** Object:  StoredProcedure [dbo].[UpdateMxEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateMxEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdateMxEdit]
GO

CREATE PROCEDURE [dbo].[UpdateMxEdit]
    @MxID uniqueidentifier,
    @AreaID uniqueidentifier,
    @Price money,
    @Rmsj datetime,
    @ManageFeeEndTime datetime,
    @KbrName nchar(10),
    @Bwjf bit,
    @Mz nchar(10),
    @SzName nvarchar(20),
    @MxName varchar(50),
    @MxTypeID int,
    @MxStatusID int,
    @MxStyleID int,
    @Pos varchar(MAX),
    @Angle int
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [MxID] FROM [dbo].[Mx]
            WHERE
                [MxID] = @MxID
        )
        BEGIN
            RAISERROR ('''dbo.MxEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Update object in dbo.Mx */
        UPDATE [dbo].[Mx]
        SET
            [AreaID] = @AreaID,
            [Price] = @Price,
            [Rmsj] = @Rmsj,
            [ManageFeeEndTime] = @ManageFeeEndTime,
            [KbrName] = @KbrName,
            [Bwjf] = @Bwjf,
            [Mz] = @Mz,
            [SzName] = @SzName,
            [MxName] = @MxName,
            [MxTypeID] = @MxTypeID,
            [MxStatusID] = @MxStatusID,
            [MxStyleID] = @MxStyleID,
            [Pos] = @Pos,
            [Angle] = @Angle
        WHERE
            [MxID] = @MxID

    END
GO

/****** Object:  StoredProcedure [dbo].[DeleteMxEdit] ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteMxEdit]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DeleteMxEdit]
GO

CREATE PROCEDURE [dbo].[DeleteMxEdit]
    @MxID uniqueidentifier
AS
    BEGIN

        SET NOCOUNT ON

        /* Check for object existance */
        IF NOT EXISTS
        (
            SELECT [MxID] FROM [dbo].[Mx]
            WHERE
                [MxID] = @MxID
        )
        BEGIN
            RAISERROR ('''dbo.MxEdit'' object not found. It was probably removed by another user.', 16, 1)
            RETURN
        END

        /* Delete MxEdit object from Mx */
        DELETE
        FROM [dbo].[Mx]
        WHERE
            [dbo].[Mx].[MxID] = @MxID

    END
GO
