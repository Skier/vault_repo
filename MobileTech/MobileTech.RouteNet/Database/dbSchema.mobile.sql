CREATE TABLE [BusinessTransaction] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[BusinessTransactionTypeId] [smallint] NOT NULL ,
	[BusinessTransactionStatusId] [smallint] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[PeriodIndex] [int] NOT NULL ,
	[EmployeeId] [int] NOT NULL ,
	[DocumentNumber] [nvarchar] (100)  NOT NULL ,
	[Odometer] [int] NULL ,
	[Password] [nvarchar] (6)  NOT NULL ,
	[DateCreated] [datetime] NOT NULL ,
	[DateCommited] [datetime] NULL 
) 
GO

CREATE TABLE [BusinessTransactionStatus] (
	[BusinessTransactionStatusId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NULL 
) 
GO

CREATE TABLE [BusinessTransactionType] (
	[BusinessTransactionTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NULL 
) 
GO

CREATE TABLE [Counter] (
	[CounterName] [nvarchar] (40)  NOT NULL ,
	[Val] [int] NOT NULL 
) 
GO

CREATE TABLE [Customer] (
	[CustomerId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [CustomerOption] (
	[CustomerId] [int] NOT NULL ,
	[OptionName] [nvarchar] (50)  NOT NULL ,
	[OptionValue] [int] NULL 
) 
GO

CREATE TABLE [CustomerOptionDescription] (
	[OptionName] [nvarchar] (50)  NOT NULL ,
	[OptionValue] [int] NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [CustomerTransaction] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[CustomerTransactionTypeId] [smallint] NOT NULL ,
	[CustomerVisitId] [int] NOT NULL 
) 
GO

CREATE TABLE [CustomerTransactionDetail] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[Quantity] [int] NOT NULL ,
	[DateCreated] [datetime] NOT NULL 
) 
GO

CREATE TABLE [CustomerTransactionType] (
	[CustomerTransactionTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NULL 
) 
GO

CREATE TABLE [CustomerVisit] (
	[SessionId] [bigint] NOT NULL ,
	[CustomerVisitId] [int] NOT NULL ,
	[CustomerId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[DateStarted] [datetime] NOT NULL ,
	[DateFinished] [datetime] NULL 
) 
GO

CREATE TABLE [DayOfWeek] (
	[Number] [tinyint] NOT NULL 
) 
GO

CREATE TABLE [Employee] (
	[LocationId] [int] NOT NULL ,
	[EmployeeId] [int] NOT NULL ,
	[FirstName] [nvarchar] (50)  NOT NULL ,
	[LastName] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [Equipment] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL 
) 
GO

CREATE TABLE [EventLog] (
	[SessionId] [bigint] NOT NULL ,
	[EventLogId] [int] NOT NULL ,
	[EventType] [int] NOT NULL ,
	[Message] [nvarchar] (2000)  NOT NULL ,
	[Source] [nvarchar] (100)  NULL ,
	[AssemblyName] [nvarchar] (100)  NULL ,
	[CreateDate] [datetime] NOT NULL 
) 
GO

CREATE TABLE [InventoryTransaction] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[InventoryTransactionTypeId] [smallint] NOT NULL 
) 
GO

CREATE TABLE [InventoryTransactionDetail] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[StorageTypeId] [int] NOT NULL ,
	[InventoryPeriodId] [int] NOT NULL ,
	[InventoryTransactionDetailTypeId] [smallint] NOT NULL ,
	[InventoryTransactionTypeId] [smallint] NULL ,
	[DateCreated] [datetime] NOT NULL ,
	[Quantity] [int] NOT NULL 
) 
GO

CREATE TABLE [InventoryTransactionDetailType] (
	[InventoryTransactionDetailTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [InventoryTransactionDetailXRef] (
	[InventoryTransactionDetailTypeId] [smallint] NOT NULL ,
	[InventoryTransactionTypeId] [smallint] NOT NULL 
) 
GO

CREATE TABLE [InventoryTransactionType] (
	[InventoryTransactionTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [Item] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL ,
	[ItemCategoryId] [int] NOT NULL ,
	[ItemTypeId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL ,
	[NameSortIndex] [int] NOT NULL ,
	[ItemNumberSortIndex] [int] NOT NULL 
) 
GO

CREATE TABLE [ItemCategory] (
	[ItemCategoryId] [int] NOT NULL ,
	[ItemTypeId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [ItemType] (
	[ItemTypeId] [int] NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [Location] (
	[LocationId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [Password] (
	[Functionality] [nvarchar] (56)  NOT NULL ,
	[PasswordId] [int] NOT NULL ,
	[PasswordValue] [nvarchar] (6)  NOT NULL 
) 
GO

CREATE TABLE [PeriodTransaction] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[PeriodTransactionTypeId] [smallint] NOT NULL 
) 
GO

CREATE TABLE [PeriodTransactionType] (
	[PeriodTransactionTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [Product] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL 
) 
GO

CREATE TABLE [Route] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[RouteStatusId] [smallint] NOT NULL ,
	[RouteTypeId] [smallint] NOT NULL ,
	[EmployeeId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NULL ,
	[DocumentNumberPrefix] [int] NOT NULL ,
	[DocumentNumberSequence] [int] NOT NULL ,
	[Active] [bit] NOT NULL ,
	[InventorySync] [bit] NOT NULL 
) 
GO

CREATE TABLE [RouteCustomer] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[CustomerId] [int] NOT NULL 
) 
GO

CREATE TABLE [RouteInventory] (
	[SessionId] [bigint] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL ,
	[StorageTypeId] [int] NOT NULL ,
	[InventoryPeriodId] [int] NOT NULL ,
	[ClosedIndicator] [bit] NOT NULL ,
	[StartQty] [int] NOT NULL ,
	[LoadQty] [int] NOT NULL ,
	[LoadAdjustmentQty] [int] NOT NULL ,
	[TransInQty] [int] NOT NULL ,
	[TransOutQty] [int] NOT NULL ,
	[ReturnQty] [int] NOT NULL ,
	[SaleQty] [int] NOT NULL ,
	[DmgStartQty] [int] NOT NULL ,
	[DmgLoadQty] [int] NOT NULL ,
	[DmgLoadAdjustmentQty] [int] NOT NULL ,
	[DmgTransInQty] [int] NOT NULL ,
	[DmgTransOutQty] [int] NOT NULL ,
	[DmgReturnQty] [int] NOT NULL ,
	[DmgSaleQty] [int] NOT NULL ,
	[DmgUnloadQty] [int] NOT NULL ,
	[RouteDmgQty] [int] NOT NULL ,
	[UnloadQty] [int] NOT NULL ,
	[EndQty] [int] NOT NULL ,
	[DmgEndQty] [int] NOT NULL 
) 
GO

CREATE TABLE [RouteOption] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[OptionName] [nvarchar] (50)  NOT NULL ,
	[OptionValue] [int] NOT NULL 
) 
GO

CREATE TABLE [RouteOptionDescription] (
	[OptionName] [nvarchar] (50)  NOT NULL ,
	[OptionValue] [int] NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL 
) 
GO

CREATE TABLE [RouteSchedule] (
	[LocationId] [int] NOT NULL ,
	[RouteScheduleId] [int] NOT NULL ,
	[DayOfWeekNumber] [tinyint] NOT NULL ,
	[CustomerId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[Frequency] [int] NOT NULL ,
	[Sequence] [int] NOT NULL ,
	[StartDate] [datetime] NOT NULL ,
	[EndDate] [datetime] NULL 
) 
GO

CREATE TABLE [RouteScheduleQueue] (
	[RouteScheduleQueueId] [int] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[RouteScheduleId] [int] NOT NULL ,
	[RouteScheduleQueueStatusId] [smallint] NOT NULL ,
	[DateCreated] [datetime] NOT NULL 
) 
GO

CREATE TABLE [RouteScheduleQueueStatus] (
	[RouteScheduleQueueStatusId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (100)  NULL 
) 
GO

CREATE TABLE [RouteStatus] (
	[RouteStatusId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (100)  NOT NULL 
) 
GO

CREATE TABLE [RouteType] (
	[RouteTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (100)  NULL 
) 
GO

CREATE TABLE [Session] (
	[SessionId] [bigint] NOT NULL ,
	[Active] [bit] NOT NULL 
) 
GO

CREATE TABLE [StorageType] (
	[StorageTypeId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) 
GO

ALTER TABLE [BusinessTransaction]  ADD 
	CONSTRAINT [PK_BusinessTransaction] PRIMARY KEY   
	(
		[SessionId],
		[BusinessTransactionId]
	)   
GO

ALTER TABLE [BusinessTransactionStatus]  ADD 
	CONSTRAINT [PK_BusinessTransactionStatus] PRIMARY KEY   
	(
		[BusinessTransactionStatusId]
	)   
GO

ALTER TABLE [BusinessTransactionType]  ADD 
	CONSTRAINT [PK_BusinessTransactionType] PRIMARY KEY   
	(
		[BusinessTransactionTypeId]
	)   
GO

ALTER TABLE [Counter]  ADD 
	CONSTRAINT [PK_Counter] PRIMARY KEY   
	(
		[CounterName]
	)   
GO

ALTER TABLE [Customer]  ADD 
	CONSTRAINT [PK_Customer_1] PRIMARY KEY   
	(
		[CustomerId]
	)   
GO

ALTER TABLE [CustomerOption]  ADD 
	CONSTRAINT [PK_CustomerOption] PRIMARY KEY   
	(
		[CustomerId],
		[OptionName]
	)   
GO

ALTER TABLE [CustomerOptionDescription]  ADD 
	CONSTRAINT [PK_CustomerOptionDescription] PRIMARY KEY   
	(
		[OptionName],
		[OptionValue]
	)   
GO

ALTER TABLE [CustomerTransaction]  ADD 
	CONSTRAINT [PK_CustomerTransaction] PRIMARY KEY   
	(
		[SessionId],
		[BusinessTransactionId]
	)   
GO

ALTER TABLE [CustomerTransactionDetail]  ADD 
	CONSTRAINT [PK_CustomerTransactionDetail] PRIMARY KEY   
	(
		[SessionId],
		[BusinessTransactionId],
		[ItemNumber],
		[RouteNumber],
		[LocationId]
	)   
GO

ALTER TABLE [CustomerTransactionType]  ADD 
	CONSTRAINT [PK_CustomerTransactionType] PRIMARY KEY   
	(
		[CustomerTransactionTypeId]
	)   
GO

ALTER TABLE [CustomerVisit]  ADD 
	CONSTRAINT [PK_CustomerVisit] PRIMARY KEY   
	(
		[SessionId],
		[CustomerVisitId]
	)   
GO

ALTER TABLE [DayOfWeek]  ADD 
	CONSTRAINT [PK_DayOfWeek] PRIMARY KEY   
	(
		[Number]
	)   
GO

ALTER TABLE [Employee]  ADD 
	CONSTRAINT [PK_Employee] PRIMARY KEY   
	(
		[LocationId],
		[EmployeeId]
	)   
GO

ALTER TABLE [Equipment]  ADD 
	CONSTRAINT [PK_Equipment] PRIMARY KEY   
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)   
GO

ALTER TABLE [EventLog]  ADD 
	CONSTRAINT [PK_EventLog] PRIMARY KEY   
	(
		[SessionId],
		[EventLogId]
	)   
GO

ALTER TABLE [InventoryTransaction]  ADD 
	CONSTRAINT [PK_InventoryTransaction] PRIMARY KEY   
	(
		[SessionId],
		[BusinessTransactionId]
	)   
GO

ALTER TABLE [InventoryTransactionDetail]  ADD 
	CONSTRAINT [PK_InventoryTransactionDetail] PRIMARY KEY   
	(
		[SessionId],
		[BusinessTransactionId],
		[ItemNumber],
		[RouteNumber],
		[LocationId],
		[StorageTypeId],
		[InventoryPeriodId],
		[InventoryTransactionDetailTypeId]
	)   
GO

ALTER TABLE [InventoryTransactionDetailType]  ADD 
	CONSTRAINT [PK_InventoryTransactionDetailType] PRIMARY KEY   
	(
		[InventoryTransactionDetailTypeId]
	)   
GO

ALTER TABLE [InventoryTransactionDetailXRef]  ADD 
	CONSTRAINT [PK_InventoryTransactionDetailXRef] PRIMARY KEY   
	(
		[InventoryTransactionDetailTypeId],
		[InventoryTransactionTypeId]
	)   
GO

ALTER TABLE [InventoryTransactionType]  ADD 
	CONSTRAINT [PK_InventoryTransactionType] PRIMARY KEY   
	(
		[InventoryTransactionTypeId]
	)   
GO

ALTER TABLE [Item]  ADD 
	CONSTRAINT [PK_Item] PRIMARY KEY   
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)   
GO

ALTER TABLE [ItemCategory]  ADD 
	CONSTRAINT [PK_ItemCategory] PRIMARY KEY   
	(
		[ItemCategoryId],
		[ItemTypeId]
	)   
GO

ALTER TABLE [ItemType]  ADD 
	CONSTRAINT [PK_InventoryItemType] PRIMARY KEY   
	(
		[ItemTypeId]
	)   
GO

ALTER TABLE [Location]  ADD 
	CONSTRAINT [PK_Location] PRIMARY KEY   
	(
		[LocationId]
	)   
GO

ALTER TABLE [Password]  ADD 
	CONSTRAINT [PK_Password] PRIMARY KEY   
	(
		[Functionality],
		[PasswordId]
	)   
GO

ALTER TABLE [PeriodTransaction]  ADD 
	CONSTRAINT [PK_PeriodTransaction] PRIMARY KEY   
	(
		[SessionId],
		[BusinessTransactionId]
	)   
GO

ALTER TABLE [PeriodTransactionType]  ADD 
	CONSTRAINT [PK_PeriodTransactionType] PRIMARY KEY   
	(
		[PeriodTransactionTypeId]
	)   
GO

ALTER TABLE [Product]  ADD 
	CONSTRAINT [PK_Product] PRIMARY KEY   
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)   
GO

ALTER TABLE [Route]  ADD 
	CONSTRAINT [PK_Route] PRIMARY KEY   
	(
		[LocationId],
		[RouteNumber]
	)   
GO

ALTER TABLE [RouteCustomer]  ADD 
	CONSTRAINT [PK_Customer] PRIMARY KEY   
	(
		[LocationId],
		[RouteNumber],
		[CustomerId]
	)   
GO

ALTER TABLE [RouteInventory]  ADD 
	CONSTRAINT [PK_RouteInventory] PRIMARY KEY   
	(
		[SessionId],
		[LocationId],
		[RouteNumber],
		[ItemNumber],
		[StorageTypeId],
		[InventoryPeriodId]
	)   
GO

ALTER TABLE [RouteOption]  ADD 
	CONSTRAINT [PK_RouteOption] PRIMARY KEY   
	(
		[LocationId],
		[RouteNumber],
		[OptionName]
	)   
GO

ALTER TABLE [RouteOptionDescription]  ADD 
	CONSTRAINT [PK_RouteOptionDescription] PRIMARY KEY   
	(
		[OptionName],
		[OptionValue]
	)   
GO

ALTER TABLE [RouteSchedule]  ADD 
	CONSTRAINT [PK_RouteSchedule] PRIMARY KEY   
	(
		[LocationId],
		[RouteScheduleId]
	)   
GO

ALTER TABLE [RouteScheduleQueue]  ADD 
	CONSTRAINT [PK_RouteScheduleQueue] PRIMARY KEY   
	(
		[RouteScheduleQueueId]
	)   
GO

ALTER TABLE [RouteScheduleQueueStatus]  ADD 
	CONSTRAINT [PK_RouteScheduleQueueStatus] PRIMARY KEY   
	(
		[RouteScheduleQueueStatusId]
	)   
GO

ALTER TABLE [RouteStatus]  ADD 
	CONSTRAINT [PK_RouteStatus] PRIMARY KEY   
	(
		[RouteStatusId]
	)   
GO

ALTER TABLE [RouteType]  ADD 
	CONSTRAINT [PK_RouteType] PRIMARY KEY   
	(
		[RouteTypeId]
	)   
GO

ALTER TABLE [Session]  ADD 
	CONSTRAINT [PK_Session] PRIMARY KEY   
	(
		[SessionId]
	)   
GO

ALTER TABLE [StorageType]  ADD 
	CONSTRAINT [PK_StorageType] PRIMARY KEY   
	(
		[StorageTypeId]
	)   
GO

ALTER TABLE [BusinessTransaction] ADD 
	CONSTRAINT [IX_BusinessTransaction] UNIQUE   
	(
		[DocumentNumber]
	)   
GO

ALTER TABLE [BusinessTransactionStatus] ADD 
	CONSTRAINT [IX_BusinessTransactionStatus] UNIQUE   
	(
		[Name]
	)   
GO

ALTER TABLE [BusinessTransactionType] ADD 
	CONSTRAINT [IX_BusinessTransactionType] UNIQUE   
	(
		[Name]
	)   
GO

ALTER TABLE [InventoryTransactionType] ADD 
	CONSTRAINT [IX_InventoryTransactionType] UNIQUE   
	(
		[Name]
	)   
GO

 CREATE  UNIQUE  INDEX [IX_Item] ON [Item]([ItemNumber]) 
GO

 CREATE  INDEX [IX_Item_NameSortIndex] ON [Item]([NameSortIndex]) 
GO

 CREATE  INDEX [IX_Item_NumberSortIndex] ON [Item]([ItemNumberSortIndex]) 
GO

ALTER TABLE [PeriodTransactionType] ADD 
	CONSTRAINT [IX_PeriodTransactionType] UNIQUE   
	(
		[Name]
	)   
GO

ALTER TABLE [RouteInventory] ADD 
	CONSTRAINT [DF_RouteInventory_StartQty] DEFAULT (0) FOR [StartQty],
	CONSTRAINT [DF_RouteInventory_LoadAdjustment] DEFAULT (0) FOR [LoadAdjustmentQty],
	CONSTRAINT [DF_RouteInventory_Return] DEFAULT (0) FOR [ReturnQty]
GO

ALTER TABLE [RouteStatus] ADD 
	CONSTRAINT [IX_RouteStatus] UNIQUE   
	(
		[Name]
	)   
GO

ALTER TABLE [RouteType] ADD 
	CONSTRAINT [IX_RouteType] UNIQUE   
	(
		[Name]
	)   
GO

ALTER TABLE [BusinessTransaction] ADD 
	CONSTRAINT [FK_BusinessTransaction_BusinessTransactionStatus] FOREIGN KEY 
	(
		[BusinessTransactionStatusId]
	) REFERENCES [BusinessTransactionStatus] (
		[BusinessTransactionStatusId]
	),
	CONSTRAINT [FK_BusinessTransaction_BusinessTransactionType] FOREIGN KEY 
	(
		[BusinessTransactionTypeId]
	) REFERENCES [BusinessTransactionType] (
		[BusinessTransactionTypeId]
	),
	CONSTRAINT [FK_BusinessTransaction_Employee] FOREIGN KEY 
	(
		[LocationId],
		[EmployeeId]
	) REFERENCES [Employee] (
		[LocationId],
		[EmployeeId]
	),
	CONSTRAINT [FK_BusinessTransaction_Route] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber]
	) REFERENCES [Route] (
		[LocationId],
		[RouteNumber]
	),
	CONSTRAINT [FK_BusinessTransaction_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [Session] (
		[SessionId]
	)
GO

ALTER TABLE [CustomerOption] ADD 
	CONSTRAINT [FK_CustomerOption_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [Customer] (
		[CustomerId]
	),
	CONSTRAINT [FK_CustomerOption_CustomerOptionDescription] FOREIGN KEY 
	(
		[OptionName],
		[OptionValue]
	) REFERENCES [CustomerOptionDescription] (
		[OptionName],
		[OptionValue]
	)
GO

ALTER TABLE [CustomerTransaction] ADD 
	CONSTRAINT [FK_CustomerTransaction_BusinessTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [BusinessTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_CustomerTransaction_CustomerTransactionType] FOREIGN KEY 
	(
		[CustomerTransactionTypeId]
	) REFERENCES [CustomerTransactionType] (
		[CustomerTransactionTypeId]
	),
	CONSTRAINT [FK_CustomerTransaction_CustomerVisit] FOREIGN KEY 
	(
		[SessionId],
		[CustomerVisitId]
	) REFERENCES [CustomerVisit] (
		[SessionId],
		[CustomerVisitId]
	)
GO

ALTER TABLE [CustomerTransactionDetail] ADD 
	CONSTRAINT [FK_CustomerTransactionDetail_CustomerTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [CustomerTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_CustomerTransactionDetail_Item] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	) REFERENCES [Item] (
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)
GO

ALTER TABLE [CustomerVisit] ADD 
	CONSTRAINT [FK_CustomerVisit_Customer1] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[CustomerId]
	) REFERENCES [RouteCustomer] (
		[LocationId],
		[RouteNumber],
		[CustomerId]
	),
	CONSTRAINT [FK_CustomerVisit_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [Session] (
		[SessionId]
	)
GO

ALTER TABLE [Employee] ADD 
	CONSTRAINT [FK_Employee_Location] FOREIGN KEY 
	(
		[LocationId]
	) REFERENCES [Location] (
		[LocationId]
	)
GO

ALTER TABLE [Equipment] ADD 
	CONSTRAINT [FK_Equipment_Item] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	) REFERENCES [Item] (
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)
GO

ALTER TABLE [EventLog] ADD 
	CONSTRAINT [FK_EventLog_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [Session] (
		[SessionId]
	)
GO

ALTER TABLE [InventoryTransaction] ADD 
	CONSTRAINT [FK_InventoryTransaction_BusinessTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [BusinessTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_InventoryTransaction_InventoryTransactionType] FOREIGN KEY 
	(
		[InventoryTransactionTypeId]
	) REFERENCES [InventoryTransactionType] (
		[InventoryTransactionTypeId]
	)
GO

ALTER TABLE [InventoryTransactionDetail] ADD 
	CONSTRAINT [FK_InventoryTransactionDetail_InventoryTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [InventoryTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_InventoryTransactionDetail_InventoryTransactionDetailXRef] FOREIGN KEY 
	(
		[InventoryTransactionDetailTypeId],
		[InventoryTransactionTypeId]
	) REFERENCES [InventoryTransactionDetailXRef] (
		[InventoryTransactionDetailTypeId],
		[InventoryTransactionTypeId]
	),
	CONSTRAINT [FK_InventoryTransactionDetail_RouteInventory] FOREIGN KEY 
	(
		[SessionId],
		[LocationId],
		[RouteNumber],
		[ItemNumber],
		[StorageTypeId],
		[InventoryPeriodId]
	) REFERENCES [RouteInventory] (
		[SessionId],
		[LocationId],
		[RouteNumber],
		[ItemNumber],
		[StorageTypeId],
		[InventoryPeriodId]
	)
GO

ALTER TABLE [InventoryTransactionDetailXRef] ADD 
	CONSTRAINT [FK_InventoryTransactionDetailXRef_InventoryTransactionDetailType] FOREIGN KEY 
	(
		[InventoryTransactionDetailTypeId]
	) REFERENCES [InventoryTransactionDetailType] (
		[InventoryTransactionDetailTypeId]
	),
	CONSTRAINT [FK_InventoryTransactionDetailXRef_InventoryTransactionType] FOREIGN KEY 
	(
		[InventoryTransactionTypeId]
	) REFERENCES [InventoryTransactionType] (
		[InventoryTransactionTypeId]
	)
GO

ALTER TABLE [Item] ADD 
	CONSTRAINT [FK_InventoryItem_Route] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber]
	) REFERENCES [Route] (
		[LocationId],
		[RouteNumber]
	),
	CONSTRAINT [FK_Item_ItemCategory] FOREIGN KEY 
	(
		[ItemCategoryId],
		[ItemTypeId]
	) REFERENCES [ItemCategory] (
		[ItemCategoryId],
		[ItemTypeId]
	)
GO

ALTER TABLE [ItemCategory] ADD 
	CONSTRAINT [FK_ItemCategory_ItemType] FOREIGN KEY 
	(
		[ItemTypeId]
	) REFERENCES [ItemType] (
		[ItemTypeId]
	)
GO

ALTER TABLE [PeriodTransaction] ADD 
	CONSTRAINT [FK_PeriodTransaction_BusinessTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [BusinessTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_PeriodTransaction_PeriodTransactionType] FOREIGN KEY 
	(
		[PeriodTransactionTypeId]
	) REFERENCES [PeriodTransactionType] (
		[PeriodTransactionTypeId]
	)
GO

ALTER TABLE [Product] ADD 
	CONSTRAINT [FK_Product_Item] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	) REFERENCES [Item] (
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)
GO

ALTER TABLE [Route] ADD 
	CONSTRAINT [FK_Route_Employee] FOREIGN KEY 
	(
		[LocationId],
		[EmployeeId]
	) REFERENCES [Employee] (
		[LocationId],
		[EmployeeId]
	),
	CONSTRAINT [FK_Route_RouteStatus] FOREIGN KEY 
	(
		[RouteStatusId]
	) REFERENCES [RouteStatus] (
		[RouteStatusId]
	),
	CONSTRAINT [FK_Route_RouteType] FOREIGN KEY 
	(
		[RouteTypeId]
	) REFERENCES [RouteType] (
		[RouteTypeId]
	)
GO

ALTER TABLE [RouteCustomer] ADD 
	CONSTRAINT [FK_Customer_Route1] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber]
	) REFERENCES [Route] (
		[LocationId],
		[RouteNumber]
	),
	CONSTRAINT [FK_RouteCustomer_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [Customer] (
		[CustomerId]
	)
GO

ALTER TABLE [RouteInventory] ADD 
	CONSTRAINT [FK_RouteInventory_Item] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	) REFERENCES [Item] (
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	),
	CONSTRAINT [FK_RouteInventory_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [Session] (
		[SessionId]
	),
	CONSTRAINT [FK_RouteInventory_StorageType] FOREIGN KEY 
	(
		[StorageTypeId]
	) REFERENCES [StorageType] (
		[StorageTypeId]
	)
GO

ALTER TABLE [RouteOption] ADD 
	CONSTRAINT [FK_RouteOption_Route] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber]
	) REFERENCES [Route] (
		[LocationId],
		[RouteNumber]
	),
	CONSTRAINT [FK_RouteOption_RouteOptionDescription] FOREIGN KEY 
	(
		[OptionName],
		[OptionValue]
	) REFERENCES [RouteOptionDescription] (
		[OptionName],
		[OptionValue]
	)
GO

ALTER TABLE [RouteSchedule] ADD 
	CONSTRAINT [FK_RouteSchedule_Customer1] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[CustomerId]
	) REFERENCES [RouteCustomer] (
		[LocationId],
		[RouteNumber],
		[CustomerId]
	),
	CONSTRAINT [FK_RouteSchedule_DayOfWeek] FOREIGN KEY 
	(
		[DayOfWeekNumber]
	) REFERENCES [DayOfWeek] (
		[Number]
	)
GO

ALTER TABLE [RouteScheduleQueue] ADD 
	CONSTRAINT [FK_RouteScheduleQueue_RouteSchedule] FOREIGN KEY 
	(
		[LocationId],
		[RouteScheduleId]
	) REFERENCES [RouteSchedule] (
		[LocationId],
		[RouteScheduleId]
	),
	CONSTRAINT [FK_RouteScheduleQueue_RouteScheduleQueueStatus] FOREIGN KEY 
	(
		[RouteScheduleQueueStatusId]
	) REFERENCES [RouteScheduleQueueStatus] (
		[RouteScheduleQueueStatusId]
	)
GO

