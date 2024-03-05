using Application.Application.Abstractions;
using Application.Application.Extensions;
using MediatR;

namespace Application.Application.Behaviors;
public class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        TResponse response = await next();

        if (request.IsQuery())
        {
            return response;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}
