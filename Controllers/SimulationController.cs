using Microsoft.AspNetCore.Mvc;
using montyHallBackend.Models;
using montyHallBackend.Services;
using System;

namespace montyHallBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SimulationController : ControllerBase
	{
		private readonly MontyHallService _montyHallService;

		public SimulationController(MontyHallService montyHallService)
		{
			_montyHallService = montyHallService;
		}

		[HttpGet("simulate")]
		public IActionResult Simulate(int games, bool switchDoor)
		{
			var result = _montyHallService.Simulate(games, switchDoor);
			return Ok(result);
		}

	}
}
