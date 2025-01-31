using Google.Cloud.Firestore;
using Models.Documents;

namespace FirestoreInfrastructureServices.Collections;

public abstract class Collection<TModel> : ICollection<TModel> where TModel : FirestoreDocument
{
    protected readonly FirestoreDb FirestoreDb;
    protected readonly CollectionReference CollectionSet;

    protected Collection(FirestoreDb firestoreDb,string collectionName)
    {
        FirestoreDb = firestoreDb;
        CollectionSet = FirestoreDb.Collection(collectionName);
    }

    public virtual async Task<TModel> AddDocument(TModel newDocument)
    {
        newDocument.DocumentId = Guid.NewGuid();
        await CollectionSet.AddAsync(newDocument);
        return newDocument;
    }

    public async Task UpdateDocument(Guid documentId, IDictionary<string, object> updates)
    {
        var documentReference = CollectionSet.Document(documentId.ToString());
        await documentReference.UpdateAsync(updates);
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

    public Task<TModel> GetById(Guid documentId)
    {
        throw new NotImplementedException();
    }
}