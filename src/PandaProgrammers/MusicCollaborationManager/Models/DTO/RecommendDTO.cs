

using Microsoft.IdentityModel.Tokens;
using MusicCollaborationManager.Utilities;
using MusicCollaborationManager.ViewModels;
using SpotifyAPI.Web;
using System.Drawing;

namespace MusicCollaborationManager.Models.DTO
{
    public class RecommendDTO
    {
        public List<string> genre = new List<string> { };
        public List<string> seed = new List<string> { };
        public List<string> artistSeed = new List<string> { };
        public string market = "US";
        public int limit { get; set; }
        public double target_acousticness { get; set; } 
        public double target_danceability { get; set; }
        public double target_energy { get; set; }
        public double target_instrumentalness { get; set; }
        public double target_liveness { get; set; }
        public double target_loudness { get; set; }
        public int target_popularity { get; set; }
        public double target_speechiness { get; set; }
        public double target_tempo { get; set; }
        public double target_valence { get; set; }

        public RecommendDTO convertToQuestionDTO(QuestionViewModel qVM)
        {
            GeneratorUtilities utility = new GeneratorUtilities();
            RecommendDTO conDTO = new RecommendDTO();
            conDTO.genre.Add(qVM.genre);
            conDTO.market = "US";
            conDTO.limit = 20;
            
            //RNG if value not provided 
            if(qVM.acousticness <= 0 || qVM.acousticness > 10 || qVM.acousticness == null){qVM.acousticness = utility.rngValue();}
            if (qVM.danceability <= 0 || qVM.danceability > 10 || qVM.danceability == null){qVM.danceability = utility.rngValue();}
            if (qVM.energy <= 0 || qVM.energy > 10 || qVM.energy == null){qVM.energy = utility.rngValue();}
            if (qVM.instrumentalness <= 0 || qVM.instrumentalness > 10 || qVM.instrumentalness == null){qVM.instrumentalness = utility.rngValue();}
            if (qVM.liveness <= 0 || qVM.liveness > 10 || qVM.liveness == null){qVM.liveness = utility.rngValue();}
            if (qVM.popularity <= 0 || qVM.popularity > 10 || qVM.popularity == null){qVM.popularity = utility.rngValue();}
            if (qVM.speechiness <= 0 || qVM.speechiness > 10 || qVM.speechiness == null){qVM.speechiness = utility.rngValue();}
            if (qVM.valence <= 0 || qVM.valence > 10 || qVM.valence == null) { qVM.valence = utility.rngValue();}

            //Converts to format accepted by api
            conDTO.target_acousticness = qVM.acousticness / 10;
            conDTO.target_danceability= qVM.danceability / 10;
            conDTO.target_energy = qVM.energy / 10;
            conDTO.target_instrumentalness = qVM.instrumentalness / 10;
            conDTO.target_liveness = qVM.liveness / 10;
            conDTO.target_popularity = qVM.popularity * 10;
            conDTO.target_speechiness = qVM.speechiness / 10;            
            conDTO.target_tempo= qVM.tempo;
            conDTO.target_valence= qVM.valence / 10;

            return conDTO;
        }

        public RecommendDTO convertToMoodDTO(MoodViewModel mVM)
        {
            GeneratorUtilities utility = new GeneratorUtilities();
            mVM.mood = mVM.moodList[int.Parse(mVM.mood) - 1];
            RecommendDTO conDTO = new RecommendDTO();
            conDTO.market = "US";
            //Sets values within certain params using rng and formats for api
            switch (mVM.mood)
            {
                case "Happy":
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    string genreHolder = mVM.happyGenreList[utility.rngValueInput(0, 10)];
                    //    if (!conDTO.genre.Contains(genreHolder))
                    //    {
                    //        conDTO.genre.Add(genreHolder);
                    //    }
                    //    else
                    //    {
                    //        i--;
                    //    }
                    //}
                    conDTO.genre.Add("happy");
                    //conDTO.market = "US";
                    conDTO.limit = 20;

                    conDTO.target_acousticness = utility.rngValue();
                    conDTO.target_acousticness /= 10;
                    conDTO.target_danceability = utility.rngValue();
                    conDTO.target_danceability /= 10;
                    conDTO.target_energy = utility.rngValueInput(3, 11);
                    conDTO.target_energy /= 10;
                    conDTO.target_speechiness = utility.rngValueInput(3, 11);
                    conDTO.target_speechiness /= 10;
                    conDTO.target_popularity = utility.rngValue();
                    conDTO.target_popularity *= 10;
                    conDTO.target_tempo = utility.rngValueInput(60, 170);
                    conDTO.target_valence = utility.rngValueInput(7, 11);
                    conDTO.target_valence /= 10;


                    break;

                case "Angry":
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    string genreHolder = mVM.angryGenreList[utility.rngValueInput(0, 5)];
                    //    if (!conDTO.genre.Contains(genreHolder))
                    //    {
                    //        conDTO.genre.Add(genreHolder);
                    //    }
                    //    else
                    //    {
                    //        i--;
                    //    }
                    //}
                    conDTO.genre.Add("death-metal");
                    //conDTO.market = "US";
                    conDTO.limit = 20;

                    conDTO.target_energy = utility.rngValueInput(7, 11);
                    conDTO.target_energy /= 10;
                    conDTO.target_tempo = utility.rngValueInput(150, 250);
                    conDTO.target_valence = utility.rngValueInput(1, 3);
                    conDTO.target_valence /= 10;
                    break;

                case "Sad":
                    //for (int i = 0; i < 1; i++)
                    //{
                    //    string genreHolder = mVM.sadGenreList[utility.rngValueInput(0, 1)];
                    //    if (!conDTO.genre.Contains(genreHolder))
                    //    {
                    //        conDTO.genre.Add(genreHolder);
                    //    }
                    //    else
                    //    {
                    //        i--;
                    //    }
                    //}
                    conDTO.genre.Add("sad");
                    //conDTO.market = "US";
                    conDTO.limit = 20;

                    conDTO.target_energy = utility.rngValueInput(1, 5);
                    conDTO.target_energy /= 10;
                    conDTO.target_valence = utility.rngValueInput(1, 4);
                    conDTO.target_valence /= 10;
                    conDTO.target_tempo = utility.rngValueInput(20, 80);
                    break;

                case "Chill":
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    string genreHolder = mVM.calmGenreList[utility.rngValueInput(0, 7)];
                    //    if (!conDTO.genre.Contains(genreHolder))
                    //    {
                    //        conDTO.genre.Add(genreHolder);
                    //    }
                    //    else
                    //    {
                    //        i--;
                    //    }
                    //}
                    conDTO.genre.Add("sleep");
                    conDTO.limit = 20;

                    conDTO.target_energy = utility.rngValueInput(1, 4);
                    conDTO.target_energy /= 10;
                    conDTO.target_tempo = utility.rngValueInput(10, 70);
                    break;

                case "Energetic":
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    string genreHolder = mVM.energyGenreList[utility.rngValueInput(0, 11)];
                    //    if (!conDTO.genre.Contains(genreHolder))
                    //    {
                    //        conDTO.genre.Add(genreHolder);
                    //    }
                    //    else
                    //    {
                    //        i--;
                    //    }
                    //}
                    conDTO.genre.Add("intenseworkout");
                    //conDTO.market = "US";
                    conDTO.limit = 20;

                    conDTO.target_energy = utility.rngValueInput(8, 11);
                    conDTO.target_energy /= 10;
                    conDTO.target_tempo = utility.rngValueInput(130, 200);
                    break;

                case "Dancing":
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    string genreHolder = mVM.danceGenreList[utility.rngValueInput(0, 9)];
                    //    if (!conDTO.genre.Contains(genreHolder))
                    //    {
                    //        conDTO.genre.Add(genreHolder);
                    //    }
                    //    else
                    //    {
                    //        i--;
                    //    }
                    //}
                    conDTO.genre.Add("dance");
                    //conDTO.market = "US";
                    conDTO.limit = 20;

                    conDTO.target_danceability = utility.rngValueInput(6, 11);
                    conDTO.target_danceability /= 10;
                    conDTO.target_popularity = utility.rngValueInput(1, 11);
                    conDTO.target_popularity *= 10;
                    conDTO.target_valence = utility.rngValueInput(5, 11);
                    conDTO.target_valence /= 10;
                    break;
            }
            return conDTO;

        }

        public RecommendDTO convertToTimeDTO(TimeViewModel tVM)
        {
            RecommendDTO conDTO = new RecommendDTO();
            GeneratorUtilities utility = new GeneratorUtilities();
            conDTO.market = "US";

            if (tVM.timeCategory == null)
            {
                throw new Exception("Category was empty");
            }

            if(tVM.timeCategory == "workDay")
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    string genreHolder = tVM.workGenres[utility.rngValueInput(0, 7)];
                //    if (!conDTO.genre.Contains(genreHolder))
                //    {
                //        conDTO.genre.Add(genreHolder);
                //    }
                //    else
                //    {
                //        i--;
                //    }
                //}
                conDTO.genre.Add("study focus");
                conDTO.limit = 20;

                conDTO.target_energy = utility.rngValueInput(1, 4);
                conDTO.target_energy /= 10;
                conDTO.target_tempo = utility.rngValueInput(40, 100);

            }
            else if (tVM.timeCategory == "workMorning")
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    string genreHolder = tVM.exerciseGenres[utility.rngValueInput(0, 11)];
                //    if (!conDTO.genre.Contains(genreHolder))
                //    {
                //        conDTO.genre.Add(genreHolder);
                //    }
                //    else
                //    {
                //        i--;
                //    }
                //}
                conDTO.genre.Add("intenseworkout");
                //conDTO.market = "US";
                conDTO.limit = 20;

                conDTO.target_energy = utility.rngValueInput(7, 11);
                conDTO.target_energy /= 10;
                conDTO.target_tempo = utility.rngValueInput(100, 250);
            }
            else if (tVM.timeCategory == "endMorning" || tVM.timeCategory == "friEvening" || tVM.timeCategory == "endEvening")
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    string genreHolder = tVM.partyGenres[utility.rngValueInput(0, 10)];
                //    if (!conDTO.genre.Contains(genreHolder))
                //    {
                //        conDTO.genre.Add(genreHolder);
                //    }
                //    else
                //    {
                //        i--;
                //    }
                //}
                conDTO.genre.Add("party");
                conDTO.limit = 20;

                conDTO.target_danceability = utility.rngValueInput(7, 11);
                conDTO.target_danceability /= 10;
                conDTO.target_energy = utility.rngValueInput(7, 11);
                conDTO.target_energy /= 10;
                conDTO.target_valence = utility.rngValueInput(5, 11);
                conDTO.target_valence /= 10;
            }
            else if (tVM.timeCategory == "workEvening")
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    string genreHolder = tVM.chillGenres[utility.rngValueInput(0, 6)];
                //    if (!conDTO.genre.Contains(genreHolder))
                //    {
                //        conDTO.genre.Add(genreHolder);
                //    }
                //    else
                //    {
                //        i--;
                //    }
                //}
                conDTO.genre.Add("relaxing");
                conDTO.limit = 20;

                conDTO.target_energy = utility.rngValueInput(1, 5);
                conDTO.target_energy /= 10;
                conDTO.target_tempo = utility.rngValueInput(10, 80);
            }
            else if (tVM.timeCategory == "bedTime")
            {
                //for (int i = 0; i < 3; i++)
                //{
                //    string genreHolder = tVM.bedGenres[utility.rngValueInput(0, 3)];
                //    if (!conDTO.genre.Contains(genreHolder))
                //    {
                //        conDTO.genre.Add(genreHolder);
                //    }
                //    else
                //    {
                //        i--;
                //    }
                //}
                conDTO.genre.Add("sleep");
                conDTO.limit = 20;

                conDTO.target_energy = utility.rngValueInput(1, 4);
                conDTO.target_energy /= 10;
                conDTO.target_tempo = utility.rngValueInput(10, 80);
            }
            else if (tVM.timeCategory == "sunDay" || tVM.timeCategory == "endDay")
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    string genreHolder = tVM.upbeatGenres[utility.rngValueInput(0, 10)];
                //    if (!conDTO.genre.Contains(genreHolder))
                //    {
                //        conDTO.genre.Add(genreHolder);
                //    }
                //    else
                //    {
                //        i--;
                //    }
                //}
                conDTO.genre.Add("happy");
                conDTO.limit = 20;

                conDTO.target_acousticness = utility.rngValue();
                conDTO.target_acousticness /= 10;
                conDTO.target_danceability = utility.rngValue();
                conDTO.target_danceability /= 10;
                conDTO.target_energy = utility.rngValueInput(3, 11);
                conDTO.target_energy /= 10;
                conDTO.target_speechiness = utility.rngValueInput(3, 11);
                conDTO.target_speechiness /= 10;
                conDTO.target_popularity = utility.rngValue();
                conDTO.target_popularity *= 10;
                conDTO.target_tempo = utility.rngValueInput(60, 170);
                conDTO.target_valence = utility.rngValueInput(7, 11);
                conDTO.target_valence /= 10;
            }

            return conDTO;
        }
        
    }

}
