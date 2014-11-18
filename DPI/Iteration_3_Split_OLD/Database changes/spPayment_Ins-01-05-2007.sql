
-- Create SP: spPayment_Ins

ALTER PROCEDURE [dbo].[spPayment_Ins]
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
    @BankAccountNumber nvarchar(20),
    @BankRoutingNumber nvarchar(20),
    @DriverLicenseNumber nvarchar(20),
    @DriverLicenseState nvarchar(20)
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
        getdate(),
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
	BankAccountNumber,
	BankRoutingNumber,
	DriverLicenseNumber,
	DriverLicenseState
            
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
        getdate(),
        @PaymentTransactionId,
        @PaymentInfoId,
        @PaymentType,
        'Public',
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
