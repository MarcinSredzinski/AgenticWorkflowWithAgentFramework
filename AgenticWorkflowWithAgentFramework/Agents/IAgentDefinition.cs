using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AgenticWorkflowWithAgentFramework.Agents;

public interface IAgentDefinition
{
    public abstract string Name { get; }
    public abstract string Instructions { get; }
    public abstract AIFunction[]? Tools { get; }   
    public AIAgent DeclareAgent(string endpoint, string modelDeploymentName) =>
      new AIProjectClient(new Uri(endpoint), new AzureCliCredential())
         .AsAIAgent(model: modelDeploymentName, name: Name, instructions: Instructions, tools: Tools); 
}