DROP DATABASE IF EXISTS taxi_game;
CREATE DATABASE taxi_game;
USE taxi_game;

-- Drop Procedure

DROP PROCEDURE IF EXISTS Create_taxi_game;
DELIMITER //

-- Create Procedure
CREATE PROCEDURE Create_taxi_game()
BEGIN

DROP TABLE IF EXISTS user;
DROP TABLE IF EXISTS tile;
DROP TABLE IF EXISTS game;
DROP TABLE IF EXISTS inventory;
DROP TABLE IF EXISTS item;
DROP TABLE IF EXISTS game_user;
DROP TABLE IF EXISTS tile_item;


-- Create Tables

CREATE TABLE user
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

CREATE TABLE tile
(
	tileID int(10) primary key not null auto_increment,
	column int(1),
	row int(1),
	homeTile int(1),
	DROPOffTile int(1),
);

CREATE TABLE game
(
	gameID int(10) primary key not null auto_increment,
	tileID int(10) foreign key references tile(tileID),
);

CREATE TABLE item
(
	itemID int(10) primary key not null auto_increment,
	name varchar(30) null,
	description varchar(255) null,
	isNPC bit not null,
);

CREATE TABLE inventory
(
	inventoryID int(10) primary key not null auto_increment,
	itemID int(10) foreign key references tile(tileID),
);

CREATE TABLE game_user
(
	gameID int(10) foreign key references game(gameID),
	userID int(10) foreign key references user(userID),
	inventoryID(10) foreign key references inventory(inventoryID),
);

CREATE TABLE tile_item
(
	tileID int(10) foreign key references tile(tileID),
	itemID int(10) foreign key references item(itemID),
);

END //
DELIMITER ;

DROP PROCEDURE IF EXISTS CREATE_data()
DELIMITER ;

CREATE PROCEDURE CREATE_data()
BEGIN
	INSERT INTO user (username, password, email, isAdmin, isLocked, numLoginAttempts, isOnline, score)
	VALUES 
	('JohnDoe', 'password123', 'johndoe@email.com', false, 0, 0, 0, 100),
	('JaneDoe', 'password456', 'janedoe@email.com', true, 0, 0, 1, 200),
	('BobSmith', 'password789', 'bobsmith@email.com', false, 1, 3, 0, 50);

	INSERT INTO tile (column, row, homeTile, DropOffTile )
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
	
END // 
DELIMITER

call CREATE_taxi_game();
call CREATE_data();

-- Login Procedure

DROP PROCEDURE IF EXISTS Log_In;
DELIMITER //

DELIMITER //
CREATE PROCEDURE Log_In(IN input_username VARCHAR(20), IN input_password VARCHAR(30))
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

DROP PROCEDURE IF EXISTS New_User;
DELIMITER //

CREATE PROCEDURE New_User(in input_username VARCHAR(20), in input_password VARCHAR(30), in input_email varchar(50))
BEGIN
	declare username_exists int(1)
	declare email_exists int(1)
	
    SELECT COUNT(*) INTO username_exists
    FROM user
    WHERE username = input_username;

    SELECT COUNT(*) INTO email_exists
    FROM user
    WHERE email = input_email;

   -- Checks to see if username or email exists
    IF username_exists = 0 AND email_exists = 0 THEN
        INSERT INTO user (username, password, email, isAdmin, isLocked, numLoginAttempts, isOnline, score)
        VALUES (input_username, input_password, input_email, false, 0, 0, 0, 0);
        SELECT 'User added successfully' AS message;
    ELSEIF username_exists > 0 THEN
        SELECT 'Username already exists' AS message;
    ELSE 
        SELECT 'Email already exists' AS message;
    END IF;
END //
DELIMITER ;


-- Log Out Procedure

DROP PROCEDURE IF EXISTS Log_Out;
DELIMITER //

CREATE PROCEDURE Log_Out()
BEGIN
	
END
DELIMITER //

-- Get Active Players Procedure

DROP PROCEDURE IF EXISTS Active_User_List;
DELIMITER //

CREATE PROCEDURE Active_User_List()
BEGIN
	
END
delimter //

-- Create Game Procedure

DROP PROCEDURE IF EXISTS Create_Game;
DELIMITER //

CREATE PROCEDURE Create_Game()
BEGIN
	
END
delimter //

-- Get All Games Procedure

DROP PROCEDURE IF EXISTS Game_List;
DELIMITER //

CREATE PROCEDURE Game_List()
BEGIN
	
END
delimter //

-- Join Game Procedure

DROP PROCEDURE IF EXISTS Join_Game;
DELIMITER //

CREATE PROCEDURE Join_Game()
BEGIN
	
END
delimter //

-- Movement Procedure

DROP PROCEDURE IF EXISTS User_Movement;
DELIMITER //

CREATE PROCEDURE User_Movement()
BEGIN
	
END
delimter //

-- Chat Procedure

DROP PROCEDURE IF EXISTS Chat_Message;
DELIMITER //

CREATE PROCEDURE Chat_Message()
BEGIN
	
END
delimter //

-- Game End Procedure

DROP PROCEDURE IF EXISTS Game_End;
DELIMITER //

CREATE PROCEDURE Game_End()
BEGIN
	
END
delimter //

-- Admin Edit User Procedure

DROP PROCEDURE IF EXISTS Admin_Edit_User;
DELIMITER //

CREATE PROCEDURE Admin_Edit_User()
BEGIN
	
END
delimter //

-- Admin Add User Procedure

DROP PROCEDURE IF EXISTS Admin_New_User;
DELIMITER //

CREATE PROCEDURE Admin_New_User()
BEGIN
	
END
delimter //

-- Admin Delete User Procedure

DROP PROCEDURE IF EXISTS Admin_Delete_User;
DELIMITER //

CREATE PROCEDURE Admin_Delete_User()
BEGIN
	
END
DELIMITER //






