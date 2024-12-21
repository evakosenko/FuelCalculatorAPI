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
        /// ����� ��� ������� ������� ������� �� �������� ���������.
        /// </summary>
        /// <remarks>
        /// ���� ����� ��������� ���������, ����� ��� ����������, ������ ������� �� 100 ��,
        /// ���� ������� �� ���� � ������� ��������, � ���������� ������ ��������� ������� � ������ ������� �������.
        /// 
        /// ������ �������:
        /// 
        ///     POST /api/FuelCalculator/ByDistance
        ///     {
        ///        "Distance": 150,                  // ��������� � ����������
        ///        "FuelConsumptionPer100Km": 8.5,    // ������ ������� �� 100 �� (�����)
        ///        "FuelPricePerLiter": 1.35,         // ���� ������� �� ����
        ///        "AverageSpeed": 60                 // ������� �������� (��/�)
        ///     }
        /// </remarks>
        /// <param name="request">��������� �������, ���������� ���������, ������ ������� �� 100 ��, ���� ������� � ������� ��������.</param>
        /// <returns>���������� ������ � ��������� ������� ������� � ��������� ������� � ������� JSON.</returns>
        /// <response code="200">���������� �������� ��������� � ��������� ��������� � ������� �������.</response>
        /// <response code="400">���� ��������� ������� ����������� (��������, ������������� ��������).</response>
        /// <response code="500">���� ��������� ������ �� ������� ��� ��������� �������.</response>
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
        /// ����� ��� ������� ������� ������� �� ���� ������.
        /// </summary>
        /// <remarks>
        /// ���� ����� ��������� ������ � ������������ ��������� � �������� ����� ��������,
        /// � ����� ���������, ����� ��� ��� �������, �������� � ������ ������� �� 100 ��,
        /// ��������� �� �� ���������� � ���������� ������ ��������� ������� � ������ ������� �������.
        ///
        /// ������ �������:
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
        /// <param name="request">������ ��� ������� ������� ������� �� ���� ������, ������� ��������� ������� � ��������.</param>
        /// <returns>��������� �������, ������� ��������� � ������ �������.</returns>
        /// <response code="200">���������� �������� ��������� � ��������� ��������� � ������� �������.</response>
        /// <response code="400">���� ��������� ������� ����������� (��������, ������������ ���������� ��� ������������ ��������).</response>
        /// <response code="500">���� ��������� ������ �� ������� ��� ��������� �������.</response>
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
        /// ����� ��� ������� ���������� ����� ����� �������.
        /// </summary>
        /// <remarks>
        /// ���� ����� ��������� ������ � ������������ ���� �����, ��������� �� �� ����������,
        /// � ���������� ������ ���������� ����� ����.
        /// 
        /// ������ �������:
        /// 
        ///     POST /api/FuelCalculator/ByPoints
        ///     {
        ///        "FirstPoint": { "Latitude": 55.7558, "Longitude": 37.6173 },
        ///        "SecondPoint": { "Latitude": 59.9343, "Longitude": 30.3351 }
        ///     }
        /// </remarks>
        /// <param name="request">������ ��� ������� ���������� ����� ����� �������.</param>
        /// <returns>��������� �������.</returns>
        /// <response code="200">���������� �������� ��������� � ������������ �����������.</response>
        /// <response code="400">���� ��������� ������� ����������� (��������, ������������ ����������).</response>
        /// <response code="500">���� ��������� ������ �� ������� ��� ��������� �������.</response>
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
