using Google.Cloud.Firestore;

namespace Models.Documents.Profile;

[FirestoreData]
public class JobDocument : FirestoreDocument
{
    [FirestoreProperty]
    public string JobTitle { get; set; }
    
    [FirestoreProperty]
    public string CompanyName { get; set; }
    
    [FirestoreProperty]
    public DateTime StartedOn { get; set; }
    
    [FirestoreProperty]
    public DateTime? EndedOn { get; set; }
    
    [FirestoreProperty]
    public ICollection<string> SkillsAndTools { get; set; }
    
    [FirestoreProperty]
    public ICollection<string> Highlights { get; set; }
    
}