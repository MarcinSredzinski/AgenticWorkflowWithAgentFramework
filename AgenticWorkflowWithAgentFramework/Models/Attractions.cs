namespace AgenticWorkflowWithAgentFramework.Models;

internal class Attractions
{
    public TouristAttraction[] Property1 { get; set; }

    public class TouristAttraction
    {
        public string name { get; set; }
        public string opening_days_and_times { get; set; }
        public string district { get; set; }
        public string[] categories { get; set; }
    }
}