// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DataAccess
{
    public class AppointmentDAO : BaseDAO<IAppointment>, IAppointmentDAO
    {
        private readonly IDomainObjectsFactory _DomainObjectsFactory;

        public AppointmentDAO(IDataAccessObjectsFactory daoFactory, IDomainObjectsFactory domainObjectsFactory) 
            : base(daoFactory)
        {
            if (domainObjectsFactory == null) throw new ArgumentNullException(nameof(domainObjectsFactory));
            _DomainObjectsFactory = domainObjectsFactory;
        }

        public IEnumerable<IAppointment> GetAll()
        {
            const string sqlComandText = "SELECT Appointment.Id, Appointment.ClientFullName, Appointment.[Date], Center.StreetAddress, Center.Name, " +
                                         "       Appointment.Center, CenterType.Value AS CenterTypeValue, Center.CenterTypeId " +
                                         "FROM Appointment INNER JOIN Center ON Appointment.Center = Center.Id " +
                                         "                 INNER JOIN CenterType ON Center.CenterTypeId = CenterType.Id";

            using (var command = new SQLiteCommand(sqlComandText))
            {
                return GetRecords(command);
            }
        }

        public IAppointment GetById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var sqlComandText = "SELECT Appointment.Id, Appointment.ClientFullName, Appointment.[Date], Center.StreetAddress, Center.Name, " +
                                "       Appointment.Center, CenterType.Value AS CenterTypeValue, Center.CenterTypeId " +
                                "FROM Appointment INNER JOIN Center ON Appointment.Center = Center.Id " +
                                "                 INNER JOIN CenterType ON Center.CenterTypeId = CenterType.Id " +
                               $"WHERE Appointment.Id = {id}";

            using (var command = new SQLiteCommand(sqlComandText))
            {
                return GetRecord(command);
            }
        }

        public IAppointment GetByCenterAndByDate(int centerId, DateTime date)
        {
            if (centerId <= 0) throw new ArgumentOutOfRangeException(nameof(centerId));

            var sqlComandText = "SELECT Appointment.Id, Appointment.ClientFullName, Appointment.[Date], Center.StreetAddress, Center.Name, " +
                                "       Appointment.Center, CenterType.Value AS CenterTypeValue, Center.CenterTypeId " +
                                "FROM Appointment INNER JOIN Center ON Appointment.Center = Center.Id " +
                                "                 INNER JOIN CenterType ON Center.CenterTypeId = CenterType.Id " +
                                $"WHERE Appointment.Center = {centerId} " +
                                $"AND date(Appointment.[Date]) = date(\"{date.Date:yyyy-MM-dd}\")";

            using (var command = new SQLiteCommand(sqlComandText))
            {
                return GetRecord(command);
            }
        }

        public int Add(IAppointment appointment)
        {
            var sqlComandText = "INSERT INTO Appointment (ClientFullName, [Date], Center) " +
                                $"VALUES (\"{appointment.ClientFullName}\", " +
                                $"\"{appointment.Date.Date}\", " +
                                $"{appointment.Center.Id})";


            using (var command = new SQLiteCommand(sqlComandText))
            {
                return SetRecord(command);
            }
        }

        public int Update(int id, IAppointment appointment)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var sqlComandText = $"UPDATE Appointment SET Id={appointment.Id}, ClientFullName = \"{appointment.ClientFullName}\", " +
                                $"[Date] = \"{appointment.Date.Date}\", Center = {appointment.Center.Id} " +
                                $"WHERE Id = {id}";


            using (var command = new SQLiteCommand(sqlComandText))
            {
                return SetRecord(command);
            }
        }

        public int Delete(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var sqlComandText = $"DELETE FROM Appointment WHERE Id = {id}";

            using (var command = new SQLiteCommand(sqlComandText))
            {
                return SetRecord(command);
            }
        }

        public override IAppointment MapRecord(SQLiteDataReader dataReader)
        {
            var appointment = _DomainObjectsFactory.CreateAppointment();
            appointment.Id = dataReader.GetInt32(dataReader.GetOrdinal(nameof(IAppointment.Id)));
            appointment.ClientFullName = (string)dataReader[nameof(IAppointment.ClientFullName)];
            appointment.Date = (DateTime) dataReader[nameof(IAppointment.Date)];
            appointment.Center.Id = dataReader.GetInt32(dataReader.GetOrdinal(nameof(IAppointment.Center)));
            appointment.Center.Name = (string)dataReader[nameof(ICenter.Name)];
            appointment.Center.StreetAddress = dataReader[nameof(ICenter.StreetAddress)].GetType() != typeof(DBNull) ? (string)dataReader[nameof(ICenter.StreetAddress)] : string.Empty;
            appointment.Center.CenterTypeId = dataReader.GetInt32(dataReader.GetOrdinal(nameof(ICenter.CenterTypeId)));
            appointment.Center.CenterTypeValue = (string)dataReader[nameof(ICenter.CenterTypeValue)];

            return appointment;
        }
    }
}