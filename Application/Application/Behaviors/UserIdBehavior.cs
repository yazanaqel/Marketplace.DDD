using Application.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Application.Behaviors;
public class UserIdBehavior<TIn, TOut>(IHttpContextAccessor contextAccessor) : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut> {
    private readonly HttpContext _httpContext = contextAccessor.HttpContext;

    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken) {

        //var userId = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (request is BaseRequest baseRequest) {

            var userId = _httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId is not null) {

                baseRequest.UserId = userId;
            }
        }

        return await next();
    }
}