using Google.Cloud.Firestore;
using Models.Documents;

namespace FirestoreInfrastructureServices.Collections;

public class ProfileCollection : Collection<ProfileDocument>
{
    public ProfileCollection(FirestoreDb firestoreDb) : base(firestoreDb, "profiles")
    {
    }
}