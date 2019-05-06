using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dapper_rest_api.Data.Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace dapper_rest_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped(typeof(IRepository<>), typeof(DapperRepository<>));
            services.AddTransient<ProductsRepository>();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("DapperApi", new Info
                {
                    Title = "This Api Tutorial of Dapper",
                    Version = "1.0.0",
                    Contact = new Contact
                    {
                        Name = "Emre Balcı",
                        Url = "https://medium.com/@emrebalcii94/"
                    }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger().UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/DapperApi/swagger.json", "Swagger DapperApi");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
