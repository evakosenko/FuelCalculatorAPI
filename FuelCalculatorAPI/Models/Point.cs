namespace FuelCalculatorAPI.Models
{
    /// <summary>
    /// Модель точки с координатами.
    /// </summary>
    public class PointRequest
    {
        public Point FirstPoint { get; set; }
        public Point SecondPoint { get; set; }
    }

    /// <summary>
    /// Модель для посчитанной дистанции по двум точкам.
    /// </summary>
    public class PointResponse
    {
        /// <summary>
        /// Найденая дистанция
        /// </summary>
        public double Distance { get; set; }

        public PointResponse(double distance)
        {
            Distance = distance;
        }
    }

    /// <summary>
    /// Модель точки с координатами.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Широта.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота.
        /// </summary>
        public double Longitude { get; set; }
    }
}
