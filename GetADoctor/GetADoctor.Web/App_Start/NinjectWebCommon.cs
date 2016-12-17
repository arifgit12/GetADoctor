[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GetADoctor.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GetADoctor.Web.App_Start.NinjectWebCommon), "Stop")]

namespace GetADoctor.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Data.Infrastructure;
    using Data.Services;
    using Data.Repositories;
    using GetADoctor.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Ninject.Activation;
    using Microsoft.AspNet.Identity.EntityFramework;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            kernel.Bind<UserManager<ApplicationUser>>().ToSelf();

            kernel.Bind<ApplicationUserManager>().ToMethod(GetOwinInjection<ApplicationUserManager>);
            kernel.Bind<ApplicationSignInManager>().ToMethod(GetOwinInjection<ApplicationSignInManager>);

            kernel.Bind<IAppointmentRepository>().To<AppointmentRepository>();
            kernel.Bind<ICityRepository>().To<CityRepository>();
            kernel.Bind<ILocationRepository>().To<LocationRepository>();
            kernel.Bind<IDoctorRepository>().To<DoctorRepository>();
            kernel.Bind<IPatientRepository>().To<PatientRepository>();
            kernel.Bind<IScheduleRepository>().To<ScheduleRepository>();
            kernel.Bind<ISpecialityRepository>().To<SpecialityRepository>();
            kernel.Bind<IStateRepository>().To<StateRepository>();
            kernel.Bind<IWaitingRepository>().To<WaitingRepository>();

            kernel.Bind<IStateService>().To<StateService>();
            kernel.Bind<ICityService>().To<CityService>();
            kernel.Bind<IDoctorService>().To<DoctorService>();
            kernel.Bind<IPatientservice>().To<PatientService>();
            kernel.Bind<ISpecialityService>().To<SpecialityService>();
        }

        private static T GetOwinInjection<T>(IContext context) where T : class
        {
            var contextBase = new HttpContextWrapper(HttpContext.Current);
            return contextBase.GetOwinContext().Get<T>();
        }
    }
}
