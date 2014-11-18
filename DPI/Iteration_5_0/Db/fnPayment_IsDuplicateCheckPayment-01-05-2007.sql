CREATE FUNCTION [dbo].[fnPayment_IsDuplicateCheckPayment] 
(
    @AccNumber INT,
    @BankAccountNumber nvarchar(20),
    @BankRoutingNumber nvarchar(20),
    @Amount MONEY,
    @DateCreated DATETIME
)
RETURNS BIT
AS
BEGIN 
  DECLARE @RET as BIT

  IF EXISTS(SELECT * 
            FROM CreditCard_Payment
            WHERE AccNumber = @AccNumber 
                AND BankAccountNumber = BankAccountNumber
	   AND BankRoutingNumber = @BankRoutingNumber
                AND Amount = @Amount
                AND PaymentType = 'Check'
                AND year(Date_Created) = year(@DateCreated) 
                AND month(Date_Created) = month(@DateCreated)
                AND day(Date_Created) = day(@DateCreated))
    SELECT @RET = 1
  ELSE
    SELECT @RET = 0

  RETURN(@RET)
END


