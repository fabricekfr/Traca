
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

using ClientModel.DomainObjects;

namespace WCFService
{
    [ServiceContract]
    public interface IBookingApplicationService
    {

        [OperationContract]
        [WebGet(UriTemplate = "centers", ResponseFormat = WebMessageFormat.Json)]
        List<ICenter> GetAllCenters();  

        [OperationContract]  
        [WebGet(UriTemplate = "/Welcome/{name}", ResponseFormat = WebMessageFormat.Json)]  
        string Welcome(string name);

       
    }


   
    [DataContract]
    public class Center : ICenter
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string StreetAddress { get; set; }
        [DataMember]
        public int CenterTypeId { get; set; }
        [DataMember]
        public string CenterTypeValue { get; set; }
    }
}
