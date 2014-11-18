CREATE TABLE [dbo].[Address] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Address1] [nvarchar] (200) NULL ,
	[Address2] [nvarchar] (200) NULL ,
	[City] [nvarchar] (100) NULL ,
	[State] [nvarchar] (30) NULL ,
	[Zip] [nvarchar] (10) NULL ,
	[Map] [nvarchar] (10) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Counter] (
	[CounterName] [nvarchar] (40) NOT NULL ,
	[Val] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CreditCardType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Customer] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[ServmanCustId] [nvarchar] (6) NULL ,
	[AddressId] [int] NULL ,
	[FirstName] [nvarchar] (100) NULL ,
	[LastName] [nvarchar] (100) NULL ,
	[Phone1] [nvarchar] (50) NULL ,
	[Phone2] [nvarchar] (50) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DashboardState] (
	[EmployeeId] [int] NOT NULL ,
	[IsDirty] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Employee] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[EmployeeTypeId] [int] NOT NULL ,
	[ServmanUserId] [nvarchar] (255) NULL ,
	[ServmanTechId] [nvarchar] (255) NULL ,
	[AddressId] [int] NULL ,
	[Login] [nvarchar] (50) NULL ,
	[FirstName] [nvarchar] (100) NULL ,
	[LastName] [nvarchar] (100) NULL ,
	[HireDate] [datetime] NULL ,
	[Phone1] [nvarchar] (50) NULL ,
	[Phone2] [nvarchar] (50) NULL ,
	[Password] [nvarchar] (100) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EmployeeType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Equipment] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[EquipmentTypeId] [int] NULL ,
	[SerialNumber] [nvarchar] (50) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EquipmentType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EventLog] (
	[EventLogId] [int] IDENTITY (1, 1) NOT NULL ,
	[EventType] [int] NOT NULL ,
	[Message] [nvarchar] (2000) NOT NULL ,
	[Source] [nvarchar] (100) NULL ,
	[AssemblyName] [nvarchar] (100) NULL ,
	[CreateDate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Item] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[ItemTypeId] [int] NOT NULL ,
	[SerialNumber] [nvarchar] (50) NULL ,
	[ItemShapeId] [int] NULL ,
	[Width] [decimal](18, 0) NULL ,
	[Height] [decimal](18, 0) NULL ,
	[Diameter] [decimal](18, 0) NULL ,
	[IsProtectorApplied] [bit] NULL ,
	[IsPaddingApplied] [bit] NULL ,
	[IsMothRepelApplied] [bit] NULL ,
	[IsRapApplied] [bit] NULL ,
	[CleanCost] [money] NULL ,
	[ProtectorCost] [money] NULL ,
	[PaddingCost] [money] NULL ,
	[MothRepelCost] [money] NULL ,
	[RapCost] [money] NULL ,
	[OtherCost] [money] NULL ,
	[SubTotalCost] [money] NULL ,
	[TaxCost] [money] NULL ,
	[TotalCost] [money] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ItemShape] (
	[ID] [int] NOT NULL ,
	[Shape] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ItemType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Job] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[CustomerId] [int] NOT NULL ,
	[ServiceAddressId] [int] NULL ,
	[JobStatusId] [int] NULL ,
	[Description] [nvarchar] (500) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JobStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Mapsco] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[IdOld] [float] NOT NULL ,
	[IdProduct] [float] NOT NULL ,
	[Map] [nvarchar] (6) NOT NULL ,
	[UpperLeftLatitude] [float] NOT NULL ,
	[UpperLeftLongitude] [float] NOT NULL ,
	[UpperRightLatitude] [float] NOT NULL ,
	[UpperRightLongitude] [float] NOT NULL ,
	[LowerLeftLatitude] [float] NOT NULL ,
	[LowerLeftLongitude] [float] NOT NULL ,
	[LowerRightLatitude] [float] NOT NULL ,
	[LowerRightLongitude] [float] NOT NULL ,
	[PrintingSc] [float] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Message] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[EmployeeId] [int] NOT NULL ,
	[MessageTypeId] [int] NOT NULL ,
	[TicketId] [int] NULL ,
	[Notes] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MessageType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PendingTicketGridState] (
	[EmployeeId] [int] NOT NULL ,
	[IsDirty] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Ticket] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[ServmanTicketNum] [nvarchar] (6) NULL ,
	[JobId] [int] NOT NULL ,
	[TicketTypeId] [int] NOT NULL ,
	[TicketStatusId] [int] NULL ,
	[Number] [nvarchar] (50) NULL ,
	[CreateDate] [datetime] NULL ,
	[ServiceDate] [datetime] NULL ,
	[PreferedTimeFrom] [datetime] NULL ,
	[PreferedTimeTo] [datetime] NULL ,
	[DurationMin] [int] NULL ,
	[Description] [nvarchar] (500) NULL ,
	[Message] [nvarchar] (500) NULL ,
	[Notes] [nvarchar] (500) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketEquipmentCapture] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TicketId] [int] NULL ,
	[EquipmentId] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketEquipmentRequirement] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TicketId] [int] NOT NULL ,
	[EquipmentTypeId] [int] NULL ,
	[ServiceQuantity] [int] NULL ,
	[LeaveQuantity] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketItemDelivery] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TicketId] [int] NOT NULL ,
	[ItemId] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketItemRequirement] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TicketId] [int] NOT NULL ,
	[ItemType] [nvarchar] (50) NULL ,
	[ServiceQuantity] [int] NULL ,
	[CaptureQuantity] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketTransaction] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TicketId] [int] NOT NULL ,
	[EmployeeId] [int] NOT NULL ,
	[TicketTransactionTypeId] [int] NOT NULL ,
	[TransactionDate] [datetime] NULL ,
	[Notes] [nvarchar] (500) NULL ,
	[IsSentToServman] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketTransactionEquipment] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TicketTransactionId] [int] NOT NULL ,
	[EquipmentId] [int] NOT NULL ,
	[IsLeft] [bit] NULL ,
	[IsCaptured] [bit] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketTransactionItem] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TicketTransactionId] [int] NOT NULL ,
	[ItemId] [int] NOT NULL ,
	[IsLeft] [bit] NULL ,
	[IsCaptured] [bit] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketTransactionType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TicketType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Van] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[ServmanTruckId] [nvarchar] (6) NULL ,
	[ServmanTruckNum] [nvarchar] (4) NULL ,
	[LicensePlateNumber] [nvarchar] (20) NULL ,
	[EngineNumber] [nvarchar] (50) NULL ,
	[BodyNumber] [nvarchar] (50) NULL ,
	[Color] [nvarchar] (50) NULL ,
	[OilChangeDue] [nvarchar] (50) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[VanDetail] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[VanId] [int] NOT NULL ,
	[DateCreated] [datetime] NULL ,
	[OilChangeDue] [numeric](18, 0) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Work] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DispatchEmployeeId] [int] NOT NULL ,
	[TechnicianEmployeeId] [int] NOT NULL ,
	[VanId] [int] NULL ,
	[StartDate] [datetime] NULL ,
	[WorkStatusId] [int] NULL ,
	[StartMessage] [nvarchar] (500) NULL ,
	[EndMessage] [nvarchar] (500) NULL ,
	[EquipmentNotes] [nvarchar] (500) NULL ,
	[IsSentToServman] [bit] NOT NULL ,
	[CreateDate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkDetail] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkId] [int] NOT NULL ,
	[TicketId] [int] NOT NULL ,
	[TimeBegin] [datetime] NOT NULL ,
	[TimeEnd] [datetime] NOT NULL ,
	[Sequence] [int] NULL ,
	[WorkDetailStatusId] [int] NULL ,
	[IsConfirmed] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkDetailStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkEquipment] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkId] [int] NOT NULL ,
	[EquipmentTypeId] [int] NOT NULL ,
	[Quantity] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransaction] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkId] [int] NOT NULL ,
	[TicketId] [int] NULL ,
	[WorkTransactionTypeId] [int] NOT NULL ,
	[TransactionDate] [datetime] NULL ,
	[AmountCollected] [money] NULL ,
	[IsSentToServman] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransactionEquipment] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkTransactionId] [int] NOT NULL ,
	[EquipmentId] [int] NOT NULL ,
	[IsLeft] [bit] NULL ,
	[IsCaptured] [bit] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransactionEtc] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkTransactionId] [int] NOT NULL ,
	[SaleAmount] [money] NULL ,
	[Hours] [int] NULL ,
	[Minutes] [int] NULL ,
	[Notes] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransactionGps] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkTransactionId] [int] NOT NULL ,
	[Latitude] [float] NOT NULL ,
	[Longitude] [float] NOT NULL ,
	[GpsTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransactionItem] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkTransactionlId] [int] NOT NULL ,
	[ItemId] [int] NULL ,
	[IsLeft] [bit] NOT NULL ,
	[IsCaptured] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransactionPayment] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkTransactionId] [int] NOT NULL ,
	[WorkTransactionPaymentTypeId] [int] NOT NULL ,
	[PaymentAmount] [money] NOT NULL ,
	[FirstName] [nvarchar] (100) NULL ,
	[LastName] [nvarchar] (100) NULL ,
	[City] [nvarchar] (100) NULL ,
	[State] [nvarchar] (30) NULL ,
	[Zip] [nvarchar] (10) NULL ,
	[CreditCardTypeId] [int] NULL ,
	[CreditCardNumber] [nvarchar] (50) NULL ,
	[CreditCardExpirationDate] [datetime] NULL ,
	[CreditCardCVV2] [nvarchar] (10) NULL ,
	[BankCheckNumber] [nvarchar] (50) NULL ,
	[BankRouteNumber] [nvarchar] (50) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransactionPaymentType] (
	[ID] [int] NOT NULL ,
	[PaymentType] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransactionType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NULL ,
	[Description] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkTransactionVanCheck] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WorkTransactionId] [int] NOT NULL ,
	[OilChecked] [bit] NULL ,
	[UnitClean] [bit] NULL ,
	[VanClean] [bit] NULL ,
	[SuppliesStocked] [bit] NULL ,
	[OdometerReading] [numeric](18, 0) NULL ,
	[HobbsReading] [numeric](18, 0) NULL ,
	[SpecialNeeds] [nvarchar] (200) NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Address] ADD 
	CONSTRAINT [PK_Address] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Counter] ADD 
	CONSTRAINT [PK_Counter] PRIMARY KEY  CLUSTERED 
	(
		[CounterName]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CreditCardType] ADD 
	CONSTRAINT [PK_CreditCardType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Customer] ADD 
	CONSTRAINT [PK_Customer] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[DashboardState] ADD 
	CONSTRAINT [PK_DashboardState] PRIMARY KEY  CLUSTERED 
	(
		[EmployeeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Employee] ADD 
	CONSTRAINT [PK_Technician] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[EmployeeType] ADD 
	CONSTRAINT [PK_EmployeeType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Equipment] ADD 
	CONSTRAINT [PK_Equipment] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[EquipmentType] ADD 
	CONSTRAINT [PK_EquipmentType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[EventLog] ADD 
	CONSTRAINT [PK_EventLog] PRIMARY KEY  CLUSTERED 
	(
		[EventLogId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Item] ADD 
	CONSTRAINT [PK_Item] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ItemShape] ADD 
	CONSTRAINT [PK_ItemShape] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ItemType] ADD 
	CONSTRAINT [PK_ItemType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Job] ADD 
	CONSTRAINT [PK_Job] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[JobStatus] ADD 
	CONSTRAINT [PK_JobStatus] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Mapsco] ADD 
	CONSTRAINT [PK_Mapsco] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Message] ADD 
	CONSTRAINT [PK_Message] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[MessageType] ADD 
	CONSTRAINT [PK_MessageType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[PendingTicketGridState] ADD 
	CONSTRAINT [PK_PendingTicketGridState] PRIMARY KEY  CLUSTERED 
	(
		[EmployeeId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ticket] ADD 
	CONSTRAINT [PK_Ticket] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketEquipmentCapture] ADD 
	CONSTRAINT [PK_TicketEquipmentCapture] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketEquipmentRequirement] ADD 
	CONSTRAINT [PK_TicketEquipment] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketItemDelivery] ADD 
	CONSTRAINT [PK_TicketItemDelivery] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketItemRequirement] ADD 
	CONSTRAINT [PK_TicketItem] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketStatus] ADD 
	CONSTRAINT [PK_TicketStatus] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketTransaction] ADD 
	CONSTRAINT [DF_TicketTransaction_IsSentToServman] DEFAULT (0) FOR [IsSentToServman],
	CONSTRAINT [PK_TicketTransaction] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketTransactionEquipment] ADD 
	CONSTRAINT [PK_TicketTransactionEquipment] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketTransactionItem] ADD 
	CONSTRAINT [PK_TicketTransactionItem] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketTransactionType] ADD 
	CONSTRAINT [PK_TicketTransactionType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TicketType] ADD 
	CONSTRAINT [PK_TicketType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Van] ADD 
	CONSTRAINT [PK_Van] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[VanDetail] ADD 
	CONSTRAINT [PK_VanDetail] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Work] ADD 
	CONSTRAINT [DF_Work_IsSentToServman] DEFAULT (0) FOR [IsSentToServman],
	CONSTRAINT [PK_Work] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkDetail] ADD 
	CONSTRAINT [PK_WorkDetail] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkDetailStatus] ADD 
	CONSTRAINT [PK_WorkDetailStatus] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkEquipment] ADD 
	CONSTRAINT [PK_WorkEquipment] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkStatus] ADD 
	CONSTRAINT [PK_WorkStatus] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransaction] ADD 
	CONSTRAINT [DF_WorkTransaction_IsSentToServman] DEFAULT (0) FOR [IsSentToServman],
	CONSTRAINT [PK_WorkTransaction] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransactionEquipment] ADD 
	CONSTRAINT [PK_WorkTransactionEquipment] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransactionEtc] ADD 
	CONSTRAINT [PK_WorkTransactionEtc] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransactionGps] ADD 
	CONSTRAINT [PK_WorkTransactionGps] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransactionItem] ADD 
	CONSTRAINT [PK_WorkTransactionItem] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransactionPayment] ADD 
	CONSTRAINT [PK_WorkTransactionPayment] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransactionPaymentType] ADD 
	CONSTRAINT [PK_WorkTransactionPaymentType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransactionType] ADD 
	CONSTRAINT [PK_WorkTransactionType] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WorkTransactionVanCheck] ADD 
	CONSTRAINT [PK_WorkTransactionVanCheck] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Customer] ADD 
	CONSTRAINT [FK_Customer_Address] FOREIGN KEY 
	(
		[AddressId]
	) REFERENCES [dbo].[Address] (
		[ID]
	)
GO

ALTER TABLE [dbo].[DashboardState] ADD 
	CONSTRAINT [FK_DashboardState_Employee] FOREIGN KEY 
	(
		[EmployeeId]
	) REFERENCES [dbo].[Employee] (
		[ID]
	)
GO

ALTER TABLE [dbo].[Employee] ADD 
	CONSTRAINT [FK_Employee_EmployeeType] FOREIGN KEY 
	(
		[EmployeeTypeId]
	) REFERENCES [dbo].[EmployeeType] (
		[ID]
	),
	CONSTRAINT [FK_Technician_Address] FOREIGN KEY 
	(
		[AddressId]
	) REFERENCES [dbo].[Address] (
		[ID]
	)
GO

ALTER TABLE [dbo].[Equipment] ADD 
	CONSTRAINT [FK_Equipment_EquipmentType] FOREIGN KEY 
	(
		[EquipmentTypeId]
	) REFERENCES [dbo].[EquipmentType] (
		[ID]
	)
GO

ALTER TABLE [dbo].[Item] ADD 
	CONSTRAINT [FK_Item_ItemShape] FOREIGN KEY 
	(
		[ItemShapeId]
	) REFERENCES [dbo].[ItemShape] (
		[ID]
	),
	CONSTRAINT [FK_Item_ItemType] FOREIGN KEY 
	(
		[ItemTypeId]
	) REFERENCES [dbo].[ItemType] (
		[ID]
	)
GO

ALTER TABLE [dbo].[Job] ADD 
	CONSTRAINT [FK_Job_Address] FOREIGN KEY 
	(
		[ServiceAddressId]
	) REFERENCES [dbo].[Address] (
		[ID]
	),
	CONSTRAINT [FK_Job_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [dbo].[Customer] (
		[ID]
	),
	CONSTRAINT [FK_Job_JobStatus] FOREIGN KEY 
	(
		[JobStatusId]
	) REFERENCES [dbo].[JobStatus] (
		[ID]
	)
GO

ALTER TABLE [dbo].[Message] ADD 
	CONSTRAINT [FK_Message_Ticket] FOREIGN KEY 
	(
		[TicketId]
	) REFERENCES [dbo].[Ticket] (
		[ID]
	),
	CONSTRAINT [FK_MessageQueue_Employee] FOREIGN KEY 
	(
		[EmployeeId]
	) REFERENCES [dbo].[Employee] (
		[ID]
	),
	CONSTRAINT [FK_MessageQueue_MessageType] FOREIGN KEY 
	(
		[MessageTypeId]
	) REFERENCES [dbo].[MessageType] (
		[ID]
	)
GO

ALTER TABLE [dbo].[PendingTicketGridState] ADD 
	CONSTRAINT [FK_PendingTicketGridState_Employee] FOREIGN KEY 
	(
		[EmployeeId]
	) REFERENCES [dbo].[Employee] (
		[ID]
	)
GO

ALTER TABLE [dbo].[Ticket] ADD 
	CONSTRAINT [FK_Ticket_Job] FOREIGN KEY 
	(
		[JobId]
	) REFERENCES [dbo].[Job] (
		[ID]
	),
	CONSTRAINT [FK_Ticket_TicketStatus] FOREIGN KEY 
	(
		[TicketStatusId]
	) REFERENCES [dbo].[TicketStatus] (
		[ID]
	),
	CONSTRAINT [FK_Ticket_TicketType] FOREIGN KEY 
	(
		[TicketTypeId]
	) REFERENCES [dbo].[TicketType] (
		[ID]
	)
GO

ALTER TABLE [dbo].[TicketEquipmentCapture] ADD 
	CONSTRAINT [FK_TicketEquipmentCapture_Equipment] FOREIGN KEY 
	(
		[EquipmentId]
	) REFERENCES [dbo].[Equipment] (
		[ID]
	),
	CONSTRAINT [FK_TicketEquipmentCapture_Ticket] FOREIGN KEY 
	(
		[TicketId]
	) REFERENCES [dbo].[Ticket] (
		[ID]
	)
GO

ALTER TABLE [dbo].[TicketEquipmentRequirement] ADD 
	CONSTRAINT [FK_TicketEquipment_EquipmentType] FOREIGN KEY 
	(
		[EquipmentTypeId]
	) REFERENCES [dbo].[EquipmentType] (
		[ID]
	),
	CONSTRAINT [FK_TicketEquipment_Ticket] FOREIGN KEY 
	(
		[TicketId]
	) REFERENCES [dbo].[Ticket] (
		[ID]
	)
GO

ALTER TABLE [dbo].[TicketItemDelivery] ADD 
	CONSTRAINT [FK_TicketItemDelivery_Item] FOREIGN KEY 
	(
		[ItemId]
	) REFERENCES [dbo].[Item] (
		[ID]
	),
	CONSTRAINT [FK_TicketItemDelivery_Ticket] FOREIGN KEY 
	(
		[TicketId]
	) REFERENCES [dbo].[Ticket] (
		[ID]
	)
GO

ALTER TABLE [dbo].[TicketItemRequirement] ADD 
	CONSTRAINT [FK_TicketItem_Ticket] FOREIGN KEY 
	(
		[TicketId]
	) REFERENCES [dbo].[Ticket] (
		[ID]
	)
GO

ALTER TABLE [dbo].[TicketTransaction] ADD 
	CONSTRAINT [FK_TicketTransaction_Employee] FOREIGN KEY 
	(
		[EmployeeId]
	) REFERENCES [dbo].[Employee] (
		[ID]
	),
	CONSTRAINT [FK_TicketTransaction_Ticket] FOREIGN KEY 
	(
		[TicketId]
	) REFERENCES [dbo].[Ticket] (
		[ID]
	),
	CONSTRAINT [FK_TicketTransaction_TicketTransactionType] FOREIGN KEY 
	(
		[TicketTransactionTypeId]
	) REFERENCES [dbo].[TicketTransactionType] (
		[ID]
	)
GO

ALTER TABLE [dbo].[TicketTransactionEquipment] ADD 
	CONSTRAINT [FK_TicketTransactionEquipment_Equipment] FOREIGN KEY 
	(
		[EquipmentId]
	) REFERENCES [dbo].[Equipment] (
		[ID]
	),
	CONSTRAINT [FK_TicketTransactionEquipment_TicketTransaction] FOREIGN KEY 
	(
		[TicketTransactionId]
	) REFERENCES [dbo].[TicketTransaction] (
		[ID]
	)
GO

ALTER TABLE [dbo].[TicketTransactionItem] ADD 
	CONSTRAINT [FK_TicketTransactionItem_Item] FOREIGN KEY 
	(
		[ItemId]
	) REFERENCES [dbo].[Item] (
		[ID]
	),
	CONSTRAINT [FK_TicketTransactionItem_TicketTransaction] FOREIGN KEY 
	(
		[TicketTransactionId]
	) REFERENCES [dbo].[TicketTransaction] (
		[ID]
	)
GO

ALTER TABLE [dbo].[VanDetail] ADD 
	CONSTRAINT [FK_VanDetail_Van] FOREIGN KEY 
	(
		[VanId]
	) REFERENCES [dbo].[Van] (
		[ID]
	)
GO

ALTER TABLE [dbo].[Work] ADD 
	CONSTRAINT [FK_Work_Employee] FOREIGN KEY 
	(
		[DispatchEmployeeId]
	) REFERENCES [dbo].[Employee] (
		[ID]
	),
	CONSTRAINT [FK_Work_Employee1] FOREIGN KEY 
	(
		[TechnicianEmployeeId]
	) REFERENCES [dbo].[Employee] (
		[ID]
	),
	CONSTRAINT [FK_Work_Van] FOREIGN KEY 
	(
		[VanId]
	) REFERENCES [dbo].[Van] (
		[ID]
	),
	CONSTRAINT [FK_Work_WorkStatus] FOREIGN KEY 
	(
		[WorkStatusId]
	) REFERENCES [dbo].[WorkStatus] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkDetail] ADD 
	CONSTRAINT [FK_WorkDetail_Ticket] FOREIGN KEY 
	(
		[TicketId]
	) REFERENCES [dbo].[Ticket] (
		[ID]
	),
	CONSTRAINT [FK_WorkDetail_Work] FOREIGN KEY 
	(
		[WorkId]
	) REFERENCES [dbo].[Work] (
		[ID]
	),
	CONSTRAINT [FK_WorkDetail_WorkDetailStatus] FOREIGN KEY 
	(
		[WorkDetailStatusId]
	) REFERENCES [dbo].[WorkDetailStatus] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkEquipment] ADD 
	CONSTRAINT [FK_WorkEquipment_EquipmentType] FOREIGN KEY 
	(
		[EquipmentTypeId]
	) REFERENCES [dbo].[EquipmentType] (
		[ID]
	),
	CONSTRAINT [FK_WorkEquipment_Work] FOREIGN KEY 
	(
		[WorkId]
	) REFERENCES [dbo].[Work] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkTransaction] ADD 
	CONSTRAINT [FK_WorkTransaction_Ticket] FOREIGN KEY 
	(
		[TicketId]
	) REFERENCES [dbo].[Ticket] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransaction_Work] FOREIGN KEY 
	(
		[WorkId]
	) REFERENCES [dbo].[Work] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransaction_WorkTransactionType] FOREIGN KEY 
	(
		[WorkTransactionTypeId]
	) REFERENCES [dbo].[WorkTransactionType] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkTransactionEquipment] ADD 
	CONSTRAINT [FK_WorkTransactionEquipment_Equipment] FOREIGN KEY 
	(
		[EquipmentId]
	) REFERENCES [dbo].[Equipment] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionEquipment_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [dbo].[WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkTransactionEtc] ADD 
	CONSTRAINT [FK_WorkTransactionEtc_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [dbo].[WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkTransactionGps] ADD 
	CONSTRAINT [FK_WorkTransactionGps_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [dbo].[WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkTransactionItem] ADD 
	CONSTRAINT [FK_WorkTransactionItem_Item] FOREIGN KEY 
	(
		[ItemId]
	) REFERENCES [dbo].[Item] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionItem_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionlId]
	) REFERENCES [dbo].[WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkTransactionPayment] ADD 
	CONSTRAINT [FK_WorkTransactionPayment_CreditCardType] FOREIGN KEY 
	(
		[CreditCardTypeId]
	) REFERENCES [dbo].[CreditCardType] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionPayment_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [dbo].[WorkTransaction] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionPayment_WorkTransactionPaymentType] FOREIGN KEY 
	(
		[WorkTransactionPaymentTypeId]
	) REFERENCES [dbo].[WorkTransactionPaymentType] (
		[ID]
	)
GO

ALTER TABLE [dbo].[WorkTransactionVanCheck] ADD 
	CONSTRAINT [FK_WorkTransactionVanCheck_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [dbo].[WorkTransaction] (
		[ID]
	)
GO

