using Application.Commands;
using Application.Queries;
using MediatR;

namespace Application.Application.Extensions;
public static class RequestExtensions {

    public static bool IsQuery<T>(this IRequest<T> request) {
        return request is IQuery<T> && !(request is ICommand<T>);
    }

    public static bool IsCommand<T>(this IRequest<T> request) {
        return !request.IsQuery();
    }
}
