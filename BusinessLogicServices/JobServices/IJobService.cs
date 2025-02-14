using Models.Documents.Profile;

namespace BusinessLogicServices.JobServices;

public interface IJobService
{
    Task<JobDocument> AddJob(JobDocument newJob);
    Task UpdateJob(JobDocument jobDocumentUpdates);
    Task<IEnumerable<JobDocument>> GetAll();
}