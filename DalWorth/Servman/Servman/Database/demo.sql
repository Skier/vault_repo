DELETE FROM `leadtype`;
DELETE FROM `lead`;
DELETE FROM `leadsourcetrackingphone`;
DELETE FROM `phonecallworkflow`;
DELETE FROM `callworkflow`;
DELETE FROM `leadsource`;
DELETE FROM `trackingphone`;
DELETE FROM `user`;


INSERT INTO `leadtype` (`Id`, `Name`, `IsActive`) VALUES (1,'unspecified', 1);

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`Id`, `QbUserId`, `Email`, `Name`, `FirstName`, `LastName`, `Phone`, `Address`, `PhotoFileId`, `IsActive`,`RoleName`) VALUES (1,'55521507.ds33','valery@affilia.com','Valery Yaworsky','Valery','Yaworsky','+12143354143',NULL,NULL,1,'Administrator'),(2,'55931817.dsnr','valery_j@yahoo.com','All Sons Moving','Valery','Yaworsky','','',NULL,1,'Staff'),(3,'55887942.be8h','bifurman@yahoo.com','Franklin Plumbing','Boris','Furman','+12143354143','',NULL,1,'Staff'),(4,'55889039.dhjk','valery@dekasoft.com.ua','Molly Maids','Molly','Maids','+19725173715','',NULL,1,'Staff');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

/*!40000 ALTER TABLE `leadsource` DISABLE KEYS */;
INSERT INTO `leadsource` (`Id`, `UserId`, `Name`, `IsActive`) VALUES
(1,2,'All Sons Moving',1),
(2,3,'Franklin Plumbing',1),
(3,4,'Molly Maids',1),
(4,NULL,'DalworthRestoration.com',1);
/*!40000 ALTER TABLE `leadsource` ENABLE KEYS */;

/*!40000 ALTER TABLE `callworkflow` DISABLE KEYS */;
INSERT INTO `callworkflow` (`Id`, `Description`) VALUES (1,'Voice mail'),(2,'Redirect call'),(3,'Queue call');
/*!40000 ALTER TABLE `callworkflow` ENABLE KEYS */;

/*!40000 ALTER TABLE `trackingphone` DISABLE KEYS */;
INSERT INTO `trackingphone` (`Id`, `Number`, `TwilioId`, `Description`, `IsTollFree`, `IsSuspended`, `IsRemoved`, `CanRotate`, `ScreenNumber`)
VALUES
(1,'+12144271965','PN64a18f6fc8c2ecc13c99945bda8047b6','',0,0,0,1, '+1(214)427-1965'),
(2,'+12544497250','PN9bf6c6a62764f9c31b78171879247f87','(254) 449-7250',0,0,0,1,'254 449-7250'),
(3,'+12147851062','PN0a7ebe4427316fbe511476ad14bd9370',NULL,0,0,0,1,'+1 214 785-1062'),
(4,'+12149195203','PN9a8508c3eb668ba36b2c35ed0a2763e3','(214) 919-5203',0,0,0,1,'(214) 919-52-03');
/*!40000 ALTER TABLE `trackingphone` ENABLE KEYS */;

/*!40000 ALTER TABLE `leadsourcetrackingphone` DISABLE KEYS */;
INSERT INTO `leadsourcetrackingphone` (`TrackingPhoneId`, `LeadSourceId`, `Notes`) VALUES (1,4,''),(4,4,''),(2,2,''),(3,3,'');
/*!40000 ALTER TABLE `leadsourcetrackingphone` ENABLE KEYS */;

INSERT INTO `workflowdetail` (`Id`, `CallWorkflowId`, `PropertyName`, `PropertyValue`) VALUES
(1,1,'Welcome message', 'Welcome to Dalworth restoration.'),
(2,1,'Message', 'Please leave your message after the tone.'),
(3,2,'Redirect phone number', '+12147851062'),
(4,3,'Welcome message 1', 'Welcome to Dalworth restoration.'),
(5,3,'Welcome message 2', 'Please wait for available dispatcher.'),
(6,3,'Sound URL', 'chopin.mp3');
