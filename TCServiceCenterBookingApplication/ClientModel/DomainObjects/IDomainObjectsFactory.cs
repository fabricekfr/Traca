namespace ClientModel.DomainObjects
{
    public interface IDomainObjectsFactory
    {
        ICenterType CreateCenterType();
        ICenter CreateCenter();
    }
}