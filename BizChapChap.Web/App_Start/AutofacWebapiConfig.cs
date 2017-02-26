using Autofac;
using Autofac.Integration.WebApi;
using BizChapChap.Web.Infrastructure;
using BizChapChap.Web.Models;
using BizChapChap.Web.Repository;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace BizChapChap.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF ApplicationDbContext
            builder.RegisterType<ApplicationDbContext>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                   .As(typeof(IEntityBaseRepository<>))
                   .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}