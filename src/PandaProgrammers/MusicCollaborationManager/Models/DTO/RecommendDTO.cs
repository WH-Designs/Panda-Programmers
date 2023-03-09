

using MusicCollaborationManager.ViewModels;

namespace MusicCollaborationManager.Models.DTO
{
    public class RecommendDTO
    {
        public List<string> genre = new List<string> { };
        public string market { get; set; }
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
            RecommendDTO conDTO = new RecommendDTO();
            conDTO.genre.Add(qVM.genre);
            conDTO.market = "US";
            conDTO.limit = 20;
            
            if(qVM.acousticness <= 0 || qVM.acousticness > 10 || qVM.acousticness == null)
            {
                qVM.acousticness = conDTO.rngValue();
            }
            if (qVM.danceability <= 0 || qVM.danceability > 10 || qVM.danceability == null)
            {
                qVM.danceability = conDTO.rngValue();
            }
            if (qVM.energy <= 0 || qVM.energy > 10 || qVM.energy == null)
            {
                qVM.energy = conDTO.rngValue();
            }
            if (qVM.instrumentalness <= 0 || qVM.instrumentalness > 10 || qVM.instrumentalness == null)
            {
                qVM.instrumentalness = conDTO.rngValue();
            }
            if (qVM.liveness <= 0 || qVM.liveness > 10 || qVM.liveness == null)
            {
                qVM.liveness = conDTO.rngValue();
            }
            if (qVM.popularity <= 0 || qVM.popularity > 10 || qVM.popularity == null)
            {
                qVM.popularity = conDTO.rngValue();
            }
            if (qVM.speechiness <= 0 || qVM.speechiness > 10 || qVM.speechiness == null)
            {
                qVM.speechiness = conDTO.rngValue();
            }
            if (qVM.valence <= 0 || qVM.valence > 10 || qVM.valence == null)
            {
                qVM.valence = conDTO.rngValue();
            }

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
            RecommendDTO conDTO = new RecommendDTO();
            
            switch(mVM.mood)
            {
                case "Happy":
                    conDTO.genre.Add("pop");
                    conDTO.genre.Add("acoustic");
                    conDTO.genre.Add("happy");
                    conDTO.genre.Add("hip-hop");
                    conDTO.genre.Add("reggae");
                    conDTO.market = "US";
                    conDTO.limit = 4;

                    conDTO.target_acousticness = conDTO.rngValue();
                    conDTO.target_acousticness /= 10;
                    conDTO.target_danceability = conDTO.rngValueInput(5, 10);
                    conDTO.target_danceability /= 10;
                    conDTO.target_liveness = conDTO.rngValueInput(1, 3);
                    conDTO.target_liveness /= 10;
                    conDTO.target_energy = conDTO.rngValueInput(5, 10);
                    conDTO.target_energy /= 10;
                    conDTO.target_popularity = conDTO.rngValueInput(4, 10);
                    conDTO.target_popularity *= 10;
                    conDTO.target_speechiness = conDTO.rngValueInput(4, 10);
                    conDTO.target_speechiness /= 10;
                    conDTO.target_tempo = conDTO.rngValueInput(70, 150);
                    conDTO.target_valence = conDTO.rngValueInput(7, 10);
                    conDTO.target_valence/= 10;

                    break;
                case "Angry":
                    conDTO.genre.Add("death-metal");
                    conDTO.genre.Add("emo");
                    conDTO.genre.Add("hardcore");
                    conDTO.genre.Add("punk-rock");
                    conDTO.genre.Add("heavy-metal");
                    conDTO.market = "US";
                    conDTO.limit = 4;

                    conDTO.target_energy = conDTO.rngValueInput(7, 10);
                    conDTO.target_energy /= 10;
                    conDTO.target_liveness = conDTO.rngValueInput(1, 3);
                    conDTO.target_liveness /= 10;
                    conDTO.target_instrumentalness = conDTO.rngValueInput(4, 10);
                    conDTO.target_instrumentalness /= 10;
                    conDTO.target_popularity = conDTO.rngValueInput(5, 10);
                    conDTO.target_popularity *= 10;
                    conDTO.target_speechiness = conDTO.rngValueInput(5, 10);
                    conDTO.target_speechiness /= 10;
                    conDTO.target_tempo = conDTO.rngValueInput(120, 250);
                    conDTO.target_valence = conDTO.rngValueInput(1, 3);
                    conDTO.target_valence /= 10;

                    break;
                case "Sad":
                    conDTO.genre.Add("sad");
                    conDTO.genre.Add("country");
                    conDTO.genre.Add("blues");
                    conDTO.genre.Add("acoustic");
                    conDTO.genre.Add("emo");
                    conDTO.market = "US";
                    conDTO.limit = 4;

                    conDTO.target_acousticness = conDTO.rngValue();
                    conDTO.target_acousticness /= 10;
                    conDTO.target_instrumentalness = conDTO.rngValue();
                    conDTO.target_instrumentalness /= 10;
                    conDTO.target_liveness = conDTO.rngValueInput(1, 3);
                    conDTO.target_liveness /= 10;
                    conDTO.target_popularity = conDTO.rngValueInput(4, 10);
                    conDTO.target_popularity *= 10;
                    conDTO.target_speechiness = conDTO.rngValue();
                    conDTO.target_speechiness /= 10;
                    conDTO.target_valence = conDTO.rngValueInput(1, 3);
                    conDTO.target_valence /= 10;

                    break;
                case "Calming":
                    conDTO.genre.Add("classical");
                    conDTO.genre.Add("chill");
                    conDTO.genre.Add("jazz");
                    conDTO.genre.Add("ambient");
                    conDTO.genre.Add("study");
                    conDTO.market = "US";
                    conDTO.limit = 4;

                    conDTO.target_energy = conDTO.rngValueInput(1, 5);
                    conDTO.target_energy /= 10;
                    conDTO.target_acousticness = conDTO.rngValue();
                    conDTO.target_acousticness /= 10;
                    conDTO.target_instrumentalness = conDTO.rngValue();
                    conDTO.target_instrumentalness /= 10;
                    conDTO.target_liveness = conDTO.rngValueInput(1, 3);
                    conDTO.target_liveness /= 10;
                    conDTO.target_popularity = conDTO.rngValueInput(4, 10);
                    conDTO.target_popularity *= 10;
                    conDTO.target_tempo = conDTO.rngValueInput(30, 90);

                    break;
                case "Energetic":
                    conDTO.genre.Add("work-out");
                    conDTO.genre.Add("rock-n-roll");
                    conDTO.genre.Add("pop");
                    conDTO.genre.Add("hip-hop");
                    conDTO.genre.Add("metal");
                    conDTO.market = "US";
                    conDTO.limit = 4;

                    conDTO.target_danceability = conDTO.rngValueInput(5, 10);
                    conDTO.target_danceability /= 10;
                    conDTO.target_energy = conDTO.rngValueInput(8, 10);
                    conDTO.target_energy /= 10;
                    conDTO.target_liveness = conDTO.rngValueInput(1, 2);
                    conDTO.target_liveness /= 10;
                    conDTO.target_popularity = conDTO.rngValueInput(4, 10);
                    conDTO.target_popularity *= 10;
                    conDTO.target_tempo = conDTO.rngValueInput(110, 250);
                    conDTO.target_valence = conDTO.rngValueInput(7, 10);
                    conDTO.target_valence /= 10;

                    break;
                case "Dancing":
                    conDTO.genre.Add("salsa");
                    conDTO.genre.Add("tango");
                    conDTO.genre.Add("dance");
                    conDTO.genre.Add("disco");
                    conDTO.genre.Add("hip-hop");
                    conDTO.market = "US";
                    conDTO.limit = 4;

                    conDTO.target_danceability = conDTO.rngValueInput(8, 10);
                    conDTO.target_danceability /= 10;
                    conDTO.target_energy = conDTO.rngValueInput(6, 10);
                    conDTO.target_energy /= 10;
                    conDTO.target_liveness = conDTO.rngValueInput(1, 3);
                    conDTO.target_liveness /= 10;
                    conDTO.target_popularity = conDTO.rngValueInput(4, 10);
                    conDTO.target_popularity *= 10;
                    conDTO.target_valence = conDTO.rngValueInput(5, 10);
                    conDTO.target_valence /= 10;

                    break;
            }
            return conDTO;            

        }

        public int rngValue()
        {
            Random rnd = new Random();
            int result = rnd.Next(1, 10);
            return result;
        }

        public int rngValueInput(int min, int max)
        {
            Random rnd = new Random();
            int result = rnd.Next(min, max);
            return result;
        }
    }

}
