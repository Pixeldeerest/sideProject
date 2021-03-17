-- Switch to the system (aka master) database
USE master;
GO

-- Delete the RPGCharacterCreatorDB Database (IF EXISTS)
DROP DATABASE IF Exists RPGCharacterCreatorDB;
GO

-- Create a new RPGCharacterCreatorDB Database
CREATE DATABASE RPGCharacterCreatorDB;
GO

-- Switch to the RPGCharacterCreatorDB Database
USE RPGCharacterCreatorDB 
GO

--Create the tables
BEGIN TRANSACTION;

CREATE TABLE Class (
	ClassId integer identity NOT NULL,
	ClassName nvarchar(100) NOT NULL,
	ClassBonus varchar(100) NOT NULL,
	ClassWeapon varchar(100) NOT NULL,
	BasicAttack varchar(100) NOT NULL,
	CONSTRAINT PK_Class_ClassID PRIMARY KEY (ClassId)
);

CREATE TABLE PlayerStat (
	PlayerStatId integer identity NOT NULL,
	PlayerId integer NOT NULL,
	Strength integer NOT NULL,
	Dexterity integer NOT NULL,
	Constitution integer NOT NULL,
	Intelligence integer NOT NULL,
	Wisdom integer NOT NULL,
	Charisma integer NOT NULL,
	CONSTRAINT PK_PlayerStat_PlayerStatId PRIMARY KEY (PlayerStatId)
);

CREATE TABLE Player (
	PlayerId integer identity NOT NULL,
	PlayerName nvarchar(75) NOT NULL,
	PlayerRace nvarchar(75) NOT NULL,
	ClassId integer NOT NULL,
	PlayerStatId integer NOT NULL,
	Height integer NOT NULL,
	Age integer NOT NULL,
	Pronouns varchar(100) null,
	Personality varchar(150) null,
	HairColor varchar(75) not null,
	Eyecolor varchar(75) not null,
	UniqueFeatures varchar(150) null,
	CONSTRAINT PK_Player_PlayerId PRIMARY KEY (PlayerId),
	CONSTRAINT FK_Player_ClassId FOREIGN KEY (ClassId) REFERENCES Class(ClassId),
	CONSTRAINT FK_Player_PlayerStatId FOREIGN KEY (PlayerStatId) REFERENCES PlayerStat(PlayerStatId)
);

-- Fill the tables
Insert into Class (ClassName, ClassBonus, ClassWeapon, BasicAttack) VALUES ('Rouge', '+2 Dexterity', 'Dagger', 'Sneak Attack');
Insert into Class (ClassName, ClassBonus, ClassWeapon, BasicAttack) VALUES ('Barbarian', '+2 Strength', 'Heavy Mace', 'Rage');
Insert into Class (ClassName, ClassBonus, ClassWeapon, BasicAttack) VALUES ('Cleric', '+2 Wisdom', 'Light Hammer', 'Healing Hands');
Insert into Class (ClassName, ClassBonus, ClassWeapon, BasicAttack) VALUES ('Bard', '+2 Charisma', 'Instrument', 'Bardic Performance');
Insert into Class (ClassName, ClassBonus, ClassWeapon, BasicAttack) VALUES ('Wizard', '+2 Intelligence', 'Spell-Casting Implement', 'Magic Missle');
Insert into Class (ClassName, ClassBonus, ClassWeapon, BasicAttack) VALUES ('Paladin', '+2 Constitution', 'Sword and Shield', 'Detect Evil');


INSERT INTO PlayerStat (PlayerId, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma) VALUES (1, 14, 10, 16, 10, 12, 18); -- Angelic-Guide Aasimar Paladin
INSERT INTO PlayerStat (PlayerId, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma) VALUES (2, 10, 14, 12, 17, 14, 8); -- Dark Elf Wizard
INSERT INTO PlayerStat (PlayerId, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma) VALUES (3, 11, 14, 12, 10, 13, 14); -- Tiefling Bard
INSERT INTO PlayerStat (PlayerId, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma) VALUES (4, 14, 12, 13, 12, 16, 12); -- Human Cleric
INSERT INTO PlayerStat (PlayerId, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma) VALUES (5, 14, 13, 15, 8, 12, 10); -- Half-Orc Barbarian
INSERT INTO PlayerStat (PlayerId, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma) VALUES (6, 10, 17, 14, 14, 12, 8); -- Hafling Rouge

INSERT INTO Player (PlayerName, PlayerRace, ClassId, PlayerStatId, Height, Age, Pronouns, Personality, HairColor, Eyecolor, UniqueFeatures)
	VALUES ('Raegln',
	'Angelic-Guide Aasimar',
	(Select ClassId from Class Where ClassName = 'Paladin'),
	(Select PlayerStatId from PlayerStat Where PlayerId = 1),
	140,
	75,
	'They/Them',
	'Jovial',
	'Reddish-Brunette',
	'Striking Emerald Green',
	'Birth mark the size of an orange on right shoulder');
INSERT INTO Player (PlayerName, PlayerRace, ClassId, PlayerStatId, Height, Age, Pronouns, Personality, HairColor, Eyecolor, UniqueFeatures)
	VALUES ('Tragor',
	'Dark Elf',
	(Select ClassId from Class Where ClassName = 'Wizard'),
	(Select PlayerStatId from PlayerStat Where PlayerId = 2),
	165,
	334,
	'He/Him',
	'Snobbish',
	'Tar Black',
	'Oil-slick Reflect',
	'White freckles cover both shoulders and the bridge of his nose');
INSERT INTO Player (PlayerName, PlayerRace, ClassId, PlayerStatId, Height, Age, Pronouns, Personality, HairColor, Eyecolor, UniqueFeatures)
	VALUES ('Melody',
	'Tiefling',
	(Select ClassId from Class Where ClassName = 'Bard'),
	(Select PlayerStatId from PlayerStat Where PlayerId = 3),
	190,
	35,
	'She/Her',
	'Guarded',
	'Soft Smoke Grey',
	'Ash',
	'Distinct high cheek bones and attentive, piercing eyes');
INSERT INTO Player (PlayerName, PlayerRace, ClassId, PlayerStatId, Height, Age, Pronouns, Personality, HairColor, Eyecolor, UniqueFeatures)
	VALUES ('Maverick',
	'Human',
	(Select ClassId from Class Where ClassName = 'Cleric'),
	(Select PlayerStatId from PlayerStat Where PlayerId = 4),
	170,
	40,
	'He/Him',
	'Loquacious',
	'Strawberry Blonde',
	'Whiskey Brown',
	'Distracting mole-speck above his left eyebrow');
INSERT INTO Player (PlayerName, PlayerRace, ClassId, PlayerStatId, Height, Age, Pronouns, Personality, HairColor, Eyecolor, UniqueFeatures)
	VALUES ('Sinarel',
	'Half-Orc',
	(Select ClassId from Class Where ClassName = 'Barbarian'),
	(Select PlayerStatId from PlayerStat Where PlayerId = 5),
	201,
	16,
	'They/Them',
	'Charismatic',
	'Ash Brown',
	'Hazel Green',
	'They are mute, and therefore have developed a personality based mostly on gestures');
INSERT INTO Player (PlayerName, PlayerRace, ClassId, PlayerStatId, Height, Age, Pronouns, Personality, HairColor, Eyecolor, UniqueFeatures)
	VALUES ('Leyola',
	'Halfling',
	(Select ClassId from Class Where ClassName = 'Rouge'),
	(Select PlayerStatId from PlayerStat Where PlayerId = 6),
	96,
	26,
	'She/Her',
	'Empathetic',
	'Fresh-cut Ginger',
	'Sea-glass Blue',
	'Her vitiligo skin-condition creates a contrast between pale white and deep mahogany');


ALTER TABLE PlayerStat ADD CONSTRAINT FK_PlayerStat_PlayerId FOREIGN KEY (PlayerId) REFERENCES Player(PlayerId);

COMMIT TRANSACTION;