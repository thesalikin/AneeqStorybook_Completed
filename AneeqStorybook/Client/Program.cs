using AneeqStorybook.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace AneeqStorybook.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddHttpClient("AneeqStorybook.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            //    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("AneeqStorybook.ServerAPI", (sp, client) => {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                client.EnableIntercept(sp);
            })
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>(); // Update for Week13 Lab28


            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AneeqStorybook.ServerAPI"));

            builder.Services.AddHttpClientInterceptor();// Week13
            builder.Services.AddScoped<HttpInterceptorService>();

            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
