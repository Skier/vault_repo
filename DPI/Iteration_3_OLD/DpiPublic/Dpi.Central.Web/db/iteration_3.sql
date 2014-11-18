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
               WHERE [name] IN ('Application', 'PaymentType', 'PaymentInfoId') 
                   AND [id] = (SELECT [id] FROM sysobjects WHERE [name] = 'CreditCard_Payment'))
BEGIN
    ALTER TABLE [CreditCard_Payment] ADD 
        [PaymentInfoId] [INT] NULL CONSTRAINT [PaymentInfoId_FK] REFERENCES PaymentInfo([id]),
    	[PaymentType] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT 'CreditCard',
    	[Application] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT 'Old',
        [CvNumber] [VARCHAR] (4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
        [ExpYear] [INT] NULL,
        [ExpMonth] [INT] NULL,
        [BankAccountNumber] [varchar] (20),
        [BankRoutingNumber] [varchar] (20),
        [DriverLicenseNumber] [varchar] (20),
        [DriverLicenseState] [varchar] (20)

    ALTER TABLE [CreditCard_Payment] 
        ALTER COLUMN [PaymentType] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
    ALTER TABLE [CreditCard_Payment] 
        ALTER COLUMN [Application] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
    ALTER TABLE [CreditCard_Payment] 
        ALTER COLUMN [TranDate] [char] (4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    ALTER TABLE [CreditCard_Payment]
        ALTER COLUMN [TranTime] [char] (6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
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
    @DriverLicenseState varchar(20)
    
AS    
    BEGIN TRANSACTION

    DECLARE @AccNumber INT
    DECLARE @PaymentInfoId INT

    -- Get account number by demand identifier
    
    SELECT @AccNumber = [BillPayer]
    FROM [dbo].[Demand]
    WHERE [id] = @DemandId

    -- Insert info PaymentInfo

    INSERT INTO [dbo].[PaymentInfo]
    (
        [Dmd], 
        [PymtDate], 
        [AmtPaid]
    )
    VALUES
    (
        @DemandId, 
        @PaymentDate,
        @Amount 
    )

    -- Get payment info identifier

    SET @PaymentInfoId = CAST(SCOPE_IDENTITY() AS INT) 

    -- Insert into CreditCard_Payment table

    INSERT INTO [dbo].[CreditCard_Payment]
    (
        [AccNumber],     
        [CardType], 
        [CardNumber], 
        [CvNumber], 
        [ExpYear], 
        [ExpMonth],
        [Amount], 
        [Date_Created], 
        [Verifone_Transaction_ID],
        [PaymentInfoId], 
        [PaymentType], 
        [Application],
        [BankAccountNumber], 
        [BankRoutingNumber],
        [DriverLicenseNumber],
        [DriverLicenseState]   
    )
    VALUES
    (
        @AccNumber,
        @CcType,
        @CcNumber,
        @CvNumber,
        @ExpYear,
        @ExpMonth,
        @Amount,
        @PaymentDate,
        @PaymentTransactionId,
        @PaymentInfoId,
        @PaymentType,
        @Application,
        @BankAccountNumber,
        @BankRoutingNumber,
        @DriverLicenseNumber,
        @DriverLicenseState
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
        pti.[Dmd] AS [DemandId],
        ccp.[Verifone_Transaction_ID] AS [PaymentTransactionId],
        ccp.[CardType] AS [CcType],
        ccp.[CardNumber] AS [CcNumber],
        ccp.[CvNumber] AS [CvNumber],
        ccp.[ExpYear],
        ccp.[ExpMonth]
    FROM [dbo].[CreditCard_Payment] ccp 
        INNER JOIN [dbo].[PaymentInfo] pti 
            ON ccp.[PaymentInfoId] = pti.[Id]
    WHERE [CreditCard_ID] = @Id 
GO



SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO