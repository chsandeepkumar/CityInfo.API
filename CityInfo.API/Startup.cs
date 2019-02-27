using System;
using CityInfo.API.DBContext;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfo_API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder =
                new ConfigurationBuilder()
                    .SetBasePath(hostingEnvironment.ContentRootPath)
                    .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appSettings.{hostingEnvironment.EnvironmentName}.json", optional: true,
                        reloadOnChange: true);
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(o=>o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            //to serailize the json string properties with same name casing, if C# class has Student than json response also can have "Student"
            //services.AddMvc().AddJsonOptions(x =>
            //{
            //    switch (x.SerializerSettings.ContractResolver)
            //    {
            //        case null:
            //            return;
            //        case DefaultContractResolver contractResolver:
            //            contractResolver.NamingStrategy = null;
            //            break;
            //    }
            //});
            services.AddDbContext<CityInfoContext>(context=>context.UseSqlServer(Configuration["connectionStrings:cityInfoDBConnectionString"]));
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,CityInfoContext cityInfoContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            cityInfoContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();
            app.UseMvc();
          
        }
    }
}
