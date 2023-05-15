using System.Diagnostics.Metrics;

namespace TrainsAPI.Data
{
    public class Seed
    {
        private readonly TrainsDataContext dataContext;
        public Seed(TrainsDataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
        }
    }
}
