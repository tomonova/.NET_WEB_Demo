
--CREATE DATABASE RWA-ERV
--GO
--USE RWA-ERV
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

bulk insert TEAMS
from 'C:\Users\TomoNova\Desktop\tim.csv'
with
(
CODEPAGE = '65001', 
DATAFILETYPE = 'Char',
ROWTERMINATOR='\n',
FIELDTERMINATOR=';'
)

create table CLIENTS
(
	IDClient int not null IDENTITY(1,1),
	Name nvarchar(max) not null,
	OIB nvarchar(max),
	Address nvarchar(max),
	Telephone nvarchar(max),
	Email nvarchar(max),
	ClientStatus int not null,
	constraint PKClients primary key(IDClient)
);
bulk insert CLIENTS
from 'C:\Users\TomoNova\Desktop\client.csv'
with
(
CODEPAGE = '65001', 
DATAFILETYPE = 'Char',
ROWTERMINATOR='\n',
FIELDTERMINATOR=';'
)
go

Create table EMPLOYEES
(
	IDEmployee INT not null IDENTITY(1,1),
	Name nvarchar(max) not null,
	Surname nvarchar(max) not null,
	Email nvarchar(150) unique not null constraint EmployeeMailConstraint check(EMail LIKE '%__@__%.__%') ,
	EmploymentDate DATE not null default (GETDATE()),
	EmployeeType int not null,
	TeamID int,
	EmployeeStatus int not null,
	constraint PKEmployees_tst primary key(IDEmployee),
	constraint FKEmployees_Teams_tst foreign key(TeamID) references TEAMS(IDTeam)
);

bulk insert EMPLOYEES
from 'C:\Users\TomoNova\Desktop\djelatnik_bezzpass.csv'
with
(
CODEPAGE = '65001', 
DATAFILETYPE = 'Char',
ROWTERMINATOR='\n',
FIELDTERMINATOR=';'
)

create table PROJECTS
(
	IDProject int not null IDENTITY(1,1),
	Name nvarchar(max),
	ClientID int,
	CreationDate date not null default(GETDATE()),
	ProjectLeadID int not null,
	ProjectStatus int not null,
	constraint PKProjects primary key(IDProject),
	constraint FKProjects_Clients foreign key(ClientID) references CLIENTS(IDClient),
	constraint FKProjectLead_Employee foreign key(ProjectLeadID) references EMPLOYEES(IDEMployee)
);
bulk insert PROJECTS
from 'C:\Users\TomoNova\Desktop\projekti.csv'
with
(
CODEPAGE = '65001', 
DATAFILETYPE = 'Char',
ROWTERMINATOR='\n',
FIELDTERMINATOR=';'
)

create table USERS
(
	IDUser int not null identity(1,1),
	Username nvarchar(150) not null constraint FKUsersEmployees foreign key(Username) references EMPLOYEES(Email) ON DELETE CASCADE ON UPDATE CASCADE,
	Password varchar(50) not null,
	Admin int not null default(0),
	constraint PKUsers primary key(IDUser)
);

bulk insert USERS
from 'C:\Users\TomoNova\Desktop\users.csv'
with
(
CODEPAGE = '65001', 
DATAFILETYPE = 'Char',
ROWTERMINATOR='\n',
FIELDTERMINATOR=';'
)
update users
set Password=upper(SUBSTRING(master.dbo.fn_varbintohexstr(HashBytes('SHA1', Password)), 3, 50))

create table EMPLOYEEPROJECT
(
	IDEmployeeProject int not null identity(1,1),
	EmployeeID int,
	ProjectID int,
	MemberFrom date not null default(GETDATE()),
	MemberTo date,
	constraint FKEmployeeProject_Employees foreign key (EmployeeID) references EMPLOYEES(IDEmployee),
	constraint FKEmployeeProject_Projects foreign key (ProjectID) references PROJECTS (IDProject)
);

bulk insert EMPLOYEEPROJECT
from 'C:\Users\TomoNova\Desktop\projekt_djelatnik.csv'
with
(
CODEPAGE = '65001', 
DATAFILETYPE = 'Char',
ROWTERMINATOR='\n',
FIELDTERMINATOR=';'
)

create table TEAMLEAD
(
	IDTeamLead int not null IDENTITY(1,1),
	EmployeeID int not null constraint FKTeamLead_Employee references EMPLOYEES(IDEmployee),
	TeamID int not null unique constraint FKTeamLead_Teams references TEAMS(IDTeam),
	constraint PKTeamLead primary key(IDTeamLead)
)

bulk insert TEAMLEAD
from 'C:\Users\TomoNova\Desktop\TeamLead.csv'
with
(
CODEPAGE = '65001', 
DATAFILETYPE = 'Char',
ROWTERMINATOR='\n',
FIELDTERMINATOR=';'
)

create table TIMESHEET
(
	IDTimeSheet int not null IDENTITY(1,1),
	EmployeeID int not null constraint FKTimeSheet_Employee references EMPLOYEES(IDEmployee),
	ProjectID int not null constraint FKTimeSheet_Projects references PROJECTS (IDProject),
	TimeSheeetDate date not null,
	WorkHours int,
	OverTimeHours int,
	constraint PKTimeSheet primary key (IDTimeSheet)
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
GO
create or alter proc CheckCredentials
	@userName nvarchar(50),
	@userPass nvarchar(50),
	@checkOutput int output
as
	if exists(
		select * from USERS
		where UserName = @userName
		and Password = @userPass)
		set @checkOutput = '1'
	else set @checkOutput = '0'
GO
CREATE OR ALTER PROC GetEmployeeAdmin
	@employeeID int
as
	select e.IDemployee,
	e.Name,
	e.Surname,
	e.Email,
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
	@Email nvarchar(max),
	@EmploymentDate datetime,
	@EmployeeType int,
	@TeamID int
as
	update EMPLOYEES
	set Name = @Name,
		Surname = @Surname,
		Email = @Email,
		EmploymentDate = @EmploymentDate,
		EmployeeType = @EmployeeType,
		TeamID = @TeamID
	where IDEmployee = @employeeID

go
create or alter proc InsertEmployee
	@Name nvarchar(max),
	@Surname nvarchar(max),
	@Email nvarchar(max),
	@EmploymentDate datetime,
	@EmployeeType int,
	@TeamID int
as
	INSERT EMPLOYEES
	(Name, Surname, EMail, EmploymentDate, EmployeeType, EmployeeStatus, TeamID) 
	VALUES(@Name,@Surname, @Email,@EmploymentDate, @EmployeeType, 1,@TeamID)
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
left outer join clients as c on p.ClientID=c.IDClient
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
where ProjectID=@IDProject and MemberTo is null
go
go
create or alter proc ProjectStatusChange
	@IDProject int,
	@Status nvarchar(5)
as
if @Status like 'D'
	update Projects
	set ProjectStatus = 3
	where IDProject=@IDProject
else if @Status like 'C'
	update Projects
	set ProjectStatus = 2
	where IDProject=@IDProject
go
create or alter proc InsertProject
	@Name nvarchar(250),
	@ClientID int,
	@UserName nvarchar(25)
as
	declare @ProjectLeadID int

	select @ProjectLeadID=EMPLOYEES.IDEmployee from users
	join EMPLOYEES on users.Username=EMPLOYEES.Email
	where users.UserName=@UserName

	insert into projects (Name, ClientID, ProjectLeadID, ProjectStatus)
	values (@Name,@ClientID,@ProjectLeadID,1)
go
create or alter proc InsertInternalProject
	@Name nvarchar(250),
	@UserName nvarchar(25)
as
	declare @ProjectLeadID int

	select @ProjectLeadID=EMPLOYEES.IDEmployee from users
	join EMPLOYEES on users.Username=EMPLOYEES.Email
	where users.UserName=@UserName

	insert into projects (Name, ProjectLeadID, ProjectStatus)
	values (@Name,@ProjectLeadID,1)
go
create or alter proc GetProjectContributors
	@PorjectID int
as
select e.IDEmployee,e.Name,e.Surname from EMPLOYEEPROJECT ep
inner join EMPLOYEES e on ep.EmployeeID = e.IDEmployee
inner join PROJECTS p on ep.ProjectID = p.IDProject
where ProjectID=@PorjectID and MemberTo is null
go
create or alter proc GetAvailableContributors
	@ProjectID int
as
select distinct( e.IDEmployee),e.Name,e.Surname from EMPLOYEES e
left outer join EMPLOYEEPROJECT ep on e.IDEmployee = ep.EmployeeID
where ep.ProjectID <> @ProjectID or ep.ProjectID IS NULL
go
create type dbo.EmployeeIDList
as table
(
	ID INT
);
go
create or alter procedure ManageContributors
	@EmployeeIDList as EmployeeIDList readonly,
	@ProjectID int
as
begin 

insert into EMPLOYEEPROJECT (EmployeeID)
select ID from @EmployeeIDList

update EMPLOYEEPROJECT
set ProjectID = @ProjectID
where ProjectID is null
end
go

go
create or alter procedure RemoveContributors
	@EmployeeIDList as EmployeeIDList readonly,
	@ProjectID int
as
begin 
update EMPLOYEEPROJECT
set MemberTo = GETDATE()
where ProjectID = @ProjectID 
and EmployeeID in (select ID from @EmployeeIDList)
end
go
create or alter proc CheckEmail
	@Email nvarchar(50),
	@checkOutput int output
as
	if exists(
		select Email from EMPLOYEES
		where Email=@Email)
		set @checkOutput = '1'
	else set @checkOutput = '0'
go
create or alter proc CheckEmailForEmployee
	@Email nvarchar(50),
	@IDEmployee int,
	@checkOutput int output
as
	if exists(
		select Email from EMPLOYEES
		where Email=@Email and IDEmployee=@IDEmployee)
		set @checkOutput = '1'
	else if exists (
		select Email from EMPLOYEES
		where Email=@Email)
	set @checkOutput = '2'
	else 
	set @checkOutput = '0'
GO