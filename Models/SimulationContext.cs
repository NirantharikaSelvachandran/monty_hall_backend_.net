using Microsoft.EntityFrameworkCore;
using System;

namespace montyHallBackend.Models
{
	public class SimulationContext : DbContext
	{
		public SimulationContext(DbContextOptions<SimulationContext> options) : base(options) { }

		public DbSet<Simulation> Simulations { get; set; }
	}
}
