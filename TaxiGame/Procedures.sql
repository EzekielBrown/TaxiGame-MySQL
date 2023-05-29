DROP DATABASE IF EXISTS taxi_game;
CREATE DATABASE taxi_game;
USE taxi_game;

-- Drop Procedure

DROP PROCEDURE IF EXISTS Create_Taxi_Game;
DELIMITER //

-- Create Procedure
CREATE PROCEDURE Create_Taxi_Game()
BEGIN

DROP TABLE IF EXISTS tblUser;
DROP TABLE IF EXISTS tblTile;
DROP TABLE IF EXISTS tblGame;
DROP TABLE IF EXISTS tblInventory;
DROP TABLE IF EXISTS tblItem;


-- Create Tables

CREATE TABLE tblUser (
    userID int(10) primary key not null AUTO_INCREMENT,
    username varchar(20) not null unique,
    `password` varchar(30) not null,
    email varchar(50) not null unique,
    isAdmin boolean default false,
    isLocked bit default 0,
    numLoginAttempts int(1) default 0,
    isOnline bit default 0,
    score int(10) default 0
);


CREATE TABLE tblItem
(
	itemID int(10) primary key not null auto_increment,
	name varchar(30) null,
	description varchar(255) null,
	isNPC bit not null,
	value int(10) null
);

CREATE TABLE tblTile
(
	tileID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
	`column` INT NOT NULL,
	`row` INT NOT NULL,
	homeTile TINYINT(1),
	DropOffTile TINYINT(1),
	itemID INT
);


CREATE TABLE tblGame
(
	gameID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
	tileID INT,
	userID INT
);



CREATE TABLE tblInventory
(
	inventoryID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
	itemID INT,
	userID INT
);


CREATE TABLE tblChat
(
	chatID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
	userID INT,
	message VARCHAR(255)
);

-- Foreign Keys

alter table tblTile
add constraint fk_item_tile
FOREIGN KEY (itemID) REFERENCES tblItem(itemID);

alter table tblGame
add constraint fk_user_game
FOREIGN KEY (userID) REFERENCES tblUser(userID);

alter table tblGame
add constraint fk_tile_game
FOREIGN KEY (tileID) REFERENCES tblTile(tileID);

alter table tblInventory
add constraint fk_item_inventory
FOREIGN KEY (itemID) REFERENCES tblItem(itemID);

alter table tblInventory
add constraint fk_user_inventory
FOREIGN KEY (userID) REFERENCES tblUser(userID);

alter table tblChat
add constraint fk_user_chat
FOREIGN KEY (userID) REFERENCES tblUser(userID);

END //
DELIMITER ;

DROP PROCEDURE IF EXISTS Create_Data()

DELIMITER //
CREATE PROCEDURE Create_Data()
BEGIN
	INSERT INTO tblUser (username, password, email, isAdmin, isLocked, numLoginAttempts, isOnline, score)
	values
	('z', '', 'z', true, 0, 0, 0, 200),
	('admin', 'admin', 'admin@admin.com', true, 0, 0, 0, 100),
	('Online', 'online', 'online@online.com', false, 0, 0, 1, 200),
	('locked', 'locked', 'locked@locked.com', false, 1, 3, 0, 50),
	('delete', 'delete', 'delete@delete.com', false, 0, 0, 0, 50);

	INSERT INTO tblTile (`column`, `row`, homeTile, DropOffTile )
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

	INSERT INTO tblItem (name, description, isNPC, value)
	VALUES 
	('Passenger','Drop off ' , 0, 10),
	('Frog', 'Avoid', 1, -10);
	
END //
DELIMITER ;

call Create_Taxi_Game();
call Create_Data();

-- Login Procedure

DROP PROCEDURE IF EXISTS Log_In;

DELIMITER //
CREATE PROCEDURE Log_In(IN pUsername VARCHAR(20), IN pPassword VARCHAR(30))
BEGIN
    DECLARE pPassword VARCHAR(30);
    DECLARE pUserID INT(10);
    DECLARE login_attempts INT(1);
    DECLARE locked BIT;

    SELECT userID, password, numLoginAttempts, isLocked INTO pUserID, pPassword, login_attempts, locked 
    FROM tblUser 
    WHERE username = pUsername;

   -- Lock user out if login attempt exceeds 5
    IF pPassword = pPassword AND locked = 0 THEN
        UPDATE tblUser 
        SET isOnline = 1, numLoginAttempts = 0
        WHERE userID = pUserID;
        SELECT 'Login Successful' AS message;
    ELSEIF locked = 1 THEN
        SELECT 'Account Locked' AS message;
    ELSE
        SET login_attempts = login_attempts + 1;
        UPDATE tblUser 
        SET numLoginAttempts = login_attempts
        WHERE userID = pUserID;
        
        IF login_attempts >= 5 THEN
            UPDATE tblUser 
            SET isLocked = 1
            WHERE userID = pUserID;
                      
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
CREATE PROCEDURE New_User(in pUsername VARCHAR(20), in pPassword VARCHAR(30), in pEmail varchar(50))
BEGIN
  If Exists (Select * 
     From tblUser
     Where username = pUsername) 
     Then
		Begin
			Select 'User Exists' As Message;
		End;
	Else
		Insert Into tblUser(username, password ,email)
        Values
			(pUsername,pPassword,pEmail);
            Select 'Login Success' As Message;
	End If;      
End //
DELIMITER ;


-- Log Out Procedure

DROP PROCEDURE IF EXISTS Log_Out;

DELIMITER //
CREATE PROCEDURE Log_Out(IN pUsername VARCHAR(20))
BEGIN
    IF EXISTS (
        SELECT *
        FROM tblUser
        WHERE username = pUsername
    ) THEN
        UPDATE tblUser
        SET isOnline = 0
        WHERE username = pUsername;
    END IF;
    
    SELECT 'Logout successful' AS Message;
end //

DELIMITER ;


-- Get Active Players Procedure

DROP PROCEDURE IF EXISTS Active_User_List;
DELIMITER //

CREATE PROCEDURE Active_User_List()
BEGIN
    SELECT userID, username
    FROM tblUser
    WHERE isOnline = 1 AND isLocked = 0;
END //

DELIMITER ;

-- Create Game Procedure

DROP PROCEDURE IF EXISTS Create_Game;
DELIMITER //

CREATE PROCEDURE Create_Game(IN pUsername VARCHAR(45), IN pXCoord INT(10), IN pYCoord INT(10))
BEGIN
	DECLARE pUserID INT(10);
	DECLARE pTileID INT(10);

	SELECT userID INTO pUserID
	FROM tblUser
	WHERE username = pUsername;

	SELECT tileID INTO pTileID
	FROM tblTile
	WHERE xCoord = pXCoord AND yCoord = pYCoord;
	
END //
DELIMITER ;

-- Get All Games Procedure

DROP PROCEDURE IF EXISTS Game_List;
DELIMITER //

CREATE PROCEDURE Game_List()
BEGIN
	SELECT * FROM tblGame;
END //
DELIMITER ;

-- Join Game Procedure

DROP PROCEDURE IF EXISTS Join_Game;
DELIMITER //

CREATE PROCEDURE Join_Game()
BEGIN
	
END //
DELIMITER ;

-- Movement Procedure

DROP PROCEDURE IF EXISTS User_Movement;
DELIMITER //

CREATE PROCEDURE User_Movement()
BEGIN
	
END //
DELIMITER ;

-- Chat Procedure

DROP PROCEDURE IF EXISTS Chat_Message;
DELIMITER //

CREATE PROCEDURE Chat_Message(In pUsername VARCHAR(20), In pMessage VARCHAR(100))
BEGIN
	INSERT INTO chat(username, message)
	VALUES (pUsername, pMessage);
	SELECT * FROM chat;
END //
DELIMITER ;

-- Game End Procedure

DROP PROCEDURE IF EXISTS Game_End;
DELIMITER //

CREATE PROCEDURE Game_End()
BEGIN
	
END //
DELIMITER ;

-- Admin Edit User Procedure

DROP PROCEDURE IF EXISTS Admin_Edit_User;
DELIMITER //

CREATE PROCEDURE Admin_Edit_User(IN pUsername VARCHAR(20), IN pPassword VARCHAR(30), IN pEmail VARCHAR(50), IN pIsLocked BIT, IN pIsAdmin BIT)
BEGIN
    IF EXISTS (SELECT email FROM tblUser WHERE email = pEmail) THEN
        UPDATE tblUser
        SET username = pUsername, password = pPassword, email = pEmail, isLocked = pIsLocked, isAdmin = pIsAdmin
        WHERE email = pEmail;
        SELECT 'User Updated' AS Message;
    ELSE
        SELECT 'Email does not exist' AS Message;
    END IF;
end //


DELIMITER ;

-- Admin Add User Procedure

DROP PROCEDURE IF EXISTS Admin_New_User;
DELIMITER //

CREATE PROCEDURE Admin_New_User(In pUsername VARCHAR(20), In pPassword VARCHAR(30), In pEmail varchar(50))
BEGIN
	IF EXISTS (SELECT username FROM tblUser WHERE username = pUsername) THEN
		SELECT 'Username already exists' AS message;
	ELSEIF EXISTS (SELECT email FROM tblUser WHERE email = pEmail) THEN
		SELECT 'Email already exists' AS message;
	ELSE
		INSERT INTO user (username, password, email, isAdmin, isLocked, numLoginAttempts, isOnline, score)
		VALUES (pUsername, pPassword, pEmail, false, 0, 0, 0, 0);
		SELECT 'User added successfully' AS message;
	END IF;
END //
DELIMITER ;

-- Admin Delete User Procedure

DROP PROCEDURE IF EXISTS Admin_Delete_User;
DELIMITER //

CREATE PROCEDURE Admin_Delete_User(In pUsername VARCHAR(20), IN adminID INT(10))
BEGIN
	DECLARE isAdmin BOOLEAN;

	SELECT isAdmin INTO isAdmin
	FROM tblUser
	WHERE userID = adminID;

	IF isAdmin = 1 THEN
		DELETE FROM tblUser
		WHERE username = pUsername;
		SELECT CONCAT(pUsername, ' deleted') AS message;
	ELSE
		SELECT 'User is not an admin' AS message;
	END IF;
END //
DELIMITER ;


-- Delete User Procedure

DROP PROCEDURE IF EXISTS Delete_User;
DELIMITER //

CREATE PROCEDURE Delete_User(In pUsername VARCHAR(20))
BEGIN
	IF EXISTS (SELECT username FROM tblUser WHERE username = pUsername) THEN
		DELETE FROM tblUser
		WHERE username = pUsername;
		SELECT CONCAT(pUsername, ' deleted') AS message;
	ELSE
		SELECT 'User does not exist' AS message;
	END IF;
END //

-- Admin End Game Procedure

DROP PROCEDURE IF EXISTS Admin_End_Game;
DELIMITER //

CREATE PROCEDURE Admin_End_Game(In pGameID INT(10), In adminID INT(10))
BEGIN
	Declare isAdmin BOOLEAN;

	SELECT isAdmin INTO isAdmin
	FROM tblUser
	WHERE userID = adminID;

	IF isAdmin = 1 THEN
		DELETE FROM game
		WHERE gameID = pGameID;
		SELECT CONCAT(pGameID, ' deleted') AS message;
	ELSE
		SELECT 'User is not an admin' AS message;
	END IF;
END //
DELIMITER ;

-- Admin Get User Data

drop procedure if exists Get_User_Data;
DELIMITER //

CREATE PROCEDURE Get_User_Data(IN pUserID INT)
BEGIN
    SELECT username, password, email
    FROM tblUser
    WHERE userID = pUserID;
end //
DELIMITER ;
