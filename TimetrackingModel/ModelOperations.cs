
namespace TimetrackingModel
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Threading.Tasks;
  using Vibrant.InfluxDB.Client;
  using Vibrant.InfluxDB.Client.Rows;

  public class ModelOperations
  {
    private NLog.Logger logger;
    public ModelOperations(NLog.Logger logger)
    {
      this.logger = logger;
    }
    public async Task ShowDatabases()
    {
      logger.Info("Show Dtabases");
      logger.Info("Create Client");
      var client = new InfluxClient(new Uri("http://localhost:8086"), "admin", "toto");
      logger.Info("Query dtabases list");
      var databases = await client.ShowDatabasesAsync();
      logger.Info("Show databases list");
      foreach (var serie in databases.Series)
      {
        //Console.WriteLine("Serie Name : {0}", serie.Name);
        foreach (var row in serie.Rows)
        {
          logger.Info(" - {0}", row.Name);
          //Console.WriteLine(" - {0}", row.Name);
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
      Dictionary<string, PerProjectStats> ret = new Dictionary<string, PerProjectStats>();

      logger.Info("Query Timetracking per projects");
      logger.Info("Create Client");
      var client = new InfluxClient(new Uri("http://localhost:8086"), "admin", "toto");
      logger.Info("Query DB...");

      // var resultSet = await client.ReadAsync<DynamicInfluxRow>(
      //   "playground",
      //   "SELECT SUM(\"parsed-timetracking\") FROM timetracking GROUP BY \"project-name\""
      //   );
      // logger.Info("Show Projects");
      // var result = resultSet.Results[0];

      // foreach (var serie in result.Series)
      // {
      //   //Console.WriteLine(" {0}", serie.Name);
      //   foreach (var tags in serie.GroupedTags)
      //   {
      //     //Console.WriteLine("   {0}: {1}", tags.Key, tags.Value);
      //     PerProjectStats stats = new PerProjectStats();
      //     stats.Name = tags.Value as string;
      //     stats.SumTime = (double)serie.Rows[0].Fields["sum"];

      //     Console.WriteLine("{0}: {1}", stats.Name, stats.SumTime);
      //     ret.Add(stats);
      //   }
      // }

      var resultSet = await client.ReadAsync<DynamicInfluxRow>(
        "playground",
        "SELECT SUM(\"parsed-timetracking\") FROM timetracking GROUP BY \"project-name\", \"user-name\", \"label\""
        );

      logger.Info("Consolidate resultats...");
      var result = resultSet.Results[0];
      foreach (var serie in result.Series)
      {
        // foreach (var tags in serie.GroupedTags)
        // {
        //   Console.WriteLine("Tasg {0}:{1}", tags.Key, tags.Value);
        // }

        var projectName = serie.GroupedTags["project-name"] as string;
        var developerName = serie.GroupedTags["user-name"] as string;
        var taskName = serie.GroupedTags["label"] as string;
        var timetracking = (double)serie.Rows[0].Fields["sum"];

        PerProjectStats projectStats;

        // accumulate project time tracking
        if (ret.ContainsKey(projectName))
        {
          projectStats = ret[projectName];
          projectStats.SumTime += timetracking;
        }
        else
        {
          projectStats = new PerProjectStats();
          projectStats.Name = projectName;
          projectStats.SumTime += timetracking;
          ret[projectName] = projectStats;
        }

        // accumulate project->task timetracking
        ProjectTask projectTask;

        if (projectStats.Tasks.ContainsKey(taskName))
        {
          projectTask = projectStats.Tasks[taskName];
          projectTask.SumTime += timetracking;
        }
        else
        {
          projectTask = new ProjectTask();
          projectTask.Name = taskName;
          projectTask.SumTime += timetracking;
          projectStats.Tasks[taskName] = projectTask;
        }

        // accumulate project->developer timetracking
        Developer projectDeveloper;

        if (projectStats.Developers.ContainsKey(developerName))
        {
          projectDeveloper = projectStats.Developers[developerName];
          projectDeveloper.SumTime += timetracking;
        }
        else
        {
          projectDeveloper = new Developer();
          projectDeveloper.Name = developerName;
          projectDeveloper.SumTime += timetracking;
          projectStats.Developers[developerName] = projectDeveloper;
        }

        // accumulate project->developer->task
        ProjectTask developerTask;

        if (projectDeveloper.Tasks.ContainsKey(taskName))
        {
          developerTask = projectDeveloper.Tasks[taskName];
          developerTask.SumTime += timetracking;
        }
        else
        {
          developerTask = new ProjectTask();
          developerTask.Name = taskName;
          developerTask.SumTime += timetracking;
          projectDeveloper.Tasks[taskName] = developerTask;
        }

      } // for each

      logger.Info("Stringify resultats...");
      var sb = new StringBuilder();
      foreach (var projStats in ret.Values)
      {
        sb.Append(projStats.ToString());
      }

      string str = sb.ToString();

      logger.Info("Show resultats...");
      Console.WriteLine(str);
    }

    private TimetrackingMeasurment[] GenerateDataFrom(DateTime from, int rowCount)
    {
      var rng = new Random();
      var projects = new[] { "Aller sur la lune", "Moteur à ordures", "Four solaire", "Composte a plastique", "Vélo à hydrogéne" };
      var users = new[] { "Bernard", "Raoul", "Thérése", "Monique", "Marcel", "Josiane" };
      var labels = new[] { "Dev", "Doc", "Design", "Support", "R&D" };

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
