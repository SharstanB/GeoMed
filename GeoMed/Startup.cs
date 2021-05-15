using GeoMed.Main.Data.Repositories;
using GeoMed.Main.IData.IRepositories;
using GeoMed.Repository.DataSet.Interface;
using GeoMed.Repository.DataSet.Repository;
using GeoMed.Share.Data;
using GeoMed.Share.IData.IRepositories;
using GeoMed.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GeoMed
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
            services.AddDbContext<GMContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("GMConnectionString"))

            );
            services.AddServerSideBlazor();

            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies"
                , options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/SignOut";
                }
                )
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:44351/";
                    options.ClientId = "secret_dash_client_id";
                    options.RequireHttpsMetadata = false;
                    options.ClientSecret = "SecretGeoMedDashBoardProject2021";
                    options.SignInScheme = "Cookies";
                    options.ResponseType = "code";
                    options.Scope.Clear();
                    options.Scope.Add("dashscope");
                    options.Scope.Add("openid");
                    options.SaveTokens = true;
                });

            //services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            //.AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            //  services.AddHttpClient();

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IStoreDataRepository, StoreDataRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();

        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Index");

                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Shared/Status", "?statusCode={0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // <-- Add it here.
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapBlazorHub();
            });

        }

    }
}
