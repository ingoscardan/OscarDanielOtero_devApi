using Google.Cloud.Firestore;
using Models.Documents.Profile;

namespace FirestoreInfrastructureServices.Collections;

public class WorkExperienceCollection : Collection<JobDocument>
{
    public WorkExperienceCollection(FirestoreDb firestoreDb) : base(firestoreDb, "work-experience")
    {
    }
}