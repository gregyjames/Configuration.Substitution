using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Configuration.Substitution.Tester;

class Program
{
    static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile("settings.json");
                builder.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string?>("env", "prod")
                });
                builder.EnableSubstitution();
            })
            .ConfigureServices((context, collection) =>
            {
                collection.Configure<Settings>(context.Configuration);
                collection.AddHostedService<BGService>();
            })
            .RunConsoleAsync(options =>
            {
                options.SuppressStatusMessages = true;
            });
    }
}