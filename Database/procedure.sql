drop database if exists taxi_game;
create database taxi_game;
use taxi_game;

-- Drop Procedure

drop procedure if exists create_taxi_game;
delimiter //

-- Create Procedure
create procedure create_taxi_game()
begin

drop table if exists user;
drop table if exists tile;
drop table if exists game;
drop table if exists inventory;
drop table if exists item;
drop table if exists game_user;
drop table if exists tile_item;


-- Create Tables

create table user
(
	userID int(10) primary key not null AUTO_INCREMENT,
	username varchar(20) not null unique,
	password varchar(30) not null,
	email varchar(50) not null unique,
	isAdmin boolean default false,
	isLocked bit,
	numLoginAttempts int(1),
	isOnline bit,
	score int(10)
);

create table tile
(
	tileID int(10) primary key not null auto_increment,
	column int(1),
	row int(1),
	homeTile int(1),
	dropOffTile int(1),
);

create table game
(
	gameID int(10) primary key not null auto_increment,
	tileID int(10) foreign key references tile(tileID),
);

create table item
(
	itemID int(10) primary key not null auto_increment,
	name varchar(30) null,
	description varchar(255) null,
	isNPC bit not null,
);

create table inventory
(
	inventoryID int(10) primary key not null auto_increment,
	itemID int(10) foreign key references tile(tileID),
);

create table game_user
(
	gameID int(10) foreign key references game(gameID),
	userID int(10) foreign key references user(userID),
	inventoryID(10) foreign key references inventory(inventoryID),
);

create table tile_item
(
	tileID int(10) foreign key references tile(tileID),
	itemID int(10) foreign key references item(itemID),
);

end //
delimiter ;

drop procedure if exists create_data()
delimiter ;

create procedure create_data()
begin
	INSERT INTO user (username, password, email, isAdmin, isLocked, numLoginAttempts, isOnline, score)
	VALUES 
	('JohnDoe', 'password123', 'johndoe@email.com', false, 0, 0, 0, 100),
	('JaneDoe', 'password456', 'janedoe@email.com', true, 0, 0, 1, 200),
	('BobSmith', 'password789', 'bobsmith@email.com', false, 1, 3, 0, 50);

	INSERT INTO tile (column, row, homeTile, dropOffTile )
	VALUES 
	(1, 1, 0, 0),
	(1, 2, 1, 0),
	(1, 3, 0, 0),
	(1, 4, 0, 1),
	(1, 5, 0, 0),
	(2, 1, 0, 0),
	(2, 2, 0, 0),
	(2, 3, 0, 0),
	(2, 4, 0, 0),
	(2, 5, 0, 0),
	(3, 1, 0, 0),
	(3, 2, 0, 0),
	(3, 3, 0, 0),
	(3, 4, 0, 0),
	(3, 5, 0, 0),
	(4, 1, 0, 0),
	(4, 2, 0, 0),
	(4, 3, 0, 0),
	(4, 4, 0, 0),
	(4, 5, 0, 0),
	(5, 1, 0, 0),
	(5, 2, 0, 0),
	(5, 3, 0, 0),
	(5, 4, 0, 0),
	(5, 5, 0, 0);

	INSERT INTO item (name, description, isNPC)
	VALUES 
	('Passenger','Drop off ' , 0 ),
	('Frog', 'Avoid', 1);
	
end // 
delimiter

call create_taxi_game();
call create_data();

-- Login Procedure

DELIMITER //
CREATE PROCEDURE Login(IN input_username VARCHAR(20), IN input_password VARCHAR(30))
BEGIN
    DECLARE found_password VARCHAR(30);
    DECLARE found_userID INT(10);
    DECLARE login_attempts INT(1);
    DECLARE locked BIT;

    SELECT userID, password, numLoginAttempts, isLocked INTO found_userID, found_password, login_attempts, locked 
    FROM user 
    WHERE username = input_username;

   -- Lock user out if login attempt exceeds 5
    IF found_password = input_password AND locked = 0 THEN
        UPDATE user 
        SET isOnline = 1, numLoginAttempts = 0
        WHERE userID = found_userID;
        SELECT 'Login Successful' AS message;
    ELSEIF locked = 1 THEN
        SELECT 'Account Locked' AS message;
    ELSE
        SET login_attempts = login_attempts + 1;
        UPDATE user 
        SET numLoginAttempts = login_attempts
        WHERE userID = found_userID;
        
        IF login_attempts >= 5 THEN
            UPDATE user 
            SET isLocked = 1
            WHERE userID = found_userID;
            
            SET @sql = CONCAT('CREATE EVENT unlock_user_', found_userID, '
                               ON SCHEDULE AT CURRENT_TIMESTAMP + INTERVAL 1 MINUTE
                               DO 
                               UPDATE user SET isLocked = 0, numLoginAttempts = 0 WHERE userID = ', found_userID, ';');
            PREPARE stmt FROM @sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
            
            SELECT 'Account Locked' AS message;
        ELSE
            SELECT 'Login Failed' AS message;
        END IF;
    END IF;
END //
DELIMITER ;


-- New User Procedure
-- Log Out Procedure
-- Get Active Players Procedure
-- Create Game Procedure
-- Get All Games Procedure
-- Join Game Procedure
-- Movement Procedure
-- Chat Procedure
-- Game End Procedure
-- Admin Edit User Procedure
-- Admin Add User Procedure
-- Admin Delete User Procedure
-- Lock User Procedure




