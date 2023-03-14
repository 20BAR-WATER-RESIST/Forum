using Forum.Contracts;

namespace Forum.Repositories
{
    public class BaseMethodsRepository : IBaseMethodsRepository
    {
        public async Task<string> TrimString(string description, int lenght)
        {
            string results = description.ToString();
            string backresults = results.Substring(0, Math.Min(results.Length, lenght));

            if (results.Length > lenght)
            {
                backresults += "...";
            }

            return backresults;
        }

        public async Task<string> TopicTimer(TimeSpan dataTime)
        {
            if (dataTime.TotalHours < 1)
            {
                int minutes = (int)dataTime.TotalMinutes;
                return $"{minutes} {(minutes == 1 ? "min" : "min's")}";
            }
            else if (dataTime.TotalHours < 24)
            {
                int hours = (int)dataTime.TotalHours;
                return $"{hours} {(hours == 1 ? "h" : "h's")}";
            }
            else
            {
                int days = (int)dataTime.TotalDays;
                return $"{days}{(days == 1 ? "d" : "d's")}+";
            }
        }
    }
}
