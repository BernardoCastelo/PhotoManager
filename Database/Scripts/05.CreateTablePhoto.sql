BEGIN TRY
    BEGIN TRANSACTION
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photo]'))
    BEGIN
        CREATE TABLE [dbo].[Photo]
        (
			[Id] [int] IDENTITY(1,1) NOT NULL,
            [DateTaken] Datetimeoffset NOT NULL,
            [CameraId] [int] NULL FOREIGN KEY REFERENCES Camera(Id),
            [CategoryId] [int] NULL FOREIGN KEY REFERENCES Category(Id),
            [FileId] [int] NULL FOREIGN KEY REFERENCES [File](Id),
            [FocalLength] [int] NULL,
            [Height] [int] NULL,
            [Iso] [int] NULL,
            [Order] [int] NULL,
            [Width] [int] NULL,
            [Exposure] NVARCHAR(1024) NULL,
            [FStop] NVARCHAR(1024) NULL,
            [Name] NVARCHAR(1024) NOT NULL,
            CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]
    END
	IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[Photo]') AND name = N'IX_Photo_Id')
	BEGIN
		CREATE UNIQUE INDEX IX_Photo_Id ON [Photo] ([Id])
	END

    IF @@TRANCOUNT > 0
    BEGIN
        PRINT 'Commit transaction'
        COMMIT TRANSACTION
    END
END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();

    PRINT CONCAT('
        ErrorNumber: ', ERROR_NUMBER(),'
        ErrorSeverity: ', @ErrorSeverity,'
        ErrorState: ', @ErrorState,'
        ErrorProcedure: ', ERROR_PROCEDURE(),'
        ErrorLine: ', ERROR_LINE(),'
        ErrorMessage: ', @ErrorMessage
        )

    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH