using Manager.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/v1/fuels")]
    public class FuelController : BaseController
    {
        private readonly IFuelService _service;

        public FuelController(IFuelService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
            => ReturnResponse(_service.Get(), null);

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
            => ReturnResponse(_service.Get(id), null);
    }
}
