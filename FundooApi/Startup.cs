//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BussinessLayer.Interfaces;
    using BussinessLayer.Services;
    using FundooNote.Interfaces;
    using FundooNote.Models;
    using FundooNote.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Main Class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.      

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSetting>(this.Configuration.GetSection("AppSetting"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ////configuring swagger to ask upload file option
            services.ConfigureSwaggerGen(options =>
            {
                //// Register File Upload Operation Filter
                options.OperationFilter<FileUploadOperation>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Fundoo", Version = "v1" });
            });

            //// services.AddDefaultIdentity<IdentityUser>()
            ////.AddDefaultUI(UIFramework.Bootstrap4)
            ////.AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            //// adding connection string
            services.AddDbContext<Authentication>(options =>
            options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(Config =>
            {
                Config.SignIn.RequireConfirmedEmail = true;
            })

                .AddEntityFrameworkStores<Authentication>();
            services.Configure<EmailModel>(this.Configuration.GetSection("EmailModel"));
            services.AddTransient<IApplicationControl, ApplicationUserServices>();
            services.AddTransient<IRepository, ApplicationRepository>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<INotes, NotesCreation>();
            services.AddTransient<INotesRepository, NotesRepository>();
            services.AddTransient<ILabels, LabelsBussiness>();
            services.AddTransient<ILablesRepository, LabelsRepository>();
            services.Configure<EmailModel>(this.Configuration);

            services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequireDigit = false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequiredLength = 6;
            });
            services.AddCors();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost";
                option.InstanceName = "master";
            });

            //// Jwt Authentication
            var key = Encoding.UTF8.GetBytes(this.Configuration["AppSetting:JWT_Secrete"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ////Validate the security key
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(bulider =>
            bulider.WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
            //// Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            //// specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        /// <summary>
        /// method to upload file
        /// </summary>
        public class FileUploadOperation : IOperationFilter
        {
            /// <summary>
            /// method for upload file
            /// </summary>
            /// <param name="operation">pass operation</param>
            /// <param name="context">pass context</param>
            public void Apply(Operation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<IParameter>();
                }

                ////adding auhtorization header in all api's
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = true //// set to false if this is optional
                });
            }
        }
    }
}