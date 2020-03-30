
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
  using NLog;
  class Program
  {
    static Logger logger;
    static void Main(string[] args)
    {
      var app = new Program();
      logger = LogManager.GetCurrentClassLogger();

      app.MainAsync(args).GetAwaiter().GetResult();
    }

    private Dictionary<string, IVerbRunner> verbRunners = new Dictionary<string, IVerbRunner>();

    private async Task MainAsync(string[] args)
    {
      logger.Info(" Start Main");
      logger.Info(" Register Verb Runners");
      RegisterVerbRunners();
      logger.Info(" Verb runners registered");
      logger.Info(" Run Verb");
      await RunVerb(args);
      logger.Info(" Verb runned");
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
      runner.Init(logger);
      verbRunners.Add(runner.Verb, runner);

      runner = new VerbSampleData();
      runner.Init(logger);
      verbRunners.Add(runner.Verb, runner);

      runner = new VerbShowPerProjects();
      runner.Init(logger);
      verbRunners.Add(runner.Verb, runner);
    }
  }
}
