using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MusicCollaborationManager.Models.DTO
{
    public class OptionInfoDTO
    {
        public string OptionText { get; set; }
        public int OptionCount { get; set; }
        public string OptionID { get; set; }

        public OptionInfoDTO()
        {

        }

        public static IEnumerable<OptionInfoDTO> FromJSON(object? obj)
        {
            JObject? jObject = null;
            try
            {
                jObject = JObject.Parse((string)obj);
            }
            catch (JsonReaderException)
            {
                Debug.WriteLine("Error parsing JSON. (OptionInfoDTO)");
            }
            if (jObject != null)
            {
                IEnumerable<OptionInfoDTO> OptionDetails = jObject["data"]["options"].Select(option => new OptionInfoDTO()
                {
                    OptionText = (string)option["text"],
                    OptionCount = (int)option["votes_count"],
                    OptionID = (string)option["id"]
                });

                return OptionDetails;
            }

            return Enumerable.Empty<OptionInfoDTO>();
        }
    }
}
