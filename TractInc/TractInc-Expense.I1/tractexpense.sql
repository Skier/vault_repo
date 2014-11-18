USE master
go

DROP DATABASE tractexpense
go

CREATE DATABASE tractexpense
go

USE tractexpense
go

CREATE TABLE Afe (
  AFE varchar(10) NOT NULL,
  ClientId int NOT NULL,
  AFEName varchar(45) NOT NULL,
  AFEStatus varchar(10) NOT NULL,
  AfeGuid varchar(255) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (AFE)
)
go
--  KEY `FK_afe_client` (`ClientId`),
--  KEY `FK_afe_afestatus` (`AFEStatus`),
--  CONSTRAINT `FK_afe_afestatus` FOREIGN KEY (`AFEStatus`) REFERENCES `afestatus` (`AFEStatus`),
--  CONSTRAINT `FK_afe_client` FOREIGN KEY (`ClientId`) REFERENCES `client` (`ClientId`)

CREATE TABLE AfeStatus (
  AFEStatus varchar(10) NOT NULL,
  PRIMARY KEY  (AFEStatus)
)
go

CREATE TABLE Asset (
  AssetId int identity NOT NULL,
  AssetType varchar(10) NOT NULL,
  ChiefAssetId int NOT NULL,
  BusinessName varchar(45) NOT NULL,
  FirstName varchar(45) NOT NULL,
  MiddleName varchar(45) NOT NULL,
  LastName varchar(45) NOT NULL,
  SSN varchar(9) NOT NULL,
  AssetGuid varchar(255) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (AssetId)
)
go
--  KEY `FK_Asset_AssetType` (`AssetType`),
--  CONSTRAINT `FK_Asset_AssetType` FOREIGN KEY (`AssetType`) REFERENCES `assettype` (`AssetType`)

CREATE TABLE AssetAssignment (
  AssetAssignmentId int identity NOT NULL,
  AFE varchar(10) NOT NULL,
  SubAFE varchar(10) NOT NULL,
  AssetId int NOT NULL,
  BillRate decimal(10,2) NOT NULL,
  PayRate decimal(10,2) NOT NULL,
  AssetAssignmentGuid varchar(255) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (AssetAssignmentId)
)
go
--  KEY `FK_assetassignment_afe` (`AFE`),
--  KEY `FK_assetassignment_subafe` (`SubAFE`),
--  KEY `FK_assetassignment_asset` (`AssetId`),
--  CONSTRAINT `FK_assetassignment_afe` FOREIGN KEY (`AFE`) REFERENCES `afe` (`AFE`),
--  CONSTRAINT `FK_assetassignment_asset` FOREIGN KEY (`AssetId`) REFERENCES `asset` (`AssetId`),
--  CONSTRAINT `FK_assetassignment_subafe` FOREIGN KEY (`SubAFE`) REFERENCES `subafe` (`SubAFE`)

CREATE TABLE AssetType (
  Type varchar(10) NOT NULL,
  PRIMARY KEY  (Type)
)
go

CREATE TABLE Bill (
  BillId int identity NOT NULL,
  BillStatus varchar(10) NOT NULL,
  Notes varchar(255) NOT NULL,
  BillGuid varchar(255) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (BillId)
)

CREATE TABLE BillStatus (
  Status varchar(10) NOT NULL,
  PRIMARY KEY  (Status)
)
go

CREATE TABLE BillItem (
  BillItemId int identity NOT NULL,
  BillId int NOT NULL,
  AssetAssignmentId int NOT NULL,
  BillingDate datetime NOT NULL,
  DayQty int NOT NULL,
  BillRate decimal(10,2) NOT NULL,
  TotalHourlyBilling decimal(10,2) NULL,
  Lodging decimal(10,2) NULL,
  Meals decimal(10,2) NULL,
  Phone decimal(10,2) NULL,
  Copies decimal(10,2) NULL,
  FilingFees decimal(10,2) NULL,
  Status varchar(10) NOT NULL,
  Notes varchar(255) NOT NULL,
  BillItemGuid varchar(255) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (BillItemId)
)
--  KEY `FK_BillItem_AssetAssignment` (`AssetAssignmentId`),
--  KEY `FK_billitem_billItemstatus` (`Status`),
--  CONSTRAINT `FK_BillItem_AssetAssignment` FOREIGN KEY (`AssetAssignmentId`) REFERENCES `assetassignment` (`AssetAssignmentId`),
--  CONSTRAINT `FK_billitem_billItemstatus` FOREIGN KEY (`Status`) REFERENCES `billitemstatus` (`Status`)

CREATE TABLE BillItemStatus (
  Status varchar(10) NOT NULL,
  PRIMARY KEY  (Status)
)
go

insert into BillItemStatus (Status) values ('NEW');
insert into BillItemStatus (Status) values ('CORRECTED');
insert into BillItemStatus (Status) values ('SUBMITTED');
insert into BillItemStatus (Status) values ('REJECTED');
insert into BillItemStatus (Status) values ('APPROVED');

CREATE TABLE Client (
  ClientId int identity NOT NULL,
  ClientName varchar(45) NOT NULL,
  Active bit NOT NULL,
  ClientGuid varchar(255) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (ClientId)
)
go

CREATE TABLE Rate (
  AssetId int NOT NULL,
  ClientId int NOT NULL,
  DateRate decimal(10,2) NOT NULL,
  MilageRate decimal(10,2) NOT NULL,
  RateGuid varchar(255) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (AssetId, ClientId)
)
go
--  KEY `FK_rate_client` (`ClientId`),
--  CONSTRAINT `FK_rate_asset` FOREIGN KEY (`AssetId`) REFERENCES `asset` (`AssetId`),
--  CONSTRAINT `FK_rate_client` FOREIGN KEY (`ClientId`) REFERENCES `client` (`ClientId`)

CREATE TABLE SubAfe (
  SubAFE varchar(10) NOT NULL,
  AFE varchar(10) NOT NULL,
  SubAFEStatus varchar(10) NOT NULL,
  SubAfeGuid varchar(255) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (SubAFE)
)
go
--  KEY `FK_subafe_afe` (`AFE`),
--  KEY `FK_subafe_subafestatus` (`SubAFEStatus`),
--  CONSTRAINT `FK_subafe_afe` FOREIGN KEY (`AFE`) REFERENCES `afe` (`AFE`),
--  CONSTRAINT `FK_subafe_subafestatus` FOREIGN KEY (`SubAFEStatus`) REFERENCES `subafestatus` (`SubAFEStatus`)

CREATE TABLE SubAfeStatus (
  SubAFEStatus varchar(10) NOT NULL,
  PRIMARY KEY  (SubAFEStatus)
)
go

CREATE TABLE SyncLog (
  SyncLogId int identity NOT NULL,
  AssetId int NOT NULL,
  DeviceId varchar(50) NOT NULL,
  SyncTimeStamp timestamp NOT NULL,
  PRIMARY KEY  (SyncLogId)
)
go
--  KEY `FK_SyncLog_Asset` (`AssetId`),
--  CONSTRAINT `FK_SyncLog_Asset` FOREIGN KEY (`AssetId`) REFERENCES `asset` (`AssetId`)

