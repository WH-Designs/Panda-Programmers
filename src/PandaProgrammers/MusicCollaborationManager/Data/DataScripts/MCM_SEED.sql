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
    ('https://youtube.com/embed/nd7dKEjJ5To'),
    ('https://youtube.com/embed/Lkak2r-mYPk'),
    ('https://youtube.com/embed/F4Vt4UPWTd8'),
    ('https://youtube.com/embed/Pc8uIPka8_k'),
    ('https://youtube.com/embed/Hpvp9Q2y3WY'),
    ('https://youtube.com/embed/93c6bVf_CjU'),
    ('https://youtube.com/embed/l53Eoq2JVT0'),
    ('https://youtube.com/embed/TqFQd9ef_xQ');

INSERT INTO [Prompts] (Prompt)
    VALUES
    ('Using these words {0}, give a detailed description of a music playlist and dont mention any specific artists.'),
    ('Give me a detailed paragraph describing an {0} music playlist and dont mention any specific artists.'),
    ('Using these words {0}, give a detailed description of a {1} music playlist and dont mention any specific artists.'),
    ('Using these description word or words {0}, create a short title for a playlist. Do not return the title in quotes.'),
    ('Using these words {0}, give a general description of the type of music that could be found in a playlist of similar songs and dont mention any specific artists and dont describe specific songs in the playlist.');

    