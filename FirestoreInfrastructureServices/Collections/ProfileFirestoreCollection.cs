using Google.Cloud.Firestore;
using Models.Documents;

namespace FirestoreInfrastructureServices.Collections;

public class ProfileFirestoreCollection : FirestoreCollection<ProfileDocument>
{
    public ProfileFirestoreCollection(FirestoreDb firestoreDb) : base(firestoreDb, "profiles")
    {
    }
}