CREATE TABLE [Address] (
	[ID] [int] NOT NULL ,
	[Address1] [nvarchar] (200) NULL ,
	[Address2] [nvarchar] (200) NULL ,
	[City] [nvarchar] (100) NULL ,
	[State] [nvarchar] (30) NULL ,
	[Zip] [nvarchar] (10) NULL ,
	[Map] [nvarchar] (10) NULL 
) 
GO

CREATE TABLE [Application] (
	[ID] [int] NOT NULL ,
	[ApplicationStateId] [int] NOT NULL ,
	[WorkId] [int] NULL 
) 
GO

CREATE TABLE [ApplicationState] (
	[ID] [int] NOT NULL ,
	[State] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [Counter] (
	[CounterName] [nvarchar] (40) NOT NULL ,
	[Val] [int] NOT NULL 
) 
GO

CREATE TABLE [Customer] (
	[ID] [int] NOT NULL ,
	[AddressId] [int] NULL ,
	[FirstName] [nvarchar] (100) NULL ,
	[LastName] [nvarchar] (100) NULL ,
	[Phone1] [nvarchar] (50) NULL ,
	[Phone2] [nvarchar] (50) NULL 
) 
GO

CREATE TABLE [Employee] (
	[ID] [int] NOT NULL ,
	[EmployeeTypeId] [int] NOT NULL ,
	[AddressId] [int] NULL ,
	[FirstName] [nvarchar] (100) NULL ,
	[LastName] [nvarchar] (100) NULL ,
	[HireDate] [datetime] NULL ,
	[Phone1] [nvarchar] (50) NULL ,
	[Phone2] [nvarchar] (50) NULL ,
	[Password] [nvarchar] (100) NULL 
) 
GO

CREATE TABLE [EmployeeType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [Equipment] (
	[ID] [int] NOT NULL ,
	[EquipmentTypeId] [int] NULL ,
	[SerialNumber] [nvarchar] (50) NULL 
) 
GO

CREATE TABLE [EquipmentType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NULL ,
	[Description] [nvarchar] (200) NULL 
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

CREATE TABLE [Item] (
	[ID] [int] NOT NULL ,
	[ServerId] [int] NULL ,
	[ItemTypeId] [int] NOT NULL ,
	[SerialNumber] [nvarchar] (50) NULL ,
	[ItemShapeId] [int] NULL ,
	[Width] [numeric](18, 0) NULL ,
	[Height] [numeric](18, 0) NULL ,
	[Diameter] [numeric](18, 0) NULL ,
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
) 
GO

CREATE TABLE [ItemShape] (
	[ID] [int] NOT NULL ,
	[Shape] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [ItemType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [Project] (
	[ID] [int] NOT NULL ,
	[CustomerId] [int] NOT NULL ,
	[ServiceAddressId] [int] NULL ,
	[ProjectTypeId] [int] NOT NULL ,
	[ProjectStatusId] [int] NULL ,
	[Description] [nvarchar] (500) NULL 
) 
GO

CREATE TABLE [ProjectStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [ProjectType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [Task] (
	[ID] [int] NOT NULL ,
	[ServerId] [int] NULL ,	
	[ProjectId] [int] NOT NULL ,
	[VisitId] [int] NULL ,
	[TaskTypeId] [int] NOT NULL ,
	[TaskStatusId] [int] NULL ,
	[Number] [nvarchar] (50) NULL ,
	[Sequence] [int] NULL ,
	[CreateDate] [datetime] NULL ,
	[ServiceDate] [datetime] NULL ,
	[DurationMin] [int] NULL ,
	[Description] [nvarchar] (500) NULL ,
	[Message] [nvarchar] (500) NULL ,
	[Notes] [nvarchar] (500) NULL 
) 
GO

CREATE TABLE [TaskEquipmentCapture] (
	[ID] [int] NOT NULL ,
	[TaskId] [int] NULL ,
	[EquipmentId] [int] NULL 
) 
GO

CREATE TABLE [TaskEquipmentRequirement] (
	[ID] [int] NOT NULL ,
	[TaskId] [int] NOT NULL ,
	[EquipmentTypeId] [int] NULL ,
	[ServiceQuantity] [int] NULL ,
	[LeaveQuantity] [int] NULL 
) 
GO

CREATE TABLE [TaskItemDelivery] (
	[ID] [int] NOT NULL ,
	[TaskId] [int] NOT NULL ,
	[ItemId] [int] NULL 
) 
GO

CREATE TABLE [TaskItemRequirement] (
	[ID] [int] NOT NULL ,
	[TaskId] [int] NOT NULL ,
	[ItemType] [nvarchar] (50) NULL ,
	[ServiceQuantity] [int] NULL ,
	[CaptureQuantity] [int] NULL 
) 
GO

CREATE TABLE [TaskStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [TaskType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [Van] (
	[ID] [int] NOT NULL ,
	[LicensePlateNumber] [nvarchar] (20) NULL ,
	[EngineNumber] [nvarchar] (50) NULL ,
	[BodyNumber] [nvarchar] (50) NULL ,
	[Color] [nvarchar] (50) NULL ,
	[OilChangeDue] [nvarchar] (50) NULL 
) 
GO

CREATE TABLE [VanDetail] (
	[ID] [int] NOT NULL ,
	[VanId] [int] NOT NULL ,
	[DateCreated] [datetime] NULL ,
	[OilChangeDue] [numeric](18, 0) NULL 
) 
GO

CREATE TABLE [Visit] (
	[ID] [int] NOT NULL ,
	[VisitStatusId] [int] NULL ,
	[CreateDate] [datetime] NULL ,
	[ServiceDate] [datetime] NULL ,
	[PreferedTimeFrom] [datetime] NULL ,
	[PreferedTimeTo] [datetime] NULL ,
	[CustomerId] [int] NULL ,
	[ServiceAddressId] [int] NULL ,
	[Notes] [nvarchar] (500) NULL 
) 
GO

CREATE TABLE [VisitStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [Work] (
	[ID] [int] NOT NULL ,
	[DispatchEmployeeId] [int] NOT NULL ,
	[TechnicianEmployeeId] [int] NOT NULL ,
	[VanId] [int] NOT NULL ,
	[StartDate] [datetime] NULL ,
	[WorkStatusId] [int] NULL ,
	[StartMessage] [nvarchar] (500) NULL ,
	[EndMessage] [nvarchar] (500) NULL ,
	[EquipmentNotes] [nvarchar] (500) NULL 
) 
GO

CREATE TABLE [WorkDetail] (
	[ID] [int] NOT NULL ,
	[WorkId] [int] NOT NULL ,
	[VisitId] [int] NOT NULL ,
	[Sequence] [int] NULL ,
	[WorkDetailStatusId] [int] NULL 
) 
GO

CREATE TABLE [WorkDetailStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [WorkEquipment] (
	[ID] [int] NOT NULL ,
	[WorkId] [int] NOT NULL ,
	[EquipmentTypeId] [int] NOT NULL ,
	[Quantity] [int] NULL 
) 
GO

CREATE TABLE [WorkStatus] (
	[ID] [int] NOT NULL ,
	[Status] [nvarchar] (50) NOT NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [WorkTransaction] (
	[ID] [int] NOT NULL ,
	[WorkId] [int] NOT NULL ,
	[VisitId] [int] NULL ,
	[WorkTransactionTypeId] [int] NOT NULL ,
	[TransactionDate] [datetime] NULL ,
	[AmountCollected] [money] NULL ,
	[IsSent] [bit] NOT NULL ,
	[Notes] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [WorkTransactionEquipment] (
	[ID] [int] NOT NULL ,
	[WorkTransactionId] [int] NOT NULL ,
	[EquipmentId] [int] NOT NULL ,
	[IsLeft] [bit] NULL ,
	[IsCaptured] [bit] NULL 
) 
GO

CREATE TABLE [WorkTransactionEtc] (
	[WorkTransactionId] [int] NOT NULL ,
	[SaleAmount] [money] NULL ,
	[Hours] [int] NULL ,
	[Minutes] [int] NULL ,
	[Notes] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [WorkTransactionGps] (
	[WorkTransactionId] [int] NOT NULL ,
	[Latitude] [float] NOT NULL ,
	[Longitude] [float] NOT NULL ,
	[GpsTime] [datetime] NOT NULL 
) 
GO

CREATE TABLE [WorkTransactionItem] (
	[ID] [int] NOT NULL ,
	[WorkTransactionId] [int] NOT NULL ,
	[ItemId] [int] NULL ,
	[IsLeft] [bit] NOT NULL ,
	[IsCaptured] [bit] NOT NULL 
) 
GO

CREATE TABLE [WorkTransactionTask] (
	[ID] [int] NOT NULL ,
	[WorkTransactionId] [int] NOT NULL ,
	[TaskId] [int] NOT NULL ,
	[TaskStatusId] [int] NOT NULL ,
	[AmountCollected] [money] NOT NULL 
) 
GO

CREATE TABLE [WorkTransactionTaskEquipment] (
	[ID] [int] NOT NULL ,
	[WorkTransactionTaskId] [int] NOT NULL ,
	[EquipmentId] [int] NOT NULL ,
	[IsLeft] [bit] NULL ,
	[IsCaptured] [bit] NULL 
) 
GO

CREATE TABLE [WorkTransactionTaskItem] (
	[ID] [int] NOT NULL ,
	[WorkTransactionTaskId] [int] NOT NULL ,
	[ItemId] [int] NOT NULL ,
	[IsLeft] [bit] NULL ,
	[IsCaptured] [bit] NULL 
) 
GO

CREATE TABLE [WorkTransactionType] (
	[ID] [int] NOT NULL ,
	[Type] [nvarchar] (50) NULL ,
	[Description] [nvarchar] (200) NULL 
) 
GO

CREATE TABLE [WorkTransactionVanCheck] (
	[WorkTransactionId] [int] NOT NULL ,
	[OilChecked] [bit] NULL ,
	[UnitClean] [bit] NULL ,
	[VanClean] [bit] NULL ,
	[SuppliesStocked] [bit] NULL ,
	[OdometerReading] [numeric](18, 0) NULL ,
	[HobbsReading] [numeric](18, 0) NULL ,
	[SpecialNeeds] [nvarchar] (200) NULL 
) 
GO

ALTER TABLE [Address] ADD 
	CONSTRAINT [PK_Address] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Application] ADD 
	CONSTRAINT [PK_Application] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [ApplicationState] ADD 
	CONSTRAINT [PK_ApplicationState] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Counter] ADD 
	CONSTRAINT [PK_Counter] PRIMARY KEY 
	(
		[CounterName]
	)   
GO

ALTER TABLE [Customer] ADD 
	CONSTRAINT [PK_Customer] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Employee] ADD 
	CONSTRAINT [PK_Technician] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [EmployeeType] ADD 
	CONSTRAINT [PK_EmployeeType] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Equipment] ADD 
	CONSTRAINT [PK_Equipment] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [EquipmentType] ADD 
	CONSTRAINT [PK_EquipmentType] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [EventLog] ADD 
	CONSTRAINT [PK_EventLog] PRIMARY KEY 
	(
		[EventLogId]
	)   
GO

ALTER TABLE [Item] ADD 
	CONSTRAINT [PK_Item] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [ItemShape] ADD 
	CONSTRAINT [PK_ItemShape] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [ItemType] ADD 
	CONSTRAINT [PK_ItemType] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Project] ADD 
	CONSTRAINT [PK_Project] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [ProjectStatus] ADD 
	CONSTRAINT [PK_ProjectStatus] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [ProjectType] ADD 
	CONSTRAINT [PK_ProjectType] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Task] ADD 
	CONSTRAINT [PK_Task] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [TaskEquipmentCapture] ADD 
	CONSTRAINT [PK_TasktEquipmentCapture] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [TaskEquipmentRequirement] ADD 
	CONSTRAINT [PK_TaskEquipment] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [TaskItemDelivery] ADD 
	CONSTRAINT [PK_TaskItemDelivery] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [TaskItemRequirement] ADD 
	CONSTRAINT [PK_TaskItem] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [TaskStatus] ADD 
	CONSTRAINT [PK_TaskStatus] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [TaskType] ADD 
	CONSTRAINT [PK_TaskType] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Van] ADD 
	CONSTRAINT [PK_Van] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [VanDetail] ADD 
	CONSTRAINT [PK_VanDetail] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Visit] ADD 
	CONSTRAINT [PK_Visit] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [VisitStatus] ADD 
	CONSTRAINT [PK_VisitStatus] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [Work] ADD 
	CONSTRAINT [PK_Work] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkDetail] ADD 
	CONSTRAINT [PK_WorkDetail] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkDetailStatus] ADD 
	CONSTRAINT [PK_WorkDetailStatus] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkEquipment] ADD 
	CONSTRAINT [PK_WorkEquipment] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkStatus] ADD 
	CONSTRAINT [PK_WorkStatus] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkTransaction] ADD 
	CONSTRAINT [PK_WorkTransaction] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkTransactionEquipment] ADD 
	CONSTRAINT [PK_WorkTransactionEquipment] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkTransactionEtc] ADD 
	CONSTRAINT [PK_WorkTransactionEtc] PRIMARY KEY 
	(
		[WorkTransactionId]
	)   
GO

ALTER TABLE [WorkTransactionGps] ADD 
	CONSTRAINT [PK_WorkTransactionGps] PRIMARY KEY 
	(
		[WorkTransactionId]
	)   
GO

ALTER TABLE [WorkTransactionItem] ADD 
	CONSTRAINT [PK_WorkTransactionItem] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkTransactionTask] ADD 
	CONSTRAINT [PK_WorkTransactionTask] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkTransactionTaskEquipment] ADD 
	CONSTRAINT [PK_WorkTransactionTaskEquipment] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkTransactionTaskItem] ADD 
	CONSTRAINT [PK_WorkTransactionTaskItem] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkTransactionType] ADD 
	CONSTRAINT [PK_WorkTransactionType] PRIMARY KEY 
	(
		[ID]
	)   
GO

ALTER TABLE [WorkTransactionVanCheck] ADD 
	CONSTRAINT [PK_WorkTransactionVanCheck] PRIMARY KEY 
	(
		[WorkTransactionId]
	)   
GO

ALTER TABLE [Application] ADD 
	CONSTRAINT [FK_Application_ApplicationState] FOREIGN KEY 
	(
		[ApplicationStateId]
	) REFERENCES [ApplicationState] (
		[ID]
	),
	CONSTRAINT [FK_Application_Work] FOREIGN KEY 
	(
		[WorkId]
	) REFERENCES [Work] (
		[ID]
	)
GO

ALTER TABLE [Customer] ADD 
	CONSTRAINT [FK_Customer_Address] FOREIGN KEY 
	(
		[AddressId]
	) REFERENCES [Address] (
		[ID]
	)
GO

ALTER TABLE [Employee] ADD 
	CONSTRAINT [FK_Employee_EmployeeType] FOREIGN KEY 
	(
		[EmployeeTypeId]
	) REFERENCES [EmployeeType] (
		[ID]
	),
	CONSTRAINT [FK_Technician_Address] FOREIGN KEY 
	(
		[AddressId]
	) REFERENCES [Address] (
		[ID]
	)
GO

ALTER TABLE [Equipment] ADD 
	CONSTRAINT [FK_Equipment_EquipmentType] FOREIGN KEY 
	(
		[EquipmentTypeId]
	) REFERENCES [EquipmentType] (
		[ID]
	)
GO

ALTER TABLE [Item] ADD 
	CONSTRAINT [FK_Item_ItemShape] FOREIGN KEY 
	(
		[ItemShapeId]
	) REFERENCES [ItemShape] (
		[ID]
	),
	CONSTRAINT [FK_Item_ItemType] FOREIGN KEY 
	(
		[ItemTypeId]
	) REFERENCES [ItemType] (
		[ID]
	)
GO

ALTER TABLE [Project] ADD 
	CONSTRAINT [FK_Project_Address] FOREIGN KEY 
	(
		[ServiceAddressId]
	) REFERENCES [Address] (
		[ID]
	),
	CONSTRAINT [FK_Project_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [Customer] (
		[ID]
	),
	CONSTRAINT [FK_Project_ProjectStatus] FOREIGN KEY 
	(
		[ProjectStatusId]
	) REFERENCES [ProjectStatus] (
		[ID]
	),
	CONSTRAINT [FK_Project_ProjectType] FOREIGN KEY 
	(
		[ProjectTypeId]
	) REFERENCES [ProjectType] (
		[ID]
	)
GO

ALTER TABLE [Task] ADD 
	CONSTRAINT [FK_Task_Project] FOREIGN KEY 
	(
		[ProjectId]
	) REFERENCES [Project] (
		[ID]
	),
	CONSTRAINT [FK_Task_TaskStatus] FOREIGN KEY 
	(
		[TaskStatusId]
	) REFERENCES [TaskStatus] (
		[ID]
	),
	CONSTRAINT [FK_Task_TaskType] FOREIGN KEY 
	(
		[TaskTypeId]
	) REFERENCES [TaskType] (
		[ID]
	),
	CONSTRAINT [FK_Task_Visit] FOREIGN KEY 
	(
		[VisitId]
	) REFERENCES [Visit] (
		[ID]
	)
GO

ALTER TABLE [TaskEquipmentCapture] ADD 
	CONSTRAINT [FK_TaskEquipmentCapture_Equipment] FOREIGN KEY 
	(
		[EquipmentId]
	) REFERENCES [Equipment] (
		[ID]
	),
	CONSTRAINT [FK_TaskEquipmentCapture_Task] FOREIGN KEY 
	(
		[TaskId]
	) REFERENCES [Task] (
		[ID]
	)
GO

ALTER TABLE [TaskEquipmentRequirement] ADD 
	CONSTRAINT [FK_TaskEquipment_EquipmentType] FOREIGN KEY 
	(
		[EquipmentTypeId]
	) REFERENCES [EquipmentType] (
		[ID]
	),
	CONSTRAINT [FK_TaskEquipmentRequirement_Task] FOREIGN KEY 
	(
		[TaskId]
	) REFERENCES [Task] (
		[ID]
	)
GO

ALTER TABLE [TaskItemDelivery] ADD 
	CONSTRAINT [FK_TaskItemDelivery_Item] FOREIGN KEY 
	(
		[ItemId]
	) REFERENCES [Item] (
		[ID]
	),
	CONSTRAINT [FK_TaskItemDelivery_Task] FOREIGN KEY 
	(
		[TaskId]
	) REFERENCES [Task] (
		[ID]
	)
GO

ALTER TABLE [TaskItemRequirement] ADD 
	CONSTRAINT [FK_TaskItemRequirement_Task] FOREIGN KEY 
	(
		[TaskId]
	) REFERENCES [Task] (
		[ID]
	)
GO

ALTER TABLE [VanDetail] ADD 
	CONSTRAINT [FK_VanDetail_Van] FOREIGN KEY 
	(
		[VanId]
	) REFERENCES [Van] (
		[ID]
	)
GO

ALTER TABLE [Visit] ADD 
	CONSTRAINT [FK_Visit_Address] FOREIGN KEY 
	(
		[ServiceAddressId]
	) REFERENCES [Address] (
		[ID]
	),
	CONSTRAINT [FK_Visit_Customer] FOREIGN KEY 
	(
		[CustomerId]
	) REFERENCES [Customer] (
		[ID]
	),
	CONSTRAINT [FK_Visit_VisitStatus] FOREIGN KEY 
	(
		[VisitStatusId]
	) REFERENCES [VisitStatus] (
		[ID]
	)
GO

ALTER TABLE [Work] ADD 
	CONSTRAINT [FK_Work_Employee] FOREIGN KEY 
	(
		[DispatchEmployeeId]
	) REFERENCES [Employee] (
		[ID]
	),
	CONSTRAINT [FK_Work_Employee1] FOREIGN KEY 
	(
		[TechnicianEmployeeId]
	) REFERENCES [Employee] (
		[ID]
	),
	CONSTRAINT [FK_Work_Van] FOREIGN KEY 
	(
		[VanId]
	) REFERENCES [Van] (
		[ID]
	),
	CONSTRAINT [FK_Work_WorkStatus] FOREIGN KEY 
	(
		[WorkStatusId]
	) REFERENCES [WorkStatus] (
		[ID]
	)
GO

ALTER TABLE [WorkDetail] ADD 
	CONSTRAINT [FK_WorkDetail_Visit] FOREIGN KEY 
	(
		[VisitId]
	) REFERENCES [Visit] (
		[ID]
	),
	CONSTRAINT [FK_WorkDetail_Work] FOREIGN KEY 
	(
		[WorkId]
	) REFERENCES [Work] (
		[ID]
	),
	CONSTRAINT [FK_WorkDetail_WorkDetailStatus] FOREIGN KEY 
	(
		[WorkDetailStatusId]
	) REFERENCES [WorkDetailStatus] (
		[ID]
	)
GO

ALTER TABLE [WorkEquipment] ADD 
	CONSTRAINT [FK_WorkEquipment_EquipmentType] FOREIGN KEY 
	(
		[EquipmentTypeId]
	) REFERENCES [EquipmentType] (
		[ID]
	),
	CONSTRAINT [FK_WorkEquipment_Work] FOREIGN KEY 
	(
		[WorkId]
	) REFERENCES [Work] (
		[ID]
	)
GO

ALTER TABLE [WorkTransaction] ADD 
	CONSTRAINT [FK_WorkTransaction_Visit] FOREIGN KEY 
	(
		[VisitId]
	) REFERENCES [Visit] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransaction_Work] FOREIGN KEY 
	(
		[WorkId]
	) REFERENCES [Work] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransaction_WorkTransactionType] FOREIGN KEY 
	(
		[WorkTransactionTypeId]
	) REFERENCES [WorkTransactionType] (
		[ID]
	)
GO

ALTER TABLE [WorkTransactionEquipment] ADD 
	CONSTRAINT [FK_WorkTransactionEquipment_Equipment] FOREIGN KEY 
	(
		[EquipmentId]
	) REFERENCES [Equipment] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionEquipment_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [WorkTransactionEtc] ADD 
	CONSTRAINT [FK_WorkTransactionEtc_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [WorkTransactionGps] ADD 
	CONSTRAINT [FK_WorkTransactionGps_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [WorkTransactionItem] ADD 
	CONSTRAINT [FK_WorkTransactionItem_Item] FOREIGN KEY 
	(
		[ItemId]
	) REFERENCES [Item] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionItem_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [WorkTransactionTask] ADD 
	CONSTRAINT [FK_WorkTransactionTask_Task] FOREIGN KEY 
	(
		[TaskId]
	) REFERENCES [Task] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionTask_TaskStatus] FOREIGN KEY 
	(
		[TaskStatusId]
	) REFERENCES [TaskStatus] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionTask_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [WorkTransaction] (
		[ID]
	)
GO

ALTER TABLE [WorkTransactionTaskEquipment] ADD 
	CONSTRAINT [FK_WorkTransactionTaskEquipment_Equipment] FOREIGN KEY 
	(
		[EquipmentId]
	) REFERENCES [Equipment] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionTaskEquipment_WorkTransactionTask] FOREIGN KEY 
	(
		[WorkTransactionTaskId]
	) REFERENCES [WorkTransactionTask] (
		[ID]
	)
GO

ALTER TABLE [WorkTransactionTaskItem] ADD 
	CONSTRAINT [FK_WorkTransactionTaskItem_Item] FOREIGN KEY 
	(
		[ItemId]
	) REFERENCES [Item] (
		[ID]
	),
	CONSTRAINT [FK_WorkTransactionTaskItem_WorkTransactionTask] FOREIGN KEY 
	(
		[WorkTransactionTaskId]
	) REFERENCES [WorkTransactionTask] (
		[ID]
	)
GO

ALTER TABLE [WorkTransactionVanCheck] ADD 
	CONSTRAINT [FK_WorkTransactionVanCheck_WorkTransaction] FOREIGN KEY 
	(
		[WorkTransactionId]
	) REFERENCES [WorkTransaction] (
		[ID]
	)
GO

