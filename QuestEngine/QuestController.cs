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
        You are a game quest generator. Analyze the following world conditions and generate a fitting quest:
        - Location: {request.Location}
        - Weather: {request.Weather}
        - Local Events/Disasters: {request.LocalEvent}
        - Faction Dynamics: {request.FactionStatus}
        """;

        // Enforce structured output from the local model using schema matching
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
    [property: JsonPropertyName("gold_reward")] int GoldReward
);