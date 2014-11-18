/*****************************************************************************/ 
/* Iteration 4 DB modification script                                        */
/*****************************************************************************/

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- Create SP: spOrder_getIlecsAll

CREATE PROCEDURE dbo.spOrder_getIlecsAll 
AS
    SELECT DISTINCT orgid, code, i.[externalname] AS [name], isdefault
    FROM ilecs i 
        JOIN location l1 ON l1.exclocowner=i.orgid
        JOIN location_composition lc ON lc.loc=l1.locid
        JOIN location l2 ON l2.locid=lc.subloc
    WHERE isprovisioning='T' AND isdefault='T'
    ORDER BY isdefault DESC, i.[externalname]
GO

CREATE PROCEDURE [dbo].[spAccountSummary_Get_Id]
    @AccNumber INT
AS

/* Uncomment for testing */
/*
DECLARE @AccNumber INT
SET @AccNumber = 50484327
*/

SELECT top 1
    cd.*,
    'PtpEligible' = 
        CASE 
            WHEN [dbo].fnPtp_IsEligible(cd.AccNumber) = 'T' THEN 'true'
            ELSE 'false'
        END,
    'IsActive' = 
        CASE
            WHEN [dbo].[fnCustomer_IsActive](0, @AccNumber) = 1 THEN 'true'
            ELSE 'false'
        END,    
    cna.Bill_Date AS 'BillDate',
    dmd.[Id] AS 'NewOrderDemandId',
    [dbo].[fnPastDueAmt](@AccNumber, getdate()) AS 'PastDueAmount',
    [dbo].[fnCurrentChargeAmt](@AccNumber, getdate()) AS 'CurrentDueAmount'
FROM CustData cd 
    left join Customer_Notice_Archive cna ON cd.AccNumber = cna.AccNumber
    left join [dbo].[Demand] dmd ON cd.AccNumber = dmd.BillPayer and dmd.DmdType = 'New'
WHERE cd.AccNumber = @AccNumber
ORDER BY
    cna.Bill_Date DESC
GO

-- Task 1569 - Do not show Other Services (Service Reconnect Fee -2 $40.00). We need to not show this product to the customers who are signing up on line. Here is a Example Zip Code 70001 (jduarte) 
begin transaction

insert into ExcludeProdRule
	select RuleId = 4, Descr, ExcProd, EffStartDate, EffEndDate from ExcludeProdRule
	where RuleId = 2

insert into ExcludeProdRule (RuleId, Descr, ExcProd, EffStartDate, EffEndDate)
	values (4, 'Excludes Service Reconnect Fee -2', 756, GETDATE(), NULL)

update Corporation
    SET ExcProdRule = 4
    WHERE CorpID = 751

commit transaction

GO

-- Task 1605 - The Description for The dPi Club Program will need to be changed to show the description in sections just like the The dPi Club Program, Gold Package. The description is table driven from the Product table the column names are Description and WebDescriptio
update Product 
	set WebDescription = 'Involuntary Unemployment Insurance:<br>If you become involuntarily unemployed dPi TeleConnect will waive your monthly payments for up to 3 months subject to the provisions of the program (1-888-600-4436).<br><br>Grocery Coupon Savings Book:<br>Get valuable coupons on the products you buy every day. Use them at any grocery store and save over $500 every year. Select from more than 1,000 brand name items.<br><br>Debt and Credit Counseling Services:<br>If you are currently living paycheck to paycheck or if credit cards bills are weighing you down? Then speak with one of our Credit Counselors and we will be happy to assist you (1-800-285-8546 ID Code: dPI).'
	where Id = 253

update Product 
	set Description = 'Involuntary Unemployment Insurance:<br>If you become involuntarily unemployed dPi TeleConnect will waive your monthly payments for up to 3 months subject to the provisions of the program (1-888-600-4436).<br><br>Grocery Coupon Savings Book:<br>Get valuable coupons on the products you buy every day. Use them at any grocery store and save over $500 every year. Select from more than 1,000 brand name items.<br><br>Debt and Credit Counseling Services:<br>If you are currently living paycheck to paycheck or if credit cards bills are weighing you down? Then speak with one of our Credit Counselors and we will be happy to assist you (1-800-285-8546 ID Code: dPI).'
	where Id = 253

GO
	
-- Add new coluns to CustData table (task 1867)

ALTER TABLE CustData
    ADD [VerbatumServiceAddress] [VARCHAR] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE CustData
    ADD [VerbatumMailingAddress] [VARCHAR] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL

GO
    
-- ALTER [dbo].[spCustData_Get_Id]

ALTER  PROCEDURE [dbo].[spCustData_Get_Id]
    @AccNumber int
AS
    SELECT
        [AccNumber],
        [ConfNum],
        [NameLast],
        [NameFirst],
        [CtNumber1],
        [CtNumber2],
        [AdrNum],
        [AdrNumSfx],
        [AdrPfx],
        [AdrStreet],
        [AdrStreetType],
        [AdrSfx],
        [AdrUnitDesc],
        [AdrUnit],
        [AdrElevation],
        [AdrFloor],
        [AdrStructureDesc],
        [AdrStructureNum],
        [AdrCity],
        [AdrState],
        [AdrZip],
        [Mail_AdrNum],
        [Mail_AdrNumSfx],
        [Mail_AdrPfx],
        [Mail_AdrStreet],
        [Mail_adrStreetType],
        [Mail_AdrSfx],
        [Mail_AdrUnitDesc],
        [Mail_AdrUnit],
        [Mail_AdrElevation],
        [Mail_AdrFloor],
        [Mail_AdrStructureDesc],
        [Mail_AdrStructureNum],
        [Mail_AdrCity],
        [Mail_AdrState],
        [Mail_AdrZip],
        [Complex],
        [PrevIlec],
        [PrevPHNum],
        [StoreCode],
        [PayDate],
        [PayTime],
        [PriceCode],
        [Ilec],
        [PhNumber],
        [ActivDate],
        [SDiscoDate],
        [ADiscoDate],
        [NOrder],
        [DOrder],
        [Status1],
        [Status3],
        [NxtPymnt],
        [Balance],
        [LstPymnt],
        [LstPayDate],
        [TotalPymnts],
        [Grace],
        [Reminder],
        [DayCredit],
        [PermCredit],
        [Bill_Initial],
        [Bill_One],
        [Bill_Two],
        [TaxCode],
        [WU_SwiftPay_ID],
        [Language],
        [Birthday],
        [Service_Month],
        [UNEP],
        [Due_Date],
        [Bill_Cycle],
        [PIC],
        [LPIC],
        [Email],
        [SourceCode],
        [WebPassword],
        [AdditionalInfo],
        [VerbatumServiceAddress],
        [VerbatumMailingAddress]
    FROM
        [dbo].[CustData]
    WHERE
        [AccNumber] = @AccNumber
GO

-- ALTER [dbo].[spCustData_Ins]

ALTER  PROCEDURE [dbo].[spCustData_Ins]
    @AccNumber int,
    @ConfNum varchar(20),
    @NameLast varchar(25),
    @NameFirst varchar(25),
    @CtNumber1 char(10),
    @CtNumber2 char(10),
    @AdrNum varchar(10),
    @AdrNumSfx varchar(10),
    @AdrPfx varchar(2),
    @AdrStreet varchar(50),
    @AdrStreetType char(4),
    @AdrSfx varchar(2),
    @AdrUnitDesc varchar(10),
    @AdrUnit varchar(8),
    @AdrElevation varchar(10),
    @AdrFloor varchar(10),
    @AdrStructureDesc varchar(10),
    @AdrStructureNum varchar(10),
    @AdrCity varchar(28),
    @AdrState char(2),
    @AdrZip char(5),
    @Mail_AdrNum varchar(10),
    @Mail_AdrNumSfx varchar(10),
    @Mail_AdrPfx varchar(2),
    @Mail_AdrStreet varchar(50),
    @Mail_adrStreetType char(4),
    @Mail_AdrSfx varchar(2),
    @Mail_AdrUnitDesc varchar(10),
    @Mail_AdrUnit varchar(8),
    @Mail_AdrElevation varchar(10),
    @Mail_AdrFloor varchar(10),
    @Mail_AdrStructureDesc varchar(10),
    @Mail_AdrStructureNum varchar(10),
    @Mail_AdrCity varchar(28),
    @Mail_AdrState char(2),
    @Mail_AdrZip char(5),
    @Complex varchar(50),
    @PrevIlec char(3),
    @PrevPHNum char(10),
    @StoreCode char(10),
    @PayDate datetime,
    @PayTime datetime,
    @PriceCode char(10),
    @Ilec varchar(3),
    @PhNumber char(10),
    @ActivDate datetime,
    @SDiscoDate datetime,
    @ADiscoDate datetime,
    @NOrder varchar(15),
    @DOrder varchar(15),
    @Status1 char(5),
    @Status3 char(5),
    @NxtPymnt money,
    @Balance money,
    @LstPymnt money,
    @LstPayDate datetime,
    @TotalPymnts money,
    @Grace int,
    @Reminder int,
    @DayCredit int,
    @PermCredit int,
    @Bill_Initial bit,
    @Bill_One bit,
    @Bill_Two bit,
    @TaxCode char(10),
    @WU_SwiftPay_ID varchar(16),
    @Language char(10),
    @Birthday varchar(10),
    @Service_Month int,
    @UNEP bit,
    @Due_Date datetime,
    @Bill_Cycle tinyint,
    @PIC varchar(20),
    @LPIC varchar(20),
    @Email varchar(50),
    @SourceCode int,
    @WebPassword varchar(25),
    @AdditionalInfo varchar(100),
    @VerbatumServiceAddress varchar(200) = null,
    @VerbatumMailingAddress varchar(200) = null
AS    
    BEGIN TRANSACTION

    INSERT INTO
        [dbo].[CustData]
    (
        [AccNumber],
        [ConfNum],
        [NameLast],
        [NameFirst],
        [CtNumber1],
        [CtNumber2],
        [AdrNum],
        [AdrNumSfx],
        [AdrPfx],
        [AdrStreet],
        [AdrStreetType],
        [AdrSfx],
        [AdrUnitDesc],
        [AdrUnit],
        [AdrElevation],
        [AdrFloor],
        [AdrStructureDesc],
        [AdrStructureNum],
        [AdrCity],
        [AdrState],
        [AdrZip],
        [Mail_AdrNum],
        [Mail_AdrNumSfx],
        [Mail_AdrPfx],
        [Mail_AdrStreet],
        [Mail_adrStreetType],
        [Mail_AdrSfx],
        [Mail_AdrUnitDesc],
        [Mail_AdrUnit],
        [Mail_AdrElevation],
        [Mail_AdrFloor],
        [Mail_AdrStructureDesc],
        [Mail_AdrStructureNum],
        [Mail_AdrCity],
        [Mail_AdrState],
        [Mail_AdrZip],
        [Complex],
        [PrevIlec],
        [PrevPHNum],
        [StoreCode],
        [PayDate],
        [PayTime],
        [PriceCode],
        [Ilec],
        [PhNumber],
        [ActivDate],
        [SDiscoDate],
        [ADiscoDate],
        [NOrder],
        [DOrder],
        [Status1],
        [Status3],
        [NxtPymnt],
        [Balance],
        [LstPymnt],
        [LstPayDate],
        [TotalPymnts],
        [Grace],
        [Reminder],
        [DayCredit],
        [PermCredit],
        [Bill_Initial],
        [Bill_One],
        [Bill_Two],
        [TaxCode],
        [WU_SwiftPay_ID],
        [Language],
        [Birthday],
        [Service_Month],
        [UNEP],
        [Due_Date],
        [Bill_Cycle],
        [PIC],
        [LPIC],
        [Email],
        [SourceCode],
        [WebPassword],
        [AdditionalInfo],
        [VerbatumServiceAddress],
        [VerbatumMailingAddress]
    )
    VALUES
    (
        @AccNumber,
        @ConfNum,
        @NameLast,
        @NameFirst,
        @CtNumber1,
        @CtNumber2,
        @AdrNum,
        @AdrNumSfx,
        @AdrPfx,
        @AdrStreet,
        @AdrStreetType,
        @AdrSfx,
        @AdrUnitDesc,
        @AdrUnit,
        @AdrElevation,
        @AdrFloor,
        @AdrStructureDesc,
        @AdrStructureNum,
        @AdrCity,
        @AdrState,
        @AdrZip,
        @Mail_AdrNum,
        @Mail_AdrNumSfx,
        @Mail_AdrPfx,
        @Mail_AdrStreet,
        @Mail_adrStreetType,
        @Mail_AdrSfx,
        @Mail_AdrUnitDesc,
        @Mail_AdrUnit,
        @Mail_AdrElevation,
        @Mail_AdrFloor,
        @Mail_AdrStructureDesc,
        @Mail_AdrStructureNum,
        @Mail_AdrCity,
        @Mail_AdrState,
        @Mail_AdrZip,
        @Complex,
        @PrevIlec,
        @PrevPHNum,
        @StoreCode,
        @PayDate,
        @PayTime,
        @PriceCode,
        @Ilec,
        @PhNumber,
        @ActivDate,
        @SDiscoDate,
        @ADiscoDate,
        @NOrder,
        @DOrder,
        @Status1,
        @Status3,
        @NxtPymnt,
        @Balance,
        @LstPymnt,
        @LstPayDate,
        @TotalPymnts,
        @Grace,
        @Reminder,
        @DayCredit,
        @PermCredit,
        @Bill_Initial,
        @Bill_One,
        @Bill_Two,
        @TaxCode,
        @WU_SwiftPay_ID,
        @Language,
        @Birthday,
        @Service_Month,
        @UNEP,
        @Due_Date,
        @Bill_Cycle,
        @PIC,
        @LPIC,
        @Email,
        @SourceCode,
        @WebPassword,
        @AdditionalInfo,
        @VerbatumServiceAddress,
        @VerbatumMailingAddress
    )
    
    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        End
    
    
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

-- ALTER [dbo].[spCustData_Upd]

ALTER  PROCEDURE [dbo].[spCustData_Upd]
    @AccNumber int,
    @ConfNum varchar(20),
    @NameLast varchar(25),
    @NameFirst varchar(25),
    @CtNumber1 char(10),
    @CtNumber2 char(10),
    @AdrNum varchar(10),
    @AdrNumSfx varchar(10),
    @AdrPfx varchar(2),
    @AdrStreet varchar(50),
    @AdrStreetType char(4),
    @AdrSfx varchar(2),
    @AdrUnitDesc varchar(10),
    @AdrUnit varchar(8),
    @AdrElevation varchar(10),
    @AdrFloor varchar(10),
    @AdrStructureDesc varchar(10),
    @AdrStructureNum varchar(10),
    @AdrCity varchar(28),
    @AdrState char(2),
    @AdrZip char(5),
    @Mail_AdrNum varchar(10),
    @Mail_AdrNumSfx varchar(10),
    @Mail_AdrPfx varchar(2),
    @Mail_AdrStreet varchar(50),
    @Mail_adrStreetType char(4),
    @Mail_AdrSfx varchar(2),
    @Mail_AdrUnitDesc varchar(10),
    @Mail_AdrUnit varchar(8),
    @Mail_AdrElevation varchar(10),
    @Mail_AdrFloor varchar(10),
    @Mail_AdrStructureDesc varchar(10),
    @Mail_AdrStructureNum varchar(10),
    @Mail_AdrCity varchar(28),
    @Mail_AdrState char(2),
    @Mail_AdrZip char(5),
    @Complex varchar(50),
    @PrevIlec char(3),
    @PrevPHNum char(10),
    @StoreCode char(10),
    @PayDate datetime,
    @PayTime datetime,
    @PriceCode char(10),
    @Ilec varchar(3),
    @PhNumber char(10),
    @ActivDate datetime,
    @SDiscoDate datetime,
    @ADiscoDate datetime,
    @NOrder varchar(15),
    @DOrder varchar(15),
    @Status1 char(5),
    @Status3 char(5),
    @NxtPymnt money,
    @Balance money,
    @LstPymnt money,
    @LstPayDate datetime,
    @TotalPymnts money,
    @Grace int,
    @Reminder int,
    @DayCredit int,
    @PermCredit int,
    @Bill_Initial bit,
    @Bill_One bit,
    @Bill_Two bit,
    @TaxCode char(10),
    @WU_SwiftPay_ID varchar(16),
    @Language char(10),
    @Birthday varchar(10),
    @Service_Month int,
    @UNEP bit,
    @Due_Date datetime,
    @Bill_Cycle tinyint,
    @PIC varchar(20),
    @LPIC varchar(20),
    @Email varchar(50),
    @SourceCode int,
    @WebPassword varchar(25),
    @AdditionalInfo varchar(100),
    @VerbatumServiceAddress varchar(200) = null,
    @VerbatumMailingAddress varchar(200) = null
AS    
    BEGIN TRANSACTION

    UPDATE
        [dbo].[CustData]
    SET
        [AccNumber] = @AccNumber,
        [ConfNum] = @ConfNum,
        [NameLast] = @NameLast,
        [NameFirst] = @NameFirst,
        [CtNumber1] = @CtNumber1,
        [CtNumber2] = @CtNumber2,
        [AdrNum] = @AdrNum,
        [AdrNumSfx] = @AdrNumSfx,
        [AdrPfx] = @AdrPfx,
        [AdrStreet] = @AdrStreet,
        [AdrStreetType] = @AdrStreetType,
        [AdrSfx] = @AdrSfx,
        [AdrUnitDesc] = @AdrUnitDesc,
        [AdrUnit] = @AdrUnit,
        [AdrElevation] = @AdrElevation,
        [AdrFloor] = @AdrFloor,
        [AdrStructureDesc] = @AdrStructureDesc,
        [AdrStructureNum] = @AdrStructureNum,
        [AdrCity] = @AdrCity,
        [AdrState] = @AdrState,
        [AdrZip] = @AdrZip,
        [Mail_AdrNum] = @Mail_AdrNum,
        [Mail_AdrNumSfx] = @Mail_AdrNumSfx,
        [Mail_AdrPfx] = @Mail_AdrPfx,
        [Mail_AdrStreet] = @Mail_AdrStreet,
        [Mail_adrStreetType] = @Mail_adrStreetType,
        [Mail_AdrSfx] = @Mail_AdrSfx,
        [Mail_AdrUnitDesc] = @Mail_AdrUnitDesc,
        [Mail_AdrUnit] = @Mail_AdrUnit,
        [Mail_AdrElevation] = @Mail_AdrElevation,
        [Mail_AdrFloor] = @Mail_AdrFloor,
        [Mail_AdrStructureDesc] = @Mail_AdrStructureDesc,
        [Mail_AdrStructureNum] = @Mail_AdrStructureNum,
        [Mail_AdrCity] = @Mail_AdrCity,
        [Mail_AdrState] = @Mail_AdrState,
        [Mail_AdrZip] = @Mail_AdrZip,
        [Complex] = @Complex,
        [PrevIlec] = @PrevIlec,
        [PrevPHNum] = @PrevPHNum,
        [StoreCode] = @StoreCode,
        [PayDate] = @PayDate,
        [PayTime] = @PayTime,
        [PriceCode] = @PriceCode,
        [Ilec] = @Ilec,
        [PhNumber] = @PhNumber,
        [ActivDate] = @ActivDate,
        [SDiscoDate] = @SDiscoDate,
        [ADiscoDate] = @ADiscoDate,
        [NOrder] = @NOrder,
        [DOrder] = @DOrder,
        [Status1] = @Status1,
        [Status3] = @Status3,
        [NxtPymnt] = @NxtPymnt,
        [Balance] = @Balance,
        [LstPymnt] = @LstPymnt,
        [LstPayDate] = @LstPayDate,
        [TotalPymnts] = @TotalPymnts,
        [Grace] = @Grace,
        [Reminder] = @Reminder,
        [DayCredit] = @DayCredit,
        [PermCredit] = @PermCredit,
        [Bill_Initial] = @Bill_Initial,
        [Bill_One] = @Bill_One,
        [Bill_Two] = @Bill_Two,
        [TaxCode] = @TaxCode,
        [WU_SwiftPay_ID] = @WU_SwiftPay_ID,
        [Language] = @Language,
        [Birthday] = @Birthday,
        [Service_Month] = @Service_Month,
        [UNEP] = @UNEP,
        [Due_Date] = @Due_Date,
        [Bill_Cycle] = @Bill_Cycle,
        [PIC] = @PIC,
        [LPIC] = @LPIC,
        [Email] = @Email,
        [SourceCode] = @SourceCode,
        [WebPassword] = @WebPassword,
        [AdditionalInfo] = @AdditionalInfo,
        [VerbatumServiceAddress] = @VerbatumServiceAddress,
        [VerbatumMailingAddress] = @VerbatumMailingAddress
    WHERE
        [AccNumber] = @AccNumber
    
    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        End
    
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

-- Create SP: [dbo].[spPayment_Get_AllByAccNumber]

CREATE PROCEDURE [dbo].[spPayment_Get_AllByAccNumber]
    @AccNumber INT
AS
    SELECT
        ccp.[CreditCard_ID] AS [Id],
        ccp.[PaymentType],        
        ccp.[Amount],
        ccp.[DemandId] AS [DemandId],
        ccp.[Verifone_Transaction_ID] AS [PaymentTransactionId],
        ccp.[Address] AS [Address],
        ccp.[CardType] AS [CcType],
        ccp.[CardNumber] AS [CcNumber],
        ccp.[CvNumber] AS [CvNumber],
        ccp.[ExpYear],
        ccp.[ExpMonth],
        ccp.[AccNumber],     
        ccp.[Date_Created] AS [PaymentDate], 
        ccp.[BankAccountNumber], 
        ccp.[BankRoutingNumber],
        ccp.[DriverLicenseNumber],
        ccp.[DriverLicenseState],        
        ccp.[NameFirst], 
        ccp.[NameLast], 
        ccp.[Address], 
        ccp.[Zip],        
        ccp.[Merchant_ID] AS [MerchantId], 
        ccp.[PaymentInfoId]
    FROM [dbo].[CreditCard_Payment] ccp 
    WHERE [AccNumber] = @AccNumber 

GO

	
-- Changed by DPI developers - it is just synchronization

ALTER TABLE CustAddress
    ADD [CLLI] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE CustAddress
    ADD [NPANXX] [VARCHAR] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL

GO

ALTER  PROCEDURE [dbo].[spCustAddress_Ins]
    @AddressID int OUT,
    @AdrStatus varchar(20),
    @AdrType int,
    @StreetNum varchar(10),
    @StreetPrefix varchar(7),
    @Street varchar(50),
    @StreetType varchar(4),
    @StreetSuffix varchar(50),
    @Unit varchar(8),
    @UnitType varchar(10),
    @City varchar(28),
    @State varchar(15),
    @Zipcode varchar(10),
    @CLLI varchar(20) = null,
    @NPANXX varchar(20) = null
AS    
    BEGIN TRANSACTION  

    INSERT INTO
        [dbo].[CustAddress]
    (
        [AdrStatus],
        [AdrType],
        [StreetNum],
        [StreetPrefix],
        [Street],
        [StreetType],
        [StreetSuffix],
        [Unit],
        [UnitType],
        [City],
        [State],
        [Zipcode],
        [CLLI],
        [NPANXX]
    )
    VALUES
    (
        @AdrStatus,
        @AdrType,
        @StreetNum,
        @StreetPrefix,
        @Street,
        @StreetType,
        @StreetSuffix,
        @Unit,
        @UnitType,
        @City,
        @State,
        @Zipcode,
        @CLLI,
        @NPANXX
    )
    
    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        End
    
    SELECT @AddressID= CAST(SCOPE_IDENTITY() AS int)
    
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

ALTER  PROCEDURE [dbo].[spCustAddress_Upd]
    @AddressID int,
    @AdrStatus varchar(20),
    @AdrType int,
    @StreetNum varchar(10),
    @StreetPrefix varchar(7),
    @Street varchar(50),
    @StreetType varchar(4),
    @StreetSuffix varchar(50),
    @Unit varchar(8),
    @UnitType varchar(10),
    @City varchar(28),
    @State varchar(15),
    @Zipcode varchar(10),
    @CLLI varchar(20),
    @NPANXX varchar(20)
AS    
    BEGIN TRANSACTION  

    UPDATE
        [dbo].[CustAddress]
    SET
        [AdrStatus] = @AdrStatus,
        [AdrType] = @AdrType,
        [StreetNum] = @StreetNum,
        [StreetPrefix] = @StreetPrefix,
        [Street] = @Street,
        [StreetType] = @StreetType,
        [StreetSuffix] = @StreetSuffix,
        [Unit] = @Unit,
        [UnitType] = @UnitType,
        [City] = @City,
        [State] = @State,
        [Zipcode] = @Zipcode,
        [CLLI]      = @CLLI,
        [NPANXX] = @NPANXX
    
    WHERE
        [AddressID] = @AddressID
    
    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        End
    
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

ALTER  PROCEDURE [dbo].[spCustAddress_Get_Id]
    @AddressID int
AS
    SELECT
        [AddressID],
        [AdrStatus],
        [AdrType],
        [StreetNum],
        [StreetPrefix],
        [Street],
        [StreetType],
        [StreetSuffix],
        [Unit],
        [UnitType],
        [City],
        [State],
        [Zipcode],
        [CLLI],
        [NPANXX]
    FROM
        [dbo].[CustAddress]
    WHERE
        [AddressID] = @AddressID

GO

ALTER TABLE CustInfo
    ADD [Language] [VARCHAR] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE CustInfo
    ADD [ActivationDate] [DATETIME] NULL

GO

ALTER   PROCEDURE [dbo].[spCustInfo_Ins]
    @CustInfoID int OUT,
    @CustInfoType varchar(20),
    @Status varchar(20),
    @LastName varchar(30),
    @FirstName varchar(25),
    @Birthday varchar(25),
    @Email varchar(50),
    @Contact varchar(50),
    @Contact2 varchar(50),
    @PrevPhone varchar(15),
    @PrevILEC char(3),
    @ServAddID int,
    @MailAddID int,
    @AccNumber int,
    @IDNumber char(20),
    @IDExpirationDate  datetime,
    @SSN   char(9),
    @DOB datetime,
    @IDType char(30),
    @IDState char(10),
    @PhNumber char(10),
    @Language varchar(15) = null,
    @ActivationDate datetime = null
AS
    
    BEGIN TRANSACTION
    


    INSERT INTO
        [dbo].[CustInfo]
    (
        [CustInfoType],
        [Status],
        [LastName],
        [FirstName],
        [Birthday],
        [Email],
        [Contact],
        [Contact2],
        [PrevPhone],
        [PrevILEC],
        [ServAddID],
        [MailAddID],
        [AccNumber],
        [IDNumber],
        [IDExpirationDate],
        [SSN],
        [DOB],
        [IDType],
        [IDState],
        [PhNumber],
        [Language],
        [ActivationDate]
    )
    VALUES
    (
        @CustInfoType,
        @Status,
        @LastName,
        @FirstName,
        @Birthday,
        @Email,
        @Contact,
        @Contact2,
        @PrevPhone,
        @PrevILEC,
        @ServAddID,
        @MailAddID,
        @AccNumber,
        @IDNumber,
        @IDExpirationDate,
        @SSN,
        @DOB,
        @IDType,
        @IDState,
        @PhNumber,
        @Language,
        @ActivationDate
    )
    
    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        End
    
    SELECT @CustInfoID= CAST(SCOPE_IDENTITY() AS int)
    
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

ALTER   PROCEDURE [dbo].[spCustInfo_Upd]
    @CustInfoID int,
    @CustInfoType varchar(20),
    @Status varchar(20),
    @LastName varchar(30),
    @FirstName varchar(25),
    @Birthday varchar(25),
    @Email varchar(50),
    @Contact varchar(50),
    @Contact2 varchar(50),
    @PrevPhone varchar(15),
    @PrevILEC char(3),
    @ServAddID int,
    @MailAddID int,
    @AccNumber int,
    @IDNumber char(20),
    @IDExpirationDate  datetime,
    @SSN   char(9),
    @DOB datetime,
    @IDType char(30),
    @IDState char(10),
    @PhNumber char(10),
    @Language varchar(15) = null,
    @ActivationDate datetime = null
AS
    
    BEGIN TRANSACTION    


    UPDATE
        [dbo].[CustInfo]
    SET
        [CustInfoType] = @CustInfoType,
        [Status] = @Status,
        [LastName] = @LastName,
        [FirstName] = @FirstName,
        [Birthday] = @Birthday,
        [Email] = @Email,
        [Contact] = @Contact,
        [Contact2] = @Contact2,
        [PrevPhone] = @PrevPhone,
        [PrevILEC] = @PrevILEC,
        [ServAddID] = @ServAddID,
        [MailAddID] = @MailAddID,
        [AccNumber] = @AccNumber,
        [IDNumber] = @IDNumber,
        [IDExpirationDate] = @IDExpirationDate,
        [SSN] = @SSN,
        [DOB] = @DOB,
        [IDType] = @IDType,
        [IDState] = @IDState,
        [PhNumber] = @PhNumber,
        [Language] = @Language,
        [ActivationDate] = @ActivationDate

    WHERE
        [CustInfoID] = @CustInfoID
    
    IF @@ROWCOUNT <> 1
        BEGIN
            GOTO HandleError
        End
    
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

ALTER  PROCEDURE [dbo].[spCustInfo_Get_Id]
    @CustInfoID int
AS
    SELECT
        [CustInfoID],
        [CustInfoType],
        [Status],
        [LastName],
        [FirstName],
        [Birthday],
        [Email],
        [Contact],
        [Contact2],
        [PrevPhone],
        [PrevILEC],
        [ServAddID],
        [MailAddID],
        [AccNumber],
        [IDNumber],
        [IDExpirationDate],	
        [SSN],	
        [DOB],
        [IDType],
        [IDState],
        [PhNumber],
        [Language],
        [ActivationDate]


    FROM
        [dbo].[CustInfo]
    WHERE
        [CustInfoID] = @CustInfoID
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
