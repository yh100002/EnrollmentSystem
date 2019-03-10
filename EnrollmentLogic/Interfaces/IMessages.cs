using CSharpFunctionalExtensions;
using EnrollmentApi.Logic.Students;

namespace EnrollmentApi.Logic.Interfaces
{
    public interface IMessages
    {
        Result Dispatch(ICommand command); 
        T Dispatch<T>(IQuery<T> query);          
    }
}