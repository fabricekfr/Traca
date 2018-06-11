using ClientModel.DomainObjects;

namespace Domain
{
    public class DomainObjectsFactory : IDomainObjectsFactory
    {
        public ICenterType CreateCenterType()
        {
            return new CenterType();
        }

        public ICenter CreateCenter()
        {
            return new Center();
        }

        public IAppointment CreateAppointment()
        {
            return new Appointment(CreateCenter());
        }
    }
}