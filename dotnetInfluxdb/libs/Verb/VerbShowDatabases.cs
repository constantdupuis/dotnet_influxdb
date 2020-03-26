using System;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;

namespace dotnetInfluxdb
{

  public class VerbShowDatabase : IVerbRunner
  {

    public string Verb { get; set; } = "ShowDatabase";
    public string Description { get; set; } = "List available InfluxDB databases";

    public bool ParseArgs(string[] args)
    {
      return true;
    }

    public void Init()
    { }

    public async Task Run()
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
    public void Close()
    { }

    public string[] GetArguments()
    {
      return new string[] { "No Arguments" };
    }
  }
}