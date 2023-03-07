--INSERT INTO [Listener] (FirstName, LastName, FriendID, ASPNetIdentityID, SpotifyID)
--   VALUES
--   ('Chad' ,'Bass' ,1 , '0', '0'),
--   ('Tiffany' ,'Fox' ,2 , '0', '0'),
--   ('Dwight' ,'Morse' ,3 , '0', '0');

INSERT INTO [Playlist] (ServiceID)
    VALUES
    (0),
    (0),
    (0);

INSERT INTO [Comment] (Likes, Message, ListenerID, PlaylistID)
    VALUES
    (10 ,'I like this playlist' ,1 , 1),
    (20 ,'I dislike this playlist' ,2 , 1),
    (55 ,'This playlist is average' ,3 , 2);

INSERT INTO [Theme] (PrimaryColor, SecondaryColor, Font, ListenerID)
    VALUES
    ('dff064' ,'21637f' ,'Times New Roman' ,1),
    ('5f605f' ,'f2f3f9' ,'Arial' ,2),
    ('e88bb4' ,'d5d6cb' ,'Papyrus' ,3);

