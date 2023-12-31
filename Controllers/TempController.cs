using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using applied_iot_api.Services;
using applied_iot_api.Models;

namespace applied_iot_api.Controllers
{
  /// <summary>
  /// Controller for handling the Pico W temperature data
  /// </summary>
  [ApiController]
  [Route("api/temperature")]
  public class TempController : ControllerBase
  {
    private readonly IAirQualityDataService airQualityDataService;

    /// <summary>
    /// Instanciates a new TempController with an AirQualityDataService.
    /// </summary>
    /// <param name="airQualityDataService">The service to handle elastic data</param>
    public TempController(IAirQualityDataService airQualityDataService)
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
        var tempData = await this.airQualityDataService.GetTempData();

        return Ok(tempData); 
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
        Console.WriteLine("Incoming", Request);

        // Convert the dynamic object to JSON and TempData
        string jsonData = JsonSerializer.Serialize(data);
        var tempData = JsonSerializer.Deserialize<TempData>(jsonData);

        Console.WriteLine("Json:", jsonData, "Temp:", tempData);

        // Add current time to data
        // DateTime currentTime = DateTime.UtcNow;
        // tempData.timeStamp = currentTime;

        var result = await this.airQualityDataService.PostTempData(tempData);

        Console.WriteLine("Result", result.ToString());

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
