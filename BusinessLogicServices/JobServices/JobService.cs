using FirestoreInfrastructureServices.Collections;
using Models.Documents.Profile;

namespace BusinessLogicServices.JobServices;

public class JobService : IJobService
{
    private readonly IWorkExperienceFirestoreCollectionQueries _workExperienceFirestoreCollection;
    
    public JobService(IWorkExperienceFirestoreCollectionQueries workExperienceFirestoreCollection)
    {
        _workExperienceFirestoreCollection = workExperienceFirestoreCollection;
    }
    
    
    /// <summary>
    /// Adds a new Job into the work experience
    /// </summary>
    /// <param name="newJob"></param>
    /// <returns></returns>
    public async Task<JobDocument> AddJob(JobDocument newJob)
    {
        newJob.DocumentId = Guid.NewGuid().ToString();
        var workExperienceTimeLine = await _workExperienceFirestoreCollection.GetWorkExperienceTimeLineAsync();
        
        // If there is nothing in work experience add it
        var experienceTimeLine = workExperienceTimeLine as JobDocument[] ?? workExperienceTimeLine.ToArray();
        if (!experienceTimeLine.Any())
        {
            await _workExperienceFirestoreCollection.AddDocument(newJob);
            return newJob;
        }

        // Validate job
        ValidateJob(newJob, experienceTimeLine);
        
        // Add Job into the work experience list
        await _workExperienceFirestoreCollection.AddDocument(newJob);
        
        return newJob;
    }

    /// <summary>
    /// Overrides an existing job.
    /// </summary>
    /// <param name="jobDocumentUpdates"></param>
    public async Task UpdateJob(JobDocument jobDocumentUpdates)
    {
        var timeLineWorkExperience = 
            (await _workExperienceFirestoreCollection.GetWorkExperienceTimeLineAsync())
            .Where(j => j.DocumentId != jobDocumentUpdates.DocumentId);
        
        ValidateJob(jobDocumentUpdates, timeLineWorkExperience);
        
        await _workExperienceFirestoreCollection.UpdateDocument(jobDocumentUpdates);
    }

    public async Task<IEnumerable<JobDocument>> GetAll()
    {
        return await _workExperienceFirestoreCollection.GetWorkExperienceTimeLineAsync();
    }


    /// <summary>
    /// Validates:
    /// <list type="bullet">
    /// <item>
    /// <description>The new job don't overlap with any other job</description>
    /// </item>
    /// <item>
    /// <description>If new job is current job vValidate there is no current job</description>
    /// </item>
    /// <item>
    /// <description>If new job is not current job, then validates it has an end date</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="job">The new job to be added</param>
    /// <param name="workExperience">The list of jobs to validate against</param>
    /// <exception cref="ArgumentException"></exception>
    private static void ValidateJob(JobDocument job, IEnumerable<JobDocument> workExperience)
    {
        var jobDocuments = workExperience as JobDocument[] ?? workExperience.ToArray();
        
        if (job.IsCurrentJob)
        {
            if(jobDocuments.Any(j => j.IsCurrentJob))
                throw new ArgumentException("There is already a current job. To maintain your data consistent first terminate your current job.");
        }
        else if (job.EndedOn == null)
        {
            throw new ArgumentException("Only current job has no end date.");
        }
        
        var jobsSegment = jobDocuments.Where(j => job.StartedOn < j.EndedOn || j.EndedOn == null);
        if (jobsSegment.Any(j => job.EndedOn > j.StartedOn))  // Correct overlap condition
        {
            throw new ArgumentException("New job overlaps with existing job"); // More informative message
        }
    }
    
}