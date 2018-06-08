using ClientModel.DomainObjects;

namespace Domain
{
    public class DomainObjectsFactory
    {
        private DomainObjectsFactory()
        {
        }

        public static DomainObjectsFactory GetInstance()
        {
            return new DomainObjectsFactory();
        }
        
        public ICenterType CreateCenterType()
        {
            return new CenterType();
        }
    }
}