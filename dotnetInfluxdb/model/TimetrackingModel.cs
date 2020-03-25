using System;
using Vibrant.InfluxDB.Client;

namespace dotnetInfluxdb
{
  public class Timetracking
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

    [InfluxField("raw-timetracking")]
    public string RawTimetracking { get; set; }

    [InfluxField("parsed-timetracking")]
    public double ParsedTimetracking { get; set; }

    [InfluxField("labels")]
    public string Labels { get; set; }

  }
}