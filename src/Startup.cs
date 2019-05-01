using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using api.db;
using api.Models;
using api.Util;

namespace api {
    public class Startup {
        private Store store = new Store();

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            store.populator = new Populator(@"/data");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.Authority = Environment.GetEnvironmentVariable("TOPAZ_OKTA_DOMAIN") + "/oauth2/default";
                options.Audience = "api://default";
            });

            services
                .AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.Converters.Add(new ModelJsonConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<Store>(store);
            services.AddDbContext<Context>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // app.UseHsts();
            }
            
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
