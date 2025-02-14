using BusinessLogicServices.JobServices;
using FirestoreInfrastructureServices.Collections;
using Models.Documents.Profile;
using Moq;

namespace Tests.BusinessLogicServicesTests.JobServices;

[TestFixture]
public class UpdateJobServiceTest
{
    private Mock<IWorkExperienceFirestoreCollectionQueries> _workExperienceCollectionMock;
    private JobCommandsService _jobCommandsService;

    [SetUp]
    public void Setup()
    {
        _workExperienceCollectionMock = new Mock<IWorkExperienceFirestoreCollectionQueries>();
        _jobCommandsService = new JobCommandsService(_workExperienceCollectionMock.Object);
    }

    [Test]
    public async Task UpdateJob_Success()
    {
        // Arrange
        var workExperienceList = JobServiceUsr.CreateNonOverlappingJobs();
        _workExperienceCollectionMock.Setup(x => x.GetWorkExperienceTimeLineAsync(CancellationToken.None))
            .ReturnsAsync(workExperienceList);

        var jobUpdates = workExperienceList.FirstOrDefault();
        if (jobUpdates != null)
        {
            jobUpdates.JobTitle = "Update Job Title";
            jobUpdates.CompanyName = "Update Company";
            await _jobCommandsService.UpdateJob(jobUpdates);
        }

        var afterUpdate = workExperienceList.FirstOrDefault();
        Assert.IsNotNull(afterUpdate);
        Assert.That(afterUpdate.JobTitle, Is.EqualTo("Update Job Title"));
    }
    
    [Test]
    public Task UpdateJob_UpdateDatesOverlaps_ThrowsException()
    {
        // Arrange
        var workExperienceList = JobServiceUsr.CreateNonOverlappingJobs();
        _workExperienceCollectionMock.Setup(x => x.GetWorkExperienceTimeLineAsync(CancellationToken.None))
            .ReturnsAsync(workExperienceList);

        var jobUpdates = workExperienceList.FirstOrDefault();
        var secondJob = workExperienceList.ElementAt(1);
        if (jobUpdates != null)
        {
            jobUpdates.EndedOn = secondJob.StartedOn.AddDays(1);
            Assert.ThrowsAsync<ArgumentException>(async () => await _jobCommandsService.UpdateJob(jobUpdates));
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task UpdateJob_NonExistingJobs_ThrowsException()
    {
        // Arrange
        var jobUpdates = new JobDocument
        {
            DocumentId = Guid.NewGuid().ToString(), 
            JobTitle = "Software Engineer",
            CompanyName = "Example Company",
            StartedOn = new DateTime(2022, 1, 1),
        };
        _workExperienceCollectionMock.Setup(x => x.GetWorkExperienceTimeLineAsync(CancellationToken.None))
            .ReturnsAsync(Enumerable.Empty<JobDocument>());

        // Act
        Assert.ThrowsAsync<ArgumentException>(async () => await _jobCommandsService.UpdateJob(jobUpdates));
        return Task.CompletedTask;
    }
}