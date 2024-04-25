using Common.Helpers;
using Ethereum.Contract;
using Microsoft.Extensions.DependencyInjection;
using RateScript.Implementation;
using RateScript.Interface;
using Repository;

internal class Program
{

    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddRepository();
        services.AddContract();
        services.AddScoped<IRateService, RateService>();
        var serviceProvider = services.BuildServiceProvider();
        Console.WriteLine("Script Started");
        Console.WriteLine(AppSettingHelper.GetDefaultConnection().Split(";")[0]);
        while (true)
        {
            try
            {
                var rateService = serviceProvider.GetService<IRateService>();
                await rateService.UpdateRate();
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

    }


}
