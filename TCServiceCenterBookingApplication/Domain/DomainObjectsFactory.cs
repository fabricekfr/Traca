using ClientModel.DomainObjects;

namespace Domain
{
    public class DomainObjectsFactory : IDomainObjectsFactory
    {
        public ICenterType CreateCenterType()
        {
            return new CenterType();
        }

        public ICenterType CreateCenterType(uint id, string value)
        {
            return new CenterType(id, value);
        }
    }
}