ALTER TABLE BusinessPartner ADD (
        FirstName varchar(50) default NULL,
        LastName varchar(50) default NULL,
        CanLogin tinyint(4) default NULL);