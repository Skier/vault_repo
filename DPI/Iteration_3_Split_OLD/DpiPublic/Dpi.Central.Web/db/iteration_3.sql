/*****************************************************************************/ 
/* Iteration 3 DB modification script                                        */
/*****************************************************************************/

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

-- Alter CreditCard_Payment table

IF NOT EXISTS (SELECT * 
               FROM syscolumns 
               WHERE [name] IN ('Application', 'PaymentType', 'DemandId') 
                   AND [id] = (SELECT [id] FROM sysobjects WHERE [name] = 'CreditCard_Payment'))
BEGIN
    ALTER TABLE [CreditCard_Payment] ADD 
		[DemandId] [INT] NULL CONSTRAINT [Demand_FK] REFERENCES Demand([id]),
    	[PaymentType] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT 'CreditCard',
    	[Application] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT 'Old',
        [CvNumber] [VARCHAR] (4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [ExpYear] [INT] NULL,
        [ExpMonth] [INT] NULL,
        [BankAccountNumber] [varchar] (20),
        [BankRoutingNumber] [varchar] (20),
        [DriverLicenseNumber] [varchar] (20),
        [DriverLicenseState] [varchar] (20),
        [PaymentInfoId] [int] NULL

    ALTER TABLE [CreditCard_Payment] 
        ALTER COLUMN [PaymentType] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
    ALTER TABLE [CreditCard_Payment] 
        ALTER COLUMN [Application] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
    ALTER TABLE [CreditCard_Payment] 
        ALTER COLUMN [TranDate] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [TranTime] [char] (12) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [Processor] [char] (4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [Merchant_ID] [varchar] (32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [CardNumber] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [CardType] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [NameFirst] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [NameLast] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [ExpDate] [char] (4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [Address] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [Zip] [char] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [Action] [char] (6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
    	ALTER COLUMN [Server_Transaction_ID] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
    ALTER TABLE [CreditCard_Payment]
    	ALTER COLUMN [Verifone_Transaction_ID] [int] NULL
    
    /* TODO: Works but not good. Improve.
    -- Delete default contstrants.
    
    DELETE FROM sysobjects
    WHERE [id] IN (
        SELECT cdefault 
        FROM syscolumns 
        WHERE [name] IN ('Application', 'PaymentType') AND [id] = 1833421951)
    
    */
END

GO

-- Create SP: spPayment_Ins

CREATE PROCEDURE [dbo].[spPayment_Ins]
    @Id INT OUT,
    @PaymentType VARCHAR(20),
    @Amount MONEY,
    @DemandId INT,    
    @PaymentTransactionId INT,
    @NameFirst VARCHAR(25),
    @NameLast VARCHAR(25),
    @Address VARCHAR(50),
    @Zip VARCHAR(5),
    @CcType VARCHAR(15),
    @CcNumber VARCHAR(20),
    @CvNumber VARCHAR(4),
    @ExpYear INT,
    @ExpMonth INT,
    @PaymentDate DateTime, 
    @Application varchar(20),
    @BankAccountNumber varchar(20),
    @BankRoutingNumber varchar(20),
    @DriverLicenseNumber varchar(20),
    @DriverLicenseState varchar(20),
    @MerchantId varchar(32),
    @PaymentInfoId int
    
AS    
    BEGIN TRANSACTION

    DECLARE @AccNumber INT	

    -- Get account number by demand identifier
    
    SELECT @AccNumber = [BillPayer]
    FROM [dbo].[Demand]
    WHERE [id] = @DemandId
    
    -- Insert into CreditCard_Payment table

    INSERT INTO [dbo].[CreditCard_Payment]
    (
		[DemandId],
        [AccNumber],     
        [CardType], 
        [CardNumber], 
        [CvNumber], 
        [ExpYear], 
        [ExpMonth],
        [Amount], 
        [Date_Created], 
        [Verifone_Transaction_ID],
        [PaymentType], 
        [Application],
        [BankAccountNumber], 
        [BankRoutingNumber],
        [DriverLicenseNumber],
        [DriverLicenseState],        
        [TranDate],
        [TranTime],
        [Processor],
        [NameFirst], 
        [NameMI], 
        [NameLast], 
        [Address], 
        [Zip],        
        [Merchant_ID], 
        [ExpDate], 
        [Action], 
        [ReferenceNum], 
        [AuthenticationNum], 
        [Server_Transaction_ID], 
        [BatchDate], 
        [PaymentInfoId]
    )
    VALUES
    (
		@DemandId,
        @AccNumber,
        @CcType,
        @CcNumber,
        @CvNumber,
        @ExpYear,
        @ExpMonth,
        @Amount,
        @PaymentDate,
        @PaymentTransactionId,
        @PaymentType,
        @Application,
        @BankAccountNumber,
        @BankRoutingNumber,
        @DriverLicenseNumber,
        @DriverLicenseState,        
        CONVERT(VARCHAR(10), GETDATE(), 20),
        CAST(RIGHT(CONVERT(VARCHAR, GETDATE(), 21), 12) as VARCHAR(12)),
        'VISA',
        @NameFirst,
        null,
        @NameLast,
        @Address,
        @Zip,        
        @MerchantId,
        str(@ExpMonth, 2) + RIGHT (str(@ExpYear), 2),
        'SALE',
        NULL,
        NULL,
        NULL,
        NULL,
        @PaymentInfoId
    )

    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        END

    SET @Id = CAST(SCOPE_IDENTITY() AS INT)
    
    IF @@TRANCOUNT > 0
        COMMIT TRANSACTION
    RETURN(0)
    
    HandleError:
    IF @@TRANCOUNT > 1
        COMMIT TRANSACTION
    ELSE IF @@TRANCOUNT = 1
        ROLLBACK TRANSACTION
    
    RAISERROR(50002, 16, 1)

    RETURN(1)
GO

-- Create SP: spPayment_Get_AllById

CREATE PROCEDURE [dbo].[spPayment_Get_AllById]
    @Id INT
AS
    SELECT
        ccp.[CreditCard_ID] AS [Id],
        ccp.[PaymentType],        
        ccp.[Amount],
        ccp.[DemandId] AS [DemandId],
        ccp.[Verifone_Transaction_ID] AS [PaymentTransactionId],
        ccp.[NameFirst] AS [NameFirst],
        ccp.[NameLast] AS [NameLast],
        ccp.[Address] AS [Address],
        ccp.[Zip] AS [Zip],
        ccp.[CardType] AS [CcType],
        ccp.[CardNumber] AS [CcNumber],
        ccp.[CvNumber] AS [CvNumber],
        ccp.[ExpYear],
        ccp.[ExpMonth]
    FROM [dbo].[CreditCard_Payment] ccp 
    WHERE [CreditCard_ID] = @Id 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO