using System.Threading.Tasks;
using TimetrackingModel;

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
      var model = new ModelOperations();
      await model.ShowDatabases();
    }
    public void Close()
    { }

    public string[] GetArguments()
    {
      return new string[] { "No Arguments" };
    }
  }
}