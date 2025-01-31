using Google.Cloud.Firestore;

namespace Models.Documents.Profile;

[FirestoreData]
public class JobDocument : FirestoreDocument
{
    /// <summary>
    /// Title of the job
    /// </summary>
    [FirestoreProperty]
    public string JobTitle { get; set; }
    
    
    /// <summary>
    /// Name of the company
    /// </summary>
    [FirestoreProperty]
    public string CompanyName { get; set; }

    [FirestoreProperty]
    public DateTime StartedOn { get; set; }
    
    [FirestoreProperty]
    public DateTime? EndedOn { get; set; }
    
    [FirestoreProperty]
    public bool IsCurrentJob { get; set; }
    
    [FirestoreProperty]
    public ICollection<string> SkillsAndTools { get; set; }
    
    [FirestoreProperty]
    public ICollection<string> Highlights { get; set; }
    
}