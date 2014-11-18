alter table AssetAssignment
    drop constraint FK_AssetAssignment_SubAfe

alter table AssetAssignment
    drop constraint FK_AssetAssignment_Afe

alter table SubAfe
    drop constraint FK_SubAfe_Afe

alter table AssetAssignment
    alter column AFE varchar(20) not null

alter table AssetAssignment
    alter column SubAFE varchar(120) not null

alter table AFE
    alter column AFE varchar(20) not null

alter table AFE
    alter column AFEName varchar(100) not null

alter table SubAFE
    alter column AFE varchar(20) not null

alter table SubAFE
    alter column SubAFE varchar(120) not null

alter table SubAFE
    alter column ShortName varchar(20) not null

alter table AssetAssignment add
    constraint FK_AssetAssignment_SubAfe foreign key(SubAFE) references SubAfe(SubAFE)
        on update no action
        on delete no action,
    constraint FK_AssetAssignment_Afe foreign key(AFE) references Afe(AFE)
        on update no action
        on delete no action

alter table SubAfe add
    constraint FK_SubAfe_Afe foreign key(AFE) references Afe(AFE)
        on update cascade
        on delete cascade
