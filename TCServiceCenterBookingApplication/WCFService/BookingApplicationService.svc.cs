using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ClientModel.DataAccessObjects;
using ClientModel.DomainObjects;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BookingApplicationService : IBookingApplicationService
    {
        private readonly ICenterDAO _CenterDAO;
        /*public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }*/

        public BookingApplicationService(ICenterDAO centerDAO)
        {
            if (centerDAO == null) throw new ArgumentNullException(nameof(centerDAO));
            _CenterDAO = centerDAO;
        }


        public string Welcome(string name)
        {
            return "Welcome to the first WCF Web Service Application " + name;
        }

        List<ICenter> IBookingApplicationService.GetAllCenters()
        {
            return _CenterDAO.GetAll().ToList();
        }
    }
}
