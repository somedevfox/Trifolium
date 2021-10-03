CREATE DATABASE IF NOT EXISTS `NeoRPGServer` DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci;
USE `NeoRPGServer`;


-- Creating USERS table
CREATE TABLE `users` (
    `userid` longtext NOT NULL,
    `username` longtext NOT NULL,
    `password` longtext NOT NULL,
    `user_role` int(2) NOT NULL,
    `banned` boolean NOT NULL,
    `friends` longtext NOT NULL,
    `guild_id` longtext NOT NULL,
);

CREATE TABLE `ips` (
    `userid` longtext NOT NULL,
    `ip` longtext NOT NULL,
    `banned` boolean NOT NULL,
);

CREATE TABLE `guilds` (
    `guildid` longtext NOT NULL,
    `leader_userid` longtext NOT NULL,
    `member_list` longtext NOT NULL
);

CREATE TABLE `mail` (
    `sender_id` longtext NOT NULL,
    `receiver_id` longtext NOT NULL,
    `date` longtext NOT NULL,
    `content` longtext NOT NULL,
    `unread` boolean NOT NULL
);