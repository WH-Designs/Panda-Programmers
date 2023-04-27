using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MusicCollaborationManager.Models.DTO
{
    public class VoteIdentifierInfoDTO
    {
        public string VoteID { get; set; }
        public string Identifier { get; set; }
        public string PollID { get; set; }
        public string OptionID { get; set; }

        public static VoteIdentifierInfoDTO FromJson(object? obj, string pollID)
        {
            VoteIdentifierInfoDTO VoteEntryDetails = new VoteIdentifierInfoDTO();
            JObject? jObject = null;
            try
            {
                jObject = JObject.Parse((string)obj);
            }
            catch (JsonReaderException)
            {
                Debug.WriteLine("Error parsing JSON. (GetAllVotesWithIdentifier)");
            }
            if (jObject != null)
            {
                //NEED TO INVESTIGATE IF THIS WILL CAUSE AN ERROR IF "docs" is empty.
                IEnumerable<VoteIdentifierInfoDTO> VoteDetails = jObject["data"]["docs"].Select(vote => new VoteIdentifierInfoDTO()
                {
                    VoteID = (string)vote["id"],
                    Identifier = (string)vote["identifier"],
                    PollID = (string)vote["poll_id"],
                    OptionID = (string)vote["option_id"]
                });

                VoteEntryDetails = VoteDetails.Where(vd => vd.PollID == pollID).FirstOrDefault();

                return VoteEntryDetails;
            }

            return VoteEntryDetails;
        }
    }
}
