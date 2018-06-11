
using System;
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
        [WebGet(UriTemplate = "/centers", ResponseFormat = WebMessageFormat.Json)]
        IList<Center> GetAllCenters();

        [OperationContract]
        [WebGet(UriTemplate = "/centers/{id}", ResponseFormat = WebMessageFormat.Json)]
        Center GetCenter(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/appointments", ResponseFormat = WebMessageFormat.Json)]
        IList<Appointment> GetAllAppointments();

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

    [DataContract]
    public class Appointment
    {
        public Appointment(IAppointment appointment)
        {
            Id = appointment.Id;
            ClientFullName = appointment.ClientFullName;
            Date = appointment.Date;
            Center = appointment.Center;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ClientFullName { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public ICenter Center { get; set; }
    }
}
