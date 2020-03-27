using System;
using System.Threading.Tasks;
using TimetrackingModel;

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
        var model = new ModelOperations();
        await model.AddSampleData();
    }

  
  }
}