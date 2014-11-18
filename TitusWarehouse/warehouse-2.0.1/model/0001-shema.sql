/*==============================================================*/
/* Database name:  ascwarehouse                                 */
/* DBMS name:      Microsoft SQL Server 2000                    */
/* Created on:     19.05.2008 11:48:55                          */
/*==============================================================*/


drop database ascwarehouse
go


/*==============================================================*/
/* Database: ascwarehouse                                       */
/*==============================================================*/
create database ascwarehouse
go


use ascwarehouse
go


/*==============================================================*/
/* Table: ASCUser                                               */
/*==============================================================*/
create table ASCUser (
   UserId               integer              identity,
   UserTypeId           integer              not null,
   BrandId              integer              not null,
   Login                varchar(100)         not null,
   Password             varchar(100)         not null,
   LastUpdateDate       datetime             not null,
   CreatedByUser        varchar(100)         not null,
   DateCreated          datetime             not null
)
go


alter table ASCUser
   add constraint PK_ASCUSER primary key  (UserId)
go


alter table ASCUser
   add constraint AK_KEY_2_ASCUSER unique  (Login)
go


/*==============================================================*/
/* Table: Brand                                                 */
/*==============================================================*/
create table Brand (
   BrandId              integer              not null,
   Code                 varchar(20)          not null,
   BrandName            varchar(255)         not null,
   ImageURLPrefix       varchar(255)         null
)
go


alter table Brand
   add constraint PK_BRAND primary key  (BrandId)
go


/*==============================================================*/
/* Table: Category                                              */
/*==============================================================*/
create table Category (
   CategoryId           integer              identity,
   BrandId              integer              not null,
   ParentCategoryId     integer              null,
   Name                 varchar(100)         not null,
   CreatedByUser        varchar(100)         not null,
   DateCreated          datetime             not null,
   LastUpdateDate       datetime             not null
)
go


alter table Category
   add constraint PK_CATEGORY primary key  (CategoryId)
go


/*==============================================================*/
/* Table: Customer                                              */
/*==============================================================*/
create table Customer (
   CustomerId           integer              identity,
   DefaultWarehouseId   integer              not null,
   BrandId              integer              not null,
   MACPACCustomerNumber varchar(100)         not null,
   SalesRepCompanyName  varchar(255)         not null,
   CreditStatus         bit                  not null,
   MaxOrderTotal        money                not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null,
   DateCreated          datetime             not null,
   Address1             varchar(100)         not null,
   Address2             varchar(100)         not null,
   City                 varchar(100)         not null,
   State                varchar(100)         not null,
   Zip                  varchar(100)         not null,
   Country              varchar(3)           not null,
   Phone                varchar(100)         not null,
   Fax                  varchar(100)         not null,
   Email                varchar(100)         not null
)
go


alter table Customer
   add constraint PK_CUSTOMER primary key  (CustomerId)
go


/*==============================================================*/
/* Table: CustomerPrice                                         */
/*==============================================================*/
create table CustomerPrice (
   CustomerId           integer              not null,
   ModelItemId          integer              not null,
   Multiplier           double precision     not null,
   MarketingProgram     varchar(15)          null
)
go


alter table CustomerPrice
   add constraint PK_CUSTOMERPRICE primary key  (CustomerId, ModelItemId)
go


/*==============================================================*/
/* Table: CustomerShippingAddress                               */
/*==============================================================*/
create table CustomerShippingAddress (
   AddressId            integer              identity,
   CustomerId           integer              not null,
   Name                 varchar(100)         not null,
   Address1             varchar(100)         not null,
   Address2             varchar(100)         not null,
   City                 varchar(100)         not null,
   State                varchar(100)         not null,
   Zip                  varchar(100)         not null,
   Country              varchar(3)           not null,
   CreatedByUser        varchar(100)         not null,
   DateCreated          datetime             not null,
   LastUpdateDate       datetime             not null
)
go


alter table CustomerShippingAddress
   add constraint PK_CUSTOMERSHIPPINGADDRESS primary key  (AddressId)
go


/*==============================================================*/
/* Table: Inventory                                             */
/*==============================================================*/
create table Inventory (
   InventoryId          integer              identity,
   WarehouseId          integer              not null,
   ItemId               integer              not null,
   Qty                  integer              not null,
   DateCreated          datetime             not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null,
   MacPac_Inventory_id  int                  null
)
go


alter table Inventory
   add constraint PK_INVENTORY primary key  (InventoryId)
go


/*==============================================================*/
/* Table: Item                                                  */
/*==============================================================*/
create table Item (
   ItemId               integer              identity,
   SKU                  varchar(100)         not null,
   Description          varchar(255)         not null,
   Width                double precision     not null,
   Length               double precision     not null,
   Height               double precision     not null,
   Weight               double precision     not null,
   QtyIncrement         integer              not null,
   IsActive             bit                  not null,
   DateCreated          datetime             not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null
)
go


alter table Item
   add constraint PK_ITEM primary key  (ItemId)
go


alter table Item
   add constraint AK_KEY_2_ITEM unique  (SKU)
go


/*==============================================================*/
/* Table: MACPAC_Multiplier                                     */
/*==============================================================*/
create table MACPAC_Multiplier (
   multiplier_Id        integer              identity,
   brand_code           varchar(10)          not null,
   marketing_program    varchar(15)          not null,
   part                 varchar(25)          not null,
   [desc]               varchar(50)          null,
   multiplier           decimal(10,5)        not null,
   LastUpdateDate       datetime             not null default getdate()
)
go


alter table MACPAC_Multiplier
   add constraint PK_MACPAC_MULTIPLIER primary key  (multiplier_Id)
go


alter table MACPAC_Multiplier
   add constraint AK_KEY_2_MACPAC_M unique  (brand_code, marketing_program, part)
go


/*==============================================================*/
/* Table: MacPac_Inventory                                      */
/*==============================================================*/
create table dbo.MacPac_Inventory (
   MacPac_Inventory_id  int                  identity,
   Plant                varchar(3)           not null,
   Brand                varchar(10)          not null,
   Part                 varchar(15)          not null,
   PartDesc             varchar(50)          not null,
   AltDesc              varchar(50)          not null,
   Model                varchar(15)          not null,
   Configuration        varchar(50)          not null,
   ContainerCode        varchar(15)          not null,
   Height               decimal(10, 3)       not null,
   Depth                decimal(10, 3)       not null,
   Width                decimal(10, 3)       not null,
   ContainerWeight      decimal(10, 0)       not null,
   partweight           decimal(10, 0)       not null,
   qtypercontainer      decimal(10, 0)       not null,
   BasePrice            decimal(10, 3)       not null,
   OnHand               int                  not null,
   Allocated            int                  not null,
   PartStatus           char(1)              not null,
   MacPacTimeStamp      datetime             not null,
   ImportTimeStamp      datetime             not null default getdate(),
   ProcessedStatus      varchar(15)          null,
   FailReason           varchar(255)         null
)
go


alter table dbo.MacPac_Inventory
   add constraint PK_MACPAC_INVENTORY primary key  (MacPac_Inventory_id)
go


/*==============================================================*/
/* Table: Model                                                 */
/*==============================================================*/
create table Model (
   ModelId              integer              identity,
   BrandId              integer              not null,
   CategoryId           integer              not null,
   ModelName            varchar(255)         not null,
   IsActive             bit                  not null,
   DateCreated          datetime             not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null
)
go


alter table Model
   add constraint PK_MODEL primary key  (ModelId)
go


/*==============================================================*/
/* Table: ModelItem                                             */
/*==============================================================*/
create table ModelItem (
   ModelItemId          integer              identity,
   ModelId              integer              not null,
   ItemId               integer              not null,
   Configuration        varchar(50)          not null,
   Price                money                not null,
   IsActive             bit                  not null,
   ImageURL             varchar(1024)        not null,
   XMLBullerDescription varchar(1024)        not null,
   DateCreated          datetime             not null,
   LastUpdateDate       datetime             not null,
   CreatedByUser        varchar(100)         not null,
   MacPac_Inventory_id  int                  null
)
go


alter table ModelItem
   add constraint PK_MODELITEM primary key  (ModelItemId)
go


alter table ModelItem
   add constraint AK_KEY_2_MODELITE unique  (ModelId, ItemId)
go


/*==============================================================*/
/* Table: OrderDetail                                           */
/*==============================================================*/
create table OrderDetail (
   OrderDetailId        integer              identity,
   OrderId              integer              not null,
   ItemId               integer              not null,
   Qty                  integer              not null,
   Price                money                not null,
   SKU                  varchar(100)         not null,
   Multiplier           double precision     not null,
   Cost                 money                not null,
   LineNumber           integer              not null,
   ShoppingCartDetailId integer              not null
)
go


alter table OrderDetail
   add constraint PK_ORDERDETAIL primary key  (OrderDetailId)
go


/*==============================================================*/
/* Table: OrderStatus                                           */
/*==============================================================*/
create table OrderStatus (
   OrderStatusId        integer              not null,
   Status               varchar(100)         not null
)
go


alter table OrderStatus
   add constraint PK_ORDERSTATUS primary key  (OrderStatusId)
go


/*==============================================================*/
/* Table: OrderStatusLog                                        */
/*==============================================================*/
create table OrderStatusLog (
   OrderStatusLogId     integer              identity,
   OrderId              integer              null,
   NewStatus            integer              not null,
   OldStatus            integer              not null,
   ChangeDate           datetime             not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null,
   DateCreated          datetime             not null
)
go


alter table OrderStatusLog
   add constraint PK_ORDERSTATUSLOG primary key  (OrderStatusLogId)
go


/*==============================================================*/
/* Table: ShippingPrice                                         */
/*==============================================================*/
create table ShippingPrice (
   PriceId              int                  identity,
   ZoneId               int                  not null,
   ShippingTypeId       integer              not null,
   StartWeight          integer              not null,
   EndWeight            integer              not null,
   Price                money                not null
)
go


alter table ShippingPrice
   add constraint PK_SHIPPINGPRICE primary key  (PriceId)
go


/*==============================================================*/
/* Table: ShippingType                                          */
/*==============================================================*/
create table ShippingType (
   ShippingTypeId       integer              not null,
   ShippingType         varchar(100)         not null,
   ShippingCode         varchar(10)          not null
)
go


alter table ShippingType
   add constraint PK_SHIPPINGTYPE primary key  (ShippingTypeId)
go


/*==============================================================*/
/* Table: ShippingZones                                         */
/*==============================================================*/
create table ShippingZones (
   ZoneId               int                  not null,
   StartDistance        int                  not null,
   EndDistance          int                  not null
)
go


alter table ShippingZones
   add constraint PK_SHIPPINGZONES primary key  (ZoneId)
go


/*==============================================================*/
/* Table: ShoppingCart                                          */
/*==============================================================*/
create table ShoppingCart (
   ShoppingCartId       integer              identity,
   IsActive             bit                  not null,
   CustomerId           integer              not null,
   OrderDate            datetime             not null,
   RepAccountNo         varchar(100)         not null,
   ShippingAddressId    integer              null,
   BrandId              integer              not null,
   IPAddress            varchar(24)          not null,
   Total                money                not null,
   ShippingTotalAllWarehouses money                not null,
   GrandTotal           money                not null,
   Phone                varchar(255)         null,
   Fax                  varchar(255)         null,
   Email                varchar(255)         null,
   SalesPerson          varchar(255)         null,
   JobsiteContactPh     varchar(255)         null,
   MarkOrder            varchar(255)         null,
   DeliveryRequest      varchar(255)         null,
   DateCreated          datetime             not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null,
   AcknFileName         varchar(255)         null,
   version              integer              not null default 1
)
go


alter table ShoppingCart
   add constraint PK_SHOPPINGCART primary key  (ShoppingCartId)
go


/*==============================================================*/
/* Table: ShoppingCartDetail                                    */
/*==============================================================*/
create table ShoppingCartDetail (
   ShoppingCartDetailId integer              identity,
   ShoppingCartId       integer              not null,
   ShipmentId           integer              not null,
   ModelItemId          integer              not null,
   QtyOrdered           integer              not null,
   QtyNeeded            integer              not null,
   Price                money                not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null,
   DateCreated          datetime             not null,
   SKU                  varchar(100)         not null
)
go


alter table ShoppingCartDetail
   add constraint PK_SHOPPINGCARTDETAIL primary key  (ShoppingCartDetailId)
go


/*==============================================================*/
/* Table: ShoppingCartShipment                                  */
/*==============================================================*/
create table ShoppingCartShipment (
   ShoppingCartShipmentId integer              identity,
   ShoppingCartId       integer              not null,
   WarehouseId          integer              not null,
   ShippingTypeId       integer              null,
   ShippingTotal        money                not null,
   DateCreated          datetime             not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null,
   PONumber             varchar(24)          null,
   NeedLiftGate         bit                  not null,
   LiftGatePrice        money                not null
)
go


alter table ShoppingCartShipment
   add constraint PK_SHOPPINGCARTSHIPMENT primary key  (ShoppingCartShipmentId)
go


/*==============================================================*/
/* Table: TheOrder                                              */
/*==============================================================*/
create table TheOrder (
   OrderId              integer              identity,
   ShippingTypeId       integer              not null,
   OrderStatusId        integer              not null,
   WarehouseId          integer              not null,
   CustomerId           integer              not null,
   BrandId              integer              not null,
   PONumber             varchar(24)          not null,
   OrderDate            datetime             not null,
   ShippingDate         datetime             null,
   MACPACOrderNumber    varchar(100)         null,
   ReleaseNumber        varchar(100)         null,
   TrackingNumber       varchar(100)         null,
   RepAccountNo         varchar(100)         not null,
   Total                money                not null,
   ShippingTotal        money                not null,
   GrandTotal           money                not null,
   JobsiteContactPh     varchar(255)         null,
   Phone                varchar(255)         null,
   Fax                  varchar(255)         null,
   Email                varchar(255)         null,
   SalesPerson          varchar(255)         not null,
   MarkOrder            varchar(255)         null,
   DeliveryRequest      varchar(255)         null,
   MACPACXML            text                 not null,
   MACPACFileName       varchar(255)         not null,
   ShoppingCartShipmentId integer              not null,
   DateCreated          datetime             not null,
   CreatedByUser        varchar(100)         not null,
   LastUpdateDate       datetime             not null,
   SoldName             varchar(100)         not null,
   SoldAddress1         varchar(100)         not null,
   SoldAddress2         varchar(100)         not null,
   SoldCity             varchar(100)         not null,
   SoldState            varchar(100)         not null,
   SoldZip              varchar(100)         not null,
   SoldCountry          varchar(3)           not null,
   ShipName             varchar(100)         not null,
   ShipAddress1         varchar(100)         not null,
   ShipAddress2         varchar(100)         not null,
   ShipCity             varchar(100)         not null,
   ShipState            varchar(100)         not null,
   ShipZip              varchar(100)         not null,
   ShipCountry          varchar(3)           not null,
   MarketingProgram     varchar(100)         null
)
go


alter table TheOrder
   add constraint PK_THEORDER primary key  (OrderId)
go


alter table TheOrder
   add constraint AK_KEY_2_THEORDER unique  (CustomerId, PONumber)
go


/*==============================================================*/
/* Table: UserType                                              */
/*==============================================================*/
create table UserType (
   UserTypeId           integer              not null,
   UserTypeName         varchar(100)         not null
)
go


insert into UserType (UserTypeId , UserTypeName) values (1, 'Finance');
alter table UserType
   add constraint PK_USERTYPE primary key  (UserTypeId)
go


/*==============================================================*/
/* Table: Warehouse                                             */
/*==============================================================*/
create table Warehouse (
   WarehouseId          integer              not null,
   WarehouseName        varchar(255)         not null,
   WarehouseCode        varchar(5)           not null,
   Address1             varchar(100)         not null,
   Address2             varchar(100)         not null,
   City                 varchar(100)         not null,
   State                varchar(100)         not null,
   Zip                  varchar(100)         not null,
   Country              varchar(3)           not null
)
go


alter table Warehouse
   add constraint PK_WAREHOUSE primary key  (WarehouseId)
go


/*==============================================================*/
/* Table: macpac_multiplier_header                              */
/*==============================================================*/
create table macpac_multiplier_header (
   macpac_multiplier_header_id integer              identity,
   brand                varchar(10)          not null,
   customer_id          varchar(30)          not null,
   marketing_program    varchar(15)          not null,
   lastupdatedate       datetime             not null default getdate()
)
go


alter table macpac_multiplier_header
   add constraint PK_MACPAC_MULTIPLIER_HEADER primary key  (macpac_multiplier_header_id)
go


alter table macpac_multiplier_header
   add constraint AK_KEY_2_MACPAC_M unique  (brand, customer_id, marketing_program)
go


alter table ASCUser
   add constraint FK_ASCUSER_REFERENCE_USERTYPE foreign key (UserTypeId)
      references UserType (UserTypeId)
go


alter table ASCUser
   add constraint FK_ASCUSER_REFERENCE_BRAND foreign key (BrandId)
      references Brand (BrandId)
go


alter table Category
   add constraint FK_CATEGORY_REFERENCE_BRAND foreign key (BrandId)
      references Brand (BrandId)
go


alter table Category
   add constraint FK_CATEGORY_REFERENCE_CATEGORY foreign key (ParentCategoryId)
      references Category (CategoryId)
go


alter table Customer
   add constraint FK_CUSTOMER_REFERENCE_WAREHOUS foreign key (DefaultWarehouseId)
      references Warehouse (WarehouseId)
go


alter table Customer
   add constraint FK_CUSTOMER_REFERENCE_BRAND foreign key (BrandId)
      references Brand (BrandId)
go


alter table CustomerPrice
   add constraint FK_CUSTOMER_REFERENCE_CUSTOMER foreign key (CustomerId)
      references Customer (CustomerId)
go


alter table CustomerPrice
   add constraint FK_CUSTOMER_REFERENCE_MODELITE foreign key (ModelItemId)
      references ModelItem (ModelItemId)
go


alter table CustomerShippingAddress
   add constraint FK_CUSTOMER_ADDR foreign key (CustomerId)
      references Customer (CustomerId)
go


alter table Inventory
   add constraint FK_INVENTOR_REFERENCE_WAREHOUS foreign key (WarehouseId)
      references Warehouse (WarehouseId)
go


alter table Inventory
   add constraint FK_INVENTOR_REFERENCE_ITEM foreign key (ItemId)
      references Item (ItemId)
go


alter table Model
   add constraint FK_MODEL_REFERENCE_BRAND foreign key (BrandId)
      references Brand (BrandId)
go


alter table Model
   add constraint FK_MODEL_REFERENCE_CATEGORY foreign key (CategoryId)
      references Category (CategoryId)
go


alter table ModelItem
   add constraint FK_MODELITE_REFERENCE_MODEL foreign key (ModelId)
      references Model (ModelId)
go


alter table ModelItem
   add constraint FK_MODELITE_REFERENCE_ITEM foreign key (ItemId)
      references Item (ItemId)
go


alter table OrderDetail
   add constraint FK_ORDERDET_REFERENCE_THEORDER foreign key (OrderId)
      references TheOrder (OrderId)
go


alter table OrderDetail
   add constraint FK_ORDERDET_REFERENCE_ITEM foreign key (ItemId)
      references Item (ItemId)
go


alter table OrderStatusLog
   add constraint FK_ORDERSTA_REFERENCE_THEORDER foreign key (OrderId)
      references TheOrder (OrderId)
go


alter table ShippingPrice
   add constraint FK_SHIPPING_PRICE_ZONE foreign key (ZoneId)
      references ShippingZones (ZoneId)
go


alter table ShippingPrice
   add constraint FK_SHIPPING_PRICE_TYPE foreign key (ShippingTypeId)
      references ShippingType (ShippingTypeId)
go


alter table ShoppingCart
   add constraint FK_ORDER_CUSTOMER foreign key (CustomerId)
      references Customer (CustomerId)
go


alter table ShoppingCart
   add constraint FK_ORDER_ADDRESS foreign key (ShippingAddressId)
      references CustomerShippingAddress (AddressId)
go


alter table ShoppingCart
   add constraint FK_SHOPPING_REFERENCE_BRAND foreign key (BrandId)
      references Brand (BrandId)
go


alter table ShoppingCartDetail
   add constraint FK_CART_TO_DETAIL foreign key (ShoppingCartId)
      references ShoppingCart (ShoppingCartId)
go


alter table ShoppingCartDetail
   add constraint FK_DETAIL_TO_SHIPMENT foreign key (ShipmentId)
      references ShoppingCartShipment (ShoppingCartShipmentId)
go


alter table ShoppingCartDetail
   add constraint FK_SHOPPING_REFERENCE_MODELITE foreign key (ModelItemId)
      references ModelItem (ModelItemId)
go


alter table ShoppingCartShipment
   add constraint FK_CART_TO_SHIPMENT foreign key (ShoppingCartId)
      references ShoppingCart (ShoppingCartId)
go


alter table ShoppingCartShipment
   add constraint FK_SHOPPING_REFERENCE_WAREHOUS foreign key (WarehouseId)
      references Warehouse (WarehouseId)
go


alter table ShoppingCartShipment
   add constraint FK_SHOPPING_REFERENCE_SHIPPING foreign key (ShippingTypeId)
      references ShippingType (ShippingTypeId)
go


alter table TheOrder
   add constraint FK_THEORDER_REFERENCE_SHIPPING foreign key (ShippingTypeId)
      references ShippingType (ShippingTypeId)
go


alter table TheOrder
   add constraint FK_THEORDER_REFERENCE_ORDERSTA foreign key (OrderStatusId)
      references OrderStatus (OrderStatusId)
go


alter table TheOrder
   add constraint FK_THEORDER_REFERENCE_WAREHOUS foreign key (WarehouseId)
      references Warehouse (WarehouseId)
go


alter table TheOrder
   add constraint FK_ORDER_TO_CUSTOMER foreign key (CustomerId)
      references Customer (CustomerId)
go


alter table TheOrder
   add constraint FK_THEORDER_REFERENCE_BRAND foreign key (BrandId)
      references Brand (BrandId)
go


