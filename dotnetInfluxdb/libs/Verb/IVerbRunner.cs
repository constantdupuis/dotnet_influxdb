using System.Threading.Tasks;

namespace dotnetInfluxdb
{

  public interface IVerbRunner
  {
    public string Verb { get; set; }
    public string Description { get; set; }
    public bool ParseArgs(string[] args);
    public void Init(NLog.Logger logger);
    public Task Run();
    public void Close();
    public string[] GetArguments();
  }
}