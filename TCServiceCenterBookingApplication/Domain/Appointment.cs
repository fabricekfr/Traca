// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DomainObjects;
using System;

namespace Domain
{
    public class Appointment : IAppointment
    {
        public Appointment(ICenter center)
        {
            Center = center;
        }

        public int Id { get; set; }
        public string ClientFullName { get; set; }
        public DateTime Date { get; set; }
        public ICenter Center { get; set; }
    }
}