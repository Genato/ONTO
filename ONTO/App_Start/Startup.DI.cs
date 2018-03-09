using Microsoft.AspNet.Identity.Owin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using ONTO.BusinessLogic;
using ONTO.Identity;
using ONTO.DbContexts;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ONTO.DAL;

namespace ONTO
{
    public partial class Startup
    {
        public void ConfigureDependencyInjection()
        {
            // We will use Dependency Injection for all controllers and other classes, so we'll need a service collection
            ServiceCollection services = new ServiceCollection();

            // configure all of the services required for DI
            ConfigureServices(services);

            // Create a new resolver from our own default implementation
            CustomDependencyResolver resolver = new CustomDependencyResolver(services.BuildServiceProvider());

            // Set the application resolver to our default resolver. This comes from "System.Web.Mvc"
            //Other services may be added elsewhere through time
            DependencyResolver.SetResolver(resolver);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Add Identity to DI(Dependency Injection) - Start
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            // ApplicationUserManager
            services.AddTransient(typeof(OntoIdentityUserManager), f => HttpContext.Current.GetOwinContext().Get<OntoIdentityUserManager>());
            // IAuthenticationManager
            // instantiation requires two parameters, [ApplicationUserManager] (defined above) and [IAuthenticationManager]
            services.AddTransient(typeof(Microsoft.Owin.Security.IAuthenticationManager), f => HttpContext.Current.GetOwinContext().Authentication);
            // ApplicationSignInManager
            services.AddTransient(typeof(OntoIdentitySignInManager), f => HttpContext.Current.GetOwinContext().Get<OntoIdentitySignInManager>());
            // ApplicationRoleManager
            services.AddTransient(typeof(OntoIdentityRoleManager), f => HttpContext.Current.GetOwinContext().Get<OntoIdentityRoleManager>());

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Add Identity to DI(Dependency Injection) - End
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Add ONTO to DI - Start
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            services.AddTransient(typeof(OntoDbContext));
            services.AddTransient(typeof(UserSettingsDAL));
            services.AddTransient(typeof(LocaleDAL));
            services.AddTransient(typeof(UserSettingsLogic));
            services.AddTransient(typeof(LocaleLogic));
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Add ONTO to DI - Start - End
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Add Controllers to DI(Dependency Injection)
            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
               .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
               .Where(t => typeof(IController).IsAssignableFrom(t) || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));
        }
    }

    internal class CustomDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider _serviceProvider;

        public CustomDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return this._serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._serviceProvider.GetServices(serviceType);
        }
    }

    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddControllersAsServices(this IServiceCollection services, IEnumerable<Type> controllerTypes)
        {
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
    }
}