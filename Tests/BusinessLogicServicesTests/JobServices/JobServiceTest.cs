using BusinessLogicServices.JobServices;
using FirestoreInfrastructureServices.Collections;
using Models.Documents.Profile;
using Moq;

namespace Tests.BusinessLogicServicesTests.JobServices;

[TestFixture]
public class JobServiceTest
{
    private Mock<IWorkExperienceCollectionQueries> _workExperienceCollectionMock;
    private JobService _jobService;

    [SetUp]
    public void Setup()
    {
        _workExperienceCollectionMock = new Mock<IWorkExperienceCollectionQueries>();
        _jobService = new JobService(_workExperienceCollectionMock.Object);
    }

    [Test]
    public async Task AddJob_NoExistingJobs_AddsJob()
    {
        // Arrange
        var newJob = new JobDocument
        {
            JobTitle = "Software Engineer",
            CompanyName = "Example Company",
            StartedOn = new DateTime(2022, 1, 1),
        };
        _workExperienceCollectionMock.Setup(x => x.GetWorkExperienceTimeLineAsync(CancellationToken.None))
            .ReturnsAsync(Enumerable.Empty<JobDocument>());

        // Act
        var result = await _jobService.AddJob(newJob);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreNotEqual(string.Empty, result.DocumentId);
        _workExperienceCollectionMock.Verify(x => x.AddDocument(It.IsAny<JobDocument>()), Times.Once);
    }

    [Test]
    public async Task AddJob_ExistingJobs_AddsJob()
    {
        // Arrange
        var existingJob = new JobDocument
        {
            JobTitle = "Software Engineer",
            CompanyName = "Example Company",
            StartedOn = new DateTime(2022, 1, 1),
            DocumentId = Guid.NewGuid().ToString()
        };

        var newJob = new JobDocument
        {
            JobTitle = "Software Engineer",
            CompanyName = "Example Company",
            StartedOn = new DateTime(2023, 1, 1),
            IsCurrentJob = true
        };

        _workExperienceCollectionMock.Setup(x => x.GetWorkExperienceTimeLineAsync(CancellationToken.None))
            .ReturnsAsync(new List<JobDocument> { existingJob });

        // Act
        var result = await _jobService.AddJob(newJob);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreNotEqual(string.Empty, result.DocumentId);
        _workExperienceCollectionMock.Verify(x => x.AddDocument(It.IsAny<JobDocument>()), Times.Once);

    }

    [Test]
    public Task AddJob_InvalidJob_ThrowsException_IsNotCurrentJobAndHaveNoEndDate()
    {
        // Arrange
        var existingCurrentJob = new JobDocument
        {
            JobTitle = "Software Engineer",
            CompanyName = "Example Company",
            StartedOn = new DateTime(2022, 1, 1),
            DocumentId = Guid.NewGuid().ToString(),
            IsCurrentJob = true
        };

        var newJob = new JobDocument
        {
            JobTitle = "Software Engineer",
            CompanyName = "Example Company",
            StartedOn = new DateTime(2022, 1, 1),
            DocumentId = Guid.NewGuid().ToString(),
            IsCurrentJob = false
        };

        _workExperienceCollectionMock.Setup(x => x.GetWorkExperienceTimeLineAsync(CancellationToken.None))
            .ReturnsAsync(new List<JobDocument> { existingCurrentJob });

        // Act
        Assert.ThrowsAsync<ArgumentException>(async () => await _jobService.AddJob(newJob));
        return Task.CompletedTask;
    }

    [Test]
    public Task AddJob_InvalidJob_ThrowsException_IsNotCurrentJobAndAnExistingCurrentJob()
    {
        // Arrange
        var existingCurrentJob = new JobDocument
        {
            JobTitle = "Software Engineer",
            CompanyName = "Example Company",
            StartedOn = new DateTime(2022, 1, 1),
            DocumentId = Guid.NewGuid().ToString(),
            IsCurrentJob = true
        };

        var newJob = new JobDocument
        {
            JobTitle = "Software Engineer",
            CompanyName = "Example Company",
            StartedOn = new DateTime(2022, 1, 1),
            DocumentId = Guid.NewGuid().ToString(),
            IsCurrentJob = true
        };

        _workExperienceCollectionMock.Setup(x => x.GetWorkExperienceTimeLineAsync(CancellationToken.None))
            .ReturnsAsync(new List<JobDocument> { existingCurrentJob });

        // Act
        Assert.ThrowsAsync<ArgumentException>(async () => await _jobService.AddJob(newJob));
        return Task.CompletedTask;
    }


    [Test]
    [TestCase("2023-12-31","2024-03-01")]
    [TestCase("2020-12-01","2021-07-01")]
    [TestCase("2020-12-01","2023-07-01")]
    public Task AddJob_InvalidJob_ThrowsException_OverlappingJobs(DateTime startDate, DateTime endDate)
    {
        // Arrange
        var workExperienceList = CreateNonOverlappingJobs();

        var newJob = new JobDocument()
        {
            JobTitle = "Overlapping Job",
            CompanyName = "Overlap Inc.",
            StartedOn = startDate,
            EndedOn = endDate,
            IsCurrentJob = false,
            DocumentId = Guid.NewGuid().ToString()
        };
        
        _workExperienceCollectionMock.Setup(x => x.GetWorkExperienceTimeLineAsync(CancellationToken.None))
            .ReturnsAsync(workExperienceList);
        
        // Act
        Assert.ThrowsAsync<ArgumentException>(async () => await _jobService.AddJob(newJob));
        return Task.CompletedTask;
    }
    
    private static List<JobDocument> CreateNonOverlappingJobs()
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