

using MusicCollaborationManager.ViewModels;

namespace MusicCollaborationManager.Models.DTO
{
    public class RecommendDTO
    {
        public string genre { get; set; }
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

        public RecommendDTO convertToDTO(QuestionViewModel qVM)
        {
            RecommendDTO conDTO = new RecommendDTO();
            conDTO.genre = qVM.genre;
            conDTO.market = "US";
            conDTO.limit = 20;
            
            if(qVM.acousticness <= 0 || qVM.acousticness > 10)
            {
                qVM.acousticness = conDTO.rngValue();
            }
            if (qVM.danceability <= 0 || qVM.danceability > 10)
            {
                qVM.danceability = conDTO.rngValue();
            }
            if (qVM.energy <= 0 || qVM.energy > 10)
            {
                qVM.energy = conDTO.rngValue();
            }
            if (qVM.instrumentalness <= 0 || qVM.instrumentalness > 10)
            {
                qVM.instrumentalness = conDTO.rngValue();
            }
            if (qVM.liveness <= 0 || qVM.liveness > 10)
            {
                qVM.liveness = conDTO.rngValue();
            }
            if (qVM.popularity <= 0 || qVM.popularity > 10)
            {
                qVM.popularity = conDTO.rngValue();
            }
            if (qVM.speechiness <= -20 || qVM.speechiness > 10)
            {
                qVM.speechiness = conDTO.rngValue();
            }
            if (qVM.valence <= 0 || qVM.valence > 10)
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
        public int rngValue()
        {
            Random rnd = new Random();
            int result = rnd.Next(1, 10);
            return result;
        }
    }

}
