namespace AgenticWorkflowWithAgentFramework.Models;

internal class Restaurants
{
    public Restaurant[] Property1 { get; set; }

    public class Restaurant
    {
        public string name { get; set; }
        public string opening_days_and_times { get; set; }
        public string district { get; set; }
        public string[] categories { get; set; }
        public Popularity popularity { get; set; }
    }

    public class Popularity
    {
        public string level { get; set; }
        public float rating { get; set; }
        public int review_count { get; set; }
    }

}