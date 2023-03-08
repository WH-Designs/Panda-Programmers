INSERT INTO [Listener] (FirstName, LastName, FriendID, ASPNetIdentityID, SpotifyID)
    VALUES
    ('Chad' ,'Bass' ,1 , '4b7959dc-2e9f-4fa9-ad38-d49ea70c8d32', '0'),
    ('Tiffany' ,'Fox' ,2 , '52f959dc-3e9a-4fa9-ad38-d49ea7448d32', '0'),
    ('Dwight' ,'Morse' ,3 , '4b7959dc-2e9f-4fa9-ad38-d49ea70c8d32', '0');

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