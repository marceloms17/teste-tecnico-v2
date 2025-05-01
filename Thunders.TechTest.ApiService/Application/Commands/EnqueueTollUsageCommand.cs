using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Application.Commands
{
    public class EnqueueTollUsageCommand : IRequest<Unit>
    {
        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string TollStation { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public string VehicleType { get; set; }
    }
}