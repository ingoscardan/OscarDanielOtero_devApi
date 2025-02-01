using Google.Cloud.Firestore;

namespace Models.Documents;

[FirestoreData]
public abstract class FirestoreDocument
{
    [FirestoreDocumentId]
    public string DocumentId { get; set; }

    [FirestoreDocumentCreateTimestamp]
    public Timestamp CreateTime { get; set; }

    [FirestoreDocumentUpdateTimestamp]
    public Timestamp UpdateTime { get; set; }

    [FirestoreDocumentReadTimestamp]
    public Timestamp ReadTime { get; set; }
}