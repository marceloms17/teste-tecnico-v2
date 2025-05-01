using Thunders.TechTest.ApiService.Models.Enums;

namespace Thunders.TechTest.ApiService.Models
{
    public class TollUsage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; }
        public string TollStation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal AmountPaid { get; set; }
        public VehicleType VehicleType { get; set; }
    }

    //public enum VehicleType//TODO - separa em uma classe na pasta enuns
    //{
    //    Motorcycle,
    //    Car,
    //    Truck
    //}
}
