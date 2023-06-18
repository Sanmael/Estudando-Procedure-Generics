using ClassLibrary1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WeebHookService2
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int inicio = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                var url = "https://webhook.site/05ebce58-7881-4b1e-983a-5560ffa8ca11";


                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

                Console.WriteLine(inicio);

                TestandoClasses testandoClasses = new TestandoClasses(configuration.GetConnectionString("DefaultConnection"));

                var pessoas = testandoClasses.ObterPessoas();
                pessoas.Select(x=> x.Data = DateTime.Now);
                HttpClient httpClient = new HttpClient();

                httpClient.Timeout = TimeSpan.FromSeconds(3000);

                var json = JsonSerializer.Serialize(pessoas);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                await httpClient.PostAsync($"{url}", stringContent);

                inicio++;
            }
        }
    }
}
