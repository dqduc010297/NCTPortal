using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.GoGo.Services.Fleet_management;
using Groove.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers.GoGo
{
    [Route("api/MasterData/VehicleFeatures")]
    public class VehicleFeatureController : BaseController
    {
        private readonly IVehicleFeatureService _vehicleFeatureService;
        public VehicleFeatureController(IVehicleFeatureService vehicleFeatureService)
        {
            _vehicleFeatureService = vehicleFeatureService;
        }

        // Đ

        // GET /api/vehicle-feature/vehicle-feature/{displayName}
        [Route("datasource/{displayName}")]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOnFilter(string displayName)
        {
            var result = await _vehicleFeatureService.GetOnFilter(displayName);
            return Ok(result);
        }
        //End Đ
    }
}