using System.Collections.Generic;

namespace TimetrackingModel
{
  public class ProjectTask
  {
    public string Name { set; get; }
    public double SumTime { set; get; }
  }

  public class Developer
  {
    public string Name { set; get; }
    public double SumTime { set; get; }
    public List<ProjectTask> Tasks = new List<ProjectTask>();
  }

  public class Project
  {
    public string Name { set; get; }
    public double SumTime { set; get; }
    public List<ProjectTask> Tasks = new List<ProjectTask>();
  }

  public class PerProjectStats : Project
  {
    public List<Developer> Developers = new List<Developer>();
  }

  public class PerDeveloperStats : Developer
  {
    public List<Project> Projects = new List<Project>();
  }
}