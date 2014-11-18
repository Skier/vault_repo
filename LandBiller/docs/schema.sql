create sequence client_clientid_seq;
create sequence tbldefaultexpitemtypes_defaultexpitemsid_seq;
create sequence tbldefaultuserrates_defaultuserrates_seq;
create sequence tblexpitems_expid_seq;
create sequence tblinvoice_tblinvoice_seq;
create sequence tblprojects_projectid_seq;
create sequence tblUserLogin_userLoginID_seq;
create sequence users_userID_seq;


CREATE TABLE client(
clientid int NOT NULL DEFAULT nextval('tblclients_clientid_seq'::regclass),
clientname varchar(150) NOT NULL ,
fname varchar(50) NOT NULL ,
mname varchar(50) ,
lname varchar(50) NOT NULL ,
primaryphone varchar(12) NOT NULL ,
altphone varchar(12) ,
add1 varchar(50) NOT NULL ,
add2 varchar(50) ,
city varchar(50) NOT NULL ,
state varchar(2) NOT NULL ,
zip varchar(10) NOT NULL ,
PRIMARY KEY (clientid)
)  WITHOUT OIDS;

-- ----------------------------
-- Table structure for public.tbldefaultexpitemtypes
-- ----------------------------
drop table public.tbldefaultexpitemtypes;
CREATE TABLE public.tbldefaultexpitemtypes(
defaultexpitemsid int8 NOT NULL DEFAULT nextval('tbldefaultexpitemtypes_defaultexpitemsid_seq'::regclass),
defultexpitemname varchar(50) NOT NULL ,
PRIMARY KEY (defaultexpitemsid)
)  WITHOUT OIDS;

-- ----------------------------
-- Table structure for public.tbldefaultuserrates
-- ----------------------------
drop table public.tbldefaultuserrates;
CREATE TABLE public.tbldefaultuserrates(
defaultuserrates int8 NOT NULL DEFAULT nextval('tbldefaultuserrates_defaultuserrates_seq'::regclass),
defaultexpitemid int8 NOT NULL ,
defaultrate numeric(18,3) NOT NULL ,
userid int8 NOT NULL ,
PRIMARY KEY (defaultuserrates)
)  WITHOUT OIDS;

-- ----------------------------
-- Table structure for public.tblexpitems
-- ----------------------------
drop table public.tblexpitems;
CREATE TABLE public.tblexpitems(
expid int8 NOT NULL DEFAULT nextval('tblexpitems_expid_seq'::regclass),
expdate date NOT NULL ,
projectid int8 NOT NULL ,
exptypeid int8 NOT NULL ,
expqty numeric(18,4) NOT NULL ,
exprate numeric(18,3) NOT NULL ,
expadj numeric(18,3) ,
expstatus int8 NOT NULL ,
PRIMARY KEY (expid)
)  WITHOUT OIDS;

-- ----------------------------
-- Table structure for public.tblinvoice
-- ----------------------------
drop table public.tblinvoice;
CREATE TABLE public.tblinvoice(
tblinvoice int8 NOT NULL DEFAULT nextval('tblinvoice_tblinvoice_seq'::regclass),
invoicedate timestamp NOT NULL ,
userid int8 NOT NULL ,
PRIMARY KEY (tblinvoice)
)  WITHOUT OIDS;

-- ----------------------------
-- Table structure for public.tblprojects
-- ----------------------------
drop table public.tblprojects;
CREATE TABLE public.tblprojects(
projectid int8 NOT NULL DEFAULT nextval('tblprojects_projectid_seq'::regclass),
afe varchar(50) ,
projectname varchar(50) NOT NULL ,
clientid int8 NOT NULL ,
userid int8 NOT NULL ,
projectstatus int2 NOT NULL ,
PRIMARY KEY (projectid)
)  WITHOUT OIDS;

-- ----------------------------
-- Table structure for public.tbluserlogin
-- ----------------------------
drop table public.tbluserlogin;
CREATE TABLE public.tbluserlogin(
userloginid int8 NOT NULL DEFAULT nextval('tblUserLogin_userLoginID_seq'::regclass),
userid int8 NOT NULL ,
pword varchar(12) NOT NULL ,
PRIMARY KEY (userloginid)
)  WITHOUT OIDS;

-- ----------------------------
-- Table structure for public.tblusers
-- ----------------------------
drop table public.tblusers;
CREATE TABLE public.tblusers(
userid int8 NOT NULL DEFAULT nextval('users_userID_seq'::regclass),
fname varchar(50) NOT NULL ,
mname varchar(50) ,
lname varchar(50) NOT NULL ,
username varchar(50) NOT NULL ,
useractstat bool NOT NULL DEFAULT false,
primaryphone varchar(12) ,
bday date ,
PRIMARY KEY (userid)
)  WITHOUT OIDS;

-- ----------------------------
-- Records 
-- ----------------------------
BEGIN; LOCK TABLE public.tblclients IN SHARE MODE;
INSERT INTO public.tblclients (clientid, companyname, fname, mname, lname, primaryphone, altphone, add1, add2, city, state, zip) VALUES (1, 'Encana', 'Jessica', 'L', 'Huprich', '2147237423', '2147081248', '14001 N. Dallas Parkway ', 'Suite 1100 ', 'Dallas', 'TX', '75240');
INSERT INTO public.tblclients (clientid, companyname, fname, mname, lname, primaryphone, altphone, add1, add2, city, state, zip) VALUES (2, 'Encana', 'Mark', 'L', 'DeCordova', '2142427326', '9402108873', '14001 N. Dallas Parkway ', 'Suite 1100 ', 'Dallas', 'TX', '75240');
COMMIT;
BEGIN; LOCK TABLE public.tbldefaultexpitemtypes IN SHARE MODE;
COMMIT;
BEGIN; LOCK TABLE public.tbldefaultuserrates IN SHARE MODE;
COMMIT;
BEGIN; LOCK TABLE public.tblexpitems IN SHARE MODE;
INSERT INTO public.tblexpitems (expid, expdate, projectid, exptypeid, expqty, exprate, expadj, expstatus) VALUES (1, '2008-12-02', 1, 1, '0.5000', '500.000', null, 1);
INSERT INTO public.tblexpitems (expid, expdate, projectid, exptypeid, expqty, exprate, expadj, expstatus) VALUES (3, '2008-12-02', 1, 3, '1.0000', '10.000', null, 1);
INSERT INTO public.tblexpitems (expid, expdate, projectid, exptypeid, expqty, exprate, expadj, expstatus) VALUES (4, '2008-12-02', 2, 1, '1.0000', '500.000', '450.000', 1);
INSERT INTO public.tblexpitems (expid, expdate, projectid, exptypeid, expqty, exprate, expadj, expstatus) VALUES (2, '2008-12-02', 1, 2, '324.0000', '0.585', null, 1);
COMMIT;
BEGIN; LOCK TABLE public.tblinvoice IN SHARE MODE;
COMMIT;
BEGIN; LOCK TABLE public.tblprojects IN SHARE MODE;
INSERT INTO public.tblprojects (projectid, afe, projectname, clientid, userid, projectstatus) VALUES (1, '48536574-o', 'Alpha', 1, 1, 1);
INSERT INTO public.tblprojects (projectid, afe, projectname, clientid, userid, projectstatus) VALUES (2, null, 'Bravo', 1, 1, 1);
INSERT INTO public.tblprojects (projectid, afe, projectname, clientid, userid, projectstatus) VALUES (3, null, 'Charlie', 2, 1, 1);
COMMIT;
BEGIN; LOCK TABLE public.tbluserlogin IN SHARE MODE;
INSERT INTO public.tbluserlogin (userloginid, userid, pword) VALUES (1, 1, '4766626');
INSERT INTO public.tbluserlogin (userloginid, userid, pword) VALUES (2, 2, '5');
INSERT INTO public.tbluserlogin (userloginid, userid, pword) VALUES (4, 28, 'suri3');
INSERT INTO public.tbluserlogin (userloginid, userid, pword) VALUES (5, 29, '1234');
INSERT INTO public.tbluserlogin (userloginid, userid, pword) VALUES (6, 30, '1234');
INSERT INTO public.tbluserlogin (userloginid, userid, pword) VALUES (7, 31, '1234');
COMMIT;
BEGIN; LOCK TABLE public.tblusers IN SHARE MODE;
INSERT INTO public.tblusers (userid, fname, mname, lname, username, useractstat, primaryphone, bday) VALUES (1, 'Rob', 'M', 'Love', 'RobL', 't', null, null);
INSERT INTO public.tblusers (userid, fname, mname, lname, username, useractstat, primaryphone, bday) VALUES (2, 'Les', null, 'Lamb', 'LesL', 'f', null, null);
INSERT INTO public.tblusers (userid, fname, mname, lname, username, useractstat, primaryphone, bday) VALUES (29, 'Rob', 'm', 'lobr', 'robLove', 'f', null, '2008-12-01');
INSERT INTO public.tblusers (userid, fname, mname, lname, username, useractstat, primaryphone, bday) VALUES (30, 'Billy', '', 'Harry', 'Billy', 'f', null, '2008-12-02');
INSERT INTO public.tblusers (userid, fname, mname, lname, username, useractstat, primaryphone, bday) VALUES (31, 'Boris', 'M', 'Furman', 'Boris', 'f', null, '2008-12-28');
INSERT INTO public.tblusers (userid, fname, mname, lname, username, useractstat, primaryphone, bday) VALUES (28, 'Tom', 'D', 'Cruise', 'TomKat', 't', null, '2008-12-02');
COMMIT;
