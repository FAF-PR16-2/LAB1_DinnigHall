using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DinnerHall
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunAsync();

            
            RandomOrdersGenerator.GetInstance().Configure();

            List<Table> tables = new List<Table>();
            List<Waiter> waiters = new List<Waiter>();

            foreach (var tableId in Enumerable.Range(0, Configuration.TableCount).ToArray())
            {
                tables.Add(new Table(tableId));
            }

            foreach (var table in tables)
            {
                table.Start();
            }
            
            foreach (var waiterId in Enumerable.Range(0, Configuration.WaitersCount).ToArray())
            {
                waiters.Add(new Waiter(waiterId, tables.ToArray()));
            }

            DistributionHallManager.GetInstance().Waiters = waiters.ToArray();
            
            foreach (var waiter in waiters)
            {
                waiter.Start();
            }
            
            
            //await randomOrdersGenerator.StartAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
