// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------


namespace ClientModel.DomainObjects
{
    public interface ICenter : IDomainObject
    {
        int Id { get; set; }
        string Name { get; set; }
        string StreetAddress { get; set; }
        int CenterTypeId { get; set; }
        string CenterTypeValue { get; set; }
    }
}