----------------------------------------------------------------------------------------------
----------------------------------------------TABLES------------------------------------------
----------------------------------------------------------------------------------------------

CREATE TABLE [Company] (
	[CompanyId] [int] NOT NULL ,
	[IsSampleCompany] [bit] NOT NULL ,
	[CompanyName] [nvarchar] (100) NULL ,
	[LegalCompanyName] [nvarchar] (100) NULL ,
	[Addr1] [nvarchar] (500) NULL ,
	[Addr2] [nvarchar] (500) NULL ,
	[Addr3] [nvarchar] (500) NULL ,
	[Addr4] [nvarchar] (500) NULL ,
	[City] [nvarchar] (255) NULL ,
	[State] [nvarchar] (255) NULL ,
	[PostalCode] [nvarchar] (30) NULL ,
	[Country] [nvarchar] (255) NULL ,
	[LegalAddr1] [nvarchar] (500) NULL ,
	[LegalAddr2] [nvarchar] (500) NULL ,
	[LegalAddr3] [nvarchar] (500) NULL ,
	[LegalAddr4] [nvarchar] (500) NULL ,
	[LegalCity] [nvarchar] (255) NULL ,
	[LegalState] [nvarchar] (255) NULL ,
	[LegalPostalCode] [nvarchar] (30) NULL ,
	[LegalCountry] [nvarchar] (255) NULL ,
	[ForCustomerAddr1] [nvarchar] (500) NULL ,
	[ForCustomerAddr2] [nvarchar] (500) NULL ,
	[ForCustomerAddr3] [nvarchar] (500) NULL ,
	[ForCustomerAddr4] [nvarchar] (500) NULL ,
	[ForCustomerCity] [nvarchar] (255) NULL ,
	[ForCustomerState] [nvarchar] (255) NULL ,
	[ForCustomerPostalCode] [nvarchar] (30) NULL ,
	[ForCustomerCountry] [nvarchar] (255) NULL ,
	[Phone] [nvarchar] (20) NULL ,
	[Email] [nvarchar] (100) NULL ,
	[CompanyEmailForCustomer] [nvarchar] (100) NULL ,
	[FirstMonthFiscalYear] [nvarchar] (20) NULL ,
	[FirstMonthIncomeTaxYear] [nvarchar] (20) NULL ,
	[CompanyType] [nvarchar] (255) NULL
) 
GO

CREATE TABLE [TransactionType] (
	[TransactionTypeId] [int] NOT NULL ,
	[TransactionTypeDescription] [nvarchar] (100) NOT NULL
) 
GO

CREATE TABLE [CreditCardType] (
	[CreditCardTypeId] [int] NOT NULL ,
	[CreditCardTypeDescription] [nvarchar] (6) NOT NULL
) 
GO

CREATE TABLE [CreditCard] (
	[CreditCardId] [int] NOT NULL ,
	[CreditCardTypeId] [int] NOT NULL ,
	[QuickBooksTxnId] [int] NULL ,
	[EntityStateId] [int] NOT NULL ,
	[EditSequence] [int] NOT NULL ,
	[TimeCreated] [datetime] NULL ,
	[TimeModified] [datetime] NULL ,
	[TxnNumber] [int] NULL ,
	[TxnDate] [datetime] NULL ,
	[AccountId] [int] NOT NULL ,
	[PayeeQBEntityId] [int] NULL ,
	[RefNumber] [nvarchar] (21) NULL ,
	[Amount] [money] NULL ,
	[Memo] [nvarchar] (4000) NULL
) 
GO

CREATE TABLE [CreditCardExpenceLine] (
	[CreditCardExpenceLineId] [int] NOT NULL ,
	[CreditCardId] [int] NOT NULL ,
	[TxnLineID] [int] NULL ,
	[AccountId] [int] NULL ,
	[Amount] [money] NULL ,
	[Memo] [nvarchar] (4000) NULL ,
	[CustomerId] [int] NULL
)
GO

CREATE TABLE [Account] (
	[AccountId] [int] NOT NULL ,
	[QuickBooksListId] [int] NULL ,
	[EntityStateId] [int] NOT NULL ,
	[EditSequence] [int] NOT NULL ,
	[Name] [nvarchar] (100)  NOT NULL ,
	[FullName] [nvarchar] (1000)  NULL ,
	[AccountTypeId] [int] NOT NULL ,
	[DetailAccountTypeId] [int] NULL ,
	[AccountNumber] [nvarchar] (7)  NULL ,
	[LastCheckNumber] [nvarchar] (21)  NULL ,
	[Descriptive] [nvarchar] (100)  NULL ,
	[Balance] [money] NULL ,
	[TotalBalance] [money] NULL 
)  
GO

CREATE TABLE [AccountType] (
	[AccountTypeId] [int] NOT NULL ,
	[AccountTypeDescription] [nvarchar] (100)  NOT NULL 
)  
GO

CREATE TABLE [Check] (
	[CheckId] [int] NOT NULL ,
	[QuickBooksTxnId] [int] NULL ,
	[EntityStateId] [int] NOT NULL ,
	[EditSequence] [int] NOT NULL ,
	[TimeCreated] [datetime] NULL ,
	[TimeModified] [datetime] NULL ,
	[TxnNumber] [int] NULL ,
	[TxnDate] [datetime] NULL ,
	[AccountId] [int] NOT NULL ,
	[PayeeQBEntityId] [int] NULL ,
	[RefNumber] [nvarchar] (21)  NULL ,
	[Amount] [money] NULL ,
	[Memo] [nvarchar] (4000)  NULL ,
	[Addr1] [nvarchar] (500)  NULL ,
	[Addr2] [nvarchar] (500)  NULL ,
	[Addr3] [nvarchar] (500)  NULL ,
	[Addr4] [nvarchar] (500)  NULL ,
	[City] [nvarchar] (255)  NULL ,
	[State] [nvarchar] (255)  NULL ,
	[PostalCode] [nvarchar] (30)  NULL ,
	[Country] [nvarchar] (255)  NULL ,
	[IsToBePrinted] [bit] NULL 
)  
GO

CREATE TABLE [Counter] (
	[CounterName] [nvarchar] (40) NOT NULL ,
	[Val] [int] NOT NULL 
)  
GO

CREATE TABLE [Customer] (
	[CustomerId] [int] NOT NULL ,
	[ModifiedCustomerId] [int] NULL ,
	[QuickBooksListId] [int] NULL ,
	[EntityStateId] [int] NOT NULL ,
	[TermsId] [int] NULL ,
	[EditSequence] [int] NOT NULL ,
	[Name] [nvarchar] (100) NOT NULL ,	
	[FullName] [nvarchar] (255)  NOT NULL ,
	[Salutation] [nvarchar] (15)  NOT NULL ,
	[FirstName] [nvarchar] (50)  NOT NULL ,
	[MiddleName] [nvarchar] (50)  NOT NULL ,
	[LastName] [nvarchar] (50)  NOT NULL ,
	[Suffix] [nvarchar] (10)  NOT NULL ,
	[CompanyName] [nvarchar] (50)  NULL ,
	[Phone] [nvarchar] (21)  NULL ,
	[Mobile] [nvarchar] (21)  NULL ,
	[Email] [nvarchar] (100)  NULL ,
	[Pager] [nvarchar] (21)  NULL ,
	[AltPhone] [nvarchar] (21)  NULL ,
	[Fax] [nvarchar] (21)  NULL ,
	[BillAddr1] [nvarchar] (500)  NULL ,
	[BillAddr2] [nvarchar] (500)  NULL ,
	[BillAddr3] [nvarchar] (500)  NULL ,
	[BillAddr4] [nvarchar] (500)  NULL ,
	[BillCity] [nvarchar] (255)  NULL ,
	[BillState] [nvarchar] (255)  NULL ,
	[BillPostalCode] [nvarchar] (30)  NULL ,
	[BillCountry] [nvarchar] (255)  NULL ,
	[ShipAddr1] [nvarchar] (500)  NULL ,
	[ShipAddr2] [nvarchar] (500)  NULL ,
	[ShipAddr3] [nvarchar] (500)  NULL ,
	[ShipAddr4] [nvarchar] (500)  NULL ,
	[ShipCity] [nvarchar] (255)  NULL ,
	[ShipState] [nvarchar] (255)  NULL ,
	[ShipPostalCode] [nvarchar] (30)  NULL ,
	[ShipCountry] [nvarchar] (255)  NULL ,
	[ShippingAddressSameAsBilling] [bit] NOT NULL ,
	[PrintAs] [nvarchar] (110) NULL ,
	[Balance] [money] NULL ,
	[BalanceDate] [datetime] NULL ,
	[ResaleNumber] [nvarchar] (16)  NULL ,
	[DeliveryMethod] [nvarchar] (16)  NOT NULL 
)  
GO

CREATE TABLE [DetailAccountType] (
	[DetailAccountTypeId] [int] NOT NULL ,
	[DetailAccountTypeDescription] [nvarchar] (100)  NOT NULL 
)  
GO

CREATE TABLE [Employee] (
	[EmployeeId] [int] NOT NULL ,
	[ModifiedEmployeeId] [int] NULL ,
	[QuickBooksListId] [int] NULL ,
	[EntityStateId] [int] NOT NULL ,
	[EditSequence] [int] NOT NULL ,
	[Name] [nvarchar] (100)  NULL ,
	[Salutation] [nvarchar] (15)  NULL ,
	[FirstName] [nvarchar] (25)  NULL ,
	[MiddleName] [nvarchar] (25)  NULL ,
	[LastName] [nvarchar] (25)  NULL ,
	[Suffix] [nvarchar] (10)  NULL ,
	[Addr1] [nvarchar] (500)  NULL ,
	[Addr2] [nvarchar] (500)  NULL ,
	[Addr3] [nvarchar] (500)  NULL ,
	[Addr4] [nvarchar] (500)  NULL ,
	[City] [nvarchar] (255)  NULL ,
	[State] [nvarchar] (255)  NULL ,
	[PostalCode] [nvarchar] (30)  NULL ,
	[Country] [nvarchar] (255)  NULL ,
	[PrintAs] [nvarchar] (110)  NULL ,
	[Phone] [nvarchar] (21)  NULL ,
	[Mobile] [nvarchar] (21)  NULL ,
	[AltPhone] [nvarchar] (21)  NULL ,
	[Email] [nvarchar] (100)  NULL ,
	[HiredDate] [datetime] NULL ,
	[ReleasedDate] [datetime] NULL 
)  
GO

CREATE TABLE [EntityState] (
	[EntityStateId] [int] NOT NULL ,
	[Name] [nvarchar] (100)  NOT NULL 
)  
GO

CREATE TABLE [EventLog] (
	[EventLogId] [int] NOT NULL ,
	[EventType] [int] NOT NULL ,
	[Message] [nvarchar] (2000) NOT NULL ,
	[Source] [nvarchar] (100) NULL ,
	[AssemblyName] [nvarchar] (100) NULL ,
	[CreateDate] [datetime] NOT NULL 
)  
GO

CREATE TABLE [CheckExpenceLine] (
	[CheckExpenceLineId] [int] NOT NULL ,
	[CheckId] [int] NOT NULL ,
	[TxnLineID] [int] NULL ,
	[AccountId] [int] NULL ,
	[Amount] [money] NULL ,
	[Memo] [nvarchar] (4000)  NULL ,
	[CustomerId] [int] NULL 
)  
GO

CREATE TABLE [Invoice] (
	[InvoiceId] [int] NOT NULL ,
	[QuickBooksTxnId] [int] NULL ,
	[EntityStateId] [int] NOT NULL ,
	[EditSequence] [int] NOT NULL ,
	[TimeCreated] [datetime] NULL ,
	[TimeModified] [datetime] NULL ,
	[TxnNumber] [int] NULL ,
	[CustomerId] [int] NOT NULL ,
	[ARAccountId] [int] NULL ,
	[TxnDate] [datetime] NULL ,
	[RefNumber] [nvarchar] (21) NULL ,
	[BillAddr1] [nvarchar] (500) NULL ,
	[BillAddr2] [nvarchar] (500) NULL ,
	[BillAddr3] [nvarchar] (500) NULL ,
	[BillAddr4] [nvarchar] (500) NULL ,
	[BillCity] [nvarchar] (255) NULL ,
	[BillState] [nvarchar] (255) NULL ,
	[BillPostalCode] [nvarchar] (30) NULL ,
	[BillCountry] [nvarchar] (255) NULL ,
	[ShipAddr1] [nvarchar] (500) NULL ,
	[ShipAddr2] [nvarchar] (500) NULL ,
	[ShipAddr3] [nvarchar] (500) NULL ,
	[ShipAddr4] [nvarchar] (500) NULL ,
	[ShipCity] [nvarchar] (255) NULL ,
	[ShipState] [nvarchar] (255) NULL ,
	[ShipPostalCode] [nvarchar] (30) NULL ,
	[ShipCountry] [nvarchar] (255) NULL ,
	[TermsId] [int] NULL ,
	[DueDate] [datetime] NULL ,
	[ShipDate] [datetime] NULL ,
	[Subtotal] [money] NULL ,
	[SalesTaxPercentage] [numeric] (38, 5) NULL ,
	[SalesTaxTotal] [money] NULL ,
	[AppliedAmount] [money] NULL ,
	[BalanceRemaining] [money] NULL ,
	[Memo] [nvarchar] (4000) NULL ,
	[IsPaid] [bit] NULL ,
	[IsToBePrinted] [bit] NULL ,
	[DiscountLineAmount] [money] NULL ,
	[DiscountLineRatePercent] [numeric] (38, 5) NULL ,
	[DiscountLineIsTaxable] [bit] NULL ,
	[DiscountLineAccountId] [int] NULL ,
	[SalesTaxLineAmount] [money] NULL ,
	[SalesTaxLineRatePercent] [numeric] (38, 5) NULL ,
	[SalesTaxLineAccountId] [int] NULL ,
	[ShippingLineAmount] [money] NULL ,
	[ShippingLineAccountId] [int] NULL ,
	[IsCustomerTaxable] [bit] NULL , 
	[TaxCalculationType] [bit] NULL
)  
GO

CREATE TABLE [InvoiceLine] (
	[InvoiceLineId] [int] NOT NULL ,
	[InvoiceId] [int] NOT NULL ,
	[QuickBooksTxnLineId] [int] NULL ,
	[ItemId] [int] NULL ,
	[LineDescription] [nvarchar] (4000) NULL ,
	[Quantity] [numeric] (38, 5) NULL ,
	[Rate] [numeric] (38, 5) NULL ,
	[RatePercent] [numeric] (38, 5) NULL ,
	[Amount] [money] NULL ,
	[ServiceDate] [datetime] NULL ,
	[IsTaxable] [bit] NULL
) 
GO

CREATE TABLE [InvoiceTransaction] (
	[InvoiceTransactionId] [int] NOT NULL ,
	[InvoiceId] [int] NOT NULL ,
	[QuickBooksTxnId] [int] NOT NULL ,
	[TransactionTypeId] [int] NOT NULL ,
	[TransactionDate] [datetime] NOT NULL ,
	[RefNumber] [nvarchar] (21) NULL ,
	[Amount] [money] NOT NULL
) 
GO

CREATE TABLE [InvoiceTransactionLineDetail] (
	[InvoiceTransactionLineDetailId] [int] NOT NULL ,
	[InvoiceTransactionId] [int] NOT NULL ,
	[QuickBooksTxnLineID] [int] NOT NULL ,
	[Amount] [money] NOT NULL
)  
GO

CREATE TABLE [Item] (
	[ItemId] [int] NOT NULL ,
	[QuickBooksListId] [int] NOT NULL ,
	[EditSequence] [int] NOT NULL ,
	[Name] [nvarchar] (255)  NOT NULL ,
	[SalesPrice] [money] NOT NULL 
)  
GO

CREATE TABLE [QBEntity] (
	[QBEntityId] [int] NOT NULL ,
	[QBEntityTypeId] [tinyint] NOT NULL 
)  
GO

CREATE TABLE [QBEntityType] (
	[QBEntityTypeId] [tinyint] NOT NULL ,
	[QBEntityTypeDescription] [nvarchar] (50)  NOT NULL 
)  
GO

CREATE TABLE [Terms] (
	[TermsId] [int] NOT NULL ,
	[QuickBooksListId] [int] NOT NULL ,
	[Name] [nvarchar] (100)  NOT NULL ,
	[TimeCreated] [datetime] NULL ,
	[TimeModified] [datetime] NULL ,
	[EditSequence] [int] NOT NULL ,
	[StdDueDays] [int] NULL ,
	[StdDiscountDays] [int] NULL ,
	[DiscountPct] [money] NULL
)  
GO

CREATE TABLE [TimeTracking] (
	[TimeTrackingId] [int] NOT NULL ,
	[QuickBooksTxnId] [int] NULL ,
	[EntityStateId] [int] NOT NULL ,
	[EditSequence] [int] NOT NULL ,
	[TimeCreated] [datetime] NULL ,
	[TimeModified] [datetime] NULL ,
	[TxnNumber] [int] NULL ,
	[TxnDate] [datetime] NULL ,
	[QBEntityId] [int] NOT NULL ,
	[CustomerId] [int] NULL ,
	[ItemId] [int] NULL ,
	[Rate] [money] NULL ,
	[Duration] [nvarchar] (50)  NOT NULL ,
	[Notes] [nvarchar] (4000)  NULL ,
	[IsBillable] [bit] NULL ,
	[IsBilled] [bit] NULL 
)  
GO

CREATE TABLE [Vendor] (
	[VendorId] [int] NOT NULL ,
	[ModifiedVendorId] [int] NULL ,
	[QuickBooksListId] [int] NULL ,
	[EntityStateId] [int] NOT NULL ,
	[EditSequence] [int] NOT NULL ,
	[Name] [nvarchar] (100)  NOT NULL ,
	[CompanyName] [nvarchar] (50)  NULL ,
	[Salutation] [nvarchar] (15)  NULL ,
	[FirstName] [nvarchar] (25)  NULL ,
	[MiddleName] [nvarchar] (25)  NULL ,
	[LastName] [nvarchar] (25)  NULL ,
	[Suffix] [nvarchar] (10)  NULL ,
	[Addr1] [nvarchar] (500)  NULL ,
	[Addr2] [nvarchar] (500)  NULL ,
	[Addr3] [nvarchar] (500)  NULL ,
	[Addr4] [nvarchar] (500)  NULL ,
	[City] [nvarchar] (255)  NULL ,
	[State] [nvarchar] (255)  NULL ,
	[PostalCode] [nvarchar] (30)  NULL ,
	[Country] [nvarchar] (255)  NULL ,
	[Phone] [nvarchar] (21)  NULL ,
	[Mobile] [nvarchar] (21)  NULL ,
	[Pager] [nvarchar] (21)  NULL ,
	[AltPhone] [nvarchar] (21)  NULL ,
	[Fax] [nvarchar] (21)  NULL ,
	[Email] [nvarchar] (100)  NULL ,
	[NameOnCheck] [nvarchar] (110)  NULL ,
	[VendorTaxIdent] [nvarchar] (20)  NULL ,
	[IsVendorEligibleFor1099] [bit] NULL ,
	[Balance] [money] NULL 
)  
GO


----------------------------------------------------------------------------------------------
----------------------------------------------PRIMARY KEYS------------------------------------
----------------------------------------------------------------------------------------------

ALTER TABLE [Company] ADD 
	CONSTRAINT [PK_Company] PRIMARY KEY 
	(
		[CompanyId]
	)  
GO

ALTER TABLE [TransactionType] ADD 
	CONSTRAINT [PK_TransactionType] PRIMARY KEY 
	(
		[TransactionTypeId]
	)  
GO

ALTER TABLE [CreditCardType] ADD 
	CONSTRAINT [PK_CreditCardType] PRIMARY KEY
	(
		[CreditCardTypeId]
	) 
GO

ALTER TABLE [CreditCard] ADD 
	CONSTRAINT [PK_CreditCard] PRIMARY KEY
	(
		[CreditCardId]
	) 
GO

ALTER TABLE [CreditCardExpenceLine] ADD 
	CONSTRAINT [PK_CreditCardExpenceLine] PRIMARY KEY
	(
		[CreditCardExpenceLineId]
	) 
GO

ALTER TABLE [Account]  ADD 
	CONSTRAINT [PK_Account] PRIMARY KEY   
	(
		[AccountId]
	)    
GO

ALTER TABLE [AccountType]  ADD 
	CONSTRAINT [PK_AccountType] PRIMARY KEY   
	(
		[AccountTypeId]
	)    
GO

ALTER TABLE [Check]  ADD 
	CONSTRAINT [PK_Check] PRIMARY KEY   
	(
		[CheckId]
	)    
GO

ALTER TABLE [Counter]  ADD 
	CONSTRAINT [PK_Counter] PRIMARY KEY   
	(
		[CounterName]
	)    
GO

ALTER TABLE [Customer]  ADD 
	CONSTRAINT [PK_Customer] PRIMARY KEY   
	(
		[CustomerId]
	)    
GO

ALTER TABLE [DetailAccountType]  ADD 
	CONSTRAINT [PK_DetailAccountType] PRIMARY KEY   
	(
		[DetailAccountTypeId]
	)    
GO

ALTER TABLE [Employee]  ADD 
	CONSTRAINT [PK_Employee] PRIMARY KEY   
	(
		[EmployeeId]
	)    
GO

ALTER TABLE [EntityState]  ADD 
	CONSTRAINT [PK_EntityState] PRIMARY KEY   
	(
		[EntityStateId]
	)    
GO

ALTER TABLE [EventLog]  ADD 
	CONSTRAINT [PK_EventLog] PRIMARY KEY   
	(
		[EventLogId]
	)    
GO

ALTER TABLE [CheckExpenceLine]  ADD 
	CONSTRAINT [PK_CheckExpenceLine] PRIMARY KEY   
	(
		[CheckExpenceLineId]
	)    
GO

ALTER TABLE [Invoice]  ADD 
	CONSTRAINT [PK_Invoice] PRIMARY KEY   
	(
		[InvoiceId]
	)    
GO

ALTER TABLE [InvoiceLine]  ADD 
	CONSTRAINT [PK_InvoiceLine] PRIMARY KEY   
	(
		[InvoiceLineId]
	)    
GO

ALTER TABLE [InvoiceTransaction] ADD 
	CONSTRAINT [PK_InvoiceTransaction] PRIMARY KEY
	(
		[InvoiceTransactionId]
	)
GO

ALTER TABLE [InvoiceTransactionLineDetail] ADD 
	CONSTRAINT [PK_InvoiceTransactionLineDetail] PRIMARY KEY
	(
		[InvoiceTransactionLineDetailId]
	)  
GO

ALTER TABLE [Item]  ADD 
	CONSTRAINT [PK_Item] PRIMARY KEY   
	(
		[ItemId]
	)    
GO

ALTER TABLE [QBEntity]  ADD 
	CONSTRAINT [PK_QBEntity] PRIMARY KEY   
	(
		[QBEntityId]
	)    
GO

ALTER TABLE [QBEntityType]  ADD 
	CONSTRAINT [PK_QBEntityType] PRIMARY KEY   
	(
		[QBEntityTypeId]
	)    
GO

ALTER TABLE [Terms]  ADD 
	CONSTRAINT [PK_Terms] PRIMARY KEY   
	(
		[TermsId]
	)    
GO

ALTER TABLE [TimeTracking]  ADD 
	CONSTRAINT [PK_TimeTracking] PRIMARY KEY   
	(
		[TimeTrackingId]
	)    
GO

ALTER TABLE [Vendor]  ADD 
	CONSTRAINT [PK_Vendor] PRIMARY KEY   
	(
		[VendorId]
	)    
GO

----------------------------------------------------------------------------------------------
----------------------------------------------FOREIGN KEYS------------------------------------
----------------------------------------------------------------------------------------------


ALTER TABLE [Account] ADD 
	CONSTRAINT [FK_Account_AccountType] FOREIGN KEY 
	(
		[AccountTypeId]
	) REFERENCES [AccountType] (
		[AccountTypeId]
	),
	CONSTRAINT [FK_Account_DetailAccountType] FOREIGN KEY 
	(
		[DetailAccountTypeId]
	) REFERENCES [DetailAccountType] (
		[DetailAccountTypeId]
	),
	CONSTRAINT [FK_Account_EntityState] FOREIGN KEY 
	(
		[EntityStateId]
	) REFERENCES [EntityState] (
		[EntityStateId]
	)
GO

ALTER TABLE [Check] ADD 
	CONSTRAINT [FK_Check_Account] FOREIGN KEY 
	(
		[AccountId]
	) REFERENCES [Account] (
		[AccountId]
	),
	CONSTRAINT [FK_Check_EntityState] FOREIGN KEY 
	(
		[EntityStateId]
	) REFERENCES [EntityState] (
		[EntityStateId]
	),
	CONSTRAINT [FK_Check_QBEntity] FOREIGN KEY 
	(
		[PayeeQBEntityId]
	) REFERENCES [QBEntity] (
		[QBEntityId]
	)
GO

ALTER TABLE [Customer] ADD 
	CONSTRAINT [FK_Customer_EntityState] FOREIGN KEY 
	(
		[EntityStateId]
	) REFERENCES [EntityState] (
		[EntityStateId]
	),
	CONSTRAINT [FK_Customer_Terms] FOREIGN KEY 
	(
		[TermsId]
	) REFERENCES [Terms] (
		[TermsId]
	)
GO

ALTER TABLE [Employee] ADD 
	CONSTRAINT [FK_Employee_EntityState] FOREIGN KEY 
	(
		[EntityStateId]
	) REFERENCES [EntityState] (
		[EntityStateId]
	)
GO

ALTER TABLE [CheckExpenceLine] ADD 
	CONSTRAINT [FK_CheckExpenceLine_Account] FOREIGN KEY 
	(
		[AccountId]
	) REFERENCES [Account] (
		[AccountId]
	),
	CONSTRAINT [FK_CheckExpenceLine_Check] FOREIGN KEY 
	(
		[CheckId]
	) REFERENCES [Check] (
		[CheckId]
	),
	CONSTRAINT [FK_CheckExpenceLine_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [Customer] (
		[CustomerId]
	)
GO

ALTER TABLE [Invoice] ADD 
	CONSTRAINT [FK_Invoice_Account] FOREIGN KEY 
	(
		[ARAccountId]
	) REFERENCES [Account] (
		[AccountId]
	),
	CONSTRAINT [FK_Invoice_Account1] FOREIGN KEY 
	(
		[DiscountLineAccountId]
	) REFERENCES [Account] (
		[AccountId]
	),
	CONSTRAINT [FK_Invoice_Account2] FOREIGN KEY 
	(
		[SalesTaxLineAccountId]
	) REFERENCES [Account] (
		[AccountId]
	),
	CONSTRAINT [FK_Invoice_Account3] FOREIGN KEY 
	(
		[ShippingLineAccountId]
	) REFERENCES [Account] (
		[AccountId]
	),
	CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [Customer] (
		[CustomerId]
	),
	CONSTRAINT [FK_Invoice_EntityState] FOREIGN KEY 
	(
		[EntityStateId]
	) REFERENCES [EntityState] (
		[EntityStateId]
	),
	CONSTRAINT [FK_Invoice_Terms] FOREIGN KEY 
	(
		[TermsId]
	) REFERENCES [Terms] (
		[TermsId]
	)
GO

ALTER TABLE [InvoiceLine] ADD 
	CONSTRAINT [FK_InvoiceLine_Invoice] FOREIGN KEY 
	(
		[InvoiceId]
	) REFERENCES [Invoice] (
		[InvoiceId]
	),
	CONSTRAINT [FK_InvoiceLine_Item] FOREIGN KEY 
	(
		[ItemId]
	) REFERENCES [Item] (
		[ItemId]
	)
GO

ALTER TABLE [InvoiceTransaction] ADD 
	CONSTRAINT [FK_InvoiceTransaction_Invoice] FOREIGN KEY 
	(
		[InvoiceId]
	) REFERENCES [Invoice] (
		[InvoiceId]
	),
	CONSTRAINT [FK_InvoiceTransaction_TransactionType] FOREIGN KEY 
	(
		[TransactionTypeId]
	) REFERENCES [TransactionType] (
		[TransactionTypeId]
	)
GO

ALTER TABLE [InvoiceTransactionLineDetail] ADD 
	CONSTRAINT [FK_InvoiceTransactionLineDetail_InvoiceTransaction] FOREIGN KEY 
	(
		[InvoiceTransactionId]
	) REFERENCES [InvoiceTransaction] (
		[InvoiceTransactionId]
	)
GO

ALTER TABLE [QBEntity] ADD 
	CONSTRAINT [FK_QBEntity_QBEntityType] FOREIGN KEY 
	(
		[QBEntityTypeId]
	) REFERENCES [QBEntityType] (
		[QBEntityTypeId]
	)
GO

ALTER TABLE [TimeTracking] ADD 
	CONSTRAINT [FK_TimeTracking_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [Customer] (
		[CustomerId]
	),
	CONSTRAINT [FK_TimeTracking_EntityState] FOREIGN KEY 
	(
		[EntityStateId]
	) REFERENCES [EntityState] (
		[EntityStateId]
	),
	CONSTRAINT [FK_TimeTracking_Item] FOREIGN KEY 
	(
		[ItemId]
	) REFERENCES [Item] (
		[ItemId]
	),
	CONSTRAINT [FK_TimeTracking_QBEntity] FOREIGN KEY 
	(
		[QBEntityId]
	) REFERENCES [QBEntity] (
		[QBEntityId]
	)
GO

ALTER TABLE [Vendor] ADD 
	CONSTRAINT [FK_Vendor_EntityState] FOREIGN KEY 
	(
		[EntityStateId]
	) REFERENCES [EntityState] (
		[EntityStateId]
	)
GO

ALTER TABLE [CreditCard] ADD 
	CONSTRAINT [FK_CreditCard_Account] FOREIGN KEY 
	(
		[AccountId]
	) REFERENCES [Account] (
		[AccountId]
	),
	CONSTRAINT [FK_CreditCard_CreditCardType] FOREIGN KEY 
	(
		[CreditCardTypeId]
	) REFERENCES [CreditCardType] (
		[CreditCardTypeId]
	),
	CONSTRAINT [FK_CreditCard_EntityState] FOREIGN KEY 
	(
		[EntityStateId]
	) REFERENCES [EntityState] (
		[EntityStateId]
	),
	CONSTRAINT [FK_CreditCard_QBEntity] FOREIGN KEY 
	(
		[PayeeQBEntityId]
	) REFERENCES [QBEntity] (
		[QBEntityId]
	)
GO

ALTER TABLE [CreditCardExpenceLine] ADD 
	CONSTRAINT [FK_CreditCardExpenceLine_Account] FOREIGN KEY 
	(
		[AccountId]
	) REFERENCES [Account] (
		[AccountId]
	),
	CONSTRAINT [FK_CreditCardExpenceLine_CreditCard] FOREIGN KEY 
	(
		[CreditCardId]
	) REFERENCES [CreditCard] (
		[CreditCardId]
	),
	CONSTRAINT [FK_CreditCardExpenceLine_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [Customer] (
		[CustomerId]
	)
GO
