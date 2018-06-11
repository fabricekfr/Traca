﻿using System.Collections.Generic;
using ClientModel.DomainObjects;

namespace ClientModel.DataAccessObjects
{
    public interface IAppointmentDAO
    {
        IEnumerable<IAppointment> GetAll();
        IAppointment GetById(int id);
    }
}