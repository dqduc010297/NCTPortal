using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.GoGo.Services.Fleet_management;
using Groove.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers
{
    [Route("api/MasterData/Warehouses")]
    [ApiController]
    public class WarehousesController : BaseController
	{
		private readonly IWarehouseService _warehouseService;

		public WarehousesController(IWarehouseService warehouseService)
		{
			_warehouseService = warehouseService;
		}

		[Route("")]
		[Authorize(Roles = "Coordinator")]
		[HttpGet]
		public async Task<IActionResult> GetDataSource([FromQuery]string value)
		{

			return Ok(await _warehouseService.GetDataSource(value));
		}

		[Route("{id}")]
		[Authorize(Roles = "Coordinator")]
		[HttpGet]
		public async Task<IActionResult> GetWarehouseDetailAsync(int id)
		{

			return Ok(await _warehouseService.GetWarehouseDetailAsync(id));
		}

        // Đ
        // GET /api/masterdata/warehouses/datasource/{displayName}
        [Route("datasource/{displayName}")]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOnFilter(string displayName)
        {
            var userId = GetCurrentUserId<long>();
            var result = await _warehouseService.GetOnFilter(displayName, userId);
            return Ok(result);
        }
        // End Đ
    }
}