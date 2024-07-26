using Xunit;
using montyHallBackend.Services;
using Microsoft.EntityFrameworkCore;
using montyHallBackend.Models;

namespace montyHallBackend.Tests
{
	public class SimulationServiceTests
	{
		private DbContextOptions<SimulationContext> GetInMemoryDbContextOptions()
		{
			return new DbContextOptionsBuilder<SimulationContext>()
				.UseInMemoryDatabase(databaseName: "MontyHallTestDatabase")
				.Options;
		}

		[Fact]
		public void Simulate_WithSwitchDoor_ShouldReturnCorrectResults()
		{
			// Arrange
			var options = GetInMemoryDbContextOptions();
			using (var context = new SimulationContext(options))
			{
				var service = new MontyHallService(context);
				int games = 1000;
				bool switchDoor = true;

				// Act
				var result = service.Simulate(games, switchDoor);

				// Assert
				Assert.Equal(games, result.TotalGames);
				Assert.True(result.Wins > 0);
				Assert.True(result.Losses > 0);
				Assert.Equal(games, result.Wins + result.Losses);

				// Verify the result is saved in the database
				var savedSimulation = context.Simulations.FirstOrDefault(s => s.Id == result.Id);
				Assert.NotNull(savedSimulation);
				Assert.Equal(result.TotalGames, savedSimulation.TotalGames);
				Assert.Equal(result.Wins, savedSimulation.Wins);
				Assert.Equal(result.Losses, savedSimulation.Losses);
			}
		}

		[Fact]
		public void Simulate_WithoutSwitchDoor_ShouldReturnCorrectResults()
		{
			// Arrange
			var options = GetInMemoryDbContextOptions();
			using (var context = new SimulationContext(options))
			{
				var service = new MontyHallService(context);
				int games = 1000;
				bool switchDoor = false;

				// Act
				var result = service.Simulate(games, switchDoor);

				// Assert
				Assert.Equal(games, result.TotalGames);
				Assert.True(result.Wins > 0);
				Assert.True(result.Losses > 0);
				Assert.Equal(games, result.Wins + result.Losses);

				// Verify result is saved in database
				var savedSimulation = context.Simulations.FirstOrDefault(s => s.Id == result.Id);
				Assert.NotNull(savedSimulation);
				Assert.Equal(result.TotalGames, savedSimulation.TotalGames);
				Assert.Equal(result.Wins, savedSimulation.Wins);
				Assert.Equal(result.Losses, savedSimulation.Losses);
			}
		}

	}
}
