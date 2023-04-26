using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MusicCollaborationManager.Models.DTO
{
    public class PollDTO
    {
        public string PollID { get; set; }
        public string OptionID { get; set; }
        public string VoteIdentifer { get; set; }

        public PollDTO()
        {

        }

        public static string? FromJSON_GetNewPollID(object? obj)
        {
            JObject? theObj = null;
            try
            {
                theObj = JObject.Parse((string)obj);
            }
            catch (JsonReaderException)
            {
                Debug.WriteLine("'Error parsing json (GetCreationStatus)");
            }
            if (theObj != null)
            {
                return (string)theObj["data"]["id"];
            }
            return null;
        }

        public static IEnumerable<PollDTO> FromJSON_GetAllPolls(object? obj)
        {
            JObject? theObj = null;
            try
            {
                theObj = JObject.Parse((string)obj);
            }
            catch (JsonReaderException)
            {
                Debug.WriteLine("Error parsing json (GetAllPolls)");
            }
            if (theObj != null)
            {
                IEnumerable<PollDTO> OptionDetails = theObj["data"]["docs"].Select(option => new PollDTO()
                {
                    PollID = (string)option["id"],
                    OptionID = "",
                    VoteIdentifer = ""
                });

                return OptionDetails;
            }

            return Enumerable.Empty<PollDTO>();
        }
    }
}
