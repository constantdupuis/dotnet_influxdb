using System;
using System.Threading.Tasks;
using TimetrackingModel;

namespace dotnetInfluxdb
{

  public class VerbSampleData : IVerbRunner
  {
    public string Verb { get; set; } = "SampleData";
    public string Description { get; set; } = "Inject sample data in the database";
    private NLog.Logger logger;

    public void Close()
    {

    }

    public string[] GetArguments()
    {
      return new string[] { "No Arguments" };
    }

    public void Init(NLog.Logger logger)
    {
      this.logger = logger;
    }

    public bool ParseArgs(string[] args)
    {
      return true;
    }

    public async Task Run()
    {
      var model = new ModelOperations(this.logger);
      await model.AddSampleData();
    }


  }
}