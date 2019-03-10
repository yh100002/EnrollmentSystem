using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentApi.Logic.Interfaces;
using EnrollmentApi.Logic.Persistence;
using EnrollmentApi.Logic.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PerformanceLogger;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using NSwag.AspNetCore;
using MassTransit;
using MassTransit.Util;
using RabbitMQ.Client;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EnrollmentLogic.Configuration;
using EnrollmentLogic.ExceptionHandler;
using EnrollmentLogic.Helpers;
using EnrollmentLogic.Log;

namespace EnrollmentApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
             /*
            Cross Origin Resource Sharing (CORS) is a W3C standard that allows a server to relax the same-origin policy. 
            Using CORS, a server can explicitly allow some cross-origin requests while rejecting others. 
            */
            services.AddCors();      

             /*
            - Adding the mvc framework which bahaves the latest version 2.2            
            - in a situation where object A has a reference object B and object B has a reference to object A (Circular reference), 
            there is a risk that the serializer will get stuck in a loop endlessly following the references between the objects. 
            To avoid this situation, the serializer throws an exception when it follows a reference to an object that it has already serialized.
            Otherwise Process is terminated due to​​ StackOverflowException!
             */                
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt => {
                    opt.SerializerSettings.ReferenceLoopHandling = 
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });   

             // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Enrollment Api Gateway Service";
                    document.Info.Description = "This Api gateway service is to expose entry point of apis for front-ends.";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Young Ho Son",
                        Email = string.Empty,
                        Url = ""
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Unlimited",
                        Url = "https://example.com/license"
                    };
                };
            }); 

            var commandsConnectionString = new CommandsConnectionString(Configuration["ConnectionString"]);
            var queriesConnectionString = new QueriesConnectionString(Configuration["QueriesConnectionString"]);
            services.AddSingleton(commandsConnectionString);
            services.AddSingleton(queriesConnectionString);
            services.AddSingleton<IDBSessionFactory, SessionFactory>();
            services.AddSingleton<IDBQuerySessionFactory, SessionQueryFactory>();
            services.AddSingleton<IMessages, Messages>();            
            services.AddHandlers();

            services.Configure<ElasticConnectionSettings>(Configuration.GetSection("ElasticConnectionSettings"));    
            services.AddSingleton(typeof(ElasticClientProvider));                  
            services.AddScoped<ILogViewRepository, LogViewRepository>();

            var builder = new ContainerBuilder();

            builder.Register(c =>
            {
                return Bus.Factory.CreateUsingRabbitMq(sbc =>
                {
                    sbc.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("rabbitmq");
                        h.Password("rabbitmq");
                    });

                    sbc.ExchangeType = ExchangeType.Fanout;
                });
            })
            .As<IBusControl>()
            .As<IBus>()
            .As<IPublishEndpoint>()
            .SingleInstance();

            builder.Populate(services);
            ApplicationContainer = builder.Build();

             //SeriLog            
            var url = Configuration.GetSection("ElasticConnectionSettings:ClusterUrl").Value;
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() //levels can be overridden per logging source
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(url)) //Logging to Elasticsearch
            {
                AutoRegisterTemplate = true //auto index template like logstash as prefix           
            }).CreateLogger();  

            return new AutofacServiceProvider(ApplicationContainer);           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
             // Handles non-success status codes with empty body
            app.UseExceptionHandler("/errors/500");            
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
             //To handle unexpected exception globally, register custome middleware
            app.UseMiddleware<CustomExceptionMiddleware>();   
            //Enable CORS with CORS Middleware for convenience   
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());           
             //Enable PerformanceLogger as middleware layer by using extention
            app.UsePerformanceLog(new LogOptions());            
            loggerFactory.AddSerilog();
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseMvc();

            var bus = ApplicationContainer.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
            lifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
