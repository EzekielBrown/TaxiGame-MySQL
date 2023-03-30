drop database if exists taxi_game;
create database taxi_game;
use taxi_game;

-- Create Tables

create table if not exists player
(
	playerID int(10) primary key not null AUTO_INCREMENT,
	username varchar(20) not null unique,
	password varchar(30) not null,
	email varchar(50) not null unique,
	isAdmin boolean default false,
	isLocked bit,
	numLoginAttempts int(1),
	isOnline bit,
	highscore int(10)
);

create table if not exists game
(
	gameID int(10) primary key not null auto_increment,
	tileID int(10),
);

create table if not exists tile
(
	tileID int(10) primary key not null auto_increment,
	column int(1),
	row int(1),
	homeTile  int(1),
);

create table if not exists inventory
(
	inventoryID int(10) primary key not null auto_increment,
	itemID int(10),
);

create table if not exists item
(
	itemID int(10) primary key not null auto_increment,
	name varchar(30) null,
	description varchar(255) null,
	isNPC bit not null,
);

create table if not exists game_player
(
	gameID int(10),
	userID int(10),
	inventoryID(10),
);

create table if not exists tile_item
(
	tileID int(10) foreign key references tile(tileID),
	itemID int(10) foreign key references item(itemID),
);



