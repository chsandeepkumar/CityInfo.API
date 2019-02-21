using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfo_API
{
    public class Startup
    {
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseMvc();
          
        }
    }
}
