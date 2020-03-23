using System;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;

namespace dotnetInfluxdb
{
  class Program
  {
    static void Main(string[] args)
    {
      var client = new InfluxClient(new Uri("http://localhost:8086"), "admin", "toto");
      //Task < InfluxResult < DatabaseRow >> = 
      var databases = client.ShowDatabasesAsync();
      databases.Wait();
      var dbs = databases.Result;
      Console.WriteLine(dbs.ToString());
    }
  }
}
