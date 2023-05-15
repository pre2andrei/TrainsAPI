namespace TrainsAPI.Models
{
    public class PostTrainModel
    {
        public Train Train { get; set; }

        public ICollection<int> CarIds { get; set; }
    }
}
