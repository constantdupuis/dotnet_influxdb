
namespace TimetrackingModel
{
  using System;
  using Vibrant.InfluxDB.Client;

  class TimetrackingMeasurment
  {
    [InfluxTimestamp]
    public DateTime Timestamp { get; set; }

    [InfluxTag("user-name")]
    public string UserName { get; set; }

    [InfluxTag("user-id")]
    public int UserId { get; set; }

    [InfluxTag("project-name")]
    public string ProjectName { get; set; }

    [InfluxTag("project-id")]
    public int ProjectId { get; set; }

    [InfluxTag("label")]
    public string Label { get; set; }

    [InfluxField("raw-timetracking")]
    public string RawTimetracking { get; set; }

    [InfluxField("parsed-timetracking")]
    public double ParsedTimetracking { get; set; }
  }
}
