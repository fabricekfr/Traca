using ClientModel.DomainObjects;

namespace Domain
{
    public class Center : ICenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public int CenterTypeId { get; set; }
        public string CenterTypeValue { get; set; }
    }
}