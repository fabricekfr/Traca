using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;

namespace WCFService
{
    public class BookingApplicationService : IBookingApplicationService
    {
        private readonly ICenterDAO _CenterDAO;
        private readonly IAppointmentDAO _AppointmentDAO;
        private readonly IDomainObjectsFactory _DomainObjectsFactory;

        public BookingApplicationService(ICenterDAO centerDAO, IAppointmentDAO appointmentDAO, IDomainObjectsFactory domainObjectsFactory)
        {
            if (centerDAO == null) throw new ArgumentNullException(nameof(centerDAO));
            if (appointmentDAO == null) throw new ArgumentNullException(nameof(appointmentDAO));
            if (domainObjectsFactory == null) throw new ArgumentNullException(nameof(domainObjectsFactory));

            _CenterDAO = centerDAO;
            _AppointmentDAO = appointmentDAO;
            _DomainObjectsFactory = domainObjectsFactory;
        }


        #region Center

        IList<CenterDataContract> IBookingApplicationService.GetAllCenters()
        {
            return _CenterDAO.GetAll().Where(x => x != null).Select(center => new CenterDataContract(center)).ToList();
        }

        public CenterDataContract GetCenter(string id)
        {
            int centerId;
            if (!int.TryParse(id, out centerId)) throw new FaultException("Invalid center Id");
            var result = _CenterDAO.GetById(centerId);
            return result == null? null : new CenterDataContract(result);
        }

        #endregion

        #region Appointment

        public IList<AppointmentGetDataContract> GetAllAppointments()
        {
            var a = _AppointmentDAO.GetAll().Where(x => x != null).Select(appointment => new AppointmentGetDataContract(appointment)).ToList();
            return _AppointmentDAO.GetAll().Where(x => x != null).Select(appointment => new AppointmentGetDataContract(appointment)).ToList();
        }

        public AppointmentGetDataContract GetAppointment(string id)
        {
            int appointmentId;
            if (!int.TryParse(id, out appointmentId)) throw new FaultException("Invalid appointment Id");
            var result = _AppointmentDAO.GetById(appointmentId);
            return result == null ? null : new AppointmentGetDataContract(result);
        }

        public string AddAppointment(AppointmentPostDataContract appointmentPostDataContract)
        {
            if (appointmentPostDataContract == null) throw new ArgumentNullException(nameof(appointmentPostDataContract));

            DateTime date;

            if (!DateTime.TryParseExact(appointmentPostDataContract.Date, "yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None,out date))
                throw new WebFaultException<string>($"ERROR : The expected date format is YYYY-MM-DD but was {appointmentPostDataContract.Date}!", HttpStatusCode.BadRequest);

            if (_AppointmentDAO.GetByDate(date) != null)
                throw new WebFaultException<string>($"ERROR : This date {appointmentPostDataContract.Date} has already been booked!", HttpStatusCode.Conflict);

            if (_CenterDAO.GetById(appointmentPostDataContract.CenterId) == null)
                throw new WebFaultException<string>($"ERROR : The center #{appointmentPostDataContract.CenterId} not exist!", HttpStatusCode.BadRequest);
  ;
            var appointment = _DomainObjectsFactory.CreateAppointment();
            appointment.ClientFullName = appointmentPostDataContract.ClientFullName;
            appointment.Date = date;
            appointment.Center = _DomainObjectsFactory.CreateCenter();
            appointment.Center.Id = appointmentPostDataContract.CenterId;
            var numberOfRows = _AppointmentDAO.Add(appointment);

            if (numberOfRows == 0)
                throw new WebFaultException<string>("ERROR : Unable to add a new appointment with given information!", HttpStatusCode.InternalServerError);
            
            return "SUCCESS : The appointment has been added.";
        }

        #endregion





        public string Welcome(string name)
        {
            return "Welcome to the first WCF Web Service Application " + name;
        }

    }
}
