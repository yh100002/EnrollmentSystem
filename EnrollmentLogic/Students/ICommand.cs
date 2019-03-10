using CSharpFunctionalExtensions;

namespace EnrollmentApi.Logic.Students
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }


}