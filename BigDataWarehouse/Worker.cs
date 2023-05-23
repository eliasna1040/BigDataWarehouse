using RestSharp;
using Newtonsoft.Json;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.Extensions.Options;

namespace BigDataWarehouse
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<ApiConfiguration> _options;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IOptions<ApiConfiguration> options)
        {
            _logger = logger;
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var client = new RestClient($"{_options.Value.ApiAddress}/action/find");
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Access-Control-Request-Headers", "*");

                using (WeatherContext weatherContext = _serviceProvider.GetService<WeatherContext>())
                {
                    DataSet? dataset = await weatherContext.DataSets.OrderByDescending(x => x.TimeStamp)
                                                                     .Include(x => x.Properties)
                                                                     .FirstOrDefaultAsync();

                    string body = $@"{{""collection"": ""Weather"", ""database"": ""WeatherDB"", ""dataSource"": ""Cluster0"" {(dataset != null ? $@", ""filter"": {{ ""timeStamp"": {{ ""$gt"": ""{dataset.TimeStamp:s}Z"" }} }}" : "")} }}";
                    request.AddStringBody(body, DataFormat.Json);

                    RestResponse response = await client.PostAsync(request);
                    Root? root = JsonConvert.DeserializeObject<Root>(response.Content);

                    if (root != null)
                    {
                        await weatherContext.DataSets.AddRangeAsync(root.Documents.Select(x => new DataSet
                        {
                            TimeStamp = x.TimeStamp,
                            Properties = x.Features.Select(x => new Property
                            {
                                Created = x.Properties.Created,
                                Observed = x.Properties.Observed,
                                ParameterId = x.Properties.ParameterId,
                                StationId = x.Properties.StationId,
                                Value = x.Properties.Value
                            }).ToList(),
                        }).ToList());
                        await weatherContext.SaveChangesAsync();
                        Console.WriteLine("New data added");
                    }
                    else
                    {
                        Console.WriteLine("No updates found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}