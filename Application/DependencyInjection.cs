using Application.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        
        services.AddHttpContextAccessor();

        services.AddMediatR(options => options.RegisterServicesFromAssemblies(
            AssemblyProvider.GetAssembly()));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserIdBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

        services.AddAutoMapper(AssemblyProvider.GetAssembly());


        return services;
    }
}