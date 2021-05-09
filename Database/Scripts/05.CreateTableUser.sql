BEGIN TRY
    BEGIN TRANSACTION
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]'))
    BEGIN
        CREATE TABLE [dbo].[User]
        (
			[Id] [int] IDENTITY(1,1) NOT NULL,
            [LockoutEnd] Datetimeoffset NULL,
            [TwoFactorEnabled] BIT,
            [PhoneNumberConfirmed] BIT,
            [PhoneNumber] NVARCHAR(20),
            [ConcurrencyStamp] NVARCHAR(1024),
            [SecurityStamp] NVARCHAR(1024),
            [PasswordHash] NVARCHAR(1024),
            [EmailConfirmed] BIT,
            [NormalizedEmail] NVARCHAR(1024),
            [Email] NVARCHAR(1024),
            [NormalizedUserName] NVARCHAR(1024),
            [UserName] NVARCHAR(1024),
            [LockoutEnabled] BIT,
            [AccessFailedCount] [int]
            CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]
    END
	IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[User]') AND name = N'IX_User_Id')
	BEGIN
		CREATE UNIQUE INDEX IX_User_Id ON [User] ([Id])
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