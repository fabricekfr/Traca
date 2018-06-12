
// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using System;

namespace ClientModel.DomainObjects
{
    public interface IAppointment : IDomainObject
    {
        int Id { get; set; }
        string ClientFullName { get; set; }
        DateTime Date { get; set; }
        ICenter Center { get; set; }
    }
}