namespace FuelCalculatorAPI.Models
{
    /// <summary>
    /// Базовый класс для параметров запроса.
    /// </summary>
    public class BaseFuelRequestParameters
    {
        /// <summary>
        /// Цена бензина за литр.
        /// </summary>
        public double FuelPricePerLiter { get; set; }

        /// <summary>
        /// Средняя скорость движения (км/ч).
        /// </summary>
        public double AverageSpeed { get; set; }
        /// <summary>
        /// Расход топлива на 100 км.
        /// </summary>
        public double FuelConsumptionPer100Km { get; set; }
    }

    /// <summary>
    /// Класс параметров запроса, включающий пройденное расстояние.
    /// </summary>
    public class DistanceFuelRequestParameters : BaseFuelRequestParameters
    {
        /// <summary>
        /// Пройденное расстояние (в километрах).
        /// </summary>
        public double Distance { get; set; }
    }

    /// <summary>
    /// Класс параметров запроса, включающий две точки (начальная и конечная).
    /// </summary>
    public class PointsFuelRequestParameters : BaseFuelRequestParameters
    {
        /// <summary>
        /// Точки на карте
        /// </summary>
        public PointRequest PointsOnMap { get; set; }

    }

    /// <summary>
    /// Модель данных для ответа.
    /// </summary>
    public class FuelResponseData
    {
        /// <summary>
        /// Общая стоимость поездки.
        /// </summary>
        public double TotalCost { get; set; }


    }

    
}
