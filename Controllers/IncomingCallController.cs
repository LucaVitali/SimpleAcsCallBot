using Azure.Communication.CallAutomation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
[Route("[controller]")]
public class IncomingCallController : ControllerBase
{
    private readonly CallAutomationClient _client;
    private readonly IConfiguration _config;

    public IncomingCallController(IConfiguration config)
    {
        _config = config;
        _client = new CallAutomationClient(_config["ACS_CONNECTION_STRING"]);
    }

    [HttpPost]
    public async Task<IActionResult> HandleIncomingCall([FromBody] JsonElement body)
    {
        try
        {
            Console.WriteLine($"Evento ricevuto: {body}");

            // ✅ Leggi il primo evento
            var firstEvent = body[0];
            var eventType = firstEvent.GetProperty("eventType").GetString();

            // ✅ 1. Validazione Event Grid
            if (eventType == "Microsoft.EventGrid.SubscriptionValidationEvent")
            {
                var validationCode = firstEvent.GetProperty("data").GetProperty("validationCode").GetString();
                Console.WriteLine($"Validation code: {validationCode}");
                return Ok(new { validationResponse = validationCode });
            }

            // ✅ 2. IncomingCall
            var incomingCallContext = firstEvent.GetProperty("data").GetProperty("incomingCallContext").GetString();
            Console.WriteLine($"IncomingCallContext: {incomingCallContext}");

            var answerResponse = await _client.AnswerCallAsync(incomingCallContext, new Uri("https://example.com/callback"));
            var callConnection = answerResponse.Value.CallConnection;

            var media = callConnection.GetCallMedia();
            await media.PlayToAllAsync(new FileSource(new Uri(_config["AUDIO_URL"])));

            Console.WriteLine("Audio riprodotto con successo.");
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore: {ex.Message}");
            return StatusCode(500, $"Errore interno: {ex.Message}");
        }
    }
}