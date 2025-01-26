using Google.Cloud.Firestore;

namespace Models.Documents;

[FirestoreData]
public class ProfileDocument : FirestoreDocument
{
    [FirestoreProperty]
    public string FirstName { get; set; }
    
    [FirestoreProperty]
    public string LastName { get; set; }
    
    [FirestoreProperty]
    public string Role { get; set; }
    
    [FirestoreProperty]
    public ICollection<string> SkillsAndTools { get; set; }
    
    [FirestoreProperty]
    public DateTime StartedOn { get; set; }
    
}