using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RecruitmentAssignment.DAL.Models;
using RecruitmentAssignment.DAL.SqlLite.DatabazeInitializer;
using RecruitmentAssignment.Models;
using System;
using System.IO;
using System.Linq;

namespace RecrutmentAssigment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //NOT PART OF FINAL SOLUTION
            if (args.Any(p => p.Contains("buildDb")))
            {
                var inputData = File.ReadAllText("../../sample-data.json");
                var accountsDto = JsonConvert.DeserializeObject<AccountDto[]>(inputData);
                var accounts = accountsDto.Select(p => new Account
                {
                    Id = p.ApplicationId,
                    ExternalId = p.Id,
                    Amount = p.Amount,
                    ClearedDate = p.ClearedDate,
                    IsCleared = p.IsCleared,
                    PostingDate = p.PostingDate,
                    Summary = Enum.Parse<AccountSummary>(p.Summary),
                    Type = Enum.Parse<AccountType>(p.Type)
                }).ToArray();
                new DatabazeInitializer().Init(accounts);
            }
            else
            {
                CreateHostBuilder(args).Build().Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
