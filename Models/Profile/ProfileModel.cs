namespace Models.Profile;

public class ProfileModel
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Role { get; set; }
    
    public ICollection<string> SkillsAndTools { get; set; }
    
    public DateTime StartedOn { get; set; }
}