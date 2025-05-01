using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Handlers.QueryHandlers;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Interfaces;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TollUsageController : ControllerBase
    {
        private readonly ITollUsageRepository _repository;
        private readonly IMediator _mediator;

        public TollUsageController(ITollUsageRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] EnqueueTollUsageCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("revenue-by-hour")]
        public async Task<IActionResult> GetRevenueByHour([FromQuery]
                                                          [DefaultValue("2025-04-29T00:00:00")] DateTime from,
                                                          [FromQuery]
                                                          [DefaultValue("2025-05-01T00:00:00")] DateTime to)
        {
            var query = new GetRevenuePerHourQuery { From = from, To = to };
            var handler = new GetRevenuePerHourHandler(_repository);
            var result = await handler.HandleAsync(query);
            return Ok(result);
        }

        [HttpGet("top-stations")]
        public async Task<IActionResult> GetTopStations([FromQuery] int year, [FromQuery] int month, [FromQuery] int topCount = 5)
        {
            var query = new GetTopStationsByMonthQuery { Year = year, Month = month, TopCount = topCount };
            var handler = new GetTopStationsByMonthHandler(_repository);
            var result = await handler.HandleAsync(query);
            return Ok(result);
        }

        [HttpGet("vehicle-count")]
        public async Task<IActionResult> GetVehicleCount([FromQuery] string tollStation)
        {
            var query = new GetVehicleCountByStationQuery { TollStation = tollStation };
            var handler = new GetVehicleCountByStationHandler(_repository);
            var result = await handler.HandleAsync(query);
            return Ok(result);
        }
    }
}