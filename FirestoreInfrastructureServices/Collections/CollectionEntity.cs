namespace FirestoreInfrastructureServices.Collections;

public class CollectionEntity<TModel> where TModel : class
{
    public string CollectionName { get; private set; } = null!;
    public TModel Resource { get; set; } = null!;
}