ALTER TABLE [Comment] DROP CONSTRAINT [Fk_Comment_Listener_ID];
ALTER TABLE [Comment] DROP CONSTRAINT [Fk_Comment_Playlist_ID];

DROP TABLE [Listener];
DROP TABLE [Comment];
DROP TABLE [Playlist];