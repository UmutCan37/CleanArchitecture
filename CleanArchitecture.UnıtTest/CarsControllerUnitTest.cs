using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest;

public sealed class CarsControllerUnitTest
{
    [Fact]
    public async Task Create_ReturnsOkResult_WhenRequestIsValid()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();

        CreateCarCommand createCarCommand = new(
            name: "Test Car",
            model: "Test Model",
            enginePower: 100);

        MessageResponse response = new("Car created successfully.");
        CancellationToken cancellationToken = CancellationToken.None;

        mockMediator
            .Setup(m => m.Send(createCarCommand, cancellationToken))
            .ReturnsAsync(response);

        CarsController carsController = new(mockMediator.Object);

        // Act
        var result = await carsController.CreateCar(createCarCommand, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

        Assert.Equal(response.Message, returnValue.Message);
        mockMediator.Verify(m => m.Send(createCarCommand, cancellationToken), Times.Once);
    }
}