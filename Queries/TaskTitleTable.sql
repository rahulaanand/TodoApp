USE TodoDB;

-- Created (TaskTitle) Table
CREATE TABLE TaskTitle (
  Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  TitleName VARCHAR(50)
);

-- Created a stored procedure (TaskTitle)
CREATE PROCEDURE InsertTaskTitle
  @TitleName VARCHAR(50)
AS
BEGIN
  INSERT INTO TaskTitle (TitleName)
  VALUES (@TitleName);
END;

EXEC InsertTaskTitle @TitleName = 'Travel';

-- Stored procedure to view all
CREATE PROCEDURE ViewAllTaskTitles
AS
BEGIN
  SELECT * FROM TaskTitle;
END;

-- Stored procedure to view by ID
CREATE PROCEDURE ViewTaskTitleById
  @TitleId UNIQUEIDENTIFIER
AS
BEGIN
  SELECT *
  FROM TaskTitle
  WHERE Id = @TitleId;
END;

-- Stored procedure to update a task title
CREATE PROCEDURE UpdateTaskTitle
  @TitleId UNIQUEIDENTIFIER,
  @TitleName VARCHAR(50)
AS
BEGIN
  UPDATE TaskTitle
  SET TitleName = @TitleName
  WHERE Id = @TitleId;
END;

-- Stored procedure to delete a task title
CREATE PROCEDURE DeleteTaskTitle
  @TitleId UNIQUEIDENTIFIER
AS
BEGIN
  DELETE FROM TaskTitle
  WHERE Id = @TitleId;
END;

-- Execute stored procedures for TaskTitle
EXEC ViewAllTaskTitles;

EXEC ViewTaskTitleById @TitleId = '094f3a6d-c856-450e-bda3-50886e7d2a37';

-- SELECT Query
SELECT * FROM Users;
SELECT * FROM TaskTitle;
SELECT * FROM Task;
