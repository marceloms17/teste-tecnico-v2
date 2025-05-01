namespace Thunders.TechTest.ApiService.Application.Messages
{
    public class RegisterTollUsageMessage
    {
        public DateTime Timestamp { get; set; }
        public string TollStation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal AmountPaid { get; set; }
        public string VehicleType { get; set; }
    }
}