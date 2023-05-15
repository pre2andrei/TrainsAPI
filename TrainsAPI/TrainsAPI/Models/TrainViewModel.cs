using System.Collections;

namespace TrainsAPI.Models
{
    public class TrainViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int EngineId { get; set; }
        public ICollection<Car> Cars { get; set; }

       public Engine Engine { get; set; }
    }
}
