namespace Thunders.TechTest.ApiService.Application.Queries
{
    public class GetTopStationsByMonthQuery
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TopCount { get; set; } = 5;
    }
}