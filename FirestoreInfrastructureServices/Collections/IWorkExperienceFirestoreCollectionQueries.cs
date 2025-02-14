using Models.Documents.Profile;

namespace FirestoreInfrastructureServices.Collections;

public interface IWorkExperienceFirestoreCollectionQueries : IFirestoreCollection<JobDocument>
{
    Task<IEnumerable<JobDocument>> GetWorkExperienceTimeLineAsync(CancellationToken cancellationToken = default);
}