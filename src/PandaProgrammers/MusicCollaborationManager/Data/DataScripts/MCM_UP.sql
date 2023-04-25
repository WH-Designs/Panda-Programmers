--CREATE DATABASE MCMDatabase;

CREATE TABLE [Listener] (
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [FirstName]         nvarchar(64)    NOT NULL,
    [LastName]          nvarchar(64)    NOT NULL,
    [FriendID]          int             NOT NULL,
    [ASPNetIdentityID]  nvarchar(64)    NOT NULL,
    [Theme]             nvarchar(255)   NULL,
    [SpotifyID]         nvarchar(128)   NULL,
    [AuthToken]         nvarchar(512)   NULL,
    [AuthRefreshToken]  nvarchar(512)   NULL
);

CREATE TABLE [Comment] (
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [Likes]             int             NOT NULL,
    [Message]           nvarchar(300)   NOT NULL,
    [ListenerID]        int             NOT NULL,
    [PlaylistID]        int             NOT NULL
);

CREATE TABLE [Playlist] (
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [ServiceID]         int             NOT NULL,
);

ALTER TABLE [Comment] ADD CONSTRAINT [Fk_Comment_Listener_ID]
    FOREIGN KEY ([ListenerID]) REFERENCES [Listener] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Comment] ADD CONSTRAINT [Fk_Comment_Playlist_ID]
    FOREIGN KEY ([PlaylistID]) REFERENCES [Playlist] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;



