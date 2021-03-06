using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TimetrackingModel
{
  public class ProjectTask
  {
    public string Name { set; get; }
    public double SumTime { set; get; } = 0.0;

    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.AppendFormat("Task: {0} - {1:0.00} Hours", Name, SumTime);
      return sb.ToString();
    }
  }

  public class Developer
  {
    public string Name { set; get; }
    public double SumTime { set; get; } = 0.0;
    public Dictionary<string, ProjectTask> Tasks = new Dictionary<string, ProjectTask>();

    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.AppendFormat("Dev: {0} - {1:0.00} Hours\n", Name, SumTime);
      foreach (var task in Tasks.Values)
      {
        sb.Append(task.ToString());
        sb.Append("\n");
      }
      return sb.ToString();
    }
  }

  public class Project
  {
    public string Name { set; get; }
    public double SumTime { set; get; } = 0.0;
    public Dictionary<string, ProjectTask> Tasks = new Dictionary<string, ProjectTask>();
  }

  public class PerProjectStats : Project
  {
    public Dictionary<string, Developer> Developers = new Dictionary<string, Developer>();

    public override string ToString()
    {
      var sb = new StringBuilder();

      sb.AppendFormat("Project Name: {0} - {1:0.00} Hours\n", Name, SumTime);
      // Add Project tasks timetracking
      foreach (var task in Tasks.Values)
      {
        sb.Append(task.ToString());
        sb.Append("\n");
      }
      sb.Append("\n");

      // Add programer timetracking
      // TODO
      foreach (var dev in Developers.Values)
      {
        sb.Append(dev.ToString());
        sb.Append("\n");
      }

      sb.Append("\n");
      return sb.ToString();
    }
  }

  public class PerDeveloperStats : Developer
  {
    public Dictionary<string, Project> Projects = new Dictionary<string, Project>();
  }

  public static class ToJson
  {
    public static string PerProjectStatsToJSON(ICollection<PerProjectStats> projectsStats)
    {
      var sb = new StringBuilder();

      sb.Append("[\n");
      var projectCount = projectsStats.Count;
      int counter = 0;
      foreach (var projStats in projectsStats)
      {
        sb.Append(" {\n");
        sb.AppendFormat("  \"name\": \"{0}\",\n", projStats.Name);
        sb.AppendFormat("  \"time\": {0},\n", projStats.SumTime.ToString("0.00", new CultureInfo("en-US")));
        sb.Append("  \"tasks\": [\n");
        var taskNbr = projStats.Tasks.Values.Count;
        var taskCounter = 0;
        foreach (var task in projStats.Tasks.Values)
        {
          sb.Append("     {\n");
          sb.AppendFormat("       \"name\" : \"{0}\",\n", task.Name);
          sb.AppendFormat("       \"time\" : {0}\n", task.SumTime.ToString("0.00", new CultureInfo("en-US")));

          taskCounter++;
          if (taskCounter < taskNbr)
          {
            sb.Append("     },\n");
          }
          else
          {
            sb.Append("     }\n");
          }
        }
        sb.Append("   ],\n");
        sb.Append("  \"developers\": [\n");
        var developerNbr = projStats.Developers.Values.Count;
        var developerCounter = 0;
        foreach (var dev in projStats.Developers.Values)
        {
          sb.Append("     {\n");
          sb.AppendFormat("       \"name\" : \"{0}\",\n", dev.Name);
          sb.AppendFormat("       \"time\" : {0},\n", dev.SumTime.ToString("0.00", new CultureInfo("en-US")));
          sb.Append("       \"tasks\": [\n");
          var devTasksNbr = dev.Tasks.Values.Count;
          var devTasksCounter = 0;
          foreach (var task in dev.Tasks.Values)
          {
            sb.Append("         {\n");
            sb.AppendFormat("           \"name\" : \"{0}\",\n", task.Name);
            sb.AppendFormat("           \"time\" : {0}\n", task.SumTime.ToString("0.00", new CultureInfo("en-US")));
            devTasksCounter++;
            if (devTasksCounter < devTasksNbr)
            {
              sb.Append("         },\n");
            }
            else
            {
              sb.Append("         }\n");
            }
          }

          sb.Append("       ]\n");
          developerCounter++;
          if (developerCounter < developerNbr)
          {
            sb.Append("     },\n");
          }
          else
          {
            sb.Append("     }\n");
          }
        }
        sb.Append("   ]\n");

        counter++;
        if (counter < projectCount)
        {
          sb.Append(" },\n");
        }
        else
        {
          sb.Append(" }\n");
        }
      }
      sb.Append("]");

      return sb.ToString();
    }
  }


}