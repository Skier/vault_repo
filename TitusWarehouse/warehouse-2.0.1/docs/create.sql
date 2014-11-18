set xact_abort on
go

begin transaction
go

if object_id('Address') is null
create table Address(
  AddressAutoKey    int          not null,
  ContactID         int,
  CustomerID        nvarchar(30),
  BillToID          nvarchar(30),
  LeadReferralKey   int,
  ContactName       nvarchar(30),
  Branch            nvarchar(5),
  [Name]            nvarchar(60),
  Address1          nvarchar(50),
  Address2          nvarchar(50),
  Address3          nvarchar(50),
  Address4          nvarchar(50),
  Address5          nvarchar(50),
  Address6          nvarchar(50),
  Address7          nvarchar(50),
  City              nvarchar(30),
  StateProvince     nvarchar(5),
  ZipPostal         nvarchar(10),
  Country           nvarchar(30),
  MaintenanceDate   datetime,
  MaintenanceUser   nvarchar(20),
  CreateDate        datetime,
  CreateUser        nvarchar(20),
  BusinessPhone1    nvarchar(25),
  BusinessPhone1Ext nvarchar(5),
  BusinessPhone2    nvarchar(25),
  BusinessPhone2Ext nvarchar(5),
  FaxNumber         nvarchar(25),
  FaxNumberExt      nvarchar(5),
  MobilePhone       nvarchar(25),
  MobilePhoneExt    nvarchar(5)
)
go

if object_id('Brand') is null
create table Brand(
  BrandId   int       not null constraint PK_Brand primary key,
  BrandName nchar(10)
)
go

if object_id('BrandCSRUser') is null
create table BrandCSRUser(
  UserId  int not null constraint PK_BrandCSRUser primary key,
  BrandId int
)
go

alter table BrandCSRUser add
  constraint FK_BrandAdminUser_Brand foreign key(BrandId) references Brand(BrandId)
go

if object_id('BrandSalesRepUser') is null
create table BrandSalesRepUser(
  UserId  int not null,
  BrandId int not null,

  constraint PK_BrandSalesRepUser primary key(UserId,BrandId) 
)
go

alter table BrandSalesRepUser add
  constraint FK_BrandSalesRepUser_Brand foreign key(BrandId) references Brand(BrandId)
go

if object_id('Category') is null
create table Category(
  CategoryId       int       not null constraint PK_Category primary key,
  ParentCategoryId int,
  BrandId          int,
  [Name]           nchar(25),
  ModelId          int
)
go

alter table Category add
  constraint FK_Category_Brand1 foreign key(CategoryId) references Brand(BrandId)
go

if object_id('Customer') is null
create table Customer(
  CustomerID          nvarchar(30)  not null,
  CustomerName        nvarchar(60),
  EmailAddress        nvarchar(60),
  LastOrderDate       datetime,
  EntLastMaintDate    datetime,
  MaintenanceDate     datetime,
  DeleteOnDate        datetime,
  CreateDate          datetime,
  CreateUser          nvarchar(20),
  MaintenanceUser     nvarchar(20),
  BillToID            nvarchar(30),
  AcceptOrders        smallint,
  CustomerConfirmRule nvarchar(20),
  FreightRule         nvarchar(20),
  CustomerPriceRule   nvarchar(20),
  HowShipRule         nvarchar(20),
  ProductAvailRule    nvarchar(20),
  AcceptBackorders    bit,
  MinOrderAmount      float,
  EntQuoteCustomerID  nvarchar(30),
  Branch              nvarchar(5),
  CorporateEntity     nvarchar(256)
)
go

if object_id('CustomerAccessXRef') is null
create table CustomerAccessXRef(
  UserID             nvarchar(60) not null,
  CustomerID         nvarchar(30) not null,
  SecondaryTierQuery bit
)
go

if object_id('Inventory') is null
create table Inventory(
  WarehouseId int not null,
  ItemId      int not null,
  Qty         int,

  constraint PK_Inventory primary key(WarehouseId,ItemId) 
)
go

if object_id('InventoryChangeLog') is null
create table InventoryChangeLog(
  ItentoryChangeLogId  int      not null constraint PK_InventoryChangeLog primary key,
  ChangeDate           datetime not null,
  WarehouseId          int      not null,
  ItemId               int      not null,
  Qty                  int      not null,
  OrderId              int,
  ChangeOrderRequestId int,
  Balance              int      not null
)
go

if not exists(select * from sysindexes where id=object_id('InventoryChangeLog') and name='IX_InventoryChangeLog')
create unique index IX_InventoryChangeLog on InventoryChangeLog(ItemId,WarehouseId)
go

if object_id('Item') is null
create table Item(
  ItemId        int       not null constraint PK_Item primary key,
  SKU           nchar(10) not null,
  Description   nchar(25),
  Configuration nchar(25),
  Width         int,
  Length        int,
  Hight         int,
  Price         money,
  IsActive      bit
)
go

if object_id('MacPac_Inventory') is null
create table MacPac_Inventory(
  MacPac_Inventory_id   int           not null identity,
  Plant                 varchar(3),
  Part                  varchar(15),
  PartDesc              varchar(50),
  AltDesc               varchar(50),
  Model                 varchar(15),
  Configuration         varchar(50),
  ContainerCode         varchar(15),
  Height                decimal(10,3),
  Depth                 decimal(10,3),
  Width                 decimal(10,3),
  BasePrice             decimal(10,3),
  OnHand                int,
  Allocated             int,
  PartStatus            char(1),
  MacPacCreateTimeStamp datetime,
  ImportCreateTimeStamp datetime      constraint DF_MacPac_Inventory_ImportCreateTimeStamp default (getdate())
)
go

if object_id('Model') is null
create table Model(
  ModelId   int       not null constraint PK_Model_1 primary key,
  BrandId   int       not null,
  ModelName nchar(10)
)
go

alter table Model add
  constraint FK_Model_Brand foreign key(BrandId) references Brand(BrandId)
go

if object_id('ModelItem') is null
create table ModelItem(
  ModelId int not null,
  ItemId  int not null,

  constraint PK_ModelItem primary key(ModelId,ItemId) 
)
go

alter table ModelItem add
  constraint FK_ModelItem_ModelItem foreign key(ItemId) references Item(ItemId),
  constraint FK_ModelItem_Model foreign key(ModelId) references Model(ModelId)
go

if object_id('[Order]') is null
create table [Order](
  OrderId                    int       not null constraint PK_Order primary key,
  PONumber                   nchar(25),
  MACPACOrderNumber          int,
  Status                     int,
  BrandId                    int,
  SalesRepCompanyId          int,
  CreatedByUserId            int,
  Total                      money,
  ShippingTotalAllWarehouses money,
  GrandTotal                 money,
  AddressID                  int
)
go

if object_id('OrderItem') is null
create table OrderItem(
  OrderItemId    int   not null constraint PK_OrderItem primary key,
  OrderId        int   not null,
  WarehouseId    int   not null,
  LineItemNumber int   not null,
  ItemId         int   not null,
  QtyOrdered     int   not null,
  QtyNeeded      int   not null,
  Price          money not null
)
go

alter table OrderItem add
  constraint FK_OrderItem_Item foreign key(ItemId) references Item(ItemId),
  constraint FK_OrderItem_Order foreign key(OrderId) references [Order](OrderId)
go

if object_id('OrderStatusLog') is null
create table OrderStatusLog(
  OrderStatusLogId nchar(10) not null constraint PK_OrderStatusLog primary key,
  OrderId          int,
  NewStatus        int,
  OldStatus        int,
  ChangeDate       datetime
)
go

alter table OrderStatusLog add
  constraint FK_OrderStatusLog_Order foreign key(OrderId) references [Order](OrderId)
go

if object_id('OrderWarehouse') is null
create table OrderWarehouse(
  OrderId       int   not null,
  WarehouseId   int   not null,
  ShippingType  int,
  ShippingTotal money,

  constraint PK_OrderWarehouse primary key(OrderId,WarehouseId) 
)
go

alter table OrderWarehouse add
  constraint FK_OrderWarehouse_Order foreign key(OrderId) references [Order](OrderId)
go

if object_id('SalesRepCompany') is null
create table SalesRepCompany(
  SalesRepCompanyId   int       not null constraint PK_SalesRepCompany primary key,
  SalesRepCompanyName nchar(10),
  CreditStatus        int,
  Multiplyer          float,
  MaxOrderTotal       money
)
go

if object_id('SalesRepCompanyShippingAddressBook') is null
create table SalesRepCompanyShippingAddressBook(
  AddressId         int       not null constraint PK_SalesRepCompanyAddress primary key,
  SalesRepCompanyId int,
  Address1          nchar(10),
  Address2          nchar(10),
  City              nchar(10),
  State             nchar(10),
  Zip               nchar(10)
)
go

alter table SalesRepCompanyShippingAddressBook add
  constraint FK_SalesRepCompanyAddress_SalesRepCompany foreign key(SalesRepCompanyId) references SalesRepCompany(SalesRepCompanyId)
go

if object_id('SalesRepUser') is null
create table SalesRepUser(
  UserId            int       not null constraint PK_SalesRepUser primary key,
  SalesRepCompanyId nchar(10)
)
go

alter table SalesRepUser add
  constraint FK_SalesRepUser_SalesRepCompany foreign key(UserId) references SalesRepCompany(SalesRepCompanyId)
go

if object_id('ShoppingCartOrder') is null
create table ShoppingCartOrder(
  ShoppingCartOrderId int       not null constraint PK_ShoppingCartOrder primary key,
  PONumber            nchar(25),
  BrandId             int,
  SalesRepCompanyId   int,
  CreatedByUserId     int,
  Total               money,
  AddressID           int
)
go

alter table ShoppingCartOrder add
  constraint FK_ShoppingCartOrder_SalesRepCompany foreign key(SalesRepCompanyId) references SalesRepCompany(SalesRepCompanyId),
  constraint FK_ShoppingCartOrder_SalesRepCompanyShippingAddressBook foreign key(AddressID) references SalesRepCompanyShippingAddressBook(AddressId),
  constraint FK_ShoppingCartOrder_Brand foreign key(BrandId) references Brand(BrandId)
go

if object_id('ShoppingCartOrderItem') is null
create table ShoppingCartOrderItem(
  ShoppingCartOrderItemId int   not null constraint PK_ShoppingCartOrderItem primary key,
  ShoppingCartOrderId     int   not null,
  WarehouseId             int   not null,
  LineItemNumber          int   not null,
  ItemId                  int   not null,
  QtyOrdered              int   not null,
  QtyNeeded               int   not null,
  Price                   money not null
)
go

alter table ShoppingCartOrderItem add
  constraint FK_ShoppingCartOrderItem_Item foreign key(ItemId) references Item(ItemId),
  constraint FK_ShoppingCartOrderItem_ShoppingCartOrder foreign key(ShoppingCartOrderId) references ShoppingCartOrder(ShoppingCartOrderId)
go

if object_id('[User]') is null
create table [User](
  UserId     int not null constraint PK_User primary key,
  LoginId    int not null,
  UserTypeId int
)
go

if object_id('UserType') is null
create table UserType(
  UserTypeId   int       not null constraint PK_UserType primary key,
  UserTypeName nchar(10)
)
go

if object_id('Warehouse') is null
create table Warehouse(
  WarehouseId   int       not null constraint PK_Warehouse primary key,
  WarehouseName nchar(10)
)
go

alter table [User] add
  constraint FK_User_UserType foreign key(UserTypeId) references UserType(UserTypeId)
go

alter table ShoppingCartOrder add
  constraint FK_ShoppingCartOrder_User foreign key(CreatedByUserId) references [User](UserId)
go

alter table SalesRepUser add
  constraint FK_SalesRepUser_User foreign key(UserId) references [User](UserId)
go

alter table OrderWarehouse add
  constraint FK_OrderWarehouse_Warehouse foreign key(WarehouseId) references Warehouse(WarehouseId)
go

alter table [Order] add
  constraint FK_Order_SalesRepCompanyShippingAddressBook foreign key(AddressID) references SalesRepCompanyShippingAddressBook(AddressId),
  constraint FK_Order_SalesRepCompany foreign key(SalesRepCompanyId) references SalesRepCompany(SalesRepCompanyId),
  constraint FK_Order_User foreign key(CreatedByUserId) references [User](UserId)
go

alter table Inventory add
  constraint FK_Inventory_Warehouse1 foreign key(WarehouseId) references Warehouse(WarehouseId),
  constraint FK_Inventory_Warehouse foreign key(ItemId) references Item(ItemId)
go

alter table Category add
  constraint FK_Category_Brand foreign key(ModelId) references Model(ModelId)
go

alter table BrandSalesRepUser add
  constraint FK_BrandSalesRepUser_SalesRepUser foreign key(UserId) references SalesRepUser(UserId)
go

alter table BrandCSRUser add
  constraint FK_BrandAdminUser_User foreign key(UserId) references [User](UserId)
go

commit
go


