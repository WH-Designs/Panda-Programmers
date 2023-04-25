using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCollaborationManager.Models.DTO;
using Moq;
using MusicCollaborationManager.ViewModels;
using MusicCollaborationManager.Services.Concrete;
using System.Net;

namespace UnitTests
{
    public class YouTubeVisitorVideos_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetTopMusicVideosFromYouTube_WithOneVideo_ShouldParseCorrectly()
        {
            //Arrange
            const string CorrectVideoID = "VID_1";
            const string CorrectThumbnailURL = "https://i.ytimg.com/vi/correct1/default.jpg";
            const string CorrectThumbnailHeight = "90";
            const string CorrectThumbnailWidth = "120";
            const string CorrectVideoTitle = "YoungBoy Never Broke Again feat. Nicki Minaj - WTF ( Official Music Video)";
            const string CorrectChannelTitle = "YoungBoy Never Broke Again";

            string jsonFromYouTubeAPI = @"{
                ""items"": [
                    {
                        ""id"": ""VID_1"",,
                        ""snippet"":{
                            ""title"": ""YoungBoy Never Broke Again feat. Nicki Minaj - WTF ( Official Music Video)"",,
                             ""thumbnails"":{
                                ""default"":{
                                    ""url"": ""https://i.ytimg.com/vi/correct1/default.jpg"",,
                                    ""width"": ""120"",,
                                    ""height"": ""90""
                                }
                            },
                            ""channelTitle"": ""YoungBoy Never Broke Again""
                        }
                    }
                ]
            }";


            //Act
            IEnumerable<MusicVideoDTO> ParsedJson = MusicVideoDTO.FromJSON(jsonFromYouTubeAPI);

            //Assert
            foreach (MusicVideoDTO v in ParsedJson)
            {
                Assert.That(v.VideoID.Equals(CorrectVideoID));
                Assert.That(v.VideoTitle.Equals(CorrectVideoTitle));
                Assert.That(v.YouTubeChannelName.Equals(CorrectChannelTitle));
                Assert.That(v.ThumbnailURL.Equals(CorrectThumbnailURL));
                Assert.That(v.ThumbnailWidth.Equals(CorrectThumbnailWidth));
                Assert.That(v.ThumbnailHeight.Equals(CorrectThumbnailHeight));
            }
        }

        [Test]
        public void GetTopMusicVideosFromYouTube_WithMoreThanOneVideo_ShouldParseCorrectly()
        {
            //Arrange
            const string VideoIDOne = "VID_ID_1";
            const string ThumbnailURLOne = "https://i.ytimg.com/vi/correct1/default.jpg";
            const string VideoTitleOne = "YoungBoy Never Broke Again feat. Nicki Minaj - WTF ( Official Music Video)";
            const string ChannelTitleOne = "YoungBoy Never Broke Again";

            const string VideoIDTwo = "VID_ID_2";
            const string ThumbnailURLTwo = "https://i.ytimg.com/vi/correct2/default.jpg";
            const string VideoTitleTwo = "Music video #2";
            const string ChannelTitleTwo = "MainstreamVEVO";


            const string DefaultThumbnailHeight = "90";
            const string DefaultThumbnailWidth = "120";

            string jsonFromYouTubeAPI = @"{
                ""items"": [
                    {
                        ""id"": ""VID_ID_1"",
                        ""snippet"":{
                            ""title"": ""YoungBoy Never Broke Again feat. Nicki Minaj - WTF ( Official Music Video)"",
                             ""thumbnails"":{
                                ""default"":{
                                    ""url"": ""https://i.ytimg.com/vi/correct1/default.jpg"",
                                    ""width"": ""120"",
                                    ""height"": ""90""
                                }
                            },
                            ""channelTitle"": ""YoungBoy Never Broke Again""
                        }
                    },
                    {
                        ""id"": ""VID_ID_2"",
                        ""snippet"":{
                            ""title"": ""Music video #2"",
                             ""thumbnails"":{
                                ""default"":{
                                    ""url"": ""https://i.ytimg.com/vi/correct2/default.jpg"",
                                    ""width"": ""120"",
                                    ""height"": ""90""
                                }
                            },
                            ""channelTitle"": ""MainstreamVEVO""
                        }
                    }
                
                ]
            }";


            //Act
            IEnumerable<MusicVideoDTO> ParsedJson = MusicVideoDTO.FromJSON(jsonFromYouTubeAPI);
            List<MusicVideoDTO> MusicVideos = new List<MusicVideoDTO>();
            foreach (MusicVideoDTO mv in ParsedJson)
            {
                MusicVideos.Add(mv);
            }

            //Assert
            Assert.That(ParsedJson.ElementAt(0).VideoID.Equals(VideoIDOne));
            Assert.That(ParsedJson.ElementAt(0).VideoTitle.Equals(VideoTitleOne));
            Assert.That(ParsedJson.ElementAt(0).YouTubeChannelName.Equals(ChannelTitleOne));
            Assert.That(ParsedJson.ElementAt(0).ThumbnailHeight.Equals(DefaultThumbnailHeight));
            Assert.That(ParsedJson.ElementAt(0).ThumbnailWidth.Equals(DefaultThumbnailWidth));
            Assert.That(ParsedJson.ElementAt(0).ThumbnailURL.Equals(ThumbnailURLOne));

            Assert.That(ParsedJson.ElementAt(1).VideoID.Equals(VideoIDTwo));
            Assert.That(ParsedJson.ElementAt(1).VideoTitle.Equals(VideoTitleTwo));
            Assert.That(ParsedJson.ElementAt(1).YouTubeChannelName.Equals(ChannelTitleTwo));
            Assert.That(ParsedJson.ElementAt(1).ThumbnailHeight.Equals(DefaultThumbnailHeight));
            Assert.That(ParsedJson.ElementAt(1).ThumbnailWidth.Equals(DefaultThumbnailWidth));
            Assert.That(ParsedJson.ElementAt(1).ThumbnailURL.Equals(ThumbnailURLTwo));
        }
    }
}
