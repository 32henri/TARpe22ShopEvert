﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TARpe22ShopEvert.SpaceshipTest
{
    public class TestBase
    {
        protected IServiceProvider ServiceProvider { get; set; }
        protected TestBase() 
        {
            var services = new ServicveCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose() { }

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<ISpaceshipsServices, SpaceshipssServices>();
            services.AddScoped<IFilesServices, FilesServices>();
            services.AddScoped<IWebHost>();

            services.AddDbContext<TARpe22ShopEvertContext>(x =>
            {
                x.UseInMemoryDatabase("Test");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionignoredWarning)
            })
        }
    }
}