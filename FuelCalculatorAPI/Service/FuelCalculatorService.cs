using FuelCalculatorAPI.Models;

namespace FuelCalculatorAPI.Service
{
    public class FuelCalculatorService
    {

        public FuelResponseData CalculateFuel(PointsFuelRequestParameters parameters)
        {
            var distance = DistanceByPointsService.HaversineDistanceByPoints(parameters.PointsOnMap.FirstPoint, parameters.PointsOnMap.SecondPoint);
            var totalFuelConsumed = (distance / 100) * parameters.FuelConsumptionPer100Km; 
            var totalCost = totalFuelConsumed * parameters.FuelPricePerLiter;
            return new FuelResponseData
            {
                TotalCost = totalCost,
            };
        }
        public FuelResponseData CalculateFuel(DistanceFuelRequestParameters parameters)
        {
            var distance = parameters.Distance;
            var totalFuelConsumed = (distance / 100) * parameters.FuelConsumptionPer100Km; 
            var totalCost = totalFuelConsumed * parameters.FuelPricePerLiter;
            return new FuelResponseData
            {
                TotalCost = totalCost,
            };
        }
    }
}
