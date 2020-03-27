using System.Threading.Tasks;
using TimetrackingModel;

namespace dotnetInfluxdb
{
  class VerbShowPerProjects : IVerbRunner
  {
    public string Verb { get; set; } = "QueryPerProjects";
    public string Description { get; set; } = "Return timetraking grouped by projects";

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
      await model.GetTimetrackingSUMPerProjects();
    }
  }
}
