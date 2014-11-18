ALTER TABLE `Customer` MODIFY COLUMN `ServmanCustId` varchar(6) character set utf8 NOT NULL;
update Customer
set ServmanCustId = LPAD(ServmanCustId, 6, '0')
where length(ServmanCustId) < 6;