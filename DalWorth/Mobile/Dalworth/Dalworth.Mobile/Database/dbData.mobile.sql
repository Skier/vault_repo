INSERT INTO [ItemShape] ([ID], [Shape], [Description])
VALUES (1, N'Rectangle', NULL)
Go

INSERT INTO [ItemShape] ([ID], [Shape], [Description])
VALUES (2, N'Round', NULL)
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

INSERT INTO [ItemType] ([ID], [Type], [Description])
VALUES (1, N'Rug', NULL)
Go

INSERT INTO [TaskStatus] ([ID], [Status], [Description])
VALUES (1, N'Not Completed', NULL)
Go

INSERT INTO [TaskStatus] ([ID], [Status], [Description])
VALUES (2, N'Completed', NULL)
Go

INSERT INTO [VisitStatus] ([ID], [Status], [Description])
VALUES (1, N'Pending', NULL)
Go

INSERT INTO [VisitStatus] ([ID], [Status], [Description])
VALUES (2, N'Completed', NULL)
Go

INSERT INTO [VisitStatus] ([ID], [Status], [Description])
VALUES (3, N'Assigned', NULL)
Go

INSERT INTO [VisitStatus] ([ID], [Status], [Description])
VALUES (4, N'AssignedForExecution', NULL)
Go

INSERT INTO [VisitStatus] ([ID], [Status], [Description])
VALUES (5, N'Accepted', NULL)
Go

INSERT INTO [VisitStatus] ([ID], [Status], [Description])
VALUES (6, N'Declined', NULL)
Go

INSERT INTO [VisitStatus] ([ID], [Status], [Description])
VALUES (7, N'Arrived', NULL)
Go

INSERT INTO [VisitStatus] ([ID], [Status], [Description])
VALUES (8, N'NoGo', NULL)
Go

INSERT INTO [TaskType] ([ID], [Type], [Description])
VALUES (1, N'Rug Pickup', NULL)
Go

INSERT INTO [TaskType] ([ID], [Type], [Description])
VALUES (2, N'Rug Delivery', NULL)
Go

INSERT INTO [TaskType] ([ID], [Type], [Description])
VALUES (3, N'Unknown', NULL)
Go

INSERT INTO [EmployeeType] ([ID], [Type], [Description])
VALUES (1, N'Technician', NULL)
Go

INSERT INTO [EmployeeType] ([ID], [Type], [Description])
VALUES (2, N'Dispatch', NULL)
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
VALUES (2, N'VisitCompleted', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (3, N'VisitDeclined', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (4, N'VisitAccepted', NULL)
Go

INSERT INTO [WorkTransactionType] ([ID], [Type], [Description])
VALUES (5, N'VisitArrived', NULL)
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

INSERT INTO [ApplicationState] ([ID], [State], [Description])
VALUES (1, N'StartDay', NULL)
Go

INSERT INTO [ApplicationState] ([ID], [State], [Description])
VALUES (2, N'StartDayDone', NULL)
Go

INSERT INTO [Application] ([ID], [ApplicationStateId], [WorkId])
VALUES (1, 1, NULL)
Go

INSERT INTO [ProjectType] ([ID], [Type], [Description])
VALUES (1, N'RugCleaning', NULL)
Go

INSERT INTO [ProjectStatus] ([ID], [Status], [Description])
VALUES (1, N'Open', NULL)
Go

INSERT INTO [ProjectStatus] ([ID], [Status], [Description])
VALUES (2, N'Completed', NULL)
Go