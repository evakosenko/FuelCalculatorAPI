using FuelCalculatorAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FuelCalculatorAPI.Service
{
    public class ValidationService
    {

        /// <summary>
        /// Проверяет параметры запроса на валидность для двух точек.
        /// </summary>
        /// <param name="parameters">Параметры запроса, включающие начальную и конечную точки.</param>
        /// <returns>Список ошибок валидации, если таковые имеются. Если список пуст, данные валидны.</returns>
        public IEnumerable<ValidationResult> ValidatePointsRequest(PointRequest parameters)
        {
            // Проверка широты и долготы начальной точки
            if (parameters.FirstPoint == null)
            {
                yield return new ValidationResult("FirstPoint cannot be null.");
            }
            else
            {
                if (!IsValidLatitude(parameters.FirstPoint.Latitude))
                {
                    yield return new ValidationResult("Latitude of FirstPoint must be between -90 and 90.");
                }

                if (!IsValidLongitude(parameters.FirstPoint.Longitude))
                {
                    yield return new ValidationResult("Longitude of FirstPoint must be between -180 and 180.");
                }
            }

            // Проверка широты и долготы конечной точки
            if (parameters.SecondPoint == null)
            {
                yield return new ValidationResult("SecondPoint cannot be null.");
            }
            else
            {
                if (!IsValidLatitude(parameters.SecondPoint.Latitude))
                {
                    yield return new ValidationResult("Latitude of SecondPoint must be between -90 and 90.");
                }

                if (!IsValidLongitude(parameters.SecondPoint.Longitude))
                {
                    yield return new ValidationResult("Longitude of SecondPoint must be between -180 and 180.");
                }
            }

            // Проверка на совпадение точек
            if (parameters.FirstPoint != null && parameters.SecondPoint != null &&
                parameters.FirstPoint.Latitude == parameters.SecondPoint.Latitude &&
                parameters.FirstPoint.Longitude == parameters.SecondPoint.Longitude)
            {
                yield return new ValidationResult("FirstPoint and SecondPoint cannot be the same.");
            }
        }

        /// <summary>
        /// Проверяет параметры запроса для расчета топлива по расстоянию.
        /// </summary>
        /// <param name="request">Параметры запроса, включающие расстояние, расход топлива на 100 км и другие параметры.</param>
        /// <returns>Список ошибок валидации, если таковые имеются. Если список пуст, данные валидны.</returns>
        public IEnumerable<ValidationResult> ValidateDistanceFuelRequest(DistanceFuelRequestParameters request)
        {
            // Проверка дистанции
            if (request.Distance <= 0)
            {
                yield return new ValidationResult("Distance must be greater than 0.");
            }

            // Проверка расхода топлива на 100 км
            if (request.FuelConsumptionPer100Km <= 0)
            {
                yield return new ValidationResult("FuelConsumptionPer100Km must be greater than 0.");
            }

            // Проверка цены топлива
            if (request.FuelPricePerLiter <= 0)
            {
                yield return new ValidationResult("FuelPricePerLiter must be greater than 0.");
            }

            // Проверка средней скорости
            if (request.AverageSpeed <= 0)
            {
                yield return new ValidationResult("AverageSpeed must be greater than 0.");
            }
        }

        /// <summary>
        /// Проверяет параметры запроса для расчета топлива по двум точкам.
        /// </summary>
        /// <param name="request">Параметры запроса, включая координаты точек и дополнительные параметры.</param>
        /// <returns>Список ошибок валидации, если таковые имеются. Если список пуст, данные валидны.</returns>
        public IEnumerable<ValidationResult> ValidatePointsFuelRequest(PointsFuelRequestParameters request)
        {
            // Валидация точек маршрута
            foreach (var validationResult in ValidatePointsRequest(request.PointsOnMap))
            {
                yield return validationResult;
            }

          

            // Валидация расхода топлива
            if (request.FuelConsumptionPer100Km <= 0)
            {
                yield return new ValidationResult("FuelConsumptionPer100Km must be greater than 0.");
            }

            // Валидация цены топлива
            if (request.FuelPricePerLiter <= 0)
            {
                yield return new ValidationResult("FuelPricePerLiter must be greater than 0.");
            }

            // Валидация средней скорости
            if (request.AverageSpeed <= 0)
            {
                yield return new ValidationResult("AverageSpeed must be greater than 0.");
            }
        }

        private bool IsValidLatitude(double latitude) => latitude >= -90 && latitude <= 90;

        private bool IsValidLongitude(double longitude) => longitude >= -180 && longitude <= 180;
    }
}
