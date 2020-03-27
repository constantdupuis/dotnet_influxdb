
/**
 * 
 * Train on DotNet Core, InfluxDB
 * 
 */
namespace dotnetInfluxdb
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  class Program
  {
    static void Main(string[] args)
    {
      var app = new Program();

      app.MainAsync(args).GetAwaiter().GetResult();
    }

    private Dictionary<string, IVerbRunner> verbRunners = new Dictionary<string, IVerbRunner>();

    private async Task MainAsync(string[] args)
    {
      RegisterVerbRunners();
      await RunVerb(args);
      // clean
    }

    private async Task RunVerb(string[] args)
    {
      if (args.Length == 0)
      {
        Messages.Usage(verbRunners);
        return;
      }

      var verb = args[0].ToUpper();
      IVerbRunner runner = null;

      foreach (var vr in verbRunners)
      {
        if (vr.Key.ToUpper().Equals(verb))
        {
          runner = vr.Value;
        }
      }

      if (runner == null)
      {
        Messages.Usage(verbRunners);
        return;
      }

      if (runner.ParseArgs(args))
      {
        await runner.Run();
      }

      return;
    }

    private void RegisterVerbRunners()
    {
      IVerbRunner runner = new VerbShowDatabase();
      runner.Init();
      verbRunners.Add(runner.Verb, runner);

      runner = new VerbSampleData();
      runner.Init();
      verbRunners.Add(runner.Verb, runner);

      runner = new VerbShowPerProjects();
      runner.Init();
      verbRunners.Add(runner.Verb, runner);
    }
  }
}
