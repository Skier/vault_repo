-- Languages
insert into VRBO_LANGUAGE (LANGUAGE, LANGUAGE_NAME)
values ('EN', 'English');

insert into VRBO_LANGUAGE (LANGUAGE, LANGUAGE_NAME)
values ('RU', 'Русский');

-- Default users (admin and test owner)
insert into VRBO_USER (USER_ID, USER_LOGIN, USER_PASSWORD, USER_TYPE, DATE_CREATED, USER_STATUS, EMAIL_ADDRESS)
values (1, 'admin', 'gfhjkm', 'CompanyUser', now(), 'Active', 'vitaly@dekasoft.com.ua');

insert into VRBO_USER (USER_ID, USER_LOGIN, USER_PASSWORD, USER_TYPE, DATE_CREATED, USER_STATUS, EMAIL_ADDRESS)
values (2, 'owner', 'gfhjkm', 'Owner', now(), 'Active', 'vitaly@dekasoft.com.ua');

-- Default owners
insert into VRBO_OWNER (USER_ID, PHONE, MOBILE_PHONE, FIRST_NAME, MIDDLE_NAME, LAST_NAME, ADDRESS, COMMENTS)
values (2, '760374602', '3806749623948', 'User', 'Userovich', 'Userson', 'Abcd st., 233d, Dallas, Texas, USA, 23445235', null);

-- Locations (continents)
insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (1, null); -- Europe

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (2, null); -- North America

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (3, null); -- South America

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (4, null); -- Asia

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (5, null); -- Africa

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (6, null); -- Australia

-- Locations (countries)
insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (7, 1); -- United Kingdom

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (8, 1); -- The Netherlands

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (9, 1); -- Greece

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (10, 1); -- Spain

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (11, 1); -- Italy

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (12, 1); -- Ukraine

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (13, 1); -- Germany

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (14, 1); -- Switzerland

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (15, 1); -- Hungary

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (16, 1); -- Poland

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (17, 1); -- Russia

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (18, 2); -- USA

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (19, 2); -- Canada

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (20, 2); -- Greenland

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (21, 12); -- Kievskaya oblast

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (22, 12); -- Khmelnitskaya oblast

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (23, 21); -- Kiev

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (24, 22); -- Khmelnitsky

insert into VRBO_LOCATION (LOCATION_ID, PARENT_LOCATION_ID)
values (25, 22); -- Kamenets-Podolsky

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (1, 'Europe', 'EN', 1);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (2, 'Европа', 'RU', 1);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (3, 'North America', 'EN', 2);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (4, 'Северная Америка', 'RU', 2);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (5, 'South America', 'EN', 3);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (6, 'Южная Америка', 'RU', 3);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (7, 'Asia', 'EN', 4);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (8, 'Азия', 'RU', 4);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (9, 'Africa', 'EN', 5);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (10, 'Африка', 'RU', 5);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (11, 'Australia', 'EN', 6);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (12, 'Австралия', 'RU', 6);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (13, 'United Kingdom', 'EN', 7);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (14, 'Великобритания', 'RU', 7);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (15, 'The Netherlands', 'EN', 8);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (16, 'Нидерланды', 'RU', 8);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (17, 'Greece', 'EN', 9);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (18, 'Греция', 'RU', 9);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (19, 'Spain', 'EN', 10);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (20, 'Испания', 'RU', 10);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (21, 'Italy', 'EN', 11);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (22, 'Италия', 'RU', 11);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (23, 'Ukraine', 'EN', 12);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (24, 'Украина', 'RU', 12);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (25, 'Germany', 'EN', 13);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (26, 'Германия', 'RU', 13);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (27, 'Switzerland', 'EN', 14);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (28, 'Швейцария', 'RU', 14);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (29, 'Hungary', 'EN', 15);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (30, 'Венгрия', 'RU', 15);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (31, 'Poland', 'EN', 16);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (32, 'Польша', 'RU', 16);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (33, 'Russia', 'EN', 17);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (34, 'Россия', 'RU', 17);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (35, 'USA', 'EN', 18);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (36, 'США', 'RU', 18);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (37, 'Canada', 'EN', 19);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (38, 'Канада', 'RU', 19);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (39, 'Greenland', 'EN', 20);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (40, 'Гренландия', 'RU', 20);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (41, 'Kievskaya oblast', 'EN', 21);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (42, 'Киевская область', 'RU', 21);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (43, 'Khmelnitskaya oblast', 'EN', 22);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (44, 'Хмельницкая область', 'RU', 22);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (45, 'Kiev', 'EN', 23);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (46, 'Киев', 'RU', 23);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (47, 'Khmelnitsky', 'EN', 24);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (48, 'Хмельницкий', 'RU', 24);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (49, 'Kamenets-Podolsky', 'EN', 25);

insert into VRBO_LOCATION_INFO (LOCATION_INFO_ID, LOCATION_NAME, LANGUAGE, LOCATION_ID)
values (50, 'Каменец-Подольский', 'RU', 25);

-- Amenities

insert into VRBO_AMENITY (AMENITY_ID)
values (1); -- Fireplace

insert into VRBO_AMENITY (AMENITY_ID)
values (2); -- Phone

insert into VRBO_AMENITY (AMENITY_ID)
values (3); -- Air Conditioneer

insert into VRBO_AMENITY (AMENITY_ID)
values (4); -- Stereo

insert into VRBO_AMENITY (AMENITY_ID)
values (5); -- TV

insert into VRBO_AMENITY (AMENITY_ID)
values (6); -- Cable TV

insert into VRBO_AMENITY (AMENITY_ID)
values (7); -- Satellite TV

insert into VRBO_AMENITY (AMENITY_ID)
values (8); -- VCR

insert into VRBO_AMENITY (AMENITY_ID)
values (9); -- Jetted Tub in Bath

insert into VRBO_AMENITY (AMENITY_ID)
values (10); -- Private Hot Tub

insert into VRBO_AMENITY (AMENITY_ID)
values (11); -- Hot Tub (shared)

insert into VRBO_AMENITY (AMENITY_ID)
values (12); -- CD Player

insert into VRBO_AMENITY (AMENITY_ID)
values (13); -- Private Pool

insert into VRBO_AMENITY (AMENITY_ID)
values (14); -- Pool (shared)

insert into VRBO_AMENITY (AMENITY_ID)
values (15); -- Sauna

insert into VRBO_AMENITY (AMENITY_ID)
values (16); -- Microwave

insert into VRBO_AMENITY (AMENITY_ID)
values (17); -- Dishwasher

insert into VRBO_AMENITY (AMENITY_ID)
values (18); -- Full Kitchen

insert into VRBO_AMENITY (AMENITY_ID)
values (19); -- Shared use of Kitchen

insert into VRBO_AMENITY (AMENITY_ID)
values (20); -- Refrigerator

insert into VRBO_AMENITY (AMENITY_ID)
values (21); -- Cooking utensils provided

insert into VRBO_AMENITY (AMENITY_ID)
values (22); -- Ice Maker

insert into VRBO_AMENITY (AMENITY_ID)
values (23); -- Covered Parking

insert into VRBO_AMENITY (AMENITY_ID)
values (24); -- Linens provided

insert into VRBO_AMENITY (AMENITY_ID)
values (25); -- Washer

insert into VRBO_AMENITY (AMENITY_ID)
values (26); -- Dryer

insert into VRBO_AMENITY (AMENITY_ID)
values (27); -- Gas Grill (BBQ)

insert into VRBO_AMENITY (AMENITY_ID)
values (28); -- Charcoal Grill (BBQ)

insert into VRBO_AMENITY (AMENITY_ID)
values (29); -- Garage

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (1, 'Fireplace', 'EN', 1);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (2, 'Phone', 'EN', 2);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (3, 'Air Conditioneer', 'EN', 3);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (4, 'Stereo', 'EN', 4);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (5, 'TV', 'EN', 5);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (6, 'Cable TV', 'EN', 6);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (7, 'Satellite TV', 'EN', 7);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (8, 'VCR', 'EN', 8);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (9, 'Jetted Tub in Bath', 'EN', 9);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (10, 'Private Hot Tub', 'EN', 10);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (11, 'Hot Tub (shared)', 'EN', 11);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (12, 'CD Player', 'EN', 12);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (13, 'Private Pool', 'EN', 13);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (14, 'Pool (shared)', 'EN', 14);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (15, 'Sauna', 'EN', 15);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (16, 'Microwave', 'EN', 16);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (17, 'Dishwasher', 'EN', 17);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (18, 'Full Kitchen', 'EN', 18);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (19, 'Shared Use of Kitchen', 'EN', 19);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (20, 'Refrigerator', 'EN', 20);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (21, 'Cooking Utensils Provided', 'EN', 21);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (22, 'Ice Maker', 'EN', 22);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (23, 'Covered Parking', 'EN', 23);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (24, 'Linens Provided', 'EN', 24);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (25, 'Washer', 'EN', 25);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (26, 'Dryer', 'EN', 26);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (27, 'Gas Grill (BBQ)', 'EN', 27);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (28, 'Charcoal Grill (BBQ)', 'EN', 28);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (29, 'Garage', 'EN', 29);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (30, 'Камин', 'RU', 1);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (31, 'Телефон', 'RU', 2);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (32, 'Кондиционер', 'RU', 3);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (33, 'Стереосистема', 'RU', 4);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (34, 'Телевизор', 'RU', 5);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (35, 'Кабельное ТВ', 'RU', 6);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (36, 'Спутниковое ТВ', 'RU', 7);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (37, 'Видеомагнитофон', 'RU', 8);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (38, 'Jetted Tub in Bath', 'RU', 9);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (39, 'Private Hot Tub', 'RU', 10);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (40, 'Hot Tub (shared)', 'RU', 11);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (41, 'CD проигрыватель', 'RU', 12);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (42, 'Private Pool', 'RU', 13);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (43, 'Pool (shared)', 'RU', 14);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (44, 'Сауна', 'RU', 15);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (45, 'Микроволновка', 'RU', 16);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (46, 'Посудомоечная машина', 'RU', 17);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (47, 'Full Kitchen', 'RU', 18);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (48, 'Shared Use of Kitchen', 'RU', 19);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (49, 'Холодильник', 'RU', 20);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (50, 'Кухонная утварь предоставляется', 'RU', 21);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (51, 'Льдогенератор', 'RU', 22);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (52, 'Covered Parking', 'RU', 23);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (53, 'Постельное белье предоставляется', 'RU', 24);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (54, 'Washer', 'RU', 25);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (55, 'Dryer', 'RU', 26);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (56, 'Gas Grill (BBQ)', 'RU', 27);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (57, 'Charcoal Grill (BBQ)', 'RU', 28);

insert into VRBO_AMENITY_INFO (AMENITY_INFO_ID, AMENITY_NAME, LANGUAGE, AMENITY_ID)
values (58, 'Гараж', 'RU', 29);

-- Activities
insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (1); -- Hiking

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (2); -- Rock Climbing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (3); -- Spelunking

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (4); -- Biking

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (5); -- Golf

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (6); -- Tennis

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (7); -- Racquetball

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (8); -- Basketball

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (9); -- Fitness Center

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (10); -- Gym

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (11); -- Shuffleboard

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (12); -- Horseshoes

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (13); -- Miniature Golf

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (14); -- Amusement Parks

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (15); -- Fishing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (16); -- Hunting

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (17); -- Wildlife Viewing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (18); -- Horseback Riding

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (19); -- Shopping

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (20); -- Restaurants

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (21); -- Live Theater

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (22); -- Cinemas

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (23); -- Museums

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (24); -- Sightseeing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (25); -- Swimming

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (26); -- Snorkeling / Diving

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (27); -- Boating

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (28); -- Waterskiing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (29); -- Sailing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (30); -- Surfing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (31); -- Windsurfing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (32); -- Parasailing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (33); -- Jet Skiing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (34); -- Shelling

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (35); -- Rafting

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (36); -- Downhill Skiing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (37); -- Cross Country Skiing

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (38); -- Snowmobiling

insert into VRBO_ACTIVITY (ACTIVITY_ID)
values (39); -- Sledding

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (1, 'Hiking', 'EN', 1);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (2, 'Rock Climbing', 'EN', 2);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (3, 'Spelunking', 'EN', 3);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (4, 'Biking', 'EN', 4);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (5, 'Golf', 'EN', 5);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (6, 'Tennis', 'EN', 6);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (7, 'Racquetball', 'EN', 7);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (8, 'Basketball', 'EN', 8);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (9, 'Fitness Center', 'EN', 9);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (10, 'Gym', 'EN', 10);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (11, 'Shuffleboard', 'EN', 11);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (12, 'Horseshoes', 'EN', 12);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (13, 'Miniature Golf', 'EN', 13);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (14, 'Amusement Parks', 'EN', 14);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (15, 'Fishing', 'EN', 15);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (16, 'Hunting', 'EN', 16);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (17, 'Wildlife Viewing', 'EN', 17);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (18, 'Horseback Riding', 'EN', 18);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (19, 'Shopping', 'EN', 19);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (20, 'Restaurants', 'EN', 20);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (21, 'Live Theater', 'EN', 21);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (22, 'Cinemas', 'EN', 22);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (23, 'Museums', 'EN', 23);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (24, 'Sightseeing', 'EN', 24);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (25, 'Swimming', 'EN', 25);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (26, 'Snorkeling / Diving', 'EN', 26);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (27, 'Boating', 'EN', 27);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (28, 'Waterskiing', 'EN', 28);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (29, 'Sailing', 'EN', 29);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (30, 'Surfing', 'EN', 30);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (31, 'Windsurfing', 'EN', 31);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (32, 'Parasailing', 'EN', 32);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (33, 'Jet Skiing', 'EN', 33);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (34, 'Shelling', 'EN', 34);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (35, 'Rafting', 'EN', 35);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (36, 'Downhill Skiing', 'EN', 36);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (37, 'Cross Country Skiing', 'EN', 37);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (38, 'Snowmobiling', 'EN', 38);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (39, 'Sledding', 'EN', 39);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (40, 'Туризм', 'RU', 1);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (41, 'Скалолазание', 'RU', 2);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (42, 'Исследование пещер', 'RU', 3);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (43, 'Велосипедный спорт', 'RU', 4);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (44, 'Гольф', 'RU', 5);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (45, 'Теннис', 'RU', 6);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (46, 'Racquetball', 'RU', 7);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (47, 'Баскетбол', 'RU', 8);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (48, 'Фитнес-центр', 'RU', 9);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (49, 'Гимнастический зал', 'RU', 10);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (50, 'Shuffleboard', 'RU', 11);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (51, 'Horseshoes', 'RU', 12);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (52, 'Miniature Golf', 'RU', 13);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (53, 'Парк с аттракционами', 'RU', 14);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (54, 'Рыбная ловля', 'RU', 15);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (55, 'Охота', 'RU', 16);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (56, 'Живая природа', 'RU', 17);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (57, 'Езда верхом', 'RU', 18);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (58, 'Шоппинг', 'RU', 19);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (59, 'Рестораны', 'RU', 20);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (60, 'Театры', 'RU', 21);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (61, 'Кино', 'RU', 22);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (62, 'Музеи', 'RU', 23);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (63, 'Осмотр достопримечательностей', 'RU', 24);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (64, 'Плавание', 'RU', 25);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (65, 'Водолазный спорт', 'RU', 26);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (66, 'Лодочный спорт', 'RU', 27);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (67, 'Воднолыжный спорт', 'RU', 28);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (68, 'Мореплавание', 'RU', 29);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (69, 'Серфинг', 'RU', 30);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (70, 'Виндсерфинг', 'RU', 31);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (71, 'Parasailing', 'RU', 32);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (72, 'Jet Skiing', 'RU', 33);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (73, 'Shelling', 'RU', 34);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (74, 'Rafting', 'RU', 35);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (75, 'Downhill Skiing', 'RU', 36);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (76, 'Cross Country Skiing', 'RU', 37);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (77, 'Snowmobiling', 'RU', 38);

insert into VRBO_ACTIVITY_INFO (ACTIVITY_INFO_ID, ACTIVITY_NAME, LANGUAGE, ACTIVITY_ID)
values (78, 'Sledding', 'RU', 39);


