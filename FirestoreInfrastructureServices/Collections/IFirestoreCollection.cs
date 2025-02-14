using Models.Documents;
using Models.Documents.Profile;

namespace FirestoreInfrastructureServices.Collections;

public interface IFirestoreCollection<TModel> where TModel : FirestoreDocument
{
    Task<TModel> AddDocument(TModel newDocument);
    Task UpdateDocument(TModel updatedDocument);
    Task DeleteDocument(Guid documentId);

    Task<IEnumerable<TModel>> GetAll();
    Task<TModel> GetById(string documentId);
}