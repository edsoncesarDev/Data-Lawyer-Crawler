
CREATE DATABASE DataLawyerDB;

USE DataLawyerDB;

DROP TABLE IF EXISTS `Users`;

CREATE TABLE `Users` (
    `Id` INT AUTO_INCREMENT,
    `Name` VARCHAR(80) NOT NULL,
    `Email` VARCHAR(50) NOT NULL,
    `Password` varchar(100) NOT NULL,
    `CreateAt` DATETIME NOT NULL,
    PRIMARY KEY (`Id`)
)   ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `Processes`;

CREATE TABLE `Processes` (
    `Id` INT AUTO_INCREMENT,
    `ProcessNumber` VARCHAR(50) NOT NULL,
    `Situation` VARCHAR(50) NOT NULL,
    `Grade` VARCHAR(50) NOT NULL,
	`Topic` VARCHAR(50) NOT NULL,
	`From` VARCHAR(500) NOT NULL,
	`Distribution` VARCHAR(50) NOT NULL,
	`Rapporteur` VARCHAR(500) NOT NULL,
    `CreateAt` DATETIME NOT NULL,
    PRIMARY KEY (`Id`)
)   ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `Movements`;

CREATE TABLE `Movements` (
    `Id` INT AUTO_INCREMENT,
    `TheMovement` VARCHAR(1000) NOT NULL,
    `DateMovement` DATETIME NOT NULL,
    `ProcessId` INT NULL,
    `CreateAt` DATETIME NOT NULL,
    PRIMARY KEY (`Id`),
    FOREIGN KEY (`ProcessId`) REFERENCES Processes(`Id`)
)   ENGINE=InnoDB DEFAULT CHARSET=utf8;


DROP TABLE IF EXISTS `Areas`;

CREATE TABLE `Areas` (
    `Id` INT AUTO_INCREMENT,
    `Description` VARCHAR(50) NOT NULL,
    `ProcessId` INT NULL,
    `CreateAt` DATETIME NOT NULL,
    PRIMARY KEY (`Id`),
	FOREIGN KEY (`ProcessId`) REFERENCES Processes(`Id`)
)   ENGINE=InnoDB DEFAULT CHARSET=utf8;