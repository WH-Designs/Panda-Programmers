# Requirements Workup

## Elicitation

1. Is the goal or outcome well defined?  Does it make sense?
    * Yes, we have clear needs defined in our vision statement for the project and clear goals for what a successful project would be. The needs our project will fill are not only things that we would want, but we also believe the needs of many other Spotify users. We have
    both features that would be new to Spotify and features that are improvements to what Spotify already has. 

2. What is not clear from the given description?
    * The only thing not clear in the description is that our features don't have a clear priority to them. Also our smaller features where we are improving upon what spotify already has don't have a clear description of what they are going to be. 

3. How about scope?  Is it clear what is included and what isn't?
    * Our main scope, which includes Spotify, is clear on what it is. The things that are not included inside of our scope are the music sources outside of Spotify, i.e. Youtube Music or Apple Music. Those other sources are well defined, yet we are still unsure if we will have enough time to fully deliever them to our users.

4. What do you not understand?
    * Technical domain knowledge
        * Connecting the API services to our program 
        * Functionality of the algorithms that our project will use

    * Business domain knowledge
        * The pricing model for the two AI API's price model and token system that we are going to use 

5. Is there something missing?
    * We do not have anything missing that we can see at this point in the project. 
    
6. Get answers to these questions.
    

## Analysis

Go through all the information gathered during the previous round of elicitation.  

1. For each attribute, term, entity, relationship, activity ... precisely determine its bounds, limitations, types and constraints in both form and function.  Write them down.
    * >Playlist Generator
        * Questionaire
            * Questions within
                * Multiple Choice
                * Drop Down
                * Ten questions ♾️
            * Open ended prompt
                * for description
                * for playlist cover
        * Algoirthm 
            * Uses input from questionaire
            * Calulates from input and sends to API
            * GETs information from API 
        * Spotify API
            * GETs information: tracks for the new playlist with the calculation from the algorithm 
            * Places new playlist into the account of the user
    * >Music Player
        * Spotify API
            * Use Spotify player to plays songs retrieved from API
            * Use Spotify player to play playlists retrieved from API
        * Embedded player
            * Allows user to play songs they uploaded from local
    * >Collaboritive Playlists
        * Allowing users to make playlists together using local music and music from spotify
    * >Spotify Profile Authentication and Display
        * Spotify API
            * Connect to users Spotify account 
            * GET recommendations for user
        * Display
            * Display recommendations for a user
            * Displays users account page, home page, playlist page, ...
    * >User Interface Customizability
        * Edit the widgets
            * Location, size and color
    * >Next Song/Playlist Voting System
        * Voting Interface
            * Connect to a public places account
            * Vote to play next song or skip 
            * QR Code that links to public player
    * >Access Music from other services
        * Youtube API
        * Apple Music API
    * >Friend and Follower System
        * Be able to follow artists and othe users
            * Determine if users are friends
            * See updates on friends profiles
        * Be able to follow certain playlists
            * See updates to that playlist

2. Do they work together or are there some conflicting requirements, specifications or behaviors?
    * The different API's have different End points and could add confliction
    * Will the voting page have the same customizations as the playlist users page? <-- set to default on the qr code page?
    * A lot of the features work along side and somewhat depend on the Spotify Authentication and Display feature
    * Are the collaborative playlists between friends only?

3. Have you discovered if something is missing?  
    * There were some things missing, but the team fixed those.

4. Return to Elicitation activities if unanswered questions remain.


## Design and Modeling
Our first goal is to create a **data model** that will support the initial requirements.

1. Identify all entities;  for each entity, label its attributes; include concrete types
2. Identify relationships between entities.  Write them out in English descriptions.
3. Draw these entities and relationships in an _informal_ Entity-Relation Diagram.
4. If you have questions about something, return to elicitation and analysis before returning here.

## Analysis of the Design
The next step is to determine how well this design meets the requirements _and_ fits into the existing system.

1. Does it support all requirements/features/behaviors?
    * Yes, we believe it can be done and currently with our data model 
2. Does it meet all non-functional requirements?
    * It meets all the non-functional requirements we currently have
