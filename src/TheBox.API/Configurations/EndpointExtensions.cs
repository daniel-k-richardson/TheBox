#region
using Microsoft.Extensions.DependencyInjection.Extensions;
using TheBox.API.Configurations.Interfaces;
#endregion

namespace TheBox.API.Configurations;

public static class EndpointExtensions
{
    public static void AddEndpoints(this IServiceCollection services)
    {
        var assembly = typeof(Program).Assembly;
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type));
        services.TryAddEnumerable(serviceDescriptors);
    }
}
