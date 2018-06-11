
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using ClientModel.DomainObjects;
using Newtonsoft.Json;

namespace WCFService
{
    [ServiceContract]
    public interface IBookingApplicationService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/centers", ResponseFormat = WebMessageFormat.Json)]
        IList<CenterDataContract> GetAllCenters();

        [OperationContract]
        [WebGet(UriTemplate = "/centers/{id}", ResponseFormat = WebMessageFormat.Json)]
        CenterDataContract GetCenter(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/appointments", ResponseFormat = WebMessageFormat.Json, Method = "GET")]
        IList<AppointmentGetDataContract> GetAllAppointments();

        [OperationContract]
        [WebInvoke(UriTemplate = "/appointments/{id}", ResponseFormat = WebMessageFormat.Json, Method = "GET")]
        AppointmentGetDataContract GetAppointment(string id);
        
        [OperationContract]
        [WebInvoke(UriTemplate = "/appointments", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        string AddAppointment(AppointmentPostDataContract appointment);

        [OperationContract]
        [WebInvoke(UriTemplate = "/appointments/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "PUT")]
        string UpdateAppointment(string id, AppointmentPutDataContract appointment);

    }

    #region Data contacts

    #region Center

    [DataContract]
    public class CenterDataContract
    {
        public CenterDataContract(ICenter center)
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

    #endregion

    #region Appointment

    [DataContract]
    public class AppointmentGetDataContract
    {
        public AppointmentGetDataContract(IAppointment appointment)
        {
            Id = appointment.Id;
            ClientFullName = appointment.ClientFullName;
            Date = appointment.Date.ToString("yyyy-MM-dd");
            Center = JsonConvert.SerializeObject(appointment.Center, Formatting.Indented);
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ClientFullName { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Center { get; set; }
    }

    [DataContract]
    public class AppointmentPostDataContract
    {
        [DataMember]
        public string ClientFullName { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public int CenterId { get; set; }
    }

    [DataContract]
    public class AppointmentPutDataContract
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ClientFullName { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public int CenterId { get; set; }
    }

    [DataContract]
    public class AppointmentDeleteDataContract
    {
        [DataMember]
        public int Id { get; set; }
    }

    #endregion

    #endregion
}
