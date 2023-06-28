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
drop table if exists tblUserGame;


-- Create Tables

CREATE TABLE tblUser (
    userID int(10) primary key not null AUTO_INCREMENT,
    username varchar(20) not null unique,
    `password` varchar(30) not null,
    email varchar(50) not null unique,
    isAdmin boolean default false,
    isLocked bit default 0,
    numLoginAttempts int(1) default 0,
    lockedUntil datetime,
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
    userID INT NOT NULL,
    itemID INT,
    passengerCount INT NOT NULL DEFAULT 0,
    CONSTRAINT fk_userID FOREIGN KEY (userID) REFERENCES tblUser(userID) ON DELETE CASCADE,
    CONSTRAINT chk_passengerCount CHECK (passengerCount >= 0 AND passengerCount <= 3)
);



CREATE TABLE tblChat
(
    chatID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    userID INT,
    message VARCHAR(255)
);

CREATE TABLE tblUserGame (
    userID INT,
    gameID INT,
    PRIMARY KEY (userID, gameID),
    FOREIGN KEY (userID) REFERENCES tblUser(userID),
    FOREIGN KEY (gameID) REFERENCES tblGame(gameID) ON DELETE CASCADE
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

	INSERT INTO tblItem (name, description, isNPC, value)
	VALUES 
	('Passenger','Drop off ' , 0, 10),
	('Frog', 'Avoid', 1, -10),
	('Road', 'Road for the user to drive on', 0, 0),
	('Wall', 'Tiles that users cant go on', 0, 0),
	('WallNoSpawn', 'Passengers wont spawn on this tile', 0, 0);

	INSERT INTO tblInventory (userID, itemID, passengerCount)
	SELECT (SELECT userID FROM tblUser WHERE username = 'z'), (SELECT itemID FROM tblItem WHERE name = 'Passenger'), 1 UNION
	SELECT (SELECT userID FROM tblUser WHERE username = 'admin'), (SELECT itemID FROM tblItem WHERE name = 'Passenger'), 0 UNION
	SELECT (SELECT userID FROM tblUser WHERE username = 'Online'), (SELECT itemID FROM tblItem WHERE name = 'Passenger'), 2 UNION
	SELECT (SELECT userID FROM tblUser WHERE username = 'locked'), (SELECT itemID FROM tblItem WHERE name = 'Passenger'), 1 UNION
	SELECT (SELECT userID FROM tblUser WHERE username = 'delete'), (SELECT itemID FROM tblItem WHERE name = 'Passenger'), 0;


	INSERT INTO tblTile (`column`, `row`, homeTile, DropOffTile, itemID)
	VALUES 
	(1, 1, 0, 0, 5),
	(2, 1, 0, 0, 4),
	(3, 1, 0, 0, 3),
	(4, 1, 0, 0, 3),
	(5, 1, 0, 0, 4),
	(6, 1, 0, 0, 4),
	(7, 1, 0, 0, 3),
	(8, 1, 0, 0, 3),
	(9, 1, 0, 0, 4),
	(10, 1, 0, 0, 4),
	(11, 1, 0, 0, 3),
	(12, 1, 0, 0, 3),
	(13, 1, 0, 0, 4),
	(14, 1, 0, 0, 4),
	(1, 2, 0, 0, 5),
	(2, 2, 1, 0, 3),
	(3, 2, 0, 0, 3),
	(4, 2, 0, 0, 3),
	(5, 2, 0, 1, 3),
	(6, 2, 0, 0, 4),
	(7, 2, 0, 0, 3),
	(8, 2, 0, 0, 3),
	(9, 2, 0, 0, 4),
	(10, 2, 0, 0, 4),
	(11, 2, 0, 0, 3),
	(12, 2, 0, 0, 3),
	(13, 2, 0, 0, 3),
	(14, 2, 0, 0, 3),
	(1, 3, 0, 0, 5),
	(2, 3, 0, 0, 4),
	(3, 3, 0, 0, 3),
	(4, 3, 0, 0, 3),
	(5, 3, 0, 0, 4),
	(6, 3, 0, 0, 4),
	(7, 3, 0, 0, 3),
	(8, 3, 0, 0, 3),
	(9, 3, 0, 0, 4),
	(10, 3, 0, 0, 4),
	(11, 3, 0, 0, 3),
	(12, 3, 0, 0, 3),
	(13, 3, 0, 0, 3),
	(14, 3, 0, 0, 3),
	(1, 4, 0, 0, 5),
	(2, 4, 0, 0, 4),
	(3, 4, 0, 0, 3),
	(4, 4, 0, 0, 3),
	(5, 4, 0, 0, 4),
	(6, 4, 0, 0, 4),
	(7, 4, 0, 0, 3),
	(8, 4, 0, 0, 3),
	(9, 4, 0, 0, 3),
	(10, 4, 0, 0, 3),
	(11, 4, 0, 0, 3),
	(12, 4, 0, 0, 3),
	(13, 4, 0, 0, 4),
	(14, 4, 0, 0, 4),
	(1, 5, 0, 0, 4),
	(2, 5, 0, 0, 4),
	(3, 5, 0, 0, 3),
	(4, 5, 0, 0, 3),
	(5, 5, 0, 0, 4),
	(6, 5, 0, 0, 4),
	(7, 5, 0, 0, 3),
	(8, 5, 0, 0, 3),
	(9, 5, 0, 0, 3),
	(10, 5, 0, 0, 3),
	(11, 5, 0, 0, 3),
	(12, 5, 0, 0, 3),
	(13, 5, 0, 0, 4),
	(14, 5, 0, 0, 5),
	(1, 6, 0, 0, 3),
	(2, 6, 0, 0, 3),
	(3, 6, 0, 0, 3),
	(4, 6, 0, 0, 3),
	(5, 6, 0, 0, 4),
	(6, 6, 0, 0, 4),
	(7, 6, 0, 0, 4),
	(8, 6, 0, 0, 4),
	(9, 6, 0, 0, 4),
	(10, 6, 0, 0, 4),
	(11, 6, 0, 0, 3),
	(12, 6, 0, 0, 3),
	(13, 6, 0, 0, 4),
	(14, 6, 0, 0, 5),
	(1, 7, 0, 0, 3),
	(2, 7, 0, 0, 3),
	(3, 7, 0, 0, 3),
	(4, 7, 0, 0, 3),
	(5, 7, 0, 0, 4),
	(6, 7, 0, 0, 4),
	(7, 7, 0, 0, 4),
	(8, 7, 0, 0, 4),
	(9, 7, 0, 0, 4),
	(10, 7, 0, 0, 4),
	(11, 7, 0, 0, 3),
	(12, 7, 0, 0, 3),
	(13, 7, 0, 0, 4),
	(14, 7, 0, 0, 4),
	(1, 8, 0, 0, 4),
	(2, 8, 0, 0, 4),
	(3, 8, 0, 0, 3),
	(4, 8, 0, 0, 3),
	(5, 8, 0, 0, 3),
	(6, 8, 0, 0, 3),
	(7, 8, 0, 0, 3),
	(8, 8, 0, 0, 3),
	(9, 8, 0, 0, 3),
	(10, 8, 0, 0, 3),
	(11, 8, 0, 0, 3),
	(12, 8, 0, 0, 3),
	(13, 8, 0, 0, 3),
	(14, 8, 0, 0, 3),
	(1, 9, 0, 0, 5),
	(2, 9, 0, 0, 4),
	(3, 9, 0, 0, 3),
	(4, 9, 0, 0, 3),
	(5, 9, 0, 0, 3),
	(6, 9, 0, 0, 3),
	(7, 9, 0, 0, 3),
	(8, 9, 0, 0, 3),
	(9, 9, 0, 0, 3),
	(10, 9, 0, 0, 3),
	(11, 9, 0, 0, 3),
	(12, 9, 0, 0, 3),
	(13, 9, 0, 0, 3),
	(14, 9, 0, 0, 3),
	(1, 10, 0, 0, 5),
	(2, 10, 0, 0, 4),
	(3, 10, 0, 0, 3),
	(4, 10, 0, 0, 3),
	(5, 10, 0, 0, 4),
	(6, 10, 0, 0, 4),
	(7, 10, 0, 0, 4),
	(8, 10, 0, 0, 4),
	(9, 10, 0, 0, 4),
	(10, 10, 0, 0, 4),
	(11, 10, 0, 0, 3),
	(12, 10, 0, 0, 3),
	(13, 10, 0, 0, 4),
	(14, 10, 0, 0, 4);

	
END //
DELIMITER ;

call Create_Taxi_Game();
call Create_Data();

-- Login Procedure

DROP PROCEDURE IF EXISTS Log_In;

DELIMITER //
CREATE PROCEDURE Log_In(IN pUsername VARCHAR(20), IN pPassword VARCHAR(30))
BEGIN
    DECLARE dbPassword VARCHAR(30);
    DECLARE pUserID INT(10);
    DECLARE login_attempts INT(1);
    DECLARE locked BIT;
    DECLARE lockedUntil DATETIME;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;

    SELECT userID, password, numLoginAttempts, isLocked, lockedUntil INTO pUserID, dbPassword, login_attempts, locked, lockedUntil
    FROM tblUser 
    WHERE username = pUsername;

    -- Check if the user is allowed to login
    IF locked = 1 AND NOW() < lockedUntil then 
        SELECT 'Account Locked' AS message;
    ELSE
        -- Unlock the account if the lock duration has passed
        IF locked = 1 THEN
            UPDATE tblUser
            SET isLocked = 0, numLoginAttempts = 0
            WHERE userID = pUserID;
        END IF;

        -- Check password
        IF dbPassword = pPassword THEN
            UPDATE tblUser 
            SET isOnline = 1, numLoginAttempts = 0
            WHERE userID = pUserID;

            COMMIT;
            SELECT 'Login Successful' AS message;
        ELSE
            SET login_attempts = login_attempts + 1;
            UPDATE tblUser 
            SET numLoginAttempts = login_attempts
            WHERE userID = pUserID;
            
            -- Lock account if login attempts exceed 5
            IF login_attempts >= 5 THEN
                UPDATE tblUser 
                SET isLocked = 1, lockedUntil = DATE_ADD(NOW(), INTERVAL 5 MINUTE)
                WHERE userID = pUserID;
                COMMIT;      
                SELECT 'Account Locked' AS message;
            ELSE

                COMMIT;
                SELECT 'Login Failed' AS message;
            END IF;
        END IF;
    END IF;
END //
DELIMITER ;




-- New User Procedure

DROP PROCEDURE IF EXISTS New_User;

DELIMITER //
CREATE PROCEDURE New_User(IN pUsername VARCHAR(20), IN pPassword VARCHAR(30), IN pEmail VARCHAR(50))
BEGIN
    DECLARE lastUserID INT;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN

        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;


    START TRANSACTION;

    IF EXISTS (SELECT * FROM tblUser WHERE username = pUsername) THEN
        SELECT 'User Exists' AS Message;
    ELSE
        INSERT INTO tblUser(username, password, email)
        VALUES (pUsername, pPassword, pEmail);
        
        SET lastUserID = LAST_INSERT_ID();
        
        INSERT INTO tblInventory (userID) VALUES (lastUserID);
        
        COMMIT;
        SELECT 'Login Success' AS Message;
    END IF;

END //
DELIMITER ;




-- Log Out Procedure

DROP PROCEDURE IF EXISTS Log_Out;

DELIMITER //
CREATE PROCEDURE Log_Out(IN pUsername VARCHAR(20))
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;
    
    IF EXISTS (
        SELECT *
        FROM tblUser
        WHERE username = pUsername
    ) THEN
        UPDATE tblUser
        SET isOnline = 0
        WHERE username = pUsername;
    END IF;

    COMMIT;
    
    SELECT 'Logout successful' AS Message;
END //
DELIMITER ;



-- Get Active Players Procedure

DROP PROCEDURE IF EXISTS Active_User_List;

DELIMITER //
CREATE PROCEDURE Active_User_List()
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;
    
    SELECT userID, username
    FROM tblUser
    WHERE isOnline = 1 AND isLocked = 0;

    COMMIT;
END //
DELIMITER ;


-- Create Game Procedure

DROP PROCEDURE IF EXISTS Create_Game;

DELIMITER //
CREATE PROCEDURE Create_Game(IN pUsername VARCHAR(20), OUT pGameID INT)
BEGIN
    DECLARE pUserID INT;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;

    SELECT userID INTO pUserID
    FROM tblUser
    WHERE username = pUsername;

    INSERT INTO tblGame (tileID, userID)
    VALUES (DEFAULT, pUserID);

    SET pGameID = LAST_INSERT_ID();
    
    COMMIT;
END //
DELIMITER ;



-- Get All Games Procedure

DROP PROCEDURE IF EXISTS Game_List;

DELIMITER //
CREATE PROCEDURE Game_List()
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN

        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;

    SELECT * FROM tblGame;

    COMMIT;
END //
DELIMITER ;


-- Join Game Procedure

DROP PROCEDURE IF EXISTS Join_Game;

DELIMITER //
CREATE PROCEDURE Join_Game(IN pGameID INT, IN pUserID INT)
BEGIN
    DECLARE gameExists INT;
    DECLARE userGameExists INT;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;

    -- Check if the game exists
    SELECT COUNT(*) INTO gameExists
    FROM tblGame
    WHERE gameID = pGameID;

    -- Check if the user is already in the game
    SELECT COUNT(*) INTO userGameExists
    FROM tblUserGame
    WHERE gameID = pGameID AND userID = pUserID;

    IF gameExists > 0 THEN
        IF userGameExists = 0 THEN
            -- User is not in the game
            INSERT INTO tblUserGame (userID, gameID)
            VALUES (pUserID, pGameID);

            COMMIT;

            SELECT 'Game Joined' AS Message;
        ELSE
            -- User is already in the game
            COMMIT;

            SELECT 'User is already in the game' AS Message;
        END IF;
    ELSE
        -- Game does not exist
        COMMIT;

        SELECT 'Game does not exist' AS Message;
    END IF;
END //
DELIMITER ;


-- Movement Procedure

DROP PROCEDURE IF EXISTS User_Movement;

DELIMITER //
CREATE PROCEDURE User_Movement(IN p_Username VARCHAR(20), IN p_TileID INT)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;
    START TRANSACTION;

    UPDATE tblGame
    INNER JOIN tblUser ON tblGame.userID = tblUser.userID
    SET tblGame.tileID = p_TileID
    WHERE tblUser.username = p_Username;
    COMMIT;
    
    SELECT 'Moved successfully' AS Message;
END //
DELIMITER ;




-- Chat Procedure

DROP PROCEDURE IF EXISTS Chat_Message;

DELIMITER //
CREATE PROCEDURE Chat_Message(In pUsername VARCHAR(20), In pMessage VARCHAR(100))
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;
    INSERT INTO chat(username, message)
    VALUES (pUsername, pMessage);
    COMMIT;
    
    SELECT * FROM chat;
END //
DELIMITER ;

-- Game End Procedure

DROP PROCEDURE IF EXISTS Game_End;

DELIMITER //
CREATE PROCEDURE Game_End(IN pGameID INT)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;
    START TRANSACTION;

    UPDATE tblGame
    SET status = 'ended',
        end_time = NOW()
    WHERE gameID = pGameID;

    COMMIT;

    SELECT 'Game marked as ended' AS Message;
END //
DELIMITER ;


-- Admin Edit User Procedure

DROP PROCEDURE IF EXISTS Admin_Edit_User;

DELIMITER //
CREATE PROCEDURE Admin_Edit_User(IN pUsername VARCHAR(20), IN pPassword VARCHAR(30), IN pEmail VARCHAR(50), IN pIsLocked BIT, IN pIsAdmin BIT)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;

    IF EXISTS (SELECT email FROM tblUser WHERE email = pEmail) THEN
        UPDATE tblUser
        SET username = pUsername, password = pPassword, email = pEmail, isLocked = pIsLocked, isAdmin = pIsAdmin
        WHERE email = pEmail;
        
        COMMIT;

        SELECT 'User Updated' AS Message;
    ELSE
        COMMIT;

        SELECT 'Email does not exist' AS Message;
    END IF;
END //
DELIMITER ;


-- Admin Add User Procedure

DROP PROCEDURE IF EXISTS Admin_New_User;

DELIMITER //
CREATE PROCEDURE Admin_New_User(In pUsername VARCHAR(20), In pPassword VARCHAR(30), In pEmail varchar(50), OUT pMessage VARCHAR(255))
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET pMessage = 'Error occurred, transaction rolled back';
    END;

    START TRANSACTION;

    IF EXISTS (SELECT username FROM tblUser WHERE username = pUsername) THEN
        SET pMessage = 'Username already exists';
        COMMIT;
    ELSEIF EXISTS (SELECT email FROM tblUser WHERE email = pEmail) THEN
        SET pMessage = 'Email already exists';
        COMMIT;
    ELSE
        INSERT INTO tblUser (username, password, email, isAdmin, isLocked, numLoginAttempts, isOnline, score)
        VALUES (pUsername, pPassword, pEmail, false, 0, 0, 0, 0);
        SET @lastUserID = LAST_INSERT_ID();
        INSERT INTO tblInventory (userID) VALUES (@lastUserID);      
        SET pMessage = 'User added successfully';
       
        COMMIT;
    END IF;
END //
DELIMITER ;



-- Admin Delete User Procedure

DROP PROCEDURE IF EXISTS Admin_Delete_User;

DELIMITER //
CREATE PROCEDURE Admin_Delete_User(IN pUserID INT)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;

    DELETE FROM tblUser
    WHERE userID = pUserID;

    COMMIT;

    IF ROW_COUNT() > 0 THEN
        SELECT CONCAT('User Deleted') AS message;
    ELSE
        SELECT 'User not found' AS message;
    END IF;
END //
DELIMITER ;



-- Delete User Procedure

DROP PROCEDURE IF EXISTS Delete_User;

DELIMITER //
CREATE PROCEDURE Delete_User(In pUsername VARCHAR(20))
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;
   
    START TRANSACTION;

    IF EXISTS (SELECT username FROM tblUser WHERE username = pUsername) THEN
        DELETE FROM tblUser
        WHERE username = pUsername;

        COMMIT;
        
        SELECT CONCAT(pUsername, ' deleted') AS message;
    ELSE
        COMMIT;
        
        SELECT 'User does not exist' AS message;
    END IF;
END //
DELIMITER ;


-- Admin End Game Procedure

DROP PROCEDURE IF EXISTS Admin_End_Game;

DELIMITER //
CREATE PROCEDURE Admin_End_Game(In pGameID INT(10), In adminID INT(10))
BEGIN
    DECLARE isAdmin BOOLEAN;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Error occurred, transaction rolled back' AS Message;
    END;

    START TRANSACTION;

    SELECT isAdmin INTO isAdmin
    FROM tblUser
    WHERE userID = adminID;

    IF isAdmin = 1 THEN
        DELETE FROM game
        WHERE gameID = pGameID;

        COMMIT;

        SELECT CONCAT(pGameID, ' deleted') AS message;
    ELSE
        COMMIT;

        SELECT 'User is not an admin' AS message;
    END IF;
END //
DELIMITER ;


-- Admin Get User Data

DROP PROCEDURE IF EXISTS Get_User_Data;

DELIMITER //
CREATE PROCEDURE Get_User_Data(IN pUserID INT)
BEGIN

    SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;
    START TRANSACTION;

    SELECT username, password, email
    FROM tblUser
    WHERE userID = pUserID;

    COMMIT;
END //
DELIMITER ;

