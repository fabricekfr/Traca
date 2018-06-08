using ClientModel;
using ClientModel.DomainObjects;

namespace Domain
{
    public class CenterType : ICenterType
    {
        public uint Id { get; set; }
        public string Value { get; set; }
    }
}
