﻿using System;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;

namespace dotnetInfluxdb
{
  class Program
  {
    static void Main(string[] args)
    {
      var app = new Program();

      app.Test();
    }

    private void Test()
    {
      // var client = new InfluxClient(new Uri("http://localhost:8086"), "admin", "toto");
      // var databases = client.ShowDatabasesAsync();
      // databases.Wait();
      // var dbs = databases.Result;
      // Console.WriteLine(dbs.ToString());
      var rows = GenerateDataFrom(DateTime.Now, 10);
    }

    private Timetracking[] GenerateDataFrom(DateTime from, int rowCount)
    {
      var rng = new Random();
      var projects = new[] { "SSV3", "OBAVC", "Walibi", "SSV2.5", "Vanguard" };
      var users = new[] { "Constant", "Loïc", "Arno", "Marie" };
      var labels = new[] { "Dev", "Doc", "Design", "Support" };

      var timestamp = from;

      var rows = new Timetracking[rowCount];

      for (int x = 0; x < rowCount; x++)
      {
        var userId = rng.Next(users.Length);
        var projId = rng.Next(projects.Length);
        var timeT = rng.NextDouble() * 8.0;

        var row = new Timetracking
        {
          Timestamp = timestamp,
          UserId = userId,
          UserName = users[userId],
          ProjectId = projId,
          ProjectName = projects[projId],
          RawTimetracking = "blabla",
          ParsedTimetracking = timeT,
          Labels = labels[rng.Next(labels.Length)]
        };
        rows[x] = row;

        timestamp = timestamp.AddDays(rng.NextDouble() * 4.0);

      }

      return rows;
    }
  }
}
