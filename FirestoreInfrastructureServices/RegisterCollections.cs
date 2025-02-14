using FirestoreInfrastructureServices.Collections;
using Google.Api.Gax;
using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;

namespace FirestoreInfrastructureServices;

public static class RegisterCollections
{
    public static async Task AddFirestoreCollectionServices(this IServiceCollection serviceCollection, string projectId, bool isDevelopment = true)
    {
        await AddFirestoreDb(serviceCollection, projectId, isDevelopment);
        
        serviceCollection.AddScoped<IWorkExperienceFirestoreCollectionQueries, WorkExperienceFirestoreCollection>();
    }

    private static async Task AddFirestoreDb(this IServiceCollection serviceCollection, string projectId, bool isDevelopment)
    {
        if (isDevelopment)
        {
            var firestoreDbBuilder = new FirestoreDbBuilder()
            {
                ProjectId = projectId,
                EmulatorDetection = EmulatorDetection.EmulatorOnly
            };
            var firestoreDb = await firestoreDbBuilder.BuildAsync();
            serviceCollection.AddSingleton(firestoreDb);
        }
        else
        {
            serviceCollection.AddFirestoreDb(projectId);
        }
    }
}