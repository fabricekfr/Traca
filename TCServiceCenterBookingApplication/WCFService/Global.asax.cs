using System;
using System.Web;
using Castle.Windsor;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using DataAccess;

namespace WCFService
{
    public class Global : HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>();
            _container.Kernel.Register(Component.For<IBookingApplicationService>()
                .ImplementedBy<BookingApplicationService>()
                .Named("WCFService.BookingApplicationService"));
            _container.Install(new DIContainerInstaller());

            InitializeDatabase();

        }

        private static void InitializeDatabase()
        {
            var tcDatabase = new TCDatabase();
            tcDatabase.InitializeDatabase();
            tcDatabase.InitializeCenterTypesTable();
            tcDatabase.InitializeCentersTable();
            tcDatabase.InitializeAppointmentsTable();
        }
    }
}