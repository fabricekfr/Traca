using ClientModel.DomainObjects;

namespace Domain
{
    public class CenterType : ICenterType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
