using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TicketSystem.Models;
using TicketSystem.Models.AgreementModels;
using TicketSystem.Models.AssetModels;
using TicketSystem.Models.ClientModels;
using TicketSystem.Models.ContactModels;
using TicketSystem.Services;

namespace TicketSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddHttpClient<IGenericService<Client>, GenericService<Client>>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44380/");
            });

            builder.Services.AddHttpClient<IGenericService<Ticket>, GenericService<Ticket>>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44380/");
            });

            builder.Services.AddHttpClient<IGenericService<Asset>, GenericService<Asset>>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44380/");
            });

            builder.Services.AddHttpClient<IGenericService<Agreement>, GenericService<Agreement>>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44380/");
            });

            builder.Services.AddHttpClient<IGenericService<Contact>, GenericService<Contact>>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44380/");
            });


            builder.Services.AddAutoMapper(typeof(TicketProfile));
            builder.Services.AddAutoMapper(typeof(AssetProfile));
            builder.Services.AddAutoMapper(typeof(AgreementProfile));
            builder.Services.AddAutoMapper(typeof(ClientProfile));
            builder.Services.AddAutoMapper(typeof(ContactProfile));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            builder.Services.AddBlazoredSessionStorage();

            await builder.Build().RunAsync();
        }
    }
}
