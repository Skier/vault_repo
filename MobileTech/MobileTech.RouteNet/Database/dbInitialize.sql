-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- Dictionaries
-------------------------------------------------------------------------
-------------------------------------------------------------------------

-- Route Status
-------------------------------------------------------------------------
Insert Into RouteStatus(RouteStatusId, [Name], Description) 
Values(
1,
'IDL',
'Initial download.  Application installed, but no data loaded to the device')

GO

Insert Into RouteStatus(RouteStatusId, [Name], Description)  
Values(
2,
'IDL_TCOM_DONE',
'Initial communication is done.  Route data loaded to the device')

GO

Insert Into RouteStatus(RouteStatusId, [Name], Description) 
Values(
3,
'SOP_DONE',
'Start of period completed')

GO

Insert Into RouteStatus(RouteStatusId, [Name], Description)  
Values(
4,
'EOP_DONE',
'End of period completed')

GO

Insert Into RouteStatus(RouteStatusId, [Name], Description) 
Values(
5,
'EOP_TCOM_DONE',
'Regular End OF Period communication is completed')

GO

Insert Into RouteStatus(RouteStatusId, [Name], Description) 
Values(
6,
'LOAD_DONE',
'Load operation completed')

GO

Insert Into RouteStatus(RouteStatusId, [Name], Description) 
Values(
7,
'UNLOAD_DONE',
'UnLoad operation completed')

GO
			
-- Route Type
-------------------------------------------------------------------------

Insert Into RouteType(RouteTypeId,[Name],Description)
Values (1, 'Delivery','Sales route')

GO

Insert Into RouteType(RouteTypeId,[Name],Description)
Values (2, 'Sales','Sales and Delivery route')

GO

Insert Into RouteType(RouteTypeId,[Name],Description)
Values (3, 'Both','Delivery route')

GO
			
-- DayOfWeek
-------------------------------------------------------------------------

Insert Into DayOfWeek(Number) values (1)
GO
Insert Into DayOfWeek(Number) values (2)
GO
Insert Into DayOfWeek(Number) values (3)
GO
Insert Into DayOfWeek(Number) values (4)
GO
Insert Into DayOfWeek(Number) values (5)
GO
Insert Into DayOfWeek(Number) values (6)
GO
Insert Into DayOfWeek(Number) values (0)
GO

-- BusinessTransactionType
-------------------------------------------------------------------------			
Insert Into BusinessTransactionType(BusinessTransactionTypeId, [Name], Description)
Values(1,'CUSTOMER','Customer Transactins')
GO
Insert Into BusinessTransactionType(BusinessTransactionTypeId, [Name], Description)
Values(2,'PERIOD','End of period or start of period')
GO
Insert Into BusinessTransactionType(BusinessTransactionTypeId, [Name], Description)
Values(3,'INVENTORY','LOAD, UNLOAD, TRANSFER')
GO

-- BusinessTransactionStatus
-------------------------------------------------------------------------	
Insert Into BusinessTransactionStatus(BusinessTransactionStatusId, [Name], Description)
Values(1,'CREATED','TRANSACTION CREATED')
GO
Insert Into BusinessTransactionStatus(BusinessTransactionStatusId, [Name], Description)
Values(2,'COMMITTED','TRANSACTION COMMITTED')
GO


-- PeriodTransactionType
-------------------------------------------------------------------------	
Insert Into PeriodTransactionType (PeriodTransactionTypeId,[Name])
Values(1,'SOP')
GO
Insert Into PeriodTransactionType (PeriodTransactionTypeId,[Name])
Values(2,'EOP')
GO

-- CustomerTransactionType
-------------------------------------------------------------------------	
Insert Into CustomerTransactionType(CustomerTransactionTypeId,[Name],Description)
Values(1,'SALES','Sales transaction')
GO
Insert Into CustomerTransactionType(CustomerTransactionTypeId,[Name],Description)
Values(2,'ORDER','ORDER')
GO
Insert Into CustomerTransactionType(CustomerTransactionTypeId,[Name],Description)
Values(3,'SURWAY','SURWAY')
GO

-- InventoryTransactionType
-------------------------------------------------------------------------	
Insert Into InventoryTransactionType(InventoryTransactionTypeId,[Name])
Values (1,'LOAD')
GO
Insert Into InventoryTransactionType(InventoryTransactionTypeId,[Name])
Values (2,'UNLOAD')
GO
Insert Into InventoryTransactionType(InventoryTransactionTypeId,[Name])
Values (3,'Transfer')
GO

-- RouteScheduleQueueStatus
-------------------------------------------------------------------------	
Insert Into RouteScheduleQueueStatus(RouteScheduleQueueStatusId,[Name],Description)
Values(1,'INIT','Queue entry initialized"')
GO
Insert Into RouteScheduleQueueStatus(RouteScheduleQueueStatusId,[Name],Description)
Values(2,'SERVICED','Customer Serviced')
GO

-- ItemType
-------------------------------------------------------------------------	
Insert Into ItemType (ItemTypeId,Description)
Values (1,'Product')
GO
Insert Into ItemType (ItemTypeId,Description)
Values (2,'Equipment')
GO

-- ItemCategory
-------------------------------------------------------------------------	
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(1,1,'Civilian goods','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(2,1,'Inferior goods','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(3,1,'Undurable goods','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(4,1,'Cultural and household goods','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(5,1,'Domestics','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(6,1,'Goods of first priority','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(7,1,'Convenience goods','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(8,1,'Manufactured products','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(9,1,'Substitute goods','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(10,1,'Airplanes','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(11,1,'Games','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(1,2,'Fridge','')
GO
Insert Into ItemCategory (ItemCategoryId, ItemTypeId, [Name],Description)
Values(2,2,'Coffee-machine','')
GO
-- StorageType
-------------------------------------------------------------------------	
Insert Into StorageType(StorageTypeId,[Name])
Values(1,'Store')
GO
Insert Into StorageType(StorageTypeId,[Name])
Values(2,'Bin')
GO
--------------------------------------------------------------------------
--------------------------------------------------------------------------
-- Initional Common Data
--------------------------------------------------------------------------
--------------------------------------------------------------------------

-- Session
--------------------------------------------------------------------------	
Insert Into Session (SessionId, Active)
Values (120000203,1)
GO

-- Location
--------------------------------------------------------------------------	
Insert Into Location (LocationId,[Name])
Values (1,'North Dallas')
GO
Insert Into Location (LocationId,[Name])
Values (2,'South Dallas')
GO

-- Employee
--------------------------------------------------------------------------	
Insert Into Employee (LocationId,EmployeeId,FirstName,LastName)
Values (1,1,'Joe','Doe')
GO
Insert Into Employee (LocationId,EmployeeId,FirstName,LastName)
Values (1,2,'Bart','Simpson')
GO
Insert Into Employee (LocationId,EmployeeId,FirstName,LastName)
Values (2,3,'Mike','Tyson')
GO

-- Route
--------------------------------------------------------------------------
Insert Into Route(LocationId,
RouteNumber,
RouteStatusId,
RouteTypeId,
EmployeeId,
[Name],
DocumentNumberPrefix,
DocumentNumberSequence,
Active,
InventorySync)
Values
(1,1,2,1,1,'The Long Road',162549524,0,1,1)
GO
Insert Into Route(LocationId,
RouteNumber,
RouteStatusId,
RouteTypeId,
EmployeeId,
[Name],
DocumentNumberPrefix,
DocumentNumberSequence,
Active,
InventorySync)
Values
(2,2,2,1,3,'The Light Way',35553333,0,1,1)
GO

-- Customer
--------------------------------------------------------------------------
Insert Into Customer(CustomerId,[Name])
Values (1,'Kroger')
GO
Insert Into Customer(CustomerId,[Name])
Values (2,'Seven11')
GO
       
-- RouteCustomer

--------------------------------------------------------------------------
Insert Into RouteCustomer(LocationId,RouteNumber,CustomerId)
Values (1,1,1)
GO
Insert Into RouteCustomer(LocationId,RouteNumber,CustomerId)
Values (1,1,2)
GO

-- RouteSchedule
--------------------------------------------------------------------------    
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          1,              1,              1,         1,          1,        1,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          2,              2,              1,         1,          1,        1,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          3,              3,              1,         1,          1,        1,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          4,              4,              1,         1,          1,        1,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          5,              5,              1,         1,          1,        1,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          6,              6,              1,         1,          1,        1,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          7,              0,              1,         1,          1,        1,      '2000-01-01','2010-01-01')
GO

Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          8,              1,              2,         1,          1,        2,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          9,              2,              2,         1,          1,        2,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          10,              3,              2,         1,          1,        2,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          11,              4,              2,         1,          1,        2,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          12,              5,              2,         1,          1,        2,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          13,              6,              2,         1,          1,        2,      '2000-01-01','2010-01-01')
GO
Insert Into RouteSchedule(LocationId, RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate)
Values (				  1,          14,              0,              2,         1,          1,        2,      '2000-01-01','2010-01-01')
GO

-- Item
--------------------------------------------------------------------------   
Insert Into Item (LocationId, RouteNumber, ItemNumber,ItemCategoryId,ItemTypeId,[Name],Description,NameSortIndex,ItemNumberSortIndex)
Values (1,1,'1',1,1,'Pepsi','',0,0)
GO
Insert Into Item (LocationId, RouteNumber, ItemNumber,ItemCategoryId,ItemTypeId,[Name],Description,NameSortIndex,ItemNumberSortIndex)
Values (1,1,'2',1,1,'Coca Cola','',0,0)
GO
Insert Into Item (LocationId, RouteNumber, ItemNumber,ItemCategoryId,ItemTypeId,[Name],Description,NameSortIndex,ItemNumberSortIndex)
Values (1,1,'3',1,1,'Staropramen','',0,0)
GO

Insert Into Item (LocationId, RouteNumber, ItemNumber,ItemCategoryId,ItemTypeId,[Name],Description,NameSortIndex,ItemNumberSortIndex)
Values (1,1,'4',1,2,'Indesit 200T','',0,0)
GO
Insert Into Item (LocationId, RouteNumber, ItemNumber,ItemCategoryId,ItemTypeId,[Name],Description,NameSortIndex,ItemNumberSortIndex)
Values (1,1,'5',1,2,'Bosh 220V','',0,0)
GO
Insert Into Item (LocationId, RouteNumber, ItemNumber,ItemCategoryId,ItemTypeId,[Name],Description,NameSortIndex,ItemNumberSortIndex)
Values (1,1,'6',2,2,'Super Espresso 1000','',0,0)
GO
Insert Into Item (LocationId, RouteNumber, ItemNumber,ItemCategoryId,ItemTypeId,[Name],Description,NameSortIndex,ItemNumberSortIndex)
Values (1,1,'7',2,2,'Mini Espresso 18','',0,0)
GO
-- InventoryTransactionDetailType
--------------------------------------------------------------------------   

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(1 , 'load')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    2 , 'reload off load product')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    3 , 'reload add on product')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    4 , 'reload damaged returns')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    5 , 'unload end inventory')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    6 , 'unload return stock')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    7 , 'unload damaged returns')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    8 , 'load request')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    9 , 'unload variance')
GO
Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    10 , 'unload damaged returns variance')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    11 , 'unload truck damaged')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    12 , 'unload end damaged returns')

GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    13 , 'reload truck damaged')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    14 , 'rtnstk return stock')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    15 , 'rtnstk truck damaged')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    16 , 'rtnstk damaged returns')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    17 , 'load adjustments')
GO

Insert Into InventoryTransactionDetailType (InventoryTransactionDetailTypeId,[Name])
Values(
    18 , 'load damage adjustments')
GO

Insert Into InventoryTransactionDetailXRef (InventoryTransactionTypeId,InventoryTransactionDetailTypeId)
Values(1,17)

GO
Insert Into InventoryTransactionDetailXRef (InventoryTransactionTypeId,InventoryTransactionDetailTypeId)
Values(1,18)

GO

Insert Into InventoryTransactionDetailXRef (InventoryTransactionTypeId,InventoryTransactionDetailTypeId)
Values(2,11)

GO

Insert Into InventoryTransactionDetailXRef (InventoryTransactionTypeId,InventoryTransactionDetailTypeId)
Values(2,5)

GO

Insert Into InventoryTransactionDetailXRef (InventoryTransactionTypeId,InventoryTransactionDetailTypeId)
Values(2,6)

GO

Insert Into RouteOptionDescription (OptionName,OptionValue,Description)
Values ('EnableDualInventory',0,'EnableDualInventory')

GO

Insert Into RouteOptionDescription (OptionName,OptionValue,Description)
Values ('EnableDualInventory',1,'EnableDualInventory')

GO

Insert Into RouteOptionDescription (OptionName,OptionValue,Description)
Values ('AutoCalcLoadIn',0,'AutoCalcLoadIn')

GO

Insert Into RouteOptionDescription (OptionName,OptionValue,Description)
Values ('AutoCalcLoadIn',1,'AutoCalcLoadIn')

GO


Insert Into RouteOption(LocationId,RouteNumber,OptionName,OptionValue)
Values (1,1,'EnableDualInventory',1)

GO

Insert Into RouteOption(LocationId,RouteNumber,OptionName,OptionValue)
Values (1,1,'AutoCalcLoadIn',1)

GO


Insert Into Password(Functionality,PasswordId,PasswordValue)
Values('StartDay',1,'1')

GO

Insert Into Password(Functionality,PasswordId,PasswordValue)
Values('InventoryMenu',2,'1')

GO
Insert Into Password(Functionality,PasswordId,PasswordValue)
Values('SetupRoute',3,'1')

GO
Insert Into Password(Functionality,PasswordId,PasswordValue)
Values('EndDay',4,'1')

GO
Insert Into Password(Functionality,PasswordId,PasswordValue)
Values('ChangeDate',5,'1')

GO
Insert Into Password(Functionality,PasswordId,PasswordValue)
Values('Exit',6,'1')

GO