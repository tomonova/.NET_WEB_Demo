﻿
CREATE DATABASE RWA_ERV
GO
USE RWA_ERV
GO
--####### CREATE TABLES #######
create table TEAMS
(
	IDTeam int not null identity(1,1),
	Name nvarchar(max) not null,
	TeamStatus int not null,
	FoundingDate date not null default (GETDATE()),
	constraint PKTeams primary key(IDTeam),
);
create table CLIENTS
(
	IDClient int not null IDENTITY(1,1),
	Name nvarchar(max) not null,
	OIB nvarchar(max),
	constraint PKClients primary key(IDClient)
);
Create table EMPLOYEES
(
	IDEmployee INT not null IDENTITY(1,1),
	Name nvarchar(max) not null,
	Surname nvarchar(max) not null,
	EmploymentDate DATE not null default (GETDATE()),
	EmployeeType int not null,
	EmployeePosition int not null,
	EmployeeStatus int not null,
	TeamID int,
	constraint PKEmployees primary key(IDEmployee),
	constraint FKEmployees_Teams foreign key(TeamID) references TEAMS(IDTeam)
);
create table PROJECTS
(
	IDProject int not null IDENTITY(1,1),
	Name nvarchar(max),
	CreationDate date not null default(GETDATE()),
	ClientID int,
	ProjectLeadID int,
	ProjectStatus int not null,
	constraint PKProjects primary key(IDProject),
	constraint FKProjects_Clients foreign key(ClientID) references CLIENTS(IDClient),
	constraint FKProjectLead_Employee foreign key(ProjectLeadID) references EMPLOYEES(IDEMployee)
);
create table USERS
(
	IDUser int not null identity(1,1),
	UserName nvarchar(max) not null,
	Password nvarchar(max) not null,
	Admin int not null default(0),
	EmployeeID int not null unique constraint FKUsersEmployee references EMPLOYEES(IDEmployee),
	constraint PKUsers primary key(IDUser)
);
create table EMPLOYEEPROJECT
(
	EmployeeID int not null,
	ProjectID int not null,
	constraint PKEmployeeProject primary key(EmployeeID, ProjectID),
	constraint FKEmployeeProject_Employees foreign key (EmployeeID) references EMPLOYEES(IDEmployee),
	constraint FKEmployeeProject_Projects foreign key (ProjectID) references PROJECTS (IDProject)
);
create table TEAMLEAD
(
	IDTeamLead int not null IDENTITY(1,1),
	EmployeeID int not null unique constraint FKTeamLead_Employee references EMPLOYEES(IDEmployee),
	TeamID int not null unique constraint FKTeamLead_Teams references TEAMS(IDTeam),
	constraint PKTeamLead primary key(IDTeamLead)
)
--###### PROCEDURES #######
GO
CREATE OR ALTER PROC GetEmployees
AS
BEGIN
	SELECT * FROM EMPLOYEES
END
GO
CREATE OR ALTER PROC GetEmployee
	@IDEMployee int
AS
BEGIN
	SELECT * FROM EMPLOYEES WHERE IDEmployee=@IDEMployee
END
GO
CREATE OR ALTER PROC GetProjects
AS
BEGIN
	SELECT * FROM PROJECTS
END
GO
CREATE OR ALTER PROC GetProject
	@IDProject int
AS
BEGIN
	SELECT * FROM EMPLOYEES WHERE IDEmployee=@IDProject
END
GO
CREATE OR ALTER PROC GetTeams
AS
BEGIN
	SELECT * FROM TEAMS
END
GO
CREATE OR ALTER PROC GetTeam
	@IDTeam int
AS
BEGIN
	SELECT * FROM TEAMS WHERE IDTeam=@IDTeam
END
GO
create or alter proc CheckCredentialsAdmin
	@userName nvarchar(50),
	@userPass nvarchar(50),
	@checkOutput int output
as
	if exists(
		select * from USERS
		where UserName = @userName
		and Password = @userPass
		and Admin = 1)
		set @checkOutput = '1'
	else set @checkOutput = '0'
go

--####### INSERT DATA #########
INSERT TEAMS(Name, TeamStatus, FoundingDate) VALUES('BACKEND',1,'2019-10-10')
INSERT TEAMS(Name, TeamStatus, FoundingDate) VALUES('FRONTEND',1,'2019-10-10')
INSERT TEAMS(Name, TeamStatus, FoundingDate) VALUES('API',1,'2019-12-10')
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus) VALUES('Diša','Boss','2020-01-01',1,1,1)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Pero','Perić','2020-01-01',1,3,1,1)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Miro','Mirić','2020-01-01',3,3,1,2)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Iva','Ivić','2020-01-01',3,3,1,1)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Marko','Markoić','2020-01-01',1,3,1,2)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Hrvoje','Horvat','2020-01-01',1,3,1,1)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Maja','Majić','2020-01-01',1,3,1,3)
INSERT TEAMLEAD(EmployeeID, TeamID) VALUES(1,1)
INSERT TEAMLEAD(EmployeeID, TeamID) VALUES(2,2)
INSERT TEAMLEAD(EmployeeID, TeamID) VALUES(6,3)
INSERT USERS (UserName, Password, Admin, EmployeeID) VALUES('admin','1234',1,1)
INSERT CLIENTS(Name, OIB) VALUES('IBM','123456789')
INSERT PROJECTS(Name, CreationDate, ClientID, ProjectLeadID, ProjectStatus) VALUES('prvi projekt','2020-05-01',1,1,1)
INSERT EMPLOYEEPROJECT(EmployeeID, ProjectID) values(1,1)
