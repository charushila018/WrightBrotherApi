using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WrightBrothersApi.Tests.Controllers
{
    public class FlightsControllerTests
    {
        private readonly ILogger<FlightsController> _logger;
        private readonly FlightsController _flightsController;

        public FlightsControllerTests()
        {
            _logger = Substitute.For<ILogger<FlightsController>>();
            _flightsController = new FlightsController(_logger);
        }

        [Fact]
        public void Get_ReturnsListOfFlights()
        {
            // Act
            var result = _flightsController.Get();

            // Assert
            var okObjectResult = (OkObjectResult)result.Result!;
            var returnedFlights = (List<Flight>)okObjectResult.Value!;
            returnedFlights.Should().NotBeEmpty();
        }

        [Fact]
        public void AddFlight_AddsFlightAndReturnsCreated()
        {
            // Arrange
            var newFlight = new Flight
            {
                Id = 99,
                FlightNumber = "WB099",
                Origin = "Dayton, OH",
                Destination = "Kitty Hawk, NC",
                DepartureTime = new DateTime(1904, 1, 1, 9, 0, 0),
                ArrivalTime = new DateTime(1904, 1, 1, 10, 0, 0),
                Status = FlightStatus.Scheduled,
                FuelRange = 120,
                FuelTankLeak = false,
                FlightLogSignature = "01011904-DEP-ARR-WB099",
                AerobaticSequenceSignature = "L1A-H1B-R1C-S1D-T1E"
            };

            // Act
            var result = _flightsController.AddFlight(newFlight);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();

            var createdAtActionResult = (CreatedAtActionResult)result.Result!;
            var returnedFlight = (Flight)createdAtActionResult.Value!;
            returnedFlight.Should().BeEquivalentTo(newFlight);
        }

        [Fact]
        public void GetById_ReturnsFlight()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _flightsController.GetById(id);

            // Assert
            var okObjectResult = (OkObjectResult)result.Result!;
            var returnedFlight = (Flight)okObjectResult.Value!;
            returnedFlight.Should().NotBeNull();
        }
    }
}