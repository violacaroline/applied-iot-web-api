using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using applied_iot_api.Services;
using applied_iot_api.Models;

namespace applied_iot_api.Controllers
{
  /// <summary>
  /// Controller for handling the Pico W humidity data
  /// </summary>
  [ApiController]
  [Route("api/humidity")]
  public class HumidityController : ControllerBase
  {
    private readonly IAirQualityDataService airQualityDataService;

    /// <summary>
    /// Instanciates a new HumidityController with an AirQualityDataService.
    /// </summary>
    /// <param name="airQualityDataService">The service to handle elastic data</param>
    public HumidityController(IAirQualityDataService airQualityDataService)
    {
      this.airQualityDataService = airQualityDataService;
    }

    /// <summary>
    /// Retreives data from Elastic Search.
    /// </summary>
    /// <returns>The data from Elastic Search</returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var humidityData = await this.airQualityDataService.GetHumidityData();

        return Ok(humidityData); 
      }
      catch (Exception error)
      {        
        return StatusCode(500, $"An error occurred while processing the request: {error.Message}");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] dynamic data)
    {
      try
      {
        // Convert the dynamic object to JSON and HumidityData
        string jsonData = JsonSerializer.Serialize(data);
        var humidityData = JsonSerializer.Deserialize<HumidityData>(jsonData);

        // Add current time to data
        // DateTime currentTime = DateTime.UtcNow;
        // humidityData.timeStamp = currentTime;

        var result = await this.airQualityDataService.PostHumidityData(humidityData);

        if (result)
          return Ok("Data posted to ElasticSearch successfully");
        else
          return StatusCode(500, "Failed to post data to ElasticSearch");
          
        }
      catch (Exception error)
      {        
        return StatusCode(500, $"An error occurred while processing the request: {error.Message}");
      }
    }
  }
}
