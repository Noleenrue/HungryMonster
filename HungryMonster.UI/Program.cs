using Microsoft.Extensions.DependencyInjection;

namespace HungryMonster.UI;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();

        services.AddHttpClient<ApiService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7237/");
        });

        var provider = services.BuildServiceProvider();

        var api = provider.GetRequiredService<ApiService>();
        Application.Run(new Form1(api));
    }
}