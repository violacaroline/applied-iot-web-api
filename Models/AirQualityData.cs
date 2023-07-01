using System.Text.Json.Serialization;


namespace applied_iot_api.Models
{
  /// <summary>
  /// Represents an AirQuality data object.
  /// </summary>
  public class AirQualityData
  {
    [JsonPropertyName("id")]
    public String id { get; set; }

    [JsonPropertyName("value")]
    public String value { get; set; }

    [JsonPropertyName("feed_id")]
    public int feed_id { get; set; }

    [JsonPropertyName("feed_key")]
    public String feed_key { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime created_at { get; set; }

    [JsonPropertyName("created_epoch")]
    public long created_epoch { get; set; }

    [JsonPropertyName("expiration")]
    public DateTime expiration { get; set; }
  }
  //   /// <summary>
  //   /// The air quality data.
  //   /// </summary>
  //   /// <value>The air quality represented as 1=no gas and 0 = gas detected</value>
  //   public int airquality { get; set; }

  //   /// <summary>
  //   /// The timestamp when the data point was retreived.
  //   /// </summary>
  //   public DateTime timeStamp { get; set; }
  // }
}
