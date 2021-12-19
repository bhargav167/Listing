using AutoMapper;
using ListingApi.Data;
using ListingApi.Helper;
using ListingApi.Reponces.Auth;
using ListingApi.Repository.AuthenticationRepo;
using ListingApi.Repository.ListRepo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace ListingApi
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup (IConfiguration config) {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext> (s => s.UseSqlServer (_config.GetConnectionString ("DefaultConnection")));
            services.AddScoped<IAuthRepo, AuthRepo> ();
            services.AddScoped<IListItem, ListItems> ();
            services.AddControllers();
            services.AddCors ();
             services.Configure<CloudinarySettings> (_config.GetSection ("CloudinarySettings"));
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration (mc => {
                mc.AddProfile (new Automapperprofiling ());
            });
            IMapper mapper = mappingConfig.CreateMapper ();
            AuthResponces responces = new AuthResponces ();
             services.AddControllers (option => { option.EnableEndpointRouting = false; })
                .SetCompatibilityVersion (CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson (options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver ();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddControllersWithViews ()
                .AddNewtonsoftJson (options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddSingleton (mapper);
            services.AddSingleton (responces);
              services.AddAuthentication ().AddGoogle (options => {
                options.ClientId = "589474938406-r32msaen3318nf03n47vmp2fk76g9jb1.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "ListingApi", Version = "v1" });
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              //  app.UseSwagger();
              //  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ListingApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
               app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod ()
                .AllowAnyHeader ()
            );
             app.UseAuthentication ();
            app.UseAuthorization ();
            app.UseStaticFiles ();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                  endpoints.MapFallbackToController ("Index", "Fallback");
            });
        }
    }
}
