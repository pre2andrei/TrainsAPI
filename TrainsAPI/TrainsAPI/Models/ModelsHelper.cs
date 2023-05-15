namespace TrainsAPI.Models
{
    public static class ModelsHelper
    {
        public static TrainViewModel toTrainViewModel(this Train train)
        {
            return new TrainViewModel
            {
                Cars = train.Cars,
                Id = train.Id,
                Code = train.Code,
                EngineId = train.EngineId,
                Engine = train.Engine ?? new Engine()
            };
        }
        public static JourneyViewModel toJourneyViewModel(this Journey journey)
        {
            return new JourneyViewModel
            {
                Code = journey.Code,
                Train = new Train
                {
                    Code = journey.Train.Code,
                    Engine= journey.Train.Engine,
                    Id = journey.Train.Id,
                    Cars = journey.Train.Cars,
                    EngineId= journey.Train.EngineId,
            
                },
                Id = journey.Id,Departure = journey.Departure.ToString(),
                roads = journey.JourneyPaths.Select(x => x.Road).ToList() ?? new List<Road>()
                
            };
        }
    }
}
