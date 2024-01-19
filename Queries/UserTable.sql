CREATE DATABASE TodoDB;
USE TodoDB;

-- Created the user table
CREATE TABLE Users (
  Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  Name VARCHAR(50),
  Password VARCHAR(50),
  Email VARCHAR(30)
);

-- Created a stored procedure for inserting into Users table
CREATE PROCEDURE InsertUser
  @Name VARCHAR(50),
  @Password VARCHAR(50),
  @Email VARCHAR(30)
AS
BEGIN
  INSERT INTO Users (Name, Password, Email)
  VALUES (@Name, @Password, @Email);
END;

-- Execute the stored procedure to insert a user
EXEC InsertUser
  @Name = 'Aanand',
  @Password = 'Aanand@12',
  @Email = 'aanand@gmail.com';

-- Stored procedure to view all users
CREATE PROCEDURE ViewAllUsers
AS
BEGIN
  SELECT * FROM Users;
END;

-- Stored procedure to view a user by ID
CREATE PROCEDURE ViewUserById
  @UserId UNIQUEIDENTIFIER
AS
BEGIN
  SELECT *
  FROM Users
  WHERE Id = @UserId;
END;

-- Stored procedure to update a user
CREATE PROCEDURE UpdateUser
  @UserId UNIQUEIDENTIFIER,
  @Name VARCHAR(50),
  @Password VARCHAR(50),
  @Email VARCHAR(30)
AS
BEGIN
  UPDATE Users
  SET
    Name = @Name,
    Password = @Password,
    Email = @Email
  WHERE Id = @UserId;
END;

-- Stored procedure to delete a user
CREATE PROCEDURE DeleteUser
  @UserId UNIQUEIDENTIFIER
AS
BEGIN
  DELETE FROM Users
  WHERE Id = @UserId;
END;

-- Execute stored procedures
EXEC ViewAllUsers;

EXEC ViewUserById @UserId = '5C0D753C-F9C8-4028-9CFC-EE1074621F27';

-- SELECT Query
SELECT * FROM Users;
SELECT * FROM TaskTitle;
SELECT * FROM Task;