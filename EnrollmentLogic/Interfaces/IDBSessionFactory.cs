using NHibernate;

namespace EnrollmentApi.Logic.Interfaces
{
    public interface IDBSessionFactory
    {
        ISession OpenSession();         
    }

    public interface IDBQuerySessionFactory
    {
        ISession OpenSession();
    }
}