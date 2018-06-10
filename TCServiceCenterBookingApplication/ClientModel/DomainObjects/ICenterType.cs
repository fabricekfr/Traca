namespace ClientModel.DomainObjects
{
    public interface ICenterType : IDomainObject
    {
        uint Id { get; set; }
        string Value { get; set; }
    }
}