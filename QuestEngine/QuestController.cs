using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using System.Text.Json.Serialization;

namespace QuestEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestController : ControllerBase
{
    private readonly IChatClient _chatClient;

    public QuestController(IChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateQuest([FromBody] WorldStateRequest request)
    {
        string prompt = $"""
        You are a quest generator. Analyze the following world conditions and generate a fitting quest:
        - Location: {request.Location}
        - Weather: {request.Weather}
        - Local Events/Disasters: {request.LocalEvent}
        - Faction Dynamics: {request.FactionStatus}
        """;

        // ideally we would spin up a new local pod and session for each request, but we will ignore that for now.
        // first we will need to refine our inputs to make sure that the request has actionable
        // context for the model to generate a quest. 
        // We will also need to ensure that the model is capable of generating structured output that matches our GeneratedQuest DTO.
        // Enforce structured output from the local model using schema matching
        // each call is a bit long since it is a local model, but we can optimize this later.
        
        var response = await _chatClient.GetResponseAsync<GeneratedQuest>(prompt);

        if (response.TryGetResult(out var quest))
        {
            return Ok(quest);
        }

        return StatusCode(500, "Failed to parse structured quest from local model.");
    }
}

// Input DTO
public record WorldStateRequest(
    string Location,
    string Weather,
    string LocalEvent,
    string FactionStatus
);

// Output DTO returned to Game Client
public record GeneratedQuest(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("summary")] string Summary,
    [property: JsonPropertyName("objectives")] List<string> Objectives,
    [property: JsonPropertyName("reward")] int Reward,
    [property: JsonPropertyName("failure_penalty")] int FailurePenalty
);