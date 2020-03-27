
namespace TimetrackingModel
{
  using System;
  using System.Threading.Tasks;
  using Vibrant.InfluxDB.Client;
  using Vibrant.InfluxDB.Client.Rows;

  public class ModelOperations
  {
    public async Task ShowDatabases()
    {
      Console.WriteLine("Show database(s)");
      var client = new InfluxClient(new Uri("http://localhost:8086"), "admin", "toto");
      var databases = await client.ShowDatabasesAsync();
      foreach (var serie in databases.Series)
      {
        //Console.WriteLine("Serie Name : {0}", serie.Name);
        foreach (var row in serie.Rows)
        {
          Console.WriteLine(" - {0}", row.Name);
        }
      }
    }

    public async Task AddSampleData()
    {
      Console.WriteLine("Inject sample data in database playground measurment timetracking");
      var dataRows = GenerateDataFrom(DateTime.Now, 100);
      var client = new InfluxClient(new Uri("http://localhost:8086"), "admin", "toto");
      await client.WriteAsync("playground", "timetracking", dataRows);
    }

    public async Task GetTimetrackingSUMPerProjects()
    {
      Console.WriteLine("Query Timetracking per projects");
      var client = new InfluxClient(new Uri("http://localhost:8086"), "admin", "toto");
      var resultSet = await client.ReadAsync<DynamicInfluxRow>(
        "playground",
        "SELECT SUM(\"parsed-timetracking\") FROM timetracking GROUP BY \"project-name\", \"label\""
        );

      var result = resultSet.Results[0];

      foreach (var serie in result.Series)
      {
        Console.WriteLine(" {0}", serie.Name);
        foreach (var tags in serie.GroupedTags)
        {
          Console.WriteLine("   {0}: {1}", tags.Key, tags.Value);
        }

        foreach (var row in serie.Rows)
        {
          foreach (var field in row.Fields)
          {
            Console.WriteLine("   {0}: {1}", field.Key, field.Value);
          }
        }
      }
    }

    private TimetrackingMeasurment[] GenerateDataFrom(DateTime from, int rowCount)
    {
      var rng = new Random();
      var projects = new[] { "SSV3", "OBAVC", "Walibi", "SSV2.5", "Vanguard" };
      var users = new[] { "Constant", "Loïc", "Arno", "Marie" };
      var labels = new[] { "Dev", "Doc", "Design", "Support" };

      var timestamp = from;

      var rows = new TimetrackingMeasurment[rowCount];

      for (int x = 0; x < rowCount; x++)
      {
        var userId = rng.Next(users.Length);
        var projId = rng.Next(projects.Length);
        var timeT = rng.NextDouble() * 8.0;

        var row = new TimetrackingMeasurment
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
