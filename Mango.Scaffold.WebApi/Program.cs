using Hangfire.HttpJob.Agent.MysqlConsole;
using Mango.Core.Authentication.Extension;
using Mango.Core.Converter.Extension;
using Mango.Core.HangfireScheduler.Extension;
using Mango.EntityFramework.Extension;
using Mango.Scaffold.Repository;
using Mango.Scaffold.Repository.Abstractions;
using Mango.Scaffold.Service;
using Mango.Scaffold.Service.Abstractions;
using Microsoft.Extensions.DependencyModel;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Mango.Scaffold.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddMangoJsonConvert();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            #region swagger����
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mango.Scaffold.Model Api",
                    Version = "v1",
                    Description = "description."
                });

                // Set the comments path for the Swagger JSON and UI.
                // ����ע��
                var assemblyList = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in assemblyList)
                {
                    var xmlFile = $"{assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                }
            });
            #endregion

            #region jwt����
            builder.Services.AddMangoJwtHandler(op =>
            {
                op.Key = "QWh7oKa0f02XE1skTzokGzu0ozZJtajF";
                op.DefalutIssuer = "Mango.BaseApi";
                op.DefalutAudience = "Mango.BaseApi";
            });
            builder.Services.AddMangoJwtAuthentication(op =>
            {
                op.Key = "QWh7oKa0f02XE1skTzokGzu0ozZJtajF";
                op.Issuer = "Mango.BaseApi";
                op.Audience = "Mango.BaseApi";
            });
            builder.Services.AddMangoRedisAuthenticationTokenStorage("182.92.80.198:6379,defaultDatabase=0,password=czh228887474/");
            #endregion

            #region log4net

            builder.Logging.AddLog4Net("log4net.config");

            #endregion

            #region dbContent
            builder.Services.AddMangoDbContext<ImpDbContext>("Database=Base_Test;Data Source=182.92.80.198;Port=3306;UserId=root;Password=czh228887474/;Charset=utf8;TreatTinyAsBoolean=false;Allow User Variables=True");

            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
            builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
            #endregion

            #region ����ע��
            builder.Services.AddScoped<IUserService, UserService>();
            #endregion

            #region ����
            builder.Services.AddCors(op =>
            {
                op.AddPolicy("allowAll", policy =>
                {
                    policy.SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            #endregion

            #region hangfireAgent

            builder.Services.AddMangoHangfireAgent();

            #endregion

            var app = builder.Build();

            app.UseMangoHangfireAgent("root", "czh228887474/");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("allowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}