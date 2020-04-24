using System.Threading.Tasks;
using EasyEvents.Core;
using EasyEvents.Core.ClientInterfaces;
using EasyEvents.Core.Configuration;
using EasyEvents.Core.Stores.FileSystem;
using EasyEvents.Core.Stores.InMemory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradingEngine.MoneyExchange.ApplicationServices;
using TradingEngine.MoneyExchange.Context;
using TradingEngine.MoneyExchange.Domain.Repositories;
using TradingEngine.User.Context;
using TradingEngine.User.Domain.Events;
using TradingEngine.User.Domain.Repositories;
using TradingEngine.MoneyExchange.User.Domain.Repositories;
using IUserRepository = TradingEngine.MoneyExchange.User.Domain.Repositories.IUserRepository;
using UserRepository = TradingEngine.MoneyExchange.User.Domain.Repositories.UserRepository;

namespace TradingEngine.API
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

            services.AddSingleton<IEasyEvents, EasyEvents.Core.EasyEvents>(); 
             
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("TradingEngine")));
            services.AddDbContext<MoneyExchangeDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("TradingEngine")));


            //UserManagement
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedHandler>();

            //TradingExchange  
            services.AddScoped<IMoneyExchangeAppService, MoneyExchangeAppService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>(); 

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            //EasyEvent 
            var easyEvents = app.ApplicationServices.GetService<IEasyEvents>();
            easyEvents.Configure(new EasyEventsConfiguration
            {
                Store = new InMemoryEventStore(),
                HandlerFactory = type => app.ApplicationServices.CreateScope().ServiceProvider.GetService(type)
            });

            easyEvents.ReplayAllEventsAsync().Wait();

            //DataSeeder
            using (var scope = app.ApplicationServices.CreateScope())
            { 
                var context = scope.ServiceProvider.GetService<MoneyExchangeDbContext>();
                DataSeeder.SeedCurrencies(context);
            }

            app.UseMvc();
        }
    }
}
