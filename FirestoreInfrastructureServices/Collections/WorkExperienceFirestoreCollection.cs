using Google.Cloud.Firestore;
using Models.Documents.Profile;

namespace FirestoreInfrastructureServices.Collections;

public class WorkExperienceFirestoreCollection : FirestoreCollection<JobDocument>, IWorkExperienceFirestoreCollectionQueries
{
    public WorkExperienceFirestoreCollection(FirestoreDb firestoreDb) : base(firestoreDb, "work-experience")
    {
    }

    public async Task<IEnumerable<JobDocument>> GetWorkExperienceTimeLineAsync(CancellationToken cancellationToken = default)
    {
        var workExperienceTimeLineQuery = CollectionSet.OrderBy("StartedOn");
        var workExperienceTimeLineSnapShot = await workExperienceTimeLineQuery.GetSnapshotAsync(cancellationToken);

        return workExperienceTimeLineSnapShot.Select(job => job.ConvertTo<JobDocument>()).ToList();
    }
}