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

        public BookingApplicationService(ICenterDAO centerDAO)
        {
            if (centerDAO == null) throw new ArgumentNullException(nameof(centerDAO));
            _CenterDAO = centerDAO;
        }


        public string Welcome(string name)
        {
            return "Welcome to the first WCF Web Service Application " + name;
        }

        IList<Center> IBookingApplicationService.GetAllCenters()
        {
            return _CenterDAO.GetAll().Select(center => new Center(center)).ToList();
        }

        public Center GetCenter(string id)
        {
            int centerId;
            if (int.TryParse(id, out centerId))
                return new Center(_CenterDAO.GetById(centerId));
            throw new FaultException("Invalid center Id");
        }
    }
}
