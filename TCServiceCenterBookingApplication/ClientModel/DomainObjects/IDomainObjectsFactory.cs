// Author: Kwitonda, Fabrice
// Date: 2018-06-10
// --------------------------------------

namespace ClientModel.DomainObjects
{
    public interface IDomainObjectsFactory
    {
        ICenterType CreateCenterType();
        ICenter CreateCenter();
        IAppointment CreateAppointment();
    }
}