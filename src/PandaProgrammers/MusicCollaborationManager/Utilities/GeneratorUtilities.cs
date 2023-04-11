using SpotifyAPI.Web;

namespace MusicCollaborationManager.Utilities
{
    public class GeneratorUtilities
    {
        //RNG that gives 1-10
        public int rngValue()
        {
            Random rnd = new Random();
            int result = rnd.Next(1, 11);
            return result;
        }
        //RNG that take min and max
        public int rngValueInput(int min, int max)
        {
            Random rnd = new Random();
            int result = rnd.Next(min, max);
            return result;
        }


        public string getTimeValue(DateTime dt)
        {
            string timeCategory = "";
            //Get current in hours using 24 hour clock
            double currTimeHours = dt.TimeOfDay.TotalHours;
            //Get string for day of the week
            string currWeekDay = dt.ToString("dddd");

            //Calculate what time category it currently is
            if (currWeekDay == "Sunday")
            {
                if (currTimeHours >= 5 && currTimeHours < 18)
                {
                    timeCategory = "sunDay";
                }
                else if (currTimeHours < 5)
                {
                    timeCategory = "endMorning";
                }
                else if (currTimeHours >= 18 && currTimeHours < 21)
                {
                    timeCategory = "workEvening";
                }
                else
                {
                    timeCategory = "bedTime";
                }
            }
            else if (currWeekDay == "Friday")
            {
                if (currTimeHours >= 9 && currTimeHours <= 17)
                {
                    timeCategory = "workDay";
                }
                else if (currTimeHours < 9)
                {
                    timeCategory = "workMorning";
                }
                else if (currTimeHours > 17 && currTimeHours < 22)
                {
                    timeCategory = "friEvening";
                }
                else
                {
                    timeCategory = "bedTime";
                }
            }
            else if (currWeekDay == "Monday" || currWeekDay == "Tuesday" || currWeekDay == "Wednesday" || currWeekDay == "Thursday")
            {
                if (currTimeHours >= 9 && currTimeHours <= 17)
                {
                    timeCategory = "workDay";
                }
                else if (currTimeHours < 9)
                {
                    timeCategory = "workMorning";
                }
                else if (currTimeHours > 17 && currTimeHours < 21)
                {
                    timeCategory = "workEvening";
                }
                else
                {
                    timeCategory = "bedTime";
                }
            }
            else
            {
                if (currTimeHours >= 9 && currTimeHours <= 17)
                {
                    timeCategory = "endDay";
                }
                else if (currTimeHours < 9 && currTimeHours >= 4)
                {
                    timeCategory = "endMorning";
                }
                else if (currTimeHours > 17 && currTimeHours < 23)
                {
                    timeCategory = "endEvening";
                }
                else
                {
                    timeCategory = "bedTime";
                }
            }

            return timeCategory;
        }

        public List<string> shuffleTracks(List<string> tracks)
        {
            int indexCount;
            int rngCount;
            if (tracks.Count <= 5)
            {
                indexCount = tracks.Count;
                rngCount = tracks.Count;
            }
            else if (tracks.Count > 20)
            {
                indexCount = 5;
                rngCount = 20;
            }
            else
            {
                indexCount = 5;
                rngCount = tracks.Count;
            }

            List<string> result = new List<string>();
            for (int i = 0; i < indexCount; i++)
            {
                string trackHolder = tracks[rngValueInput(0, rngCount)];
                if (!result.Contains(trackHolder))
                {
                    result.Add(trackHolder);
                }
                else
                {
                    i--;
                }
            }
            return result;
        }
    }
}
