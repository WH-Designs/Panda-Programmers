ALTER TABLE [SpotifyAuthorizationNeededListener] DROP CONSTRAINT [Fk_Comment_Listener_ID];
ALTER TABLE [Comment] DROP CONSTRAINT [Fk_Comment_Listener_ID];

DROP TABLE [Listener];
DROP TABLE [SpotifyAuthorizationNeededListener];
DROP TABLE [Comment];
DROP TABLE [Playlist];
DROP TABLE [Polls];
DROP TABLE [Tutorials];
DROP TABLE [Prompts];