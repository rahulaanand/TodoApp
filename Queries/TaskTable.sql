use TodoDB;

-- Created the Task table
CREATE TABLE Task (
  TaskId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  UserId UNIQUEIDENTIFIER,
  TitleId UNIQUEIDENTIFIER,
  Description VARCHAR(20), 
  CreatedAt DATETIME, 
  DueTime DATETIME,
  Status VARCHAR(20),
  FOREIGN KEY (UserId) REFERENCES Users(Id),
  FOREIGN KEY (TitleId) REFERENCES TaskTitle(Id)
);

-- Created a stored procedure (Task)
CREATE PROCEDURE InsertTask
  @UserId UNIQUEIDENTIFIER,
  @TitleId UNIQUEIDENTIFIER,
  @Description TEXT,
  @DueTime DATETIME, 
  @Status VARCHAR(20)
AS
BEGIN
  INSERT INTO Task (UserId, TitleId, Description, CreatedAt, DueTime, Status)
  VALUES (@UserId, @TitleId, @Description, GETDATE(), @DueTime, @Status);
END;

    DROP PROCEDURE InsertTask;

-- stored procedure to insert a task
EXEC InsertTask
  @UserId = '907A5248-7120-4DAE-B756-3AFC4493F67E',
  @TitleId = '094F3A6D-C856-450E-BDA3-50886E7D2A37',
  @Description = 'Submit daily report',
  @DueTime = '2024-02-01',
  @Status = 'Not Completed';

-- Stored procedure to view all tasks
CREATE PROCEDURE ViewAllTasks
AS
BEGIN
  SELECT * FROM Task;
END;

EXEC ViewAllTasks;

-- Stored procedure task by ID
CREATE PROCEDURE ViewTaskById
  @TaskId UNIQUEIDENTIFIER
AS
BEGIN
  SELECT *
  FROM Task
  WHERE TaskId = @TaskId;
END;

-- Stored procedure to update a task
CREATE PROCEDURE UpdateTask
  @TaskId UNIQUEIDENTIFIER,
  @UserId UNIQUEIDENTIFIER,
  @TitleId UNIQUEIDENTIFIER,
  @Description TEXT,
  @DueTime DATETIME,
  @Status VARCHAR(20)
AS
BEGIN
  UPDATE Task
  SET
    UserId = @UserId,
    TitleId = @TitleId,
    Description = @Description,
    DueTime = @DueTime,
    Status = @Status
  WHERE TaskId = @TaskId;
END;

-- Stored procedure to delete a task
CREATE PROCEDURE DeleteTask
  @TaskId UNIQUEIDENTIFIER
AS
BEGIN
  DELETE FROM Task
  WHERE TaskId = @TaskId;
END;

-- Execute 
EXEC ViewAllTasks;

EXEC ViewTaskById @TaskId = '60224F20-9B8A-4180-BDC9-50916E0837E3';

-- SELECT Query
SELECT * FROM Users;
SELECT * FROM TaskTitle;
SELECT * FROM Task;