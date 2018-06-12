// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DomainObjects;
using System;
using System.Collections.Generic;

namespace ClientModel.DataAccessObjects
{
    public interface IAppointmentDAO
    {
        IEnumerable<IAppointment> GetAll();
        IAppointment GetById(int id);
        IAppointment GetByCenterAndByDate(int centerId, DateTime date);
        int Add(IAppointment appointment);
        int Update(int id, IAppointment appointment);
        int Delete(int id);
    }
}