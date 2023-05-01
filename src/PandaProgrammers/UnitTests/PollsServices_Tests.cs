using MusicCollaborationManager.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    //Only testing the parsing methods used by/within each DTO.
    public class PollsServices_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PollDTOFromJSONGetNewPollID_WithAResponse_ShouldParseCorrectly()
        {
            //Arrange
            const string expectedPollID = "644fd40125e6ed0010f8a8a7";

            string jsonFromPollsAPI = @"{
                ""status"": ""success"",
                ""statusCode"": 200,
                ""data"": {
                    ""data"": ""null"",
                    ""identifier"": ""a_testing_of_parsing"",
                    ""question"": ""Add track to playlist?"",
                    ""created_at"": ""2023-05-01T15:00:17.711Z"",
                    ""updated_at"": ""2023-05-01T15:00:17.711Z"",
                    ""id"": ""644fd40125e6ed0010f8a8a7"",
                    ""entity"": ""Poll"",
                    ""options"": [
                        {
                            ""data"": ""null"",
                            ""text"": ""Yes"",
                            ""votes_count"": 0,
                            ""poll_id"": ""644fd40125e6ed0010f8a8a7"",
                            ""created_at"": ""2023-05-01T15:00:17.716Z"",
                            ""updated_at"": ""2023-05-01T15:00:17.716Z"",
                            ""id"": ""644fd40125e6ed0010f8a8a8"",
                            ""entity"": ""Option""
                        },
                        {
                            ""data"": ""null"",
                            ""text"": ""No"",
                            ""votes_count"": 0,
                            ""poll_id"": ""644fd40125e6ed0010f8a8a7"",
                            ""created_at"": ""2023-05-01T15:00:17.716Z"",
                            ""updated_at"": ""2023-05-01T15:00:17.716Z"",
                            ""id"": ""644fd40125e6ed0010f8a8a9"",
                            ""entity"": ""Option""
                        }
                    ]
                }
            }";


            //Act
            string NewPollID = PollDTO.FromJSON_GetNewPollID(jsonFromPollsAPI);


            //Assert
            Assert.That(NewPollID, Is.EqualTo(expectedPollID));

        }

        [Test]
        public void OptionInfoDTOFromJSON_WithAResponse_ShouldParseCorrectly()
        {
            //Arrange
            const string expectedOptionOneID = "644fd40125e6ed0010f8a8a8";
            const string expectedOptionOneText = "Yes";
            const int expectedOptionOneTotalVoteCount = 10;

            const string expectedOptionTwoID = "644fd40125e6ed0010f8a8a9";
            const string expectedOptionTwoText = "No";
            const int expectedOptionTwoTotalVoteCount = 5;

            string jsonFromPollsAPI = @"{
                ""status"": ""success"",
                ""statusCode"": 200,
                ""data"": {
                    ""data"": ""null"",
                    ""identifier"": ""a_testing_of_parsing"",
                    ""question"": ""Add track to playlist?"",
                    ""created_at"": ""2023-05-01T15:00:17.711Z"",
                    ""updated_at"": ""2023-05-01T15:00:17.711Z"",
                    ""id"": ""644fd40125e6ed0010f8a8a7"",
                    ""entity"": ""Poll"",
                    ""options"": [
                        {
                            ""data"": ""null"",
                            ""text"": ""Yes"",
                            ""votes_count"": 10,
                            ""poll_id"": ""644fd40125e6ed0010f8a8a7"",
                            ""created_at"": ""2023-05-01T15:00:17.716Z"",
                            ""updated_at"": ""2023-05-01T15:00:17.716Z"",
                            ""id"": ""644fd40125e6ed0010f8a8a8"",
                            ""entity"": ""Option""
                        },
                        {
                            ""data"": ""null"",
                            ""text"": ""No"",
                            ""votes_count"": 5,
                            ""poll_id"": ""644fd40125e6ed0010f8a8a7"",
                            ""created_at"": ""2023-05-01T15:00:17.716Z"",
                            ""updated_at"": ""2023-05-01T15:00:17.716Z"",
                            ""id"": ""644fd40125e6ed0010f8a8a9"",
                            ""entity"": ""Option""
                        }
                    ]
                }
            }";


            //Act
            IEnumerable<OptionInfoDTO> NewPollOptions = OptionInfoDTO.FromJSON(jsonFromPollsAPI);
            List<OptionInfoDTO> OptionList = NewPollOptions.ToList();


            //Assert
            Assert.That(OptionList.ElementAt(0).OptionID, Is.EqualTo(expectedOptionOneID));
            Assert.That(OptionList.ElementAt(0).OptionText, Is.EqualTo(expectedOptionOneText));
            Assert.That(OptionList.ElementAt(0).OptionCount, Is.EqualTo(expectedOptionOneTotalVoteCount));

            Assert.That(OptionList.ElementAt(1).OptionID, Is.EqualTo(expectedOptionTwoID));
            Assert.That(OptionList.ElementAt(1).OptionText, Is.EqualTo(expectedOptionTwoText));
            Assert.That(OptionList.ElementAt(1).OptionCount, Is.EqualTo(expectedOptionTwoTotalVoteCount));
        }

        [Test]
        public void VoteIdentifierInfoDTOFromJson_WithAResponse_ShouldParseCorrectly()
        {
            //Arrange
            const string CurPollID = "644fd40125e6ed0010f8a8a7";
            const string CurUser = "twinchad@gmail.com";
            const string OptionUserVotedForID = "644fd40125e6ed0010f8a8a8";
            const string VoteID = "644fdc9025e6ed0010f8a8aa";


            string jsonFromPollsAPI = @"{
            ""status"": ""success"",
            ""statusCode"": 200,
            ""data"": {
                ""docs"": [
                    {
                        ""identifier"": ""twinchad@gmail.com"",
                        ""poll_id"": ""644fd40125e6ed0010f8a8a7"",
                        ""option_id"": ""644fd40125e6ed0010f8a8a8"",
                        ""created_at"": ""2023-05-01T15:36:48.018Z"",
                        ""updated_at"": ""2023-05-01T15:36:48.018Z"",
                        ""id"": ""644fdc9025e6ed0010f8a8aa"",
                        ""entity"": ""Vote""
                    }
                ],
                ""totalDocs"": 1,
                ""offset"": 0,
                ""limit"": 25,
                ""totalPages"": 1,
                ""page"": 1,
                ""pagingCounter"": 1,
                ""hasPrevPage"": false,
                ""hasNextPage"": false,
                ""prevPage"": null,
                ""nextPage"": null
            }
        }";


            //Act
            VoteIdentifierInfoDTO UserVoteInfoForSpecificPoll = VoteIdentifierInfoDTO.FromJson(jsonFromPollsAPI, CurPollID, CurUser);


            //Assert
            Assert.That(UserVoteInfoForSpecificPoll.Identifier.Equals(CurUser));
            Assert.That(UserVoteInfoForSpecificPoll.PollID.Equals(CurPollID));
            Assert.That(UserVoteInfoForSpecificPoll.VoteID.Equals(VoteID));
            Assert.That(UserVoteInfoForSpecificPoll.OptionID.Equals(OptionUserVotedForID));
        }
    }
}
