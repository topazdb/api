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
using Newtonsoft.Json;
using api.db;
using api.Models;
using api.Util;

namespace api {
    public class Startup {
        private Store store = new Store();

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            store.populator = new Populator(@"/data");
            
            FetchSchemaVersion();
            ValidateSchemaVersion();

            new System.Threading.Timer((e) => new Populator("@/data"), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
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
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
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

        /// <summary>
        /// Retrieve the schema version from the database and save it
        /// </summary>
        private void FetchSchemaVersion() {
            Context context = new Context();
            string raw = (from setting in context.Settings where setting.name == "version" select setting.value).FirstOrDefault();

            try {
                store.schemaVersion = Int64.Parse(raw);
            } catch(Exception e) {
                store.schemaVersion = 1;
            }
        }

        private void ValidateSchemaVersion() {
            if(store.schemaVersion != Configuration.GetValue<long>("SchemaVersion", 1)) {
                System.Console.WriteLine("Unhandled database schema version.  Exiting...");
                System.Environment.Exit(1);
            }
        }
    }
}
