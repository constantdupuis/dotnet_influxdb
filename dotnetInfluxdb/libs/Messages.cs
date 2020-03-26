using System;
using System.Collections.Generic;

namespace dotnetInfluxdb
{
  public class Messages
  {
    public static void Usage(Dictionary<string, IVerbRunner> verbRunners)
    {
      Console.Write("DotNetInfluxDB v0.1");
      Console.WriteLine("");
      Console.WriteLine("Usage:  dotnetinlfuxdb <VERB> [VERB AGRUMENTS]");
      Console.WriteLine("");
      Console.WriteLine("Available verbs:");
      foreach (var verbRunner in verbRunners)
      {
        Console.WriteLine(" - {0} : {1}", verbRunner.Key, verbRunner.Value.Description);
      }
      Console.WriteLine("");
    }
  }
}