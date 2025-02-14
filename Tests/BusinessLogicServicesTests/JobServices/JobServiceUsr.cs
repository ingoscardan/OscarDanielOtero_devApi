using Models.Documents.Profile;

namespace Tests.BusinessLogicServicesTests.JobServices;

public static class JobServiceUsr
{
    public static List<JobDocument> CreateNonOverlappingJobs()
    {
        var jobs = new List<JobDocument>();

        // First Job (Past, Non-Current)
        jobs.Add(new JobDocument
        {
            JobTitle = "Software Engineer Intern",
            CompanyName = "Acme Corp",
            StartedOn = new DateTime(2021, 1, 1),
            EndedOn = new DateTime(2021, 6, 30),
            IsCurrentJob = false,
            DocumentId = Guid.NewGuid().ToString()
        });

        // Second Job (Past, Non-Current)
        jobs.Add(new JobDocument
        {
            JobTitle = "Junior Developer",
            CompanyName = "Beta Solutions",
            StartedOn = new DateTime(2021, 7, 1),
            EndedOn = new DateTime(2023, 12, 31),
            IsCurrentJob = false,
            DocumentId = Guid.NewGuid().ToString()

        });

        // Third Job (Current)
        jobs.Add(new JobDocument
        {
            JobTitle = "Senior Developer",
            CompanyName = "Gamma Dynamics",
            StartedOn = new DateTime(2024, 1, 1),
            IsCurrentJob = true,  // Only the last job is current
            DocumentId = Guid.NewGuid().ToString()
        });

        return jobs;
    }
}