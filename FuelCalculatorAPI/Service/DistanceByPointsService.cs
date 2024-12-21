using FuelCalculatorAPI.Models;

namespace FuelCalculatorAPI.Service
{
    public class DistanceByPointsService
    {
        const double EarthRadiusKm = 6371.0;
        public static PointResponse HaversineDistance(PointRequest point)
        {
            double lat1 = point.FirstPoint.Latitude;
            double lon1 = point.FirstPoint.Longitude;
            double lat2 = point.SecondPoint.Latitude;
            double lon2 = point.SecondPoint.Longitude;

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return new PointResponse( EarthRadiusKm * c);
        }



        public static double HaversineDistanceByPoints(Point point1, Point point2)
        {
            double lat1 = point1.Latitude;
            double lon1 = point1.Longitude;
            double lat2 = point2.Latitude;
            double lon2 = point2.Longitude;

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;

        }
    }
}
