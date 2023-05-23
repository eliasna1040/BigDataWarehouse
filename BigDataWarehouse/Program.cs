using BigDataWarehouse;
using DAL;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<ApiConfiguration>(configuration.GetSection(nameof(ApiConfiguration)));
        services.AddHostedService<Worker>().AddDbContext<WeatherContext>(x => x.UseSqlServer(hostContext.Configuration.GetConnectionString("Warehouse")));
    })
    .Build();

host.Run();
