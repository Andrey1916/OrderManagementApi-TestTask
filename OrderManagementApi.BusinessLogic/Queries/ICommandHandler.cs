namespace OrderManagementApi.BusinessLogic.Commands;

public interface ICommandHandler<in TRequest>
{
    Task Handle(TRequest request, CancellationToken? cancellationToken = null);
}

public interface ICommandHandler<in TRequest, TResult>
{
    Task<TResult> Handle(TRequest request, CancellationToken? cancellationToken = null);
}