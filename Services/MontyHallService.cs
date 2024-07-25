using montyHallBackend.Models;

namespace montyHallBackend.Services
{
	public class MontyHallService
	{
		private readonly SimulationContext _context;

		public MontyHallService(SimulationContext context)
		{
			_context = context;
		}

		public Simulation Simulate(int games, bool switchDoor)
		{
			int wins = 0;
			int losses = 0;

			Random random = new Random();

			for (int i = 0; i < games; i++)
			{
				int prizeDoor = random.Next(3);
				int chosenDoor = random.Next(3);

				if (switchDoor)
				{
					chosenDoor = 3 - (chosenDoor + prizeDoor); 
				}

				if (chosenDoor == prizeDoor)
				{
					wins++;
				}
				else
				{
					losses++;
				}
			}

			var simulation = new Simulation
			{
				TotalGames = games,
				Wins = wins,
				Losses = losses
			};

			_context.Simulations.Add(simulation);
			_context.SaveChanges();

			return simulation;
		}
	}
}
