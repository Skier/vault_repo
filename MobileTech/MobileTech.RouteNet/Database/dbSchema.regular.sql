CREATE TABLE [dbo].[BusinessTransaction] (
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
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BusinessTransactionStatus] (
	[BusinessTransactionStatusId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BusinessTransactionType] (
	[BusinessTransactionTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Counter] (
	[CounterName] [nvarchar] (40)  NOT NULL ,
	[Val] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Customer] (
	[CustomerId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CustomerOption] (
	[CustomerId] [int] NOT NULL ,
	[OptionName] [nvarchar] (50)  NOT NULL ,
	[OptionValue] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CustomerOptionDescription] (
	[OptionName] [nvarchar] (50)  NOT NULL ,
	[OptionValue] [int] NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CustomerTransaction] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[CustomerTransactionTypeId] [smallint] NOT NULL ,
	[CustomerVisitId] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CustomerTransactionDetail] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[Quantity] [int] NOT NULL ,
	[DateCreated] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CustomerTransactionType] (
	[CustomerTransactionTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CustomerVisit] (
	[SessionId] [bigint] NOT NULL ,
	[CustomerVisitId] [int] NOT NULL ,
	[CustomerId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[DateStarted] [datetime] NOT NULL ,
	[DateFinished] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DayOfWeek] (
	[Number] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Employee] (
	[LocationId] [int] NOT NULL ,
	[EmployeeId] [int] NOT NULL ,
	[FirstName] [nvarchar] (50)  NOT NULL ,
	[LastName] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Equipment] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EventLog] (
	[SessionId] [bigint] NOT NULL ,
	[EventLogId] [int] NOT NULL ,
	[EventType] [int] NOT NULL ,
	[Message] [nvarchar] (2000)  NOT NULL ,
	[Source] [nvarchar] (100)  NULL ,
	[AssemblyName] [nvarchar] (100)  NULL ,
	[CreateDate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[InventoryTransaction] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[InventoryTransactionTypeId] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[InventoryTransactionDetail] (
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
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[InventoryTransactionDetailType] (
	[InventoryTransactionDetailTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[InventoryTransactionDetailXRef] (
	[InventoryTransactionDetailTypeId] [smallint] NOT NULL ,
	[InventoryTransactionTypeId] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[InventoryTransactionType] (
	[InventoryTransactionTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Item] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL ,
	[ItemCategoryId] [int] NOT NULL ,
	[ItemTypeId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL ,
	[NameSortIndex] [int] NOT NULL ,
	[ItemNumberSortIndex] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ItemCategory] (
	[ItemCategoryId] [int] NOT NULL ,
	[ItemTypeId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ItemType] (
	[ItemTypeId] [int] NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Location] (
	[LocationId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Password] (
	[Functionality] [nvarchar] (56)  NOT NULL ,
	[PasswordId] [int] NOT NULL ,
	[PasswordValue] [nvarchar] (6)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PeriodTransaction] (
	[SessionId] [bigint] NOT NULL ,
	[BusinessTransactionId] [int] NOT NULL ,
	[PeriodTransactionTypeId] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PeriodTransactionType] (
	[PeriodTransactionTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Product] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[ItemNumber] [nvarchar] (8)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Route] (
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
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteCustomer] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[CustomerId] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteInventory] (
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
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteOption] (
	[LocationId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[OptionName] [nvarchar] (50)  NOT NULL ,
	[OptionValue] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteOptionDescription] (
	[OptionName] [nvarchar] (50)  NOT NULL ,
	[OptionValue] [int] NOT NULL ,
	[Description] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteSchedule] (
	[LocationId] [int] NOT NULL ,
	[RouteScheduleId] [int] NOT NULL ,
	[DayOfWeekNumber] [tinyint] NOT NULL ,
	[CustomerId] [int] NOT NULL ,
	[RouteNumber] [int] NOT NULL ,
	[Frequency] [int] NOT NULL ,
	[Sequence] [int] NOT NULL ,
	[StartDate] [datetime] NOT NULL ,
	[EndDate] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteScheduleQueue] (
	[RouteScheduleQueueId] [int] NOT NULL ,
	[LocationId] [int] NOT NULL ,
	[RouteScheduleId] [int] NOT NULL ,
	[RouteScheduleQueueStatusId] [smallint] NOT NULL ,
	[DateCreated] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteScheduleQueueStatus] (
	[RouteScheduleQueueStatusId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (100)  NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteStatus] (
	[RouteStatusId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (100)  NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RouteType] (
	[RouteTypeId] [smallint] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL ,
	[Description] [nvarchar] (100)  NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Session] (
	[SessionId] [bigint] NOT NULL ,
	[Active] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[StorageType] (
	[StorageTypeId] [int] NOT NULL ,
	[Name] [nvarchar] (50)  NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BusinessTransaction] WITH NOCHECK ADD 
	CONSTRAINT [PK_BusinessTransaction] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[BusinessTransactionId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BusinessTransactionStatus] WITH NOCHECK ADD 
	CONSTRAINT [PK_BusinessTransactionStatus] PRIMARY KEY  CLUSTERED 
	(
		[BusinessTransactionStatusId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BusinessTransactionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_BusinessTransactionType] PRIMARY KEY  CLUSTERED 
	(
		[BusinessTransactionTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Counter] WITH NOCHECK ADD 
	CONSTRAINT [PK_Counter] PRIMARY KEY  CLUSTERED 
	(
		[CounterName]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Customer] WITH NOCHECK ADD 
	CONSTRAINT [PK_Customer_1] PRIMARY KEY  CLUSTERED 
	(
		[CustomerId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CustomerOption] WITH NOCHECK ADD 
	CONSTRAINT [PK_CustomerOption] PRIMARY KEY  CLUSTERED 
	(
		[CustomerId],
		[OptionName]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CustomerOptionDescription] WITH NOCHECK ADD 
	CONSTRAINT [PK_CustomerOptionDescription] PRIMARY KEY  CLUSTERED 
	(
		[OptionName],
		[OptionValue]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CustomerTransaction] WITH NOCHECK ADD 
	CONSTRAINT [PK_CustomerTransaction] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[BusinessTransactionId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CustomerTransactionDetail] WITH NOCHECK ADD 
	CONSTRAINT [PK_CustomerTransactionDetail] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[BusinessTransactionId],
		[ItemNumber],
		[RouteNumber],
		[LocationId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CustomerTransactionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_CustomerTransactionType] PRIMARY KEY  CLUSTERED 
	(
		[CustomerTransactionTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CustomerVisit] WITH NOCHECK ADD 
	CONSTRAINT [PK_CustomerVisit] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[CustomerVisitId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[DayOfWeek] WITH NOCHECK ADD 
	CONSTRAINT [PK_DayOfWeek] PRIMARY KEY  CLUSTERED 
	(
		[Number]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Employee] WITH NOCHECK ADD 
	CONSTRAINT [PK_Employee] PRIMARY KEY  CLUSTERED 
	(
		[LocationId],
		[EmployeeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Equipment] WITH NOCHECK ADD 
	CONSTRAINT [PK_Equipment] PRIMARY KEY  CLUSTERED 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[EventLog] WITH NOCHECK ADD 
	CONSTRAINT [PK_EventLog] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[EventLogId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[InventoryTransaction] WITH NOCHECK ADD 
	CONSTRAINT [PK_InventoryTransaction] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[BusinessTransactionId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[InventoryTransactionDetail] WITH NOCHECK ADD 
	CONSTRAINT [PK_InventoryTransactionDetail] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[BusinessTransactionId],
		[ItemNumber],
		[RouteNumber],
		[LocationId],
		[StorageTypeId],
		[InventoryPeriodId],
		[InventoryTransactionDetailTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[InventoryTransactionDetailType] WITH NOCHECK ADD 
	CONSTRAINT [PK_InventoryTransactionDetailType] PRIMARY KEY  CLUSTERED 
	(
		[InventoryTransactionDetailTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[InventoryTransactionDetailXRef] WITH NOCHECK ADD 
	CONSTRAINT [PK_InventoryTransactionDetailXRef] PRIMARY KEY  CLUSTERED 
	(
		[InventoryTransactionDetailTypeId],
		[InventoryTransactionTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[InventoryTransactionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_InventoryTransactionType] PRIMARY KEY  CLUSTERED 
	(
		[InventoryTransactionTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Item] WITH NOCHECK ADD 
	CONSTRAINT [PK_Item] PRIMARY KEY  CLUSTERED 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ItemCategory] WITH NOCHECK ADD 
	CONSTRAINT [PK_ItemCategory] PRIMARY KEY  CLUSTERED 
	(
		[ItemCategoryId],
		[ItemTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ItemType] WITH NOCHECK ADD 
	CONSTRAINT [PK_InventoryItemType] PRIMARY KEY  CLUSTERED 
	(
		[ItemTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Location] WITH NOCHECK ADD 
	CONSTRAINT [PK_Location] PRIMARY KEY  CLUSTERED 
	(
		[LocationId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Password] WITH NOCHECK ADD 
	CONSTRAINT [PK_Password] PRIMARY KEY  CLUSTERED 
	(
		[Functionality],
		[PasswordId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[PeriodTransaction] WITH NOCHECK ADD 
	CONSTRAINT [PK_PeriodTransaction] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[BusinessTransactionId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[PeriodTransactionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_PeriodTransactionType] PRIMARY KEY  CLUSTERED 
	(
		[PeriodTransactionTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Product] WITH NOCHECK ADD 
	CONSTRAINT [PK_Product] PRIMARY KEY  CLUSTERED 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Route] WITH NOCHECK ADD 
	CONSTRAINT [PK_Route] PRIMARY KEY  CLUSTERED 
	(
		[LocationId],
		[RouteNumber]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteCustomer] WITH NOCHECK ADD 
	CONSTRAINT [PK_Customer] PRIMARY KEY  CLUSTERED 
	(
		[LocationId],
		[RouteNumber],
		[CustomerId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteInventory] WITH NOCHECK ADD 
	CONSTRAINT [PK_RouteInventory] PRIMARY KEY  CLUSTERED 
	(
		[SessionId],
		[LocationId],
		[RouteNumber],
		[ItemNumber],
		[StorageTypeId],
		[InventoryPeriodId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteOption] WITH NOCHECK ADD 
	CONSTRAINT [PK_RouteOption] PRIMARY KEY  CLUSTERED 
	(
		[LocationId],
		[RouteNumber],
		[OptionName]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteOptionDescription] WITH NOCHECK ADD 
	CONSTRAINT [PK_RouteOptionDescription] PRIMARY KEY  CLUSTERED 
	(
		[OptionName],
		[OptionValue]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteSchedule] WITH NOCHECK ADD 
	CONSTRAINT [PK_RouteSchedule] PRIMARY KEY  CLUSTERED 
	(
		[LocationId],
		[RouteScheduleId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteScheduleQueue] WITH NOCHECK ADD 
	CONSTRAINT [PK_RouteScheduleQueue] PRIMARY KEY  CLUSTERED 
	(
		[RouteScheduleQueueId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteScheduleQueueStatus] WITH NOCHECK ADD 
	CONSTRAINT [PK_RouteScheduleQueueStatus] PRIMARY KEY  CLUSTERED 
	(
		[RouteScheduleQueueStatusId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteStatus] WITH NOCHECK ADD 
	CONSTRAINT [PK_RouteStatus] PRIMARY KEY  CLUSTERED 
	(
		[RouteStatusId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteType] WITH NOCHECK ADD 
	CONSTRAINT [PK_RouteType] PRIMARY KEY  CLUSTERED 
	(
		[RouteTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Session] WITH NOCHECK ADD 
	CONSTRAINT [PK_Session] PRIMARY KEY  CLUSTERED 
	(
		[SessionId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[StorageType] WITH NOCHECK ADD 
	CONSTRAINT [PK_StorageType] PRIMARY KEY  CLUSTERED 
	(
		[StorageTypeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BusinessTransaction] ADD 
	CONSTRAINT [IX_BusinessTransaction] UNIQUE  NONCLUSTERED 
	(
		[DocumentNumber]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BusinessTransactionStatus] ADD 
	CONSTRAINT [IX_BusinessTransactionStatus] UNIQUE  NONCLUSTERED 
	(
		[Name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BusinessTransactionType] ADD 
	CONSTRAINT [IX_BusinessTransactionType] UNIQUE  NONCLUSTERED 
	(
		[Name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[InventoryTransactionType] ADD 
	CONSTRAINT [IX_InventoryTransactionType] UNIQUE  NONCLUSTERED 
	(
		[Name]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  INDEX [IX_Item] ON [dbo].[Item]([ItemNumber]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Item_NameSortIndex] ON [dbo].[Item]([NameSortIndex]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Item_NumberSortIndex] ON [dbo].[Item]([ItemNumberSortIndex]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PeriodTransactionType] ADD 
	CONSTRAINT [IX_PeriodTransactionType] UNIQUE  NONCLUSTERED 
	(
		[Name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteInventory] ADD 
	CONSTRAINT [DF_RouteInventory_StartQty] DEFAULT (0) FOR [StartQty],
	CONSTRAINT [DF_RouteInventory_LoadAdjustment] DEFAULT (0) FOR [LoadAdjustmentQty],
	CONSTRAINT [DF_RouteInventory_Return] DEFAULT (0) FOR [ReturnQty]
GO

ALTER TABLE [dbo].[RouteStatus] ADD 
	CONSTRAINT [IX_RouteStatus] UNIQUE  NONCLUSTERED 
	(
		[Name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RouteType] ADD 
	CONSTRAINT [IX_RouteType] UNIQUE  NONCLUSTERED 
	(
		[Name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BusinessTransaction] ADD 
	CONSTRAINT [FK_BusinessTransaction_BusinessTransactionStatus] FOREIGN KEY 
	(
		[BusinessTransactionStatusId]
	) REFERENCES [dbo].[BusinessTransactionStatus] (
		[BusinessTransactionStatusId]
	),
	CONSTRAINT [FK_BusinessTransaction_BusinessTransactionType] FOREIGN KEY 
	(
		[BusinessTransactionTypeId]
	) REFERENCES [dbo].[BusinessTransactionType] (
		[BusinessTransactionTypeId]
	),
	CONSTRAINT [FK_BusinessTransaction_Employee] FOREIGN KEY 
	(
		[LocationId],
		[EmployeeId]
	) REFERENCES [dbo].[Employee] (
		[LocationId],
		[EmployeeId]
	),
	CONSTRAINT [FK_BusinessTransaction_Route] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber]
	) REFERENCES [dbo].[Route] (
		[LocationId],
		[RouteNumber]
	),
	CONSTRAINT [FK_BusinessTransaction_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [dbo].[Session] (
		[SessionId]
	)
GO

ALTER TABLE [dbo].[CustomerOption] ADD 
	CONSTRAINT [FK_CustomerOption_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [dbo].[Customer] (
		[CustomerId]
	),
	CONSTRAINT [FK_CustomerOption_CustomerOptionDescription] FOREIGN KEY 
	(
		[OptionName],
		[OptionValue]
	) REFERENCES [dbo].[CustomerOptionDescription] (
		[OptionName],
		[OptionValue]
	)
GO

ALTER TABLE [dbo].[CustomerTransaction] ADD 
	CONSTRAINT [FK_CustomerTransaction_BusinessTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [dbo].[BusinessTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_CustomerTransaction_CustomerTransactionType] FOREIGN KEY 
	(
		[CustomerTransactionTypeId]
	) REFERENCES [dbo].[CustomerTransactionType] (
		[CustomerTransactionTypeId]
	),
	CONSTRAINT [FK_CustomerTransaction_CustomerVisit] FOREIGN KEY 
	(
		[SessionId],
		[CustomerVisitId]
	) REFERENCES [dbo].[CustomerVisit] (
		[SessionId],
		[CustomerVisitId]
	)
GO

ALTER TABLE [dbo].[CustomerTransactionDetail] ADD 
	CONSTRAINT [FK_CustomerTransactionDetail_CustomerTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [dbo].[CustomerTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_CustomerTransactionDetail_Item] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	) REFERENCES [dbo].[Item] (
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)
GO

ALTER TABLE [dbo].[CustomerVisit] ADD 
	CONSTRAINT [FK_CustomerVisit_Customer1] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[CustomerId]
	) REFERENCES [dbo].[RouteCustomer] (
		[LocationId],
		[RouteNumber],
		[CustomerId]
	),
	CONSTRAINT [FK_CustomerVisit_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [dbo].[Session] (
		[SessionId]
	)
GO

ALTER TABLE [dbo].[Employee] ADD 
	CONSTRAINT [FK_Employee_Location] FOREIGN KEY 
	(
		[LocationId]
	) REFERENCES [dbo].[Location] (
		[LocationId]
	)
GO

ALTER TABLE [dbo].[Equipment] ADD 
	CONSTRAINT [FK_Equipment_Item] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	) REFERENCES [dbo].[Item] (
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)
GO

ALTER TABLE [dbo].[EventLog] ADD 
	CONSTRAINT [FK_EventLog_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [dbo].[Session] (
		[SessionId]
	)
GO

ALTER TABLE [dbo].[InventoryTransaction] ADD 
	CONSTRAINT [FK_InventoryTransaction_BusinessTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [dbo].[BusinessTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_InventoryTransaction_InventoryTransactionType] FOREIGN KEY 
	(
		[InventoryTransactionTypeId]
	) REFERENCES [dbo].[InventoryTransactionType] (
		[InventoryTransactionTypeId]
	)
GO

ALTER TABLE [dbo].[InventoryTransactionDetail] ADD 
	CONSTRAINT [FK_InventoryTransactionDetail_InventoryTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [dbo].[InventoryTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_InventoryTransactionDetail_InventoryTransactionDetailXRef] FOREIGN KEY 
	(
		[InventoryTransactionDetailTypeId],
		[InventoryTransactionTypeId]
	) REFERENCES [dbo].[InventoryTransactionDetailXRef] (
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
	) REFERENCES [dbo].[RouteInventory] (
		[SessionId],
		[LocationId],
		[RouteNumber],
		[ItemNumber],
		[StorageTypeId],
		[InventoryPeriodId]
	)
GO

ALTER TABLE [dbo].[InventoryTransactionDetailXRef] ADD 
	CONSTRAINT [FK_InventoryTransactionDetailXRef_InventoryTransactionDetailType] FOREIGN KEY 
	(
		[InventoryTransactionDetailTypeId]
	) REFERENCES [dbo].[InventoryTransactionDetailType] (
		[InventoryTransactionDetailTypeId]
	),
	CONSTRAINT [FK_InventoryTransactionDetailXRef_InventoryTransactionType] FOREIGN KEY 
	(
		[InventoryTransactionTypeId]
	) REFERENCES [dbo].[InventoryTransactionType] (
		[InventoryTransactionTypeId]
	)
GO

ALTER TABLE [dbo].[Item] ADD 
	CONSTRAINT [FK_InventoryItem_Route] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber]
	) REFERENCES [dbo].[Route] (
		[LocationId],
		[RouteNumber]
	),
	CONSTRAINT [FK_Item_ItemCategory] FOREIGN KEY 
	(
		[ItemCategoryId],
		[ItemTypeId]
	) REFERENCES [dbo].[ItemCategory] (
		[ItemCategoryId],
		[ItemTypeId]
	)
GO

ALTER TABLE [dbo].[ItemCategory] ADD 
	CONSTRAINT [FK_ItemCategory_ItemType] FOREIGN KEY 
	(
		[ItemTypeId]
	) REFERENCES [dbo].[ItemType] (
		[ItemTypeId]
	)
GO

ALTER TABLE [dbo].[PeriodTransaction] ADD 
	CONSTRAINT [FK_PeriodTransaction_BusinessTransaction] FOREIGN KEY 
	(
		[SessionId],
		[BusinessTransactionId]
	) REFERENCES [dbo].[BusinessTransaction] (
		[SessionId],
		[BusinessTransactionId]
	),
	CONSTRAINT [FK_PeriodTransaction_PeriodTransactionType] FOREIGN KEY 
	(
		[PeriodTransactionTypeId]
	) REFERENCES [dbo].[PeriodTransactionType] (
		[PeriodTransactionTypeId]
	)
GO

ALTER TABLE [dbo].[Product] ADD 
	CONSTRAINT [FK_Product_Item] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	) REFERENCES [dbo].[Item] (
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	)
GO

ALTER TABLE [dbo].[Route] ADD 
	CONSTRAINT [FK_Route_Employee] FOREIGN KEY 
	(
		[LocationId],
		[EmployeeId]
	) REFERENCES [dbo].[Employee] (
		[LocationId],
		[EmployeeId]
	),
	CONSTRAINT [FK_Route_RouteStatus] FOREIGN KEY 
	(
		[RouteStatusId]
	) REFERENCES [dbo].[RouteStatus] (
		[RouteStatusId]
	),
	CONSTRAINT [FK_Route_RouteType] FOREIGN KEY 
	(
		[RouteTypeId]
	) REFERENCES [dbo].[RouteType] (
		[RouteTypeId]
	)
GO

ALTER TABLE [dbo].[RouteCustomer] ADD 
	CONSTRAINT [FK_Customer_Route1] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber]
	) REFERENCES [dbo].[Route] (
		[LocationId],
		[RouteNumber]
	),
	CONSTRAINT [FK_RouteCustomer_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [dbo].[Customer] (
		[CustomerId]
	)
GO

ALTER TABLE [dbo].[RouteInventory] ADD 
	CONSTRAINT [FK_RouteInventory_Item] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	) REFERENCES [dbo].[Item] (
		[LocationId],
		[RouteNumber],
		[ItemNumber]
	),
	CONSTRAINT [FK_RouteInventory_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [dbo].[Session] (
		[SessionId]
	),
	CONSTRAINT [FK_RouteInventory_StorageType] FOREIGN KEY 
	(
		[StorageTypeId]
	) REFERENCES [dbo].[StorageType] (
		[StorageTypeId]
	)
GO

ALTER TABLE [dbo].[RouteOption] ADD 
	CONSTRAINT [FK_RouteOption_Route] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber]
	) REFERENCES [dbo].[Route] (
		[LocationId],
		[RouteNumber]
	),
	CONSTRAINT [FK_RouteOption_RouteOptionDescription] FOREIGN KEY 
	(
		[OptionName],
		[OptionValue]
	) REFERENCES [dbo].[RouteOptionDescription] (
		[OptionName],
		[OptionValue]
	)
GO

ALTER TABLE [dbo].[RouteSchedule] ADD 
	CONSTRAINT [FK_RouteSchedule_Customer1] FOREIGN KEY 
	(
		[LocationId],
		[RouteNumber],
		[CustomerId]
	) REFERENCES [dbo].[RouteCustomer] (
		[LocationId],
		[RouteNumber],
		[CustomerId]
	),
	CONSTRAINT [FK_RouteSchedule_DayOfWeek] FOREIGN KEY 
	(
		[DayOfWeekNumber]
	) REFERENCES [dbo].[DayOfWeek] (
		[Number]
	)
GO

ALTER TABLE [dbo].[RouteScheduleQueue] ADD 
	CONSTRAINT [FK_RouteScheduleQueue_RouteSchedule] FOREIGN KEY 
	(
		[LocationId],
		[RouteScheduleId]
	) REFERENCES [dbo].[RouteSchedule] (
		[LocationId],
		[RouteScheduleId]
	),
	CONSTRAINT [FK_RouteScheduleQueue_RouteScheduleQueueStatus] FOREIGN KEY 
	(
		[RouteScheduleQueueStatusId]
	) REFERENCES [dbo].[RouteScheduleQueueStatus] (
		[RouteScheduleQueueStatusId]
	)
GO

