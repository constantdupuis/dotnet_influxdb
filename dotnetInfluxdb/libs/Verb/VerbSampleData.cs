using System;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;

namespace dotnetInfluxdb
{

  public class VerbSampleData : IVerbRunner
  {
    public string Verb { get; set; } = "SampleData";
    public string Description { get; set; } = "Inject sample data in the database";

    public void Close()
    {

    }

    public string[] GetArguments()
    {
      return new string[] { "No Arguments" };
    }

    public void Init()
    {

    }

    public bool ParseArgs(string[] args)
    {
      return true;
    }

    public async Task Run()
    {
      Console.WriteLine("Inject sample data in database playground measurment timetracking");
      var dataRows = GenerateDataFrom(DateTime.Now, 100);
      var client = new InfluxClient(new Uri("http://localhost:8086"), "admin", "toto");
      await client.WriteAsync("playground", "timetracking", dataRows);
    }

    private TimetrackingModel[] GenerateDataFrom(DateTime from, int rowCount)
    {
      var rng = new Random();
      var projects = new[] { "SSV3", "OBAVC", "Walibi", "SSV2.5", "Vanguard" };
      var users = new[] { "Constant", "Loïc", "Arno", "Marie" };
      var labels = new[] { "Dev", "Doc", "Design", "Support" };

      var timestamp = from;

      var rows = new TimetrackingModel[rowCount];

      for (int x = 0; x < rowCount; x++)
      {
        var userId = rng.Next(users.Length);
        var projId = rng.Next(projects.Length);
        var timeT = rng.NextDouble() * 8.0;

        var row = new TimetrackingModel
        {
          Timestamp = timestamp,
          UserId = userId,
          UserName = users[userId],
          ProjectId = projId,
          ProjectName = projects[projId],
          RawTimetracking = "blabla",
          ParsedTimetracking = timeT,
          Label = labels[rng.Next(labels.Length)]
        };
        rows[x] = row;

        timestamp = timestamp.AddDays(rng.NextDouble() * 4.0);
      }

      return rows;
    }
  }
}