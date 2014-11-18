INSERT INTO [dbo].[Address] ([Address1], [Address2], [City], [State], [Zip], [Map])
VALUES (N'7016 Randall Way', NULL, N'Plano', N'TX', N'75025', N'B34')
Go

INSERT INTO [dbo].[Address] ([Address1], [Address2], [City], [State], [Zip], [Map])
VALUES (N'2592 Main Way', NULL, N'Dallas', N'TX', N'76012', N'Q53')
Go

INSERT INTO [ItemShape] ([ID], [Shape], [Description])
VALUES (1, N'Rectangle', NULL)
Go

INSERT INTO [ItemShape] ([ID], [Shape], [Description])
VALUES (2, N'Round', NULL)
Go

INSERT INTO [MessageType] ([ID], [Type], [Description])
VALUES (1, N'TicketIncome', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (1, N'Assignment', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (2, N'Complete', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (3, N'AssignmentForExecution', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (4, N'Accepted', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (5, N'Declined', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (6, N'Arrived', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (7, N'NoGo', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (8, N'Reschedule', NULL)
Go

INSERT INTO [TicketTransactionType] ([ID], [Type], [Description])
VALUES (9, N'Reassign', NULL)
Go

INSERT INTO [Customer] ([ServmanCustId], [AddressId], [FirstName], [LastName], [Phone1], [Phone2])
VALUES (NULL, 1, N'Corcoran', N'Carol', N'(185) 593-2357', N'(973) 343-8922')
Go

INSERT INTO [Customer] ([ServmanCustId], [AddressId], [FirstName], [LastName], [Phone1], [Phone2])
VALUES (NULL, 2, N'Erica', N'Gabriel', N'(185) 093-9832', N'(973) 509-8723')
Go

INSERT INTO [EquipmentType] ([ID], [Type], [Description])
VALUES (1, N'Fan', NULL)
Go

INSERT INTO [EquipmentType] ([ID], [Type], [Description])
VALUES (2, N'Dehumidifier', NULL)
Go

INSERT INTO [EquipmentType] ([ID], [Type], [Description])
VALUES (3, N'Humidifier', NULL)
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1111')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1112')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1113')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1114')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1115')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1116')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1117')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1118')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1119')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (1, N'1110')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1120')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1121')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1122')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1123')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1124')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1125')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1126')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1127')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1128')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (2, N'1129')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1130')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1131')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1132')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1133')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1134')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1135')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1136')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1137')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1138')
Go

INSERT INTO [Equipment] ([EquipmentTypeId], [SerialNumber])
VALUES (3, N'1139')
Go

INSERT INTO [ItemType] ([ID], [Type], [Description])
VALUES (1, N'Rug', NULL)
Go

INSERT INTO [dbo].[Item] ([ItemTypeId], [SerialNumber], [ItemShapeId], [Width], [Height], [Diameter], [IsProtectorApplied], [IsPaddingApplied], [IsMothRepelApplied], [IsRapApplied], [CleanCost], [ProtectorCost], [PaddingCost], [MothRepelCost], [RapCost], [OtherCost], [SubTotalCost], [TaxCost], [TotalCost])
VALUES (1, N'0943', 1, 3, 4, NULL, 1, 0, 1, 0, 120.0000, 30.0000, NULL, 15.0000, NULL, NULL, 165.0000, 8.2500, 173.2500)
Go

INSERT INTO [dbo].[Item] ([ItemTypeId], [SerialNumber], [ItemShapeId], [Width], [Height], [Diameter], [IsProtectorApplied], [IsPaddingApplied], [IsMothRepelApplied], [IsRapApplied], [CleanCost], [ProtectorCost], [PaddingCost], [MothRepelCost], [RapCost], [OtherCost], [SubTotalCost], [TaxCost], [TotalCost])
VALUES (1, N'9532', 1, 2, 6, NULL, 0, 1, 1, 0, 90.0000, NULL, 20.0000, 12.0000, NULL, NULL, 122.0000, 6.1000, 128.1000)
Go

INSERT INTO [dbo].[Item] ([ItemTypeId], [SerialNumber], [ItemShapeId], [Width], [Height], [Diameter], [IsProtectorApplied], [IsPaddingApplied], [IsMothRepelApplied], [IsRapApplied], [CleanCost], [ProtectorCost], [PaddingCost], [MothRepelCost], [RapCost], [OtherCost], [SubTotalCost], [TaxCost], [TotalCost])
VALUES (1, N'0234', 2, NULL, NULL, 4, 1, 1, 1, 1, 70.0000, 12.0000, 32.0000, 13.0000, 30.0000, NULL, 157.0000, 7.8500, 164.8500)
Go

INSERT INTO [Van] ([ServmanTruckId], [ServmanTruckNum], [LicensePlateNumber], [EngineNumber], [BodyNumber], [Color], [OilChangeDue])
VALUES (N'000328', N'1342', N'409568', N'908034', N'093455', N'Black', N'2000 miles')
Go

INSERT INTO [Van] ([ServmanTruckId], [ServmanTruckNum], [LicensePlateNumber], [EngineNumber], [BodyNumber], [Color], [OilChangeDue])
VALUES (N'000329', N'1034', N'098445', N'2309u8', N'098454', N'Silver', N'2000 miles')
Go

INSERT INTO [Van] ([ServmanTruckId], [ServmanTruckNum], [LicensePlateNumber], [EngineNumber], [BodyNumber], [Color], [OilChangeDue])
VALUES (N'000330', N'1345', N'457323', N'2309u8', N'098454', N'Silver', N'2000 miles')
Go

INSERT INTO [Van] ([ServmanTruckId], [ServmanTruckNum], [LicensePlateNumber], [EngineNumber], [BodyNumber], [Color], [OilChangeDue])
VALUES (N'000331', N'1336', N'745678', N'2309u8', N'098454', N'Silver', N'2000 miles')
Go

INSERT INTO [Job] ([CustomerId], [ServiceAddressId], [JobStatusId], [Description])
VALUES (1, 1, NULL, NULL)
Go

INSERT INTO [Job] ([CustomerId], [ServiceAddressId], [JobStatusId], [Description])
VALUES (2, 2, NULL, NULL)
Go

INSERT INTO [TicketStatus] ([ID], [Status], [Description])
VALUES (1, N'Pending', NULL)
Go

INSERT INTO [TicketStatus] ([ID], [Status], [Description])
VALUES (2, N'Completed', NULL)
Go

INSERT INTO [TicketStatus] ([ID], [Status], [Description])
VALUES (3, N'Assigned', NULL)
Go

INSERT INTO [TicketStatus] ([ID], [Status], [Description])
VALUES (4, N'AssignedForExecution', NULL)
Go

INSERT INTO [TicketStatus] ([ID], [Status], [Description])
VALUES (5, N'Accepted', NULL)
Go

INSERT INTO [TicketStatus] ([ID], [Status], [Description])
VALUES (6, N'Declined', NULL)
Go

INSERT INTO [TicketStatus] ([ID], [Status], [Description])
VALUES (7, N'Arrived', NULL)
Go

INSERT INTO [TicketStatus] ([ID], [Status], [Description])
VALUES (8, N'NoGo', NULL)
Go

INSERT INTO [TicketType] ([ID], [Type], [Description])
VALUES (1, N'Rug Pickup', NULL)
Go

INSERT INTO [TicketType] ([ID], [Type], [Description])
VALUES (2, N'Rug Delivery', NULL)
Go

INSERT INTO [TicketType] ([ID], [Type], [Description])
VALUES (3, N'Unknown', NULL)
Go

INSERT INTO [Ticket] ([ServmanTicketNum], [JobId], [TicketTypeId], [TicketStatusId], [Number], [CreateDate], [ServiceDate], [Description], [Message], [Notes])
VALUES (NULL, 1, 1, 1, N'1001', '20070611', '20070611', N'description', N'message', N'notes')
Go

INSERT INTO [Ticket] ([ServmanTicketNum], [JobId], [TicketTypeId], [TicketStatusId], [Number], [CreateDate], [ServiceDate], [Description], [Message], [Notes])
VALUES (NULL, 1, 2, 1, N'1002', '20070611', '20070611', N'desc', N'message', N'notes')
Go

INSERT INTO [Ticket] ([ServmanTicketNum], [JobId], [TicketTypeId], [TicketStatusId], [Number], [CreateDate], [ServiceDate], [Description], [Message], [Notes])
VALUES (NULL, 2, 2, 1, N'1003', '20070611', '20070611', N'desc', N'message', N'notes')
Go

INSERT INTO [TicketItemDelivery] ([TicketId], [ItemId])
VALUES (2, 1)
Go

INSERT INTO [TicketItemDelivery] ([TicketId], [ItemId])
VALUES (2, 2)
Go

INSERT INTO [TicketItemDelivery] ([TicketId], [ItemId])
VALUES (3, 3)
Go

INSERT INTO [EmployeeType] ([ID], [Type], [Description])
VALUES (1, N'Technician', NULL)
Go

INSERT INTO [EmployeeType] ([ID], [Type], [Description])
VALUES (2, N'Dispatch', NULL)
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (2, N'514', NULL, NULL, NULL, N'Laura', N'Trager', NULL, NULL, NULL, N'1')
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (2, N'493', NULL, NULL, NULL, N'Tina', N'Franks', NULL, NULL, NULL, N'1')
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (1, NULL, N'519', NULL, NULL, N'Shane', N'Hobbs', NULL, NULL, NULL, N'1')
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (1, NULL, N'237', NULL, NULL, N'Nick', N'Hobbs', NULL, NULL, NULL, N'1')
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (1, NULL, N'760', NULL, NULL, N'Wade', N'Hanry', NULL, NULL, NULL, N'1')
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (1, NULL, N'730', NULL, NULL, N'Ross', N'Gary', NULL, NULL, NULL, N'1')
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (1, NULL, N'909', NULL, NULL, N'Robles', N'Paul', NULL, NULL, NULL, N'1')
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (1, NULL, N'131', NULL, NULL, N'Dewayne', N'Brethower', NULL, NULL, NULL, N'1')
Go

INSERT INTO [Employee] ([EmployeeTypeId], [ServmanUserId], [ServmanTechId], [AddressId], [Login], [FirstName], [LastName], [HireDate], [Phone1], [Phone2], [Password])
VALUES (1, NULL, N'303', NULL, NULL, N'Dillan', N'Blew', NULL, NULL, NULL, N'1')
Go

INSERT INTO [WorkStatus] ([ID], [Status], [Description])
VALUES (1, N'ReadyForStartDay', NULL)
Go

INSERT INTO [WorkStatus] ([ID], [Status], [Description])
VALUES (2, N'StartDayDone', NULL)
Go

INSERT INTO [WorkStatus] ([ID], [Status], [Description])
VALUES (3, N'Pending', NULL)
Go

INSERT INTO [WorkStatus] ([ID], [Status], [Description])
VALUES (4, N'Completed', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (1, N'StartDayDone', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (2, N'TicketCompleted', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (3, N'TicketDeclined', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (4, N'TicketAccepted', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (5, N'TicketArrived', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (6, N'Completed', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (7, N'SubmitETC', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (8, N'NoGo', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (9, N'GPS', NULL)
Go

INSERT INTO [WorkTransactionPaymentType] ([ID], [PaymentType], [Description])
VALUES (1, N'CreditCard', NULL)
Go

INSERT INTO [WorkTransactionPaymentType] ([ID], [PaymentType], [Description])
VALUES (2, N'BankCheck', NULL)
Go

INSERT INTO [WorkTransactionPaymentType] ([ID], [PaymentType], [Description])
VALUES (3, N'Cash', NULL)
Go

INSERT INTO [CreditCardType] ([ID], [Type], [Description])
VALUES (1, N'Visa', NULL)
Go

INSERT INTO [CreditCardType] ([ID], [Type], [Description])
VALUES (2, N'MasterCard', NULL)
Go

INSERT INTO [CreditCardType] ([ID], [Type], [Description])
VALUES (3, N'Dinner', NULL)
Go

INSERT INTO [CreditCardType] ([ID], [Type], [Description])
VALUES (4, N'Discover', NULL)
Go

INSERT INTO [dbo].[TicketEquipmentCapture] ([TicketId], [EquipmentId])
VALUES (1, 3)
Go

INSERT INTO [dbo].[TicketEquipmentCapture] ([TicketId], [EquipmentId])
VALUES (2, 5)
Go

INSERT INTO [dbo].[TicketEquipmentCapture] ([TicketId], [EquipmentId])
VALUES (3, 2)
Go

INSERT INTO [dbo].[TicketEquipmentCapture] ([TicketId], [EquipmentId])
VALUES (1, 12)
Go

INSERT INTO [dbo].[TicketEquipmentCapture] ([TicketId], [EquipmentId])
VALUES (1, 4)
Go

INSERT INTO [dbo].[TicketEquipmentRequirement] ([TicketId], [EquipmentTypeId], [ServiceQuantity], [LeaveQuantity])
VALUES (1, 1, 4, 1)
Go

INSERT INTO [dbo].[TicketEquipmentRequirement] ([TicketId], [EquipmentTypeId], [ServiceQuantity], [LeaveQuantity])
VALUES (1, 2, 2, 0)
Go

INSERT INTO [dbo].[TicketEquipmentRequirement] ([TicketId], [EquipmentTypeId], [ServiceQuantity], [LeaveQuantity])
VALUES (1, 3, 0, 3)
Go

INSERT INTO [dbo].[TicketEquipmentRequirement] ([TicketId], [EquipmentTypeId], [ServiceQuantity], [LeaveQuantity])
VALUES (2, 2, 6, 1)
Go

INSERT INTO [dbo].[TicketEquipmentRequirement] ([TicketId], [EquipmentTypeId], [ServiceQuantity], [LeaveQuantity])
VALUES (3, 3, 3, 2)
Go
