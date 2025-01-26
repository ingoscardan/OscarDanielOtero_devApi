using Google.Cloud.Firestore;
using Models.Documents;
using Models.Profile;

namespace FirestoreInfrastructureServices.Collections;

public class ProfileCollection : Collection<ProfileDocument>
{
    public ProfileCollection(FirestoreDb firestoreDb) : base(firestoreDb, "profiles")
    {
    }

    public override Task<ProfileDocument> AddDocument(ProfileDocument newDocument)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDocument(ProfileDocument document)
    {
        throw new NotImplementedException();
    }

    public override Task DeleteDocument(Guid documentId)
    {
        throw new NotImplementedException();
    }
}