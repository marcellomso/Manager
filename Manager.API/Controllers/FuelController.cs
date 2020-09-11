using Manager.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/v1/fuels/")]
    public class FuelController: BaseController
    {
        private readonly IFuelService _service;

        public FuelController(IFuelService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }
    }
}
