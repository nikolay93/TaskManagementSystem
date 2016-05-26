use TaskManagementSystem

BEGIN TRAN 
CREATE TABLE Users
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(255) NOT NULL,
	Email varchar(255) NOT NULL
);

CREATE TABLE Tasks
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	CreateDate DateTimeOffSet NOT NULL,
	TaskName varchar(255) NOT NULL,
	RequiredByDate DateTimeOffSet NULL,
	TaskDescription varchar(4000) NOT NULL,
	TaskStatus int NOT NULL,
	TaskType int NOT NULL,
	NextActionDate DateTimeOffSet NULL
);

CREATE TABLE Comments
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	DateAdded DateTimeOffSet NOT NULL,
	[Text] varchar(500) NOT NULL,
	CommentType int NOT NULL,
	ReminderDate DateTimeOffSet NULL,
	TaskId int NOT NULL,

	CONSTRAINT TaskComments FOREIGN KEY (TaskId)
	REFERENCES Tasks(Id) ON DELETE CASCADE 
	
);

CREATE TABLE TasksUsers
(

	TaskId int NOT NULL,
	UserId int NOT NULL,
	PRIMARY KEY (TaskId, UserId), 

CONSTRAINT Task FOREIGN KEY (TaskId)
REFERENCES Tasks(Id) ON DELETE CASCADE,

CONSTRAINT [User] FOREIGN KEY (UserId)
REFERENCES Users(Id) ON DELETE CASCADE
);

 COMMIT TRAN
