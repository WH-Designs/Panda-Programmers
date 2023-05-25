--CREATE DATABASE MCMDatabase;

CREATE TABLE [Listener] (
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [FirstName]         nvarchar(64)    NOT NULL,
    [LastName]          nvarchar(64)    NOT NULL,
    [FriendID]          int             NOT NULL,
    [ASPNetIdentityID]  nvarchar(64)    NOT NULL,
    [SearchConsentFlag] bit             NOT NULL,
    [Theme]             nvarchar(255)   NULL,
    [SpotifyID]         nvarchar(128)   NULL,
    [SpotifyUserName]   nvarchar(128)   NULL,
    [AuthToken]         nvarchar(512)   NULL,
    [AuthRefreshToken]  nvarchar(512)   NULL
);

CREATE TABLE [SpotifyAuthorizationNeededListener] (
    [ID]                int             PRIMARY KEY IDENTITY (1, 1),
    [Email]             nvarchar(256)   NOT NULL,
    [Name]              nvarchar(64)    NOT NULL,
    [ListenerID]        int             NOT NULL
);

CREATE TABLE [Comment] (
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [Likes]             int             NOT NULL,
    [Message]           nvarchar(300)   NOT NULL,
    [ListenerID]        int             NOT NULL,
    [SpotifyID]         nvarchar(128)   NOT NULL
);

CREATE TABLE [Playlist] (
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [ServiceID]         int             NOT NULL,
);

CREATE TABLE [Polls](
     [ID]                   int             PRIMARY KEY IDENTITY(1, 1),
     [PollID]               nvarchar(64)    NOT NULL,
     [SpotifyPlaylistID]    nvarchar(64)    NOT NULL,
     [SpotifyTrackUri]      nvarchar(64)    NOT NULL
);

CREATE TABLE [Tutorials] (
    [ID]                int                 PRIMARY KEY IDENTITY(1, 1),
    [Link]              nvarchar(512)       NOT NULL,
);

CREATE TABLE [Prompts] (
    [ID]                int                 PRIMARY KEY IDENTITY(1, 1),
    [Prompt]            nvarchar(512)       NOT NULL,
);

ALTER TABLE [SpotifyAuthorizationNeededListener] ADD CONSTRAINT [Fk_SpotifyAuthorizationNeededListener_Listener_ID]
    FOREIGN KEY ([ListenerID]) REFERENCES [Listener] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Comment] ADD CONSTRAINT [Fk_Comment_Listener_ID]
    FOREIGN KEY ([ListenerID]) REFERENCES [Listener] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;



