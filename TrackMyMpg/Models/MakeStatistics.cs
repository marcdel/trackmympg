namespace TrackMyMpg.Models
{
    public class MakeStatistics
    {
        public int MakeId { get; set; }
        public string Make { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public decimal Average { get; set; }
    }
}