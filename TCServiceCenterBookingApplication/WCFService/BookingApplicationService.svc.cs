﻿// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;

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
            if (!int.TryParse(id, out centerId) || centerId==0)
                throw new WebFaultException<string>($"Invalid center Id {id}", HttpStatusCode.BadRequest);
            var result = _CenterDAO.GetById(centerId);


            return result == null? null : new CenterDataContract(result);
        }

        #endregion

        #region Appointment

        public IList<AppointmentGetDataContract> GetAllAppointments()
        {
            return _AppointmentDAO.GetAll().Where(x => x != null).Select(appointment => new AppointmentGetDataContract(appointment)).ToList();
        }

        public AppointmentGetDataContract GetAppointment(string id)
        {
            int appointmentId;
            if (!int.TryParse(id, out appointmentId) || appointmentId == 0 )
                throw new WebFaultException<string>($"Invalid appointment Id {id}", HttpStatusCode.BadRequest);

            var result = _AppointmentDAO.GetById(appointmentId);
            return result == null ? null : new AppointmentGetDataContract(result);
        }

        public string AddAppointment(AppointmentPostDataContract appointmentPostDataContract)
        {
            if (appointmentPostDataContract == null) throw new ArgumentNullException(nameof(appointmentPostDataContract));

            DateTime date;

            if (!DateTime.TryParseExact(appointmentPostDataContract.Date, "yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None,out date))
                throw new WebFaultException<string>($"ERROR : The expected date format is YYYY-MM-DD but was {appointmentPostDataContract.Date}!", HttpStatusCode.BadRequest);

            if (_CenterDAO.GetById(appointmentPostDataContract.CenterId) == null)
                throw new WebFaultException<string>($"ERROR : The center #{appointmentPostDataContract.CenterId} not exist!", HttpStatusCode.BadRequest);

            if (_AppointmentDAO.GetByCenterAndByDate(appointmentPostDataContract.CenterId, date) != null)
                throw new WebFaultException<string>($"ERROR : This date {appointmentPostDataContract.Date} has already been booked for the center #{appointmentPostDataContract.CenterId}!", HttpStatusCode.Conflict);

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

        public string UpdateAppointment(string id, AppointmentPutDataContract appointmentPostDataContract)
        {

            int appointmentId;
            if (!int.TryParse(id, out appointmentId) || appointmentId == 0)
                throw new WebFaultException<string>($"Invalid appointment Id {id}", HttpStatusCode.BadRequest);

            if (_AppointmentDAO.GetById(appointmentId) == null)
                throw new WebFaultException<string>($"ERROR : The appointment #{id} not exist!", HttpStatusCode.BadRequest);

            if (appointmentId != appointmentPostDataContract.Id && GetAppointment(appointmentPostDataContract.Id.ToString()) != null)
                throw new WebFaultException<string>($"ERROR : The appointment #{id} Already exists!", HttpStatusCode.BadRequest);

            DateTime date;
            if (!DateTime.TryParseExact(appointmentPostDataContract.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                throw new WebFaultException<string>($"ERROR : The expected date format is YYYY-MM-DD but was {appointmentPostDataContract.Date}!", HttpStatusCode.BadRequest);

            if (_AppointmentDAO.GetByCenterAndByDate(appointmentPostDataContract.CenterId, date) != null)
                throw new WebFaultException<string>($"ERROR : This date {appointmentPostDataContract.Date} has already been booked for the center #{appointmentPostDataContract.CenterId}!", HttpStatusCode.Conflict);

            var appointment = _DomainObjectsFactory.CreateAppointment();
            appointment.Id = appointmentPostDataContract.Id;
            appointment.ClientFullName = appointmentPostDataContract.ClientFullName;
            appointment.Date = date;
            appointment.Center = _DomainObjectsFactory.CreateCenter();
            appointment.Center.Id = appointmentPostDataContract.CenterId;
            var numberOfRows = _AppointmentDAO.Update(appointmentId, appointment);

            if (numberOfRows == 0)
                throw new WebFaultException<string>("ERROR : Unable to update a new appointment with given information!", HttpStatusCode.InternalServerError);

            return "SUCCESS : The appointment has been updated.";
        }

        public string DeleteAppointment(string id)
        {
            int appointmentId;
            if (!int.TryParse(id, out appointmentId) || appointmentId == 0)
                throw new WebFaultException<string>($"Invalid appointment Id {id}", HttpStatusCode.BadRequest);

            if (_AppointmentDAO.GetById(appointmentId) == null)
                throw new WebFaultException<string>($"ERROR : The appointment #{id} not exist!", HttpStatusCode.BadRequest);

            var numberOfRows = _AppointmentDAO.Delete(appointmentId);

            if (numberOfRows == 0)
                throw new WebFaultException<string>("ERROR : Unable to delete an appointment with given information!", HttpStatusCode.InternalServerError);

            return "SUCCESS : The appointment has been deleted.";
        }

        #endregion
    }
}
