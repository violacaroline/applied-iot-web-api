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
      var airQualityData = await this.airQualityDataService.GetTempData();

      return Ok(airQualityData);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] dynamic data)
    {
      // Convert the dynamic object to TempData using Newtonsoft.Json
      string jsonData = JsonSerializer.Serialize(data);
      var tempData = JsonSerializer.Deserialize<TempData>(jsonData);
      var result = await this.airQualityDataService.PostTempData(tempData);

      if (result)
        return Ok("Data posted to ElasticSearch successfully");
      else
        return StatusCode(500, "Failed to post data to ElasticSearch");
    }
  }
}

