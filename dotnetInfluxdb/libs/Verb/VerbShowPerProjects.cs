using System;
using System.Threading.Tasks;
using TimetrackingModel;

namespace dotnetInfluxdb
{
  class VerbShowPerProjects : IVerbRunner
  {
    public string Verb { get; set; } = "QueryPerProjects";
    public string Description { get; set; } = "Return timetraking grouped by projects";
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
      var model = new ModelOperations(logger);
      var json = await model.GetTimetrackingSUMPerProjects();
      Console.WriteLine(json);
    }
  }
}
