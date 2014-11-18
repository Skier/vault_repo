--
-- Table structure for table `partnerphonenumber`
--

DROP TABLE IF EXISTS `partnerphonenumber`;
CREATE TABLE `PartnerPhoneNumber` (
  `Id` int(10) NOT NULL auto_increment,
  `BusinessPartnerId` int(10) NOT NULL,
  `PhoneNumber` varchar(50) NOT NULL,
  `Description` varchar(250) ,
  PRIMARY KEY  (`Id`),
  KEY `FK_PartnerPhoneNumber_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_PartnerPhoneNumber_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `BusinessPartner` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Alter Table structure for table `businesspartner` Add SalesRep column
--

ALTER TABLE `Lead` 
 ADD COLUMN `BusinessPartnerId` int(10) AFTER `CampaignId`;

ALTER TABLE `Lead`
 ADD CONSTRAINT `FK_Lead_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`)
    REFERENCES `businesspartner` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;


UPDATE `lead` l
INNER JOIN `campaign` c ON c.Id = l.CampaignId
SET l.BusinessPartnerId = c.BusinessPartnerId;

ALTER TABLE `partnerphonenumber` 
 ADD COLUMN `PhoneDigits` varchar(10) AFTER `Description`;
