using FuelCalculatorAPI.Models;
using FuelCalculatorAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FuelCalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuelCalculatorController : ControllerBase
    {
        private readonly ValidationService _validationService;
        private readonly FuelCalculatorService _fuelCalculatorService;

        public FuelCalculatorController(ValidationService validationService, FuelCalculatorService fuelCalculatorService)
        {
            _validationService = validationService;
            _fuelCalculatorService = fuelCalculatorService;
        }

        /// <summary>
        /// Метод для расчета расхода топлива по заданной дистанции.
        /// </summary>
        /// <remarks>
        /// Этот метод принимает параметры, такие как расстояние, расход топлива на 100 км,
        /// цену бензина на литр и среднюю скорость, и возвращает расчет стоимости поездки и общего расхода топлива.
        /// 
        /// Пример запроса:
        /// 
        ///     POST /api/FuelCalculator/ByDistance
        ///     {
        ///        "Distance": 150,                  // Дистанция в километрах
        ///        "FuelConsumptionPer100Km": 8.5,    // Расход топлива на 100 км (литры)
        ///        "FuelPricePerLiter": 1.35,         // Цена бензина за литр
        ///        "AverageSpeed": 60                 // Средняя скорость (км/ч)
        ///     }
        /// </remarks>
        /// <param name="request">Параметры запроса, включающие дистанцию, расход топлива на 100 км, цену бензина и среднюю скорость.</param>
        /// <returns>Возвращает объект с расчетами расхода топлива и стоимости поездки в формате JSON.</returns>
        /// <response code="200">Возвращает успешный результат с расчетами стоимости и расхода топлива.</response>
        /// <response code="400">Если параметры запроса некорректны (например, отрицательные значения).</response>
        /// <response code="500">Если произошла ошибка на сервере при обработке запроса.</response>
        [HttpPost("ByDistance")]
        [ProducesResponseType(typeof(FuelResponseData), 200)]
        public IActionResult CalculateFuelConsumption([FromBody] DistanceFuelRequestParameters request)
        {
            var validationResults = _validationService.ValidateDistanceFuelRequest(request);
            if (validationResults.Any())
            {
                return BadRequest(validationResults);
            }
            var result = _fuelCalculatorService.CalculateFuel(request);
            return Ok(result);
        }

        /// <summary>
        /// Метод для расчета расхода топлива по двум точкам.
        /// </summary>
        /// <remarks>
        /// Этот метод принимает данные с координатами начальной и конечной точек маршрута,
        /// а также параметры, такие как тип топлива, скорость и расход топлива на 100 км,
        /// проверяет их на валидность и возвращает расчет стоимости поездки и общего расхода топлива.
        ///
        /// Пример запроса:
        /// 
        ///     POST /api/FuelCalculator/ByPoints
        ///     {
        ///        "pointsOnMap": {
        ///            "firstPoint": { "latitude": 55.7558, "longitude": 37.6173 },
        ///            "secondPoint": { "latitude": 59.9343, "longitude": 30.3351 } 
        ///        },
        ///        "averageSpeed": 60,              
        ///        "fuelConsumptionPer100Km": 8.5,  
        ///        "fuelPricePerLiter": 1.35       
        ///     }
        /// </remarks>
        /// <param name="request">Данные для расчета расхода топлива по двум точкам, включая параметры топлива и скорости.</param>
        /// <returns>Результат расчета, включая стоимость и расход топлива.</returns>
        /// <response code="200">Возвращает успешный результат с расчетами стоимости и расхода топлива.</response>
        /// <response code="400">Если параметры запроса некорректны (например, некорректные координаты или недопустимые значения).</response>
        /// <response code="500">Если произошла ошибка на сервере при обработке запроса.</response>
        [HttpPost("ByPoints")]
        [ProducesResponseType(typeof(FuelResponseData), 200)]
        public IActionResult CalculateFuelConsumption([FromBody] PointsFuelRequestParameters request)
        {
            var validationResults = _validationService.ValidatePointsFuelRequest(request);
            if (validationResults.Any())
            {
                return BadRequest(validationResults);
            }

            var result = _fuelCalculatorService.CalculateFuel(request);
            return Ok(result);
        }



        /// <summary>
        /// Метод для расчета расстояния между двумя точками.
        /// </summary>
        /// <remarks>
        /// Этот метод принимает данные с координатами двух точек, проверяет их на валидность,
        /// и возвращает расчет расстояния между ними.
        /// 
        /// Пример запроса:
        /// 
        ///     POST /api/FuelCalculator/ByPoints
        ///     {
        ///        "FirstPoint": { "Latitude": 55.7558, "Longitude": 37.6173 },
        ///        "SecondPoint": { "Latitude": 59.9343, "Longitude": 30.3351 }
        ///     }
        /// </remarks>
        /// <param name="request">Данные для расчета расстояния между двумя точками.</param>
        /// <returns>Результат расчета.</returns>
        /// <response code="200">Возвращает успешный результат с рассчитанным расстоянием.</response>
        /// <response code="400">Если параметры запроса некорректны (например, некорректные координаты).</response>
        /// <response code="500">Если произошла ошибка на сервере при обработке запроса.</response>
        [HttpPost("DistanceByPoints")]
        [ProducesResponseType(typeof(PointResponse), 200)]
        public IActionResult CalculateFuelConsumption([FromBody] PointRequest request)
        {
            var validationResults = _validationService.ValidatePointsRequest(request);
            if (validationResults.Any())
            {
                return BadRequest(validationResults);
            }
            var result = DistanceByPointsService.HaversineDistance(request);
            return Ok(result);
        }
    }
}
