/*==============================================================*/
/* Database name:  PhysicalDataModel_1                          */
/* DBMS name:      MySQL 3.23                                   */
/* Created on:     4/6/2006 4:10:14 PM                          */
/*==============================================================*/

drop database vacations;

create database vacations default character set cp1251 collate cp1251_general_ci;

use vacations;

/*==============================================================*/
/* Table: VRBO_ACTIVITY                                         */
/*==============================================================*/
create table if not exists VRBO_ACTIVITY
(
   ACTIVITY_ID                    int                            not null AUTO_INCREMENT,
   primary key (ACTIVITY_ID)
);

/*==============================================================*/
/* Table: VRBO_ACTIVITY_INFO                                    */
/*==============================================================*/
create table if not exists VRBO_ACTIVITY_INFO
(
   ACTIVITY_INFO_ID               int                            not null AUTO_INCREMENT,
   ACTIVITY_NAME                  varchar(50) character set cp1251 collate cp1251_general_ci not null,
   LANGUAGE                       varchar(2)                     not null,
   ACTIVITY_ID                    int                            not null,
   primary key (ACTIVITY_INFO_ID)
);

/*==============================================================*/
/* Table: VRBO_AMENITY                                          */
/*==============================================================*/
create table if not exists VRBO_AMENITY
(
   AMENITY_ID                     int                            not null AUTO_INCREMENT,
   primary key (AMENITY_ID)
);

/*==============================================================*/
/* Table: VRBO_AMENITY_INFO                                     */
/*==============================================================*/
create table if not exists VRBO_AMENITY_INFO
(
   AMENITY_INFO_ID                int                            not null AUTO_INCREMENT,
   AMENITY_NAME                   varchar(50) character set cp1251 collate cp1251_general_ci not null,
   LANGUAGE                       varchar(2)                     not null,
   AMENITY_ID                     int                            not null,
   primary key (AMENITY_INFO_ID)
);

/*==============================================================*/
/* Table: VRBO_COMPANY_USER                                     */
/*==============================================================*/
create table if not exists VRBO_COMPANY_USER
(
   USER_ID                        int                            not null,
   COMPANY_USER_TYPE              varchar(20)                    not null,
   primary key (USER_ID)
);

/*==============================================================*/
/* Table: VRBO_EVENT_LOG                                        */
/*==============================================================*/
create table if not exists VRBO_EVENT_LOG
(
   EVENT_ID                       int                            not null AUTO_INCREMENT,
   USER_ID                        int                            not null,
   EVENT_TYPE                     varchar(20)                    not null,
   EVENT_TEXT                     text                           not null,
   DATE_CREATED                   datetime                       not null,
   primary key (EVENT_ID)
);

/*==============================================================*/
/* Table: VRBO_LANGUAGE                                         */
/*==============================================================*/
create table if not exists VRBO_LANGUAGE
(
   LANGUAGE                       VARCHAR(2)                     not null,
   LANGUAGE_NAME                  VARCHAR(30) character set cp1251 collate cp1251_general_ci not null,
   primary key (LANGUAGE)
);

/*==============================================================*/
/* Table: VRBO_LOCATION                                         */
/*==============================================================*/
create table if not exists VRBO_LOCATION
(
   LOCATION_ID                    int                            not null AUTO_INCREMENT,
   PARENT_LOCATION_ID             int,
   primary key (LOCATION_ID)
);

/*==============================================================*/
/* Table: VRBO_LOCATION_INFO                                    */
/*==============================================================*/
create table if not exists VRBO_LOCATION_INFO
(
   LOCATION_INFO_ID               int                            not null AUTO_INCREMENT,
   LOCATION_NAME                  varchar(50) character set cp1251 collate cp1251_general_ci not null,
   LANGUAGE                       varchar(2)                     not null,
   LOCATION_ID                    int                            not null,
   primary key (LOCATION_INFO_ID)
);

/*==============================================================*/
/* Index: Index_16                                              */
/*==============================================================*/
create index Index_16 on VRBO_LOCATION_INFO
(
   LOCATION_ID
);

/*==============================================================*/
/* Table: VRBO_OWNER                                            */
/*==============================================================*/
create table if not exists VRBO_OWNER
(
   USER_ID                        int                            not null,
   PHONE                          varchar(15)                    not null,
   MOBILE_PHONE                   varchar(15),
   FIRST_NAME                     varchar(30)                    not null,
   MIDDLE_NAME                    varchar(30),
   LAST_NAME                      varchar(30)                    not null,
   ADDRESS                        varchar(200)                   not null,
   COMMENTS                       text                               null,
   primary key (USER_ID)
);

/*==============================================================*/
/* Table: VRBO_RENTAL_LISTING_REQUEST                           */
/*==============================================================*/
create table if not exists VRBO_RENTAL_LISTING_REQUEST
(
   REQUEST_ID                     int                            not null AUTO_INCREMENT,
   USER_ID                        int                            not null,
   LOCATION_ID                    int                                null,
   REQUEST_STATUS                 varchar(20)                    not null,
   DATE_CREATED                   datetime                       not null,
   RENTAL_NAME                    varchar(100)                   not null,
   RENTAL_ADDRESS                 varchar(100)                   not null,
   RENTAL_DESCRIPTION             text,
   BEDROOMS_NUMBER                int                            not null,
   BEDROOMS_ADDITIONAL            varchar(200)                       null,
   BATHS_NUMBER                   int                            not null,
   SLEEPING_PLACES_NUMBER         int                            not null,
   IS_PET_FRIENDLY                int                            not null,
   IS_NO_SMOKING                  int                            not null,
   AMENITIES                      varchar(1000),
   ACTIVITIES                     varchar(1000),
   RATES_DESCRIPTION              varchar(200)                       null,
   LOCATION_DESCRIPTION           varchar(200)                       null,
   primary key (REQUEST_ID)
);

/*==============================================================*/
/* Table: VRBO_RENTAL_LISTING_REQUEST_PHOTO                     */
/*==============================================================*/
create table if not exists VRBO_RENTAL_LISTING_REQUEST_PHOTO
(
   PHOTO_ID                       int                            not null AUTO_INCREMENT,
   REQUEST_ID                     int                            not null,
   FILE_NAME                      varchar(50)                    not null,
   primary key (PHOTO_ID)
);

/*==============================================================*/
/* Index: Index_7                                               */
/*==============================================================*/
create index Index_7 on VRBO_RENTAL_LISTING_REQUEST_PHOTO
(
   REQUEST_ID
);

/*==============================================================*/
/* Table: VRBO_RENTAL_LISTING_REQUEST_PHOTO_INFO                */
/*==============================================================*/
create table if not exists VRBO_RENTAL_LISTING_REQUEST_PHOTO_INFO
(
   PHOTO_INFO_ID                  int                            not null AUTO_INCREMENT,
   PHOTO_DESCRIPTION              varchar(50) character set cp1251 collate cp1251_general_ci not null,
   LANGUAGE                       varchar(2)                     not null,
   PHOTO_ID                       int                            not null,
   primary key (PHOTO_INFO_ID)
);

/*==============================================================*/
/* Table: VRBO_USER                                             */
/*==============================================================*/
create table if not exists VRBO_USER
(
   USER_ID                        int                            not null AUTO_INCREMENT,
   USER_LOGIN                     varchar(30)                    not null,
   USER_PASSWORD                  varchar(50)                    not null,
   USER_TYPE                      varchar(20)                    not null,
   DATE_CREATED                   datetime                       not null,
   USER_STATUS                    varchar(20)                    not null,
   EMAIL_ADDRESS                  varchar(50)                    not null,
   primary key (USER_ID)
);

/*==============================================================*/
/* Table: VRBO_RENTAL_RATE                                      */
/*==============================================================*/
create table if not exists VRBO_RENTAL_RATE
(
   RENTAL_RATE_ID                 int                            not null AUTO_INCREMENT,
   RENTAL_ID                      int                            not null,
   START_DATE                     date                           not null,
   END_DATE                       date                           not null,
   RENTAL_RATE                    int                            not null,
   primary key (RENTAL_RATE_ID)
);

