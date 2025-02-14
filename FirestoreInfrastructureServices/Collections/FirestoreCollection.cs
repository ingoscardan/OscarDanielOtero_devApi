using Google.Cloud.Firestore;
using Models.Documents;
using Models.Documents.Profile;

namespace FirestoreInfrastructureServices.Collections;

public abstract class FirestoreCollection<TModel> : IFirestoreCollection<TModel> where TModel : FirestoreDocument
{
    protected readonly FirestoreDb FirestoreDb;
    protected readonly CollectionReference CollectionSet;

    protected FirestoreCollection(FirestoreDb firestoreDb,string collectionName)
    {
        FirestoreDb = firestoreDb;
        CollectionSet = FirestoreDb.Collection(collectionName);
    }

    public virtual async Task<TModel> AddDocument(TModel newDocument)
    {
        await CollectionSet.Document(newDocument.DocumentId).SetAsync(newDocument);
        return newDocument;
    }

    public async Task UpdateDocument(TModel updatedDocument)
    {
        var documentReference = CollectionSet.Document(updatedDocument.DocumentId);
        await documentReference.SetAsync(updatedDocument);
    }

    public virtual async Task DeleteDocument(Guid documentId)
    {
        var documentReference = CollectionSet.Document(documentId.ToString());
        await documentReference.DeleteAsync();
    }

    public Task<IEnumerable<TModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<TModel> GetById(string documentId)
    {
        throw new NotImplementedException();
    }
}