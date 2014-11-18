ALTER TABLE `websitearticle` CHANGE COLUMN `Article` `ArticlePart1` LONGTEXT NOT NULL;
ALTER TABLE `websitearticle` ADD COLUMN `ArticlePart2` longtext NOT NULL AFTER `ArticlePart1`;
ALTER TABLE `websitearticle` ADD COLUMN `ArticlePart3` longtext NOT NULL AFTER `ArticlePart2`;

INSERT INTO `sysversion` (`Version`) VALUES
(25);