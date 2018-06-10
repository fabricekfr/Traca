namespace ClientModel.DomainObjects
{
    public interface IDomainObjectsFactory
    {
        ICenterType CreateCenterType();
        ICenterType CreateCenterType(uint id, string value);
    }
}