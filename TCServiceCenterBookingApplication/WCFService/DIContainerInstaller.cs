// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;
using DataAccess;
using Domain;

namespace WCFService
{
    public class DIContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDataAccessObjectsFactory>().ImplementedBy<DataAccessObjectsFactory>().LifestyleSingleton());
            container.Register(Component.For<IDomainObjectsFactory>().ImplementedBy<DomainObjectsFactory>().LifestyleSingleton());
            container.Register(Component.For<ICenterDAO>().ImplementedBy<CenterDAO>().LifestyleSingleton());
            container.Register(Component.For<IAppointmentDAO>().ImplementedBy<AppointmentDAO>().LifestyleSingleton());
        }
    }
}