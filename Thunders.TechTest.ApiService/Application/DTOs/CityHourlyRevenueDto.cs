namespace Thunders.TechTest.ApiService.Application.DTOs
{
    public class CityHourlyRevenueDto
    {
        public string City { get; set; }
        public List<HourRevenueDto> HourlyRevenues { get; set; }
    }

    public class HourRevenueDto
    {
        public int Hour { get; set; }
        public decimal Revenue { get; set; }
    }
}