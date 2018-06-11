
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
        IList<Center> GetAllCenters();  

        [OperationContract]  
        [WebGet(UriTemplate = "/Welcome/{name}", ResponseFormat = WebMessageFormat.Json)]  
        string Welcome(string name);

       
    }


   
    [DataContract]
    public class Center
    {
        public Center(ICenter center)
        {
            Id = center.Id;
            Name = center.Name;
            StreetAddress = center.StreetAddress;
            CenterTypeValue = center.CenterTypeValue;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string StreetAddress { get; set; }
        [DataMember]
        public string CenterTypeValue { get; set; }
    }
}
