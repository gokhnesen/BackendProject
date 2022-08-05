using BackendProject.Data.Abstract;
using BackendProject.Data.Concrete;
using BackendProject.Data.Concrete.Elastic;
using BackendProject.Data.Validators;
using BackendProject.Entity.Context;
using Elasticsearch.Net;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nest;
using System;
using System.Text;
using TechBuddy.Middlewares.RequestResponse;

namespace BackendProject
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Appsettings:Token").Value);

            services.AddLogging(conf =>
            {
                conf.AddConsole();

            });

            services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddPersonRequestValidator>());
            services.AddControllers()
           .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdatePersonRequestValidator>());
            services.AddControllers()
  .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddTeamRequestValidator>());
            services.AddControllers()
.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdatePersonRequestValidator>());
            services.AddDbContext<PersonAdminContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PersonAdminPortalDb")));
            services.AddScoped<IPersonRepository, SqlPersonRepository>();
            services.AddScoped<IKPIRepository, SqlKPIRepository>();
            services.AddScoped<ITeamRepository, SqlTeamRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IGenderRepository, SqlGenderRepository>();

            services.AddScoped<IMonitorRepository, ElasticRepository>();



            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendProject", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

        

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey=true, //Tokenlara herkezin erişimini kısıtlama
                    IssuerSigningKey=new SymmetricSecurityKey(key),
                    ValidateIssuer=false,
                    ValidateAudience=false
                };
            });

            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(pool)
                .DefaultIndex("backendproject-development-2022-07");
            var client = new ElasticClient(settings);
            services.AddSingleton(client);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","BackendProject v1"));
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MY API v1"));
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyPolicy");

            app.UseAuthentication();

            
            app.UseAuthorization();

            app.AddTBRequestResponseMiddleware(opt =>
            {
                opt.UseLogger(app.ApplicationServices.GetService<ILoggerFactory>(), opt =>
                 {
                     opt.LoggingFields.Add(LogFields.Response);
                     opt.LoggingFields.Add(LogFields.Request);
                     opt.LoggingFields.Add(LogFields.ResponseTiming);
                  

                 });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
