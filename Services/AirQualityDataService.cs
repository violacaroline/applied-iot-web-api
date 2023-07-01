using applied_iot_api.Models;
using Nest;
using System.Text.Json;
using DotNetEnv;


namespace applied_iot_api.Services
{
  /// <summary>
  /// Service handling interactions with ElasticSearch instance.
  /// </summary>
  public class AirQualityDataService : IAirQualityDataService
  {
    private readonly IElasticClient elasticClient;
    private String? apiKey;

    /// <summary>
    /// Instanciates a new AirQualityDataService.
    /// </summary>
    /// <param name="elasticClient">The ElasticClient used by the service</param>
    public AirQualityDataService(IElasticClient elasticClient)
    {
      DotNetEnv.Env.Load();
      this.elasticClient = elasticClient;
    }

    /// <summary>
    /// Retreives a list of TempData objects.
    /// </summary>
    /// <returns>List of TempData objects</returns>
    public async Task<List<double>> GetTempData()
    {
      // Create an instance of HttpClient to make the HTTP request
      var httpClient = new HttpClient();

      var apiUrl =
      "https://io.adafruit.com/api/v2/CarolineA/feeds/temperature-and-humidity-and-airquality-and-led.temperature/data";
        this.apiKey = Environment.GetEnvironmentVariable("ADAFRUIT_API_KEY");

      var requestUrl = $"{apiUrl}?X-AIO-Key={this.apiKey}";

      var response = await httpClient.GetAsync(requestUrl);

      // Read the response content as a string
      var data = await response.Content.ReadAsStringAsync();
      var jsonObject = JsonSerializer.Deserialize<dynamic>(data);

      var values = new List<double>();

      foreach (var dataPoint in jsonObject.EnumerateArray())
      {
        var tempData = JsonSerializer.Deserialize<TempData>(dataPoint.GetRawText());

        string formattedValue = tempData.value.Replace('.', ',');

        double value = double.Parse(formattedValue);

        values.Add(value);
      }
      return values;

      // ---------------- ELASTIC FETCH --------------

      // var tempDataResponse = await elasticClient.SearchAsync<TempData>(s => s
      //   .Index("picow-temperature")
      //   .MatchAll()
      // );

      // if (tempDataResponse.IsValid)
      // {
      //   var temperatureData = tempDataResponse.Documents.ToList();
      //   return temperatureData;
      // }

      // // Handle error case
      // throw new Exception("Failed to retrieve temperature data from Elasticsearch.");

    }

    /// <summary>
    /// Posts a TempData object to ElasticSearch.
    /// </summary>
    /// <param name="tempData">The TempData object to be posted.</param>
    /// <returns>A boolean indicating the success or failure of the operation.</returns>
    public async Task<bool> PostTempData(TempData tempData)
    {
      var indexResponse = await elasticClient.IndexAsync(tempData, idx => idx
          .Index("picow-temperature")
      );

      return indexResponse.IsValid;
    }

    /// <summary>
    /// Retreives a list of HumidityData objects.
    /// </summary>
    /// <returns>List of HumidityData objects</returns>
    public async Task<List<double>> GetHumidityData()
    {
      // --------------- ADAFRUIT API FETCH ------------

      // Create an instance of HttpClient to make the HTTP request
      var httpClient = new HttpClient();

      var apiUrl =
      "https://io.adafruit.com/api/v2/CarolineA/feeds/temperature-and-humidity-and-airquality-and-led.humidity/data";
      this.apiKey = Environment.GetEnvironmentVariable("ADAFRUIT_API_KEY");

      var requestUrl = $"{apiUrl}?X-AIO-Key={this.apiKey}";

      var response = await httpClient.GetAsync(requestUrl);

      // Read the response content as a string
      var data = await response.Content.ReadAsStringAsync();

      var jsonObject = JsonSerializer.Deserialize<dynamic>(data);

      var values = new List<double>();

      foreach (var dataPoint in jsonObject.EnumerateArray())
      {
        var humidityData = JsonSerializer.Deserialize<HumidityData>(dataPoint.GetRawText());

        string formattedValue = humidityData.value.Replace('.', ',');

        double value = double.Parse(formattedValue);

        values.Add(value);
      }
      return values;

      // --------------- ELASTIC FETCH ----------------

      // var humidityDataResponse = await elasticClient.SearchAsync<HumidityData>(s => s
      //   .Index("picow-humidity")
      //   .MatchAll()
      // );

      // if (humidityDataResponse.IsValid)
      // {
      //   var humidityData = humidityDataResponse.Documents.ToList();
      //   return humidityData;
      // }

      // // Handle error case
      // throw new Exception("Failed to retrieve humidity data from Elasticsearch.");

    }

    /// <summary>
    /// Posts a HumidityData object to ElasticSearch.
    /// </summary>
    /// <param name="humidityData">The HumidityData object to be posted.</param>
    /// <returns>A boolean indicating the success or failure of the operation.</returns>
    public async Task<bool> PostHumidityData(HumidityData humidityData)
    {
      var indexResponse = await elasticClient.IndexAsync(humidityData, idx => idx
          .Index("picow-humidity")
      );

      return indexResponse.IsValid;
    }

    /// <summary>
    /// Retreives a list of AirQualityData objects.
    /// </summary>
    /// <returns>List of AirQualityData objects</returns>
    public async Task<List<int>> GetAirqualityData()
    {
      // ------------ ADAFRUIT API FETCH ---------------
      // Create an instance of HttpClient to make the HTTP request
        var httpClient = new HttpClient();

        var apiUrl =
        "https://io.adafruit.com/api/v2/CarolineA/feeds/temperature-and-humidity-and-airquality-and-led.airquality/data";
        this.apiKey = Environment.GetEnvironmentVariable("ADAFRUIT_API_KEY");

        var requestUrl = $"{apiUrl}?X-AIO-Key={this.apiKey}";

        var response = await httpClient.GetAsync(requestUrl);

        // Read the response content as a string
        var data = await response.Content.ReadAsStringAsync();
        var jsonObject = JsonSerializer.Deserialize<dynamic>(data);

        var values = new List<int>();

        foreach (var dataPoint in jsonObject.EnumerateArray())
        {
          var airQualityData = JsonSerializer.Deserialize<AirQualityData>(dataPoint.GetRawText());

          int value = int.Parse(airQualityData.value);

          values.Add(value);
        }

        return values;

      // ------------ ELASTIC FETCH --------------
      // var airqualityDataResponse = await elasticClient.SearchAsync<AirQualityData>(x => x
      //   .Index("picow-airquality")
      //   .MatchAll()
      // );

      // if (airqualityDataResponse.IsValid)
      // {
      //   var airqualityData = airqualityDataResponse.Documents.ToList();
      //   return airqualityData;
      // }

      // // Handle error case
      // throw new Exception("Failed to retrieve airquality data from Elasticsearch.");
    }

    /// <summary>
    /// Posts an AirQualityData object to ElasticSearch.
    /// </summary>
    /// <param name="airQualityData">The AirQualityData object to be posted.</param>
    /// <returns>A boolean indicating the success or failure of the operation.</returns>
    public async Task<bool> PostAirqualityData(AirQualityData airQualityData)
    {
      var indexResponse = await elasticClient.IndexAsync(airQualityData, idx => idx
          .Index("picow-airquality")
      );

      return indexResponse.IsValid;
    }
  }
}