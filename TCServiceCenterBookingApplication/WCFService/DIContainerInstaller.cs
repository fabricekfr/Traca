using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ClientModel.DataAccessObjects;
using DataAccess;

namespace WCFService
{
    public class DIContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICenterDAO, CenterTypeDAO>());
        }
    }
}