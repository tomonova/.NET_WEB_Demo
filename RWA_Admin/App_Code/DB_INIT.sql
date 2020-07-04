
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
	OIB nvarchar(max) not null,
	Address nvarchar(max),
	ClientStatus int not null,
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
	TeamID int not null default 1,
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
	EmployeeID int not null constraint FKTeamLead_Employee references EMPLOYEES(IDEmployee),
	TeamID int not null unique constraint FKTeamLead_Teams references TEAMS(IDTeam),
	constraint PKTeamLead primary key(IDTeamLead)
)
--###### PROCEDURES #######
GO
CREATE OR ALTER PROC GetEmployees
AS
BEGIN
	SELECT * FROM EMPLOYEES
	where EmployeeStatus =1
END
GO
CREATE OR ALTER PROC GetEmployee
	@IDEMployee int
AS
BEGIN
	SELECT * FROM EMPLOYEES WHERE IDEmployee=@IDEMployee
	and EmployeeStatus =1
END
GO
CREATE OR ALTER PROC GetProjects
AS
BEGIN
	select IDProject,Name from PROJECTS
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
	SELECT * FROM TEAMS WHERE TeamStatus=1
END
GO
CREATE OR ALTER PROC GetTeam
	@IDTeam int
AS
BEGIN
	SELECT * FROM TEAMS WHERE IDTeam=@IDTeam
END
GO
CREATE OR ALTER PROC GetTeamLeads
as
begin
select IDEmployee, Name, Surname from EMPLOYEES
where EmployeePosition=2
end
go
CREATE OR ALTER PROC GetClients
AS
BEGIN
	SELECT * FROM CLIENTS
END
GO
CREATE OR ALTER PROC GetClient
	@IDClient int
AS
BEGIN
	SELECT * FROM CLIENTS WHERE IDClient=@IDClient
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
CREATE OR ALTER PROC GetEmployeeAdmin
	@employeeID int
as
	select e.IDemployee,
	e.Name,
	e.Surname,
	e.EmploymentDate,
	e.EmployeeType,
	e.EmployeePosition,
	e.EmployeeStatus, 
	t.IDTeam as'TeamId' from EMPLOYEES e
	join TEAMS t on t.idteam=e.teamid
	where e.idemployee=@employeeID
	and e.EmployeeStatus=1
go
CREATE OR ALTER PROC DeleteEmployee
	@IDEmployee int
AS
BEGIN
update employees
set EmployeeStatus=2
WHERE IDEmployee=@IDEMployee
END
GO
create or alter proc UpdateEmployee
	@employeeID int,
	@Name nvarchar(max),
	@Surname nvarchar(max),
	@EmploymentDate datetime,
	@EmployeeType int,
	@EmployeePosition int,
	@TeamID int
as
	update EMPLOYEES
	set Name = @Name,
		Surname = @Surname,
		EmploymentDate = @EmploymentDate,
		EmployeeType = @EmployeeType,
		EmployeePosition=@EmployeePosition,
		TeamID = @TeamID
	where IDEmployee = @employeeID

go
create or alter proc InsertEmployee
	@Name nvarchar(max),
	@Surname nvarchar(max),
	@EmploymentDate datetime,
	@EmployeeType int,
	@EmployeePosition int,
	@TeamID int
as
	INSERT EMPLOYEES
	(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) 
	VALUES(@Name,@Surname, @EmploymentDate, @EmployeeType,@EmployeePosition, 1,@TeamID)
GO
create or alter proc InsertTeam
	@Name nvarchar(max),
	@TeamLead int
as
begin
	declare @IDTeam int
	INSERT TEAMS
	(Name,TeamStatus) 
	VALUES(@Name,1)
	set @IDTeam=SCOPE_IDENTITY()

	insert TEAMLEAD
	(EmployeeID, TeamID)
	VALUES(@TeamLead,@IDTeam)
end
GO
create or alter proc UpdateClient
	@IDClient int,
	@Name nvarchar(max),
	@OIB nvarchar(max),
	@Address nvarchar(max),
	@ClientStatus int
as
	update CLIENTS
	set Name = @Name,
		OIB = @OIB,
		Address = @Address,
		ClientStatus = @ClientStatus
	where IDClient = @IDClient

go
go
create or alter proc UpdateTeam
	@IDTeam int,
	@Name nvarchar(max),
	@TeamLead int
as
begin
	update TEAMS
	set Name = @Name
	where IDTeam = @IDTeam

	update TEAMLEAD
	set EmployeeID = @TeamLead
	where TeamID=@IDTeam
end
go
create or alter proc InsertClient
	@Name nvarchar(max),
	@OIB nvarchar(max),
	@Address nvarchar(max),
	@ClientStatus int
as
	INSERT CLIENTS
	(Name, OIB, Address, ClientStatus) 
	VALUES(@Name,@OIB, @Address, @ClientStatus)
go
go
CREATE OR ALTER PROC DeactivateClient
	@IDClient int
AS
BEGIN
update CLIENTS
set ClientStatus=0
WHERE IDClient=@IDClient
END
go
CREATE OR ALTER PROC DeactivateTeam
	@IDTeam int
AS
BEGIN
update TEAMS
set TeamStatus=0
WHERE IDTeam=@IDTeam

update EMPLOYEES
set TeamID=1
where TeamID=@IDTeam
END
go
create or alter proc GetTeamLead
	@IDTeam int,
	@IDEmpleyee int output
as
set @IDEmpleyee=(
select top 1 IDEmployee as name from EMPLOYEES
join TEAMLEAD on EMPLOYEES.IDEmployee=TEAMLEAD.EmployeeID
where teamlead.TeamID =@IDTeam)
go
create or alter view ProjectDetails
as
select p.IDProject, p.Name as ProjectName,c.Name as ClientName ,e.Name+' '+e.surname as ProjectLead, p.CreationDate,p.ProjectStatus from projects as p
join clients as c on p.ClientID=c.IDClient
join EMPLOYEES as e on p.ProjectLeadID=e.IDEmployee
go
create or alter proc GetProjectDetails
	@IDProject int
as
select * from ProjectDetails where IDProject = @IDProject
go
create or alter proc GetProjectEmployees
	@IDProject int
as
select e.IDEmployee, e.Surname+' '+e.Name as FullName from EMPLOYEEPROJECT as ep
join EMPLOYEES as e on e.IDEmployee=ep.EmployeeID
join PROJECTS as p on p.IDProject = ep.ProjectID
where ProjectID=@IDProject
go

--####### INSERT DATA #########
INSERT TEAMS(Name, TeamStatus, FoundingDate) VALUES('NONE',1,'1970-01-01')
INSERT TEAMS(Name, TeamStatus, FoundingDate) VALUES('BACKEND',1,'2019-10-10')
INSERT TEAMS(Name, TeamStatus, FoundingDate) VALUES('FRONTEND',1,'2019-10-10')
INSERT TEAMS(Name, TeamStatus, FoundingDate) VALUES('API',1,'2019-12-10')
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus) VALUES('Diša','Boss','2020-01-01',1,1,1)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Josip','Josipović','2015-01-01',1,2,1,2)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Ana','Anić','2014-01-01',1,2,1,3)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Milivoj','Milivojić','2015-01-01',1,2,1,4)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Pero','Perić','2020-01-01',1,3,1,1)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Miro','Mirić','2020-01-01',3,3,1,2)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Iva','Ivić','2020-01-01',3,3,1,1)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Marko','Markoić','2020-01-01',1,3,1,2)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Hrvoje','Horvat','2020-01-01',1,3,1,1)
INSERT EMPLOYEES(Name, Surname, EmploymentDate, EmployeeType, EmployeePosition, EmployeeStatus, TeamID) VALUES('Maja','Majić','2020-01-01',1,3,1,3)
INSERT TEAMLEAD(EmployeeID, TeamID) VALUES(2,2)
INSERT TEAMLEAD(EmployeeID, TeamID) VALUES(3,3)
INSERT TEAMLEAD(EmployeeID, TeamID) VALUES(4,4)
INSERT USERS (UserName, Password, Admin, EmployeeID) VALUES('admin','1234',1,1)
INSERT CLIENTS(Name, OIB,Address,ClientStatus) VALUES('IBM','123456789','Miramarska 10',1)
INSERT PROJECTS(Name, CreationDate, ClientID, ProjectLeadID, ProjectStatus) VALUES('prvi projekt','2020-05-01',1,1,1)
INSERT EMPLOYEEPROJECT(EmployeeID, ProjectID) values(1,1)

