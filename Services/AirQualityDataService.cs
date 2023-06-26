using applied_iot_api.Models;
using Nest;

namespace applied_iot_api.Services
{
  /// <summary>
  /// Service handling interactions with ElasticSearch instance.
  /// </summary>
  public class AirQualityDataService : IAirQualityDataService
  {
    private readonly IElasticClient elasticClient;

    /// <summary>
    /// Instanciates a new AirQualityDataService.
    /// </summary>
    /// <param name="elasticClient">The ElasticClient used by the service</param>
    public AirQualityDataService(IElasticClient elasticClient)
    {
      this.elasticClient = elasticClient;
    }

    /// <summary>
    /// Retreives a list of TempData objects.
    /// </summary>
    /// <returns>List of TempData objects</returns>
    public async Task<List<TempData>> GetTempData()
    {
      var tempDataResponse = await elasticClient.SearchAsync<TempData>(s => s
        .Index("picow-temperature")
        .MatchAll()
      );

      if (tempDataResponse.IsValid)
      {
        var temperatureData = tempDataResponse.Documents.ToList();
        return temperatureData;
      }

      // Handle error case
      throw new Exception("Failed to retrieve temperature data from Elasticsearch.");

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

    // /// <summary>
    // /// Retreives a list of HumidityData objects.
    // /// </summary>
    // /// <returns>List of HumidityData objects</returns>
    // public async Task<List<HumidityData>> GetHumidityData()
    // {

    // }

    // /// <summary>
    // /// Posts a HumidityData object to ElasticSearch.
    // /// </summary>
    // /// <param name="humidityData">The HumidityData object to be posted.</param>
    // /// <returns>A boolean indicating the success or failure of the operation.</returns>
    // public async Task<bool> PostHumidityData(HumidityData humidityData)
    // {

    // }

    // /// <summary>
    // /// Retreives a list of AirQualityData objects.
    // /// </summary>
    // /// <returns>List of AirQualityData objects</returns>
    // public async Task<List<AirQualityData>> GetAirQualityData()
    // {

    // }

    // /// <summary>
    // /// Posts an AirQualityData object to ElasticSearch.
    // /// </summary>
    // /// <param name="airQualityData">The AirQualityData object to be posted.</param>
    // /// <returns>A boolean indicating the success or failure of the operation.</returns>
    // public async Task<bool> PostAirQualityData(AirQualityData airQualityData)
    // {


    // }
  }
}