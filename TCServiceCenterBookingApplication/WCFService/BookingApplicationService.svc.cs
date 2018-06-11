using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ClientModel.DataAccessObjects;

namespace WCFService
{
    public class BookingApplicationService : IBookingApplicationService
    {
        private readonly ICenterDAO _CenterDAO;
        private readonly IAppointmentDAO _AppointmentDAO;

        public BookingApplicationService(ICenterDAO centerDAO, IAppointmentDAO appointmentDAO)
        {
            if (centerDAO == null) throw new ArgumentNullException(nameof(centerDAO));
            if (appointmentDAO == null) throw new ArgumentNullException(nameof(appointmentDAO));

            _CenterDAO = centerDAO;
            _AppointmentDAO = appointmentDAO;
        }


        public string Welcome(string name)
        {
            return "Welcome to the first WCF Web Service Application " + name;
        }

        IList<Center> IBookingApplicationService.GetAllCenters()
        {
            return _CenterDAO.GetAll().Where(x => x != null).Select(center => new Center(center)).ToList();
        }

        public Center GetCenter(string id)
        {
            int centerId;
            if (!int.TryParse(id, out centerId)) throw new FaultException("Invalid center Id");
            var result = _CenterDAO.GetById(centerId);
            return result == null? null : new Center(result);

        }

        public IList<Appointment> GetAllAppointments()
        {
            return _AppointmentDAO.GetAll().Where(x => x != null).Select(appointment => new Appointment(appointment)).ToList();
        }
    }
}
