using AgenticWorkflowWithAgentFramework.Agents;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using System.Text;

ConfigurationBuilder configurationBuilder = new();
IConfiguration configuration = configurationBuilder.AddUserSecrets<Program>().Build();
const string deploymentName = "gpt-5-mini";
string endpoint = configuration.GetSection("AZURE_PROJECT_ENDPOINT").Value!;

IAgentDefinition attactionFinderAgent = new AttractionFinderDefinition();
var attractionAgent = attactionFinderAgent.DeclareAgent(endpoint, deploymentName);

IAgentDefinition schedulePlannerAgent = new SchedulePlannerDefinition();
var scheduleAgent = schedulePlannerAgent.DeclareAgent(endpoint, deploymentName);

IAgentDefinition restaurantPlannerAgent = new RestaurantFinderDefinition();
var restaurantAgent = restaurantPlannerAgent.DeclareAgent(endpoint, deploymentName);

IAgentDefinition tripPlannerAgent = new TripPlannerDefinition();
var tripAgent = tripPlannerAgent.DeclareAgent(endpoint, deploymentName);

Workflow workflow = new WorkflowBuilder(attractionAgent)
    .AddEdge(attractionAgent, scheduleAgent)
    .AddEdge(scheduleAgent, restaurantAgent)
    .AddEdge(restaurantAgent, tripAgent)
    .Build();


await using StreamingRun run = await InProcessExecution.RunStreamingAsync(workflow,
    new ChatMessage(ChatRole.User,
    "I'm planning my first trip to Paris. I want to see the most famous Paris attractions, I'm a huge fan of Picasso and Monet. " +
    "I like to sit in nice gardens. I'm a huge fan of traditional french cuisine. " +
    "I will be visiting from 4th October to 12th October this year."));

var responses = new Dictionary<string, StringBuilder>();

await run.TrySendMessageAsync(new TurnToken(emitEvents: true));

await foreach (WorkflowEvent evt in run.WatchStreamAsync())
{
    if(evt is ExecutorEvent executorEvent) //Inspecting executor events is very useful for debugging.
    {
        var ev = executorEvent;
    }
    if (evt is AgentResponseUpdateEvent update)
    {
        if (!responses.TryGetValue(update.ExecutorId, out var buffer))
        {
            buffer = new StringBuilder();
            responses[update.ExecutorId] = buffer;
        }

        buffer.Append(update.Data);
    }
}

// Everything has finished — now print complete responses
foreach (var (executorId, response) in responses)
{
    Console.WriteLine();
    Console.WriteLine($"========== {executorId} ==========");
    Console.WriteLine(response.ToString());
    Console.WriteLine("================================");
}