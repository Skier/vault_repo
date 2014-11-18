INSERT INTO `businesspartner` (`Id`, `PartnerName`, `Email`, `Phone`, `IsActive`) VALUES
(1,'Affilia','valery@affilia.com','(214)335-4141',1),
(2,'Exceleron','valery@exceleron.com','(214)335-5454',1),
(3,'Dekasoft','valery@dekasoft.com.ua','(214)335-3232',1);

update `user` set BusinessPartnerId=1 where Id=1;
update `user` set BusinessPartnerId=2 where Id=2;
update `user` set BusinessPartnerId=3 where Id=3;

INSERT INTO `campaign` (`Id`, `CampaignName`, `DateCreated`, `DateStart`, `DateEnd`, `BusinessPartnerId`, `UserId`) VALUES
(1,'GoogleAdvertizement','2011-04-04','2011-05-05', null, 1, 1),
(2,'Yellow Pages','2011-04-04','2011-05-05', null, null, 2),
(3,'White Pages','2011-04-04','2011-05-05', null, null, 3),
(4,'Dallas Morning News','2011-04-04','2011-05-05', null, 1, 1),
(5,'Newspaper','2011-04-04','2011-05-05', null, 2, 2),
(6,'Yahoo hotline','2011-04-04','2011-05-05', null, 3, 3),
(7,'Spam assistant','2011-04-04','2011-05-05', null, 1, 1),
(8,'Exceleron Software','2011-04-04','2011-05-05', null, 2, 2),
(9,'Dekasoft Adv','2011-04-04','2011-05-05', null, 3, 3);

INSERT INTO `trackingphone` (`Id`, `PhoneNumber`, `FriendlyNumber`, `Description`, `RedirectPhoneNumber`, `TwilioNumberId`, `DateCreated`, `IsSuspended`, `IsRemoved`, `IsTollFree`, `CallerIdLookup`, `TranscribeCalls`) VALUES
(1,'+12143352121','(214)335-2121', '(214)335-4141', '(214)335-4143', '123456789123123123','2011-05-05',0,0,0,0,0),
(2,'+12143353232','(214)335-3232', '(214)335-4141', '(214)335-4143', '123456789123123124','2011-05-05',0,0,0,0,0),
(3,'+12143354343','(214)335-4343', '(214)335-4141', '(214)335-4143', '123456789123123125','2011-05-05',0,0,0,0,0),
(4,'+12143355454','(214)335-5454', '(214)335-4141', '(214)335-4143', '123456789123123126','2011-05-05',0,0,0,0,0);

INSERT INTO `compaigntrackingphone` (`CampaignId`, `TrackingPhoneId`, `DateAssigned`, `DateReleased`) VALUES
(1, 1, '2011-05-05', null),
(2, 2, '2011-05-05', null),
(3, 3, '2011-05-05', null),
(4, 4, '2011-05-05', null);

INSERT INTO `phonecall` (`Id`, `TrackingPhoneId`, `TrackingPhoneNumber`, `CallDuration`, `RecordingUrl`, `DateCreated`, `Status`, `CampaignId`, `CallerName`, `FromPhone`, `FromCity`, `FromState`, `FromZip`, `FromCountry`, `TwilioCallId`, `TrackingPhoneRotationId`) VALUES
(1,  1,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 1, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(2,  2,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 2, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(3,  3,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 3, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(4,  4,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 4, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(5,  1,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 1, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(6,  2,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 2, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(7,  3,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 3, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(8,  4,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 4, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(9,  1,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 1, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(10, 2,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 2, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(11, 3,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 3, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(12, 4,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 4, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(13, 1,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 1, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(14, 2,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 2, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null),
(15, 3,  '(214)335-2121',123, '/REbbec9e4b05ed7896b35357dda662ea4a.mp3','2011-05-05', 'completed', 3, 'unknown','(214)335-2121', 'Plano', 'TX', '75025','USA', 'ABC', null);

INSERT INTO `phonesms` (`Id`, `TrackingPhoneId`, `TrackingPhoneNumber`, `Message`, `DateCreated`, `CampaignId`, `FromPhone`, `Status`, `TwilioSmsId`, `TrackingPhoneRotationId`) VALUES
(1, 1, '(214)335-2121', 'sample sms messsage', '2011-05-05', 1, '(214)335-2121', 'completed', 'ABC', null),
(2, 2, '(214)335-2121', 'sample sms messsage', '2011-05-05', 2, '(214)335-2121', 'completed', 'ABC', null),
(3, 3, '(214)335-2121', 'sample sms messsage', '2011-05-05', 3, '(214)335-2121', 'completed', 'ABC', null),
(4, 4, '(214)335-2121', 'sample sms messsage', '2011-05-05', 4, '(214)335-2121', 'completed', 'ABC', null),
(5, 1, '(214)335-2121', 'sample sms messsage', '2011-05-05', 1, '(214)335-2121', 'completed', 'ABC', null),
(6, 2, '(214)335-2121', 'sample sms messsage', '2011-05-05', 2, '(214)335-2121', 'completed', 'ABC', null),
(7, 1, '(214)335-2121', 'sample sms messsage', '2011-05-05', 1, '(214)335-2121', 'completed', 'ABC', null);

INSERT INTO `webform` (`Id`, `CampagnId`, `DateCreated`, `FirstName`, `LastName`, `Phone`, `Message`, `WebPageUri`) VALUES
(1, 5, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', 'sample message', 'http://website.com/pageUri'),
(2, 6, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', 'sample message', 'http://website.com/pageUri'),
(3, 7, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', 'sample message', 'http://website.com/pageUri'),
(4, 8, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', 'sample message', 'http://website.com/pageUri'),
(5, 9, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', 'sample message', 'http://website.com/pageUri');

INSERT INTO `source` (`Id`, `PhoneCallId`, `PhoneSmsId`, `WebFormId`, `UserId`) VALUES
(1, 1, null, null, null),
(2, 2, null, null, null),
(3, 3, null, null, null),
(4, 4, null, null, null),
(5, 5, null, null, null),
(6, 6, null, null, null),
(7, 7, null, null, null),
(8, 8, null, null, null),
(9, 9, null, null, null),
(10, 10, null, null, null),
(11, 11, null, null, null),
(12, 12, null, null, null),
(13, 13, null, null, null),
(14, 14, null, null, null),
(15, 15, null, null, null),
(16, null, 1, null, null),
(17, null, 2, null, null),
(18, null, 3, null, null),
(19, null, 4, null, null),
(20, null, 5, null, null),
(21, null, 6, null, null),
(22, null, 7, null, null),
(23, null, null, 1, null),
(24, null, null, 2, null),
(25, null, null, 3, null),
(26, null, null, 4, null),
(27, null, null, 5, null),
(28, null, null, null, 1),
(29, null, null, null, 2),
(30, null, null, null, 3),
(31, null, null, null, 4),
(32, null, null, null, 5);

INSERT INTO `lead` (`Id`, `LeadStatusId`, `CampaignId`, `DateCreated`, `FirstName`, `LastName`, `Phone`, `Address`, `CustomerNotes`, `SourceId`) VALUES
(1, 1, 1, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 1),
(2, 1, 2, '2011-05-05', 'Boris', 'Furman',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 2 ),
(3, 2, 3, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 3),
(4, 1, 4, '2011-05-05', 'Boris', 'Furman',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 4),
(5, 1, 5, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 5),
(6, 3, 6, '2011-05-05', 'Boris', 'Furman',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 6),
(7, 1, 7, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 7),
(8, 3, 8, '2011-05-05', 'Boris', 'Furman',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 8),
(9, 1, 9, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 9),
(10, 2, 1, '2011-05-05', 'John', 'Silver',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 10),
(11, 1, 2, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 11),
(12, 2, 3, '2011-05-05', 'John', 'Silver',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 12),
(13, 1, 4, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 13),
(14, 4, 5, '2011-05-05', 'John', 'Doe',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 14),
(15, 1, 6, '2011-05-05', 'John', 'Doe',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 15),
(16, 2, 1, '2011-05-05', 'John', 'Silver',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 16),
(17, 1, 2, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 17),
(18, 3, 3, '2011-05-05', 'John', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 18),
(19, 1, 4, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 19),
(20, 4, 5, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 20),
(21, 4, 1, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 21),
(22, 2, 2, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 22),
(23, 4, 3, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 23),
(24, 3, 4, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 24),
(25, 4, 5, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 25),
(26, 1, 6, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 26),
(27, 1, 7, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 27),
(28, 1, 8, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 28),
(29, 1, 9, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 29),
(30, 2, 1, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 30),
(31, 2, 2, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 31),
(32, 2, 3, '2011-05-05', 'Valery', 'Yaworsky',  '(214)335-2121', '7016 Randall Way Plano TX 75025', 'need help', 32);

INSERT INTO `transaction` (`TransactionTypeId`, `DateCreated`, `SourceId`, `Description`, `Cost`, `Quantity`, `Amount`, `CurrentBalance`) VALUES
(10, '2011-06-01', null, '', 10, 1, 8, 8),
(10, '2011-06-01', null, '', 12, 1, 12, 20),
(10, '2011-06-01', null, '', 7, 1, 7, 27);
