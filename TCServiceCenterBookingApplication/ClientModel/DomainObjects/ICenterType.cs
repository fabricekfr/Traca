namespace ClientModel.DomainObjects
{
    public interface ICenterType : IDomainObject
    {
        int Id { get; set; }
        string Value { get; set; }
    }
}