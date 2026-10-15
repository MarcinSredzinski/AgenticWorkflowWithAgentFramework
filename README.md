# Agentic workflow with Microsoft Agent Framework example

Simple multi-agent trip planner built using Microsoft Agent Framework with models hosted in Microsoft Foundry.
Project demonstrates how to build a sequential agentic workflow, in which specialized agents work to devise a trip plan, based on user's desired destination, travel dates and culinary preferences. 
**For simplicity available tools consist of hardcoded lists of restaurants and tourist attractions in Paris.** 

## Workflow 
1. **Attraction Finder** — identifies attractions matching the user's interests.
2. **Schedule Planner** — organizes the attractions into a travel schedule.
3. **Restaurant Finder** — recommends restaurants based on culinary preferences.
4. **Trip Planner** — combines the results into a final itinerary.
Each agent processes the output of the previous step and passes its results to the next agent through a workflow built with `WorkflowBuilder`.

## Prerequisites

- .NET 10 SDK,
- Visual Studio or another .NET-compatible IDE,
- An Azure account with access to Microsoft Foundry,
- A deployed `gpt-5-mini` model,
- Azure authentication configured for the application,

Some of the nuget packages are in beta and prerelease versions and are subject to change.

## Configuration

1. Create a model deployment in [Microsoft Foundry](https://ai.azure.com/).
2. Copy your Foundry project endpoint.
3. Initialize user secrets:

   ```bash
   dotnet user-secrets init
   ```

4. Store the endpoint:

   ```bash
   dotnet user-secrets set "AZURE_PROJECT_ENDPOINT" "YOUR_PROJECT_ENDPOINT"
   ```

5. Ensure that your Azure identity has the necessary permissions to access the project and model deployment.

## Running the application

Restore dependencies and run the project:

```bash
dotnet restore
dotnet run
```

## Notes

This project is an educational example intended to demonstrate agentic workflow concepts, agent specialization, and sequential orchestration.

It is not a production-ready travel planning system. Agent outputs may be inaccurate, and recommendations should be verified independently.

APIs and package versions may change while Microsoft Agent Framework is evolving. Check the project dependencies if the example needs adjustments.

## Related resources

- [Microsoft Agent Framework - workflows](https://learn.microsoft.com/en-us/agent-framework/get-started/workflows)
- [Microsoft Foundry](https://ai.azure.com/)
- [My blog post with detailed explanation]()
