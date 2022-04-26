using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;
using API.Services;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace API
{
    public class Startup
    {
        // readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            //services.AddControllers();
            services.AddDbContext<minidressContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsApi",
            //        builder => builder.WithOrigins("http://localhost:3000", "http://localhost:5000")
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowCredentials());
            //});
            //services.AddDbContext<minidressContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);

            //// configure strongly typed settings objects
            //var appSettingsSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            //// configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});
            //services.AddControllers().AddNewtonsoftJson();
            //services.AddRazorPages();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });

            services.AddScoped<IAdminReposity, AdminReposity>();
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "Public/images")),
                RequestPath = "/Public/images"
            });
            //app.UseRouting();
            //app.UseCors("CorsApi");
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               // endpoints.MapRazorPages();
            });
            // app.UseCors(MyAllowSpecificOrigins);
            //app.UseCors(x => x
            //     .SetIsOriginAllowed(origin => true)
            //     .AllowAnyMethod()
            //     .AllowAnyHeader()
            //     .AllowCredentials());
            
            //app.UseEndpoints(x => x.MapControllers());
        }
    }
}
