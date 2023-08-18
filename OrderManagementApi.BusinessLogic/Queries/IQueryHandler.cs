namespace OrderManagementApi.BusinessLogic.Commands;

public interface IQueryHandler<TResult>
{
    Task<TResult> Handle(CancellationToken? cancellationToken = null);
}

public interface IQueryHandler<in TRequest, TResult>
{
    Task<TResult> Handle(TRequest request, CancellationToken? cancellationToken = null);
}