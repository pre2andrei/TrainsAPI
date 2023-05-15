namespace TrainsAPI.Models
{
    public class JourneyViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Departure { get; set; }

        public List<Road> roads { get; set; }   

        public Train Train { get; set; }
    }
}
