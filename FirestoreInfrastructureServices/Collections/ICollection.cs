using Models.Documents;

namespace FirestoreInfrastructureServices.Collections;

public interface ICollection<TModel> where TModel : FirestoreDocument
{
    Task<TModel> AddDocument(TModel newDocument);
    Task UpdateDocument(Guid documentId, IDictionary<string, object> updates);
    Task DeleteDocument(Guid documentId);
}