using System.Collections.Generic;
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

      // Add programer timetracking
      // TODO

      sb.Append("\n");
      return sb.ToString();
    }
  }

  public class PerDeveloperStats : Developer
  {
    public Dictionary<string, Project> Projects = new Dictionary<string, Project>();
  }
}