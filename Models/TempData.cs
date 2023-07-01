using System.Text.Json.Serialization;

namespace applied_iot_api.Models
{
  /// <summary>
  /// Represents a Temperature data object.
  /// </summary>
  public class TempData
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
}
