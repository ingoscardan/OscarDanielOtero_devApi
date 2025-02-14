using BusinessLogicServices.JobServices;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicServices;

public static class BusinessLogicServicesConfiguration
{
    public static void AddJobServices(this IServiceCollection services)
    {
        services.AddScoped<IJobService, JobCommandsService>();
    }
}