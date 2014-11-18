DROP TABLE IF EXISTS `DeletedTask`;
CREATE TABLE `DeletedTask` (
  `ServmanOrderNum` varchar(6) character set utf8 NOT NULL,
  `LastSyncDate` datetime default NULL,
  PRIMARY KEY  (`ServmanOrderNum`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;