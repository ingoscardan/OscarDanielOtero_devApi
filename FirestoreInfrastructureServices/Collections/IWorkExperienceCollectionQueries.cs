using Models.Documents.Profile;

namespace FirestoreInfrastructureServices.Collections;

public interface IWorkExperienceCollectionQueries : ICollection<JobDocument>
{
    Task<IEnumerable<JobDocument>> GetWorkExperienceTimeLineAsync(CancellationToken cancellationToken = default);
}