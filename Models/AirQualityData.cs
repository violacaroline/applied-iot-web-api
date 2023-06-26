namespace applied_iot_api.Models
{
  /// <summary>
  /// Represents an AirQuality data object.
  /// </summary>
  public class AirQualityData
  {
    /// <summary>
    /// The air quality data.
    /// </summary>
    /// <value>The air quality represented as 1=no gas and 0 = gas detected</value>
    public int airQuality { get; set; }
  }
}