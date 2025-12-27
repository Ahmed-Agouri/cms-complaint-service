using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.complaint;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Models;
using ComplaintService.Application.Services;
using Moq;
using Xunit;

public class ComplaintServiceTests
{
    private readonly Mock<IComplaintRepository> _repoMock;
    private readonly ComplaintService.Application.Services.ComplaintService _service;

    public ComplaintServiceTests()
    {
        _repoMock = new Mock<IComplaintRepository>();
        _service = new ComplaintService.Application.Services.ComplaintService(_repoMock.Object);
    }

    [Fact]
    public async Task CreateComplaintAsync_Should_Set_Base_Fields_And_Save()
    {
        // Arrange
        var dto = new CreateComplaintDto
        {
            Title = "Test complaint",
            Description = "Something broke",
            Priority = PriorityLevel.High,
            Category = ComplaintCategory.TechnicalIssue
        };

        var tenantId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var result = await _service.CreateComplaintAsync(dto, tenantId, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test complaint", result.Title);
        Assert.Equal(Status.Open, result.Status);

        _repoMock.Verify(r => r.AddAsync(It.IsAny<Complaint>()), Times.Once);
    }

    [Fact]
    public async Task GetComplaintsByUserAsync_Should_Return_Mapped_Dtos()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tenantId = Guid.NewGuid();

        _repoMock.Setup(r => r.GetByUserAndTenantAsync(userId, tenantId))
            .ReturnsAsync(new List<Complaint>
            {
                new Complaint { Title = "One" },
                new Complaint { Title = "Two" }
            });

        // Act
        var result = await _service.GetComplaintsByUserAsync(userId, tenantId);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Title == "One");
        Assert.Contains(result, c => c.Title == "Two");
    }

    [Fact]
    public async Task UpdateComplaintAsync_Should_Return_Null_When_Not_Found()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAndTenantAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync((Complaint?)null);

        // Act
        var result = await _service.UpdateComplaintAsync(
            Guid.NewGuid(),
            new UpdateComplaintDto(),
            Guid.NewGuid()
        );

        // Assert
        Assert.Null(result);
    }
}
