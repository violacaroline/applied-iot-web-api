using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using applied_iot_api.Services;
using applied_iot_api.Models;

namespace applied_iot_api.Controllers
{
  /// <summary>
  /// Controller for handling the Pico W Airquality data
  /// </summary>
  [ApiController]
  [Route("api/airquality")]
  public class AirqualityController : ControllerBase
  {
    private readonly IAirQualityDataService airQualityDataService;

    /// <summary>
    /// Instanciates a new AirqualityController with an AirQualityDataService.
    /// </summary>
    /// <param name="airQualityDataService">The service to handle elastic data</param>
    public AirqualityController(IAirQualityDataService airQualityDataService)
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
        var airQualityData = await this.airQualityDataService.GetAirqualityData();

        return Ok(airQualityData);
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
        // Convert the dynamic object to JSON and AirqualityData
        string jsonData = JsonSerializer.Serialize(data);
        var airqualityData = JsonSerializer.Deserialize<AirQualityData>(jsonData);

        // Add current time to data
        // DateTime currentTime = DateTime.UtcNow;
        // airqualityData.timeStamp = currentTime;

        var result = await this.airQualityDataService.PostAirqualityData(airqualityData);

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