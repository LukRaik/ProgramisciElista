using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Core.Interfaces;
using Data;
using Newtonsoft.Json;
using ProgramisciElista.Impl;
using ProgramisciElista.Interfaces;
using ProgramisciElista.MessageHandler;
using ProgramisciElista.Session;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using WebApiServer.Misc;

namespace ProgramisciElista
{
    class Program
    {
        private static string host = "http://localhost:50000";
        public static HttpSelfHostConfiguration Config = new HttpSelfHostConfiguration(host);
        static void Main(string[] args)
        {



            using (var container = GetContainer())
            {

                using (var db = new ElistaDbContext())
                {
                    foreach (var workTime in db.WorkTimes)
                    {
                        workTime.HourEnd = DateTime.Now;
                    }

                    db.SaveChanges();
                }
                



                ILogger logger = container.GetInstance<ILogger>();
                logger.Log($"Init webApi server at {host}");

                Config.Filters.Clear();

                var us = container.GetInstance<IUserService>();
                var ss = container.GetInstance<ISessionService>();


                Config.Filters.Add(new SimpleAuthFilter(ss, us));

                using (HttpSelfHostServer server = new HttpSelfHostServer(Config))
                {
                    server.OpenAsync().Wait();
                    logger.Log("webApi server started");
                    Console.ReadLine();
                }
            }
        }

        public static Container GetContainer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            //Rejestracja serwisów
            container.Register<ILogger, Logger>(Lifestyle.Singleton);
            container.Register<IUserService, UserService>(Lifestyle.Singleton);
            container.Register<ISessionService, SessionService>(Lifestyle.Singleton);
            container.Register<ITeamService,TeamService>(Lifestyle.Singleton);
            container.Register<IPlansDiaryService,PlansDiaryService>(Lifestyle.Singleton);
            container.Register<IWorkLoggingService,WorkLoggingService>(Lifestyle.Singleton);

            //WebApi config
            WebApiConfig();

            container.RegisterWebApiControllers(Config);

            Config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            container.Verify();

            return container;
        }

        public static void WebApiConfig()
        {
            Config.MessageHandlers.Add(new CorsHandler());
            Config.Routes.MapHttpRoute(
                   "API Default", "{controller}/{action}/{id}",
                   new { id = RouteParameter.Optional });
            Config.Formatters.Clear();
            var jsonFormatter =
                new JsonMediaTypeFormatter {SerializerSettings = {NullValueHandling = NullValueHandling.Ignore}};
            Config.Formatters.Add(jsonFormatter);
        }
    }

}
