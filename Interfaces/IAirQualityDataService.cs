using applied_iot_api.Models;

namespace applied_iot_api.Services
{
  /// <summary>
  /// Interface for collecting air quality data from IoT soruce.
  /// </summary>
  public interface IAirQualityDataService
  {
    /// <summary>
    /// Retreives a list of TempData objects.
    /// </summary>
    /// <returns>List of TempData objects</returns>
    public Task<List<TempData>> GetTempData();

    /// <summary>
    /// Posts a TempData object to ElasticSearch.
    /// </summary>
    /// <param name="tempData">The TempData object to be posted.</param>
    /// <returns>A boolean indicating the success or failure of the operation.</returns>
    public Task<bool> PostTempData(TempData tempData);

    // /// <summary>
    // /// Retreives a list of HumidityData objects.
    // /// </summary>
    // /// <returns>List of HumidityData objects</returns>
    // public Task<List<HumidityData>> GetHumidityData();

    // /// <summary>
    // /// Posts a HumidityData object to ElasticSearch.
    // /// </summary>
    // /// <param name="humidityData">The HumidityData object to be posted.</param>
    // /// <returns>A boolean indicating the success or failure of the operation.</returns>
    // public Task<bool> PostHumidityData(HumidityData humidityData);

    // /// <summary>
    // /// Retreives a list of AirQualityData objects.
    // /// </summary>
    // /// <returns>List of AirQualityData objects</returns>
    // public Task<List<AirQualityData>> GetAirQualityData();

    // /// <summary>
    // /// Posts an AirQualityData object to ElasticSearch.
    // /// </summary>
    // /// <param name="airQualityData">The AirQualityData object to be posted.</param>
    // /// <returns>A boolean indicating the success or failure of the operation.</returns>
    // public Task<bool> PostAirQualityData(AirQualityData airQualityData);

  }
}
