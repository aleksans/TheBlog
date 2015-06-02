using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using TheBlog.DAL;
using TheBlog.DAL.Interfaces;
using TheBlog.Service;
using TheBlog.Service.Interfaces;

namespace TheBlog
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<BlogContext>().As<IBlogContext>().InstancePerRequest();
            builder.RegisterType<PostService>().As<IPostService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<TagService>().As<ITagService>();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>();
            builder.RegisterGeneric(typeof (Repository<>)).As(typeof (IRepository<>));


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
