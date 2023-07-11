namespace ECSA.WebSocketPractice.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(webBuilder =>
          {
            webBuilder.UseStartup<Startup>();
            webBuilder.UseUrls("http://localhost:5000");
          })
          .Build();

      host.Run();
    }
  }
}