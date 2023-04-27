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

INSERT INTO [Comment] (Likes, Message, ListenerID, SpotifyID)
    VALUES
    (10 ,'I like this playlist' ,1 , '0wbYwQItyK648wmeNcqP5z'),
    (20 ,'I dislike this playlist' ,2 , '0wbYwQItyK648wmeNcqP5z'),
    (55 ,'This playlist is average' ,3 , '0wbYwQItyK848wyeNcqP5z');