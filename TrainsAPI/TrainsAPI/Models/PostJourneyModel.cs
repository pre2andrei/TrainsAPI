namespace TrainsAPI.Models
{
    public class PostJourneyModel
    {
        public string Code { get; set; }

        public int TrainId { get; set; }

        public String Departure { get; set; }

        public ICollection<int> roadsIds { get; set; }
    }
}
