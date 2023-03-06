using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCollaborationManager.Data
{
    public class UserInfoData
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; } = true;
        public int FriendID { get; set; }
        public string SpotifyId { get; set; }
    }

    public class SeedData 
    {
        public static readonly UserInfoData[] UserSeedData = new UserInfoData[]
        {
            new UserInfoData{UserName = "chadb@gmail.com", Email = "chadb@gmail.com", FirstName = "Chad", LastName = "Bass", FriendID = 1, SpotifyId = "31apsehiff3z54ok4i6fr6g4ks5q"},
            new UserInfoData{UserName = "tiffanyf@gmail.com", Email = "tiffanyf@gmail.com", FirstName = "Tiffany", LastName = "Fox", FriendID = 2, SpotifyId = "31apsehiff3z54ok4i6fr6g4ks5q"},
            new UserInfoData{UserName = "dwightm@gmail.com", Email = "dwightm@gmail.com", FirstName = "Dwight", LastName = "Morse", FriendID = 3, SpotifyId = "31apsehiff3z54ok4i6fr6g4ks5q"}
        };
    }
}




