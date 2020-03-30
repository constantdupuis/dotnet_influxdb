using System.Threading.Tasks;
using TimetrackingModel;

namespace dotnetInfluxdb
{
  public class VerbShowDatabase : IVerbRunner
  {

    public string Verb { get; set; } = "ShowDatabase";
    public string Description { get; set; } = "List available InfluxDB databases";
    private NLog.Logger logger;

    public bool ParseArgs(string[] args)
    {
      return true;
    }

    public void Init(NLog.Logger logger)
    {
      this.logger = logger;
    }

    public async Task Run()
    {
      var model = new ModelOperations(this.logger);
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