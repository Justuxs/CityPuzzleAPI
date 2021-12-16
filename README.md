------------------------------------------------------Running CityPuzzleAPI for CityPuzzle app---------------------------------------------------
1)	Clone https://github.com/Justuxs/CityPuzzleAPI github repo to Visual Studio
2)	Run / Build solution with 'IIS Express' option. This will create applicationhost.config
3)	Go to CityPuzzleAPI file path. It should look something like this: *\source\repos\CityPuzzleAPI
4)	Then go to *\CityPuzzleAPI\.vs\CityPuzzleAPI\config and open applicationhost.config
     If .vs folder is not visible you have to anable hidden files in file explorers tool bar View tab
5) Inside the config file edit bindings by adding binding protocol. This should be somewhere around 155 line
     Make sure you have these binding protocols:
          <binding protocol="http" bindingInformation=":8080:localhost" />
          <binding protocol="http" bindingInformation=":8080:127.0.0.1" />
6) Edit more bindings somewhere around 163 line. 
     Make sure you have these binding protocols:
          <binding protocol="http" bindingInformation="*:26790:localhost" />
          <binding protocol="http" bindingInformation="*:26790:127.0.0.1" />
          <binding protocol="http" bindingInformation="*:26790:86.38.160.86" />
7) Change ConnectionString in CityPuzzleApi\Model\CityPuzzleContex into your data base connString. This would be your local db connString for testing
8) Run solution with 'CityPuzzleAPI' run option to use it

------------------------------------------------------------Changing data base connString--------------------------------------------------------

DB connString can be changed remotely using Swagger UI opended by CityPuzzleAPI. Using post command api/ChangeConectionString.
Press 'Post' then 'try out' then provide new connString and token in given json format. Finally press 'Execute' and if no errors are returned the connString to DB should have changed.
     Note: currently the only valid token is 'CityPuzzle'
     
--------------------------------------------------------Creating local data base for testing-----------------------------------------------------
1) Follow https://docs.microsoft.com/en-us/visualstudio/data-tools/create-a-sql-database-by-using-a-designer?view=vs-2022 tutorial to create a local data base with Visual Studio
     Note: default ConnectionString uses data base named 'LocalCityPuzzleDB' so if you name your DB the same 7) step in running CityPuzzleAPI is not necessary
3) Add these exact tables to the data base:
     CREATE TABLE [dbo].[Participants] (
         [RoomID]		INT	NOT NULL,
         [UserID]		INT	NOT NULL,
         CONSTRAINT [PK_Participants] 
             PRIMARY KEY CLUSTERED ([RoomID] ASC, [UserID] ASC)
     );

     CREATE TABLE [dbo].[Puzzles] (
         [Id]		INT		IDENTITY (1, 1) NOT NULL,
         [Name]		NVARCHAR (MAX)	NOT NULL,
         [About]		NVARCHAR (MAX)	NULL,
         [Quest]		NVARCHAR (MAX)	NULL,
         [Latitude]	FLOAT (53)		NULL,
         [Longitude]	FLOAT (53)		NULL,
         [ImgAdress]	NVARCHAR (MAX)	NULL,
         PRIMARY KEY CLUSTERED ([Id] ASC)
     );

     CREATE TABLE [dbo].[Rooms] (
         [Id]		INT		IDENTITY (1, 1) NOT NULL,
         [RoomPin]	VARCHAR (MAX)	NULL,
         [Owner]		INT		NULL,
         [RoomSize]	INT		NULL,
         PRIMARY KEY CLUSTERED ([Id] ASC)
     );

     CREATE TABLE [dbo].[RoomTasks] (
         [RoomID]		INT	NOT NULL,
         [PuzzleID]	INT	NOT NULL,
         PRIMARY KEY CLUSTERED ([RoomID] ASC, [PuzzleID] ASC)
     );

     CREATE TABLE [dbo].[Tasks] (
         [UserID]   INT	NOT NULL,
         [PuzzleID] INT	NOT NULL,
         PRIMARY KEY CLUSTERED ([UserID] ASC, [PuzzleID] ASC)
     );

     CREATE TABLE [dbo].[Users] (
         [Id]		INT		IDENTITY (1, 1) NOT NULL,
         [UserName]	NVARCHAR (MAX)	NULL,
         [FirstName]	NVARCHAR (MAX)	NULL,
         [LastName]	NVARCHAR (MAX)	NULL,
         [Pass]		NVARCHAR (MAX)	NULL,
         [Email]		NVARCHAR (MAX)	NULL,
         [MaxQuestDistance]	INT		NULL,
         PRIMARY KEY CLUSTERED ([Id] ASC)
     );

     CREATE TABLE [dbo].[CompletedPuzzles] (
         [CompletedTaskId]	INT	IDENTITY (1, 1) NOT NULL,
         [UserID]		INT 	NOT NULL,
         [PuzzleID]	INT 	NOT NULL,
         [Score]		INT 	NOT NULL,
         PRIMARY KEY CLUSTERED ([CompletedTaskId] ASC)
     );
