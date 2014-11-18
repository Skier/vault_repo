DROP TABLE IF EXISTS `predictionignore`;
CREATE TABLE `predictionignore` (
  `IgnoreDate` datetime NOT NULL,
  PRIMARY KEY  (`IgnoreDate`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `sysversion` (`Version`) VALUES 
(11);