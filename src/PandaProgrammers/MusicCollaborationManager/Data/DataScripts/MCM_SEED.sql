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

INSERT INTO [Comment] (Likes, Message, ListenerID, SpotifyID)
    VALUES
    (10 ,'I like this playlist' ,1 , '0wbYwQItyK648wmeNcqP5z'),
    (20 ,'I dislike this playlist' ,2 , '0wbYwQItyK648wmeNcqP5z'),
    (55 ,'This playlist is average' ,3 , '0wbYwQItyK648wmeNcqP5z');

INSERT INTO [Tutorials] (Link)
    VALUES
    ('https://youtu.be/nd7dKEjJ5To'),
    ('https://youtu.be/Lkak2r-mYPk'),
    ('https://youtu.be/F4Vt4UPWTd8'),
    ('https://youtu.be/Pc8uIPka8_k'),
    ('https://youtu.be/Hpvp9Q2y3WY'),
    ('https://youtu.be/93c6bVf_CjU'),
    ('https://youtu.be/93c6bVf_CjU'),
    ('https://youtu.be/TqFQd9ef_xQ');