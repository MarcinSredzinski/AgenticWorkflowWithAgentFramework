using System.ComponentModel;
using static AgenticWorkflowWithAgentFramework.Models.Attractions;
using static AgenticWorkflowWithAgentFramework.Models.Restaurants;

namespace AgenticWorkflowWithAgentFramework.Tools;

public static class ToolsSet
{
    [Description("Get list of distinct categories of tourist attractions")]
    internal static List<string> GetTouristAttractionCategories()
    {
        var attractionsListContent = File.ReadAllText("Data/AttractionsList.json");
        if (attractionsListContent is null)
            return [];

        var attractions = System.Text.Json.JsonSerializer.Deserialize<List<TouristAttraction>>(attractionsListContent)!;
        var distinctCategories = attractions.Where(a => a.categories != null)
            .SelectMany(a => a.categories)
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Distinct()
            .ToList();

        return distinctCategories;
    }

    [Description("Get a list of tourist attractions including their names, categories and opening times based on user interests")]
    internal static List<TouristAttraction> GetTouristAttractionsIn([Description("List of user interests")] List<string> interests)
    {
        var attractionsListContent = File.ReadAllText("Data/AttractionsList.json");
        if (attractionsListContent is null)
            return [];

        var attractions = System.Text.Json.JsonSerializer.Deserialize<List<TouristAttraction>>(attractionsListContent);
        if (attractions is null)
            return [];

        var interestingAttractions = attractions.Where(a => interests.Intersect(a.categories).Any());
        return attractions ?? [];
    }

    [Description("Gets the list of dietary restrictions of a given user")]
    internal static List<string> GetUsersDietaryRestrictions([Description("Id of the user")] string usersName)
    {
        return ["red meat", "nuts"];
    }

    [Description("Get the best restaurants for a given location according to my private list of favourites")]
    internal static List<Restaurant> GetRestaurantsIn([Description("District the user is going to visit")] string district)
    {
        var restaurantsListContent = File.ReadAllText("Data/RestaurantsList.json");
        if (restaurantsListContent is null)
            return [];

        var attractions = System.Text.Json.JsonSerializer.Deserialize<List<Restaurant>>(restaurantsListContent);
        return attractions ?? [];
    }
}
