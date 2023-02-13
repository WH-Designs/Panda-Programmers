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
    }

    public class SeedData 
    {
        public static readonly UserInfoData[] UserSeedData = new UserInfoData[]
        {
            new UserInfoData{UserName = "chadb@gmail.com", Email = "chadb@gmail.com", FirstName = "Chad", LastName = "Bass"},
            new UserInfoData{UserName = "tiffanyf@gmail.com", Email = "tiffanyf@gmail.com", FirstName = "Tiffany", LastName = "Fox"},
            new UserInfoData{UserName = "dwightm@gmail.com", Email = "dwightm@gmail.com", FirstName = "Dwight", LastName = "Morse"}
        };
    }
}




