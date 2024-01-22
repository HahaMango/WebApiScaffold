using AspectCore.Extensions.DependencyInjection;
using Hangfire.HttpJob.Agent.MysqlConsole;
using Mango.Core.Authentication.Extension;
using Mango.Core.Converter.Extension;
using Mango.Core.HangfireScheduler.Extension;
using Mango.Core.Ioc.Extension;
using Mango.EntityFramework.Extension;
using Mango.Scaffold.Repository;
using Mango.Scaffold.Repository.Abstractions;
using Mango.Scaffold.Service;
using Mango.Scaffold.Service.Abstractions;
using Microsoft.AspNetCore.HttpOverrides;
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

            var config = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers().AddMangoJsonConvert();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            #region swagger配置
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mango.Scaffold.Model Api",
                    Version = "v1",
                    Description = "description."
                });

                // Set the comments path for the Swagger JSON and UI.
                // 设置注释
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

            #region jwt配置
            builder.Services.AddMangoJwtHandler(op =>
            {
                op.Key = config.GetValue<string>("Jwt:Key");
                op.DefalutIssuer = config.GetValue<string>("Jwt:Issuer");
                op.DefalutAudience = config.GetValue<string>("Jwt:Audience");
            });
            builder.Services.AddMangoJwtAuthentication(op =>
            {
                op.Key = config.GetValue<string>("Jwt:Key");
                op.Issuer = config.GetValue<string>("Jwt:Issuer");
                op.Audience = config.GetValue<string>("Jwt:Audience");
            });
            builder.Services.AddMangoRedisAuthenticationTokenStorage(config.GetValue<string>("Jwt:RedisTokenStorage"));
            #endregion

            #region log4net

            builder.Logging.AddLog4Net("log4net.config");

            #endregion

            #region dbContent
            builder.Services.AddMangoDbContext<ImpDbContext>(config.GetValue<string>("DbConnectionString"));
            #endregion

            #region 跨域
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

            #region 自动服务注入
            builder.Services.AutoDetectService();
            #endregion

            #region AOP动态代理配置
            builder.Services.ConfigureDynamicProxy();
            builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
            #endregion

            var app = builder.Build();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMangoHangfireAgent(config.GetValue<string>("HangfireAgent:UserName"), config.GetValue<string>("HangfireAgent:Password"));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler("/api/error");

            app.UseCors("allowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}