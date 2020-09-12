using Manager.Domain.Commands.VehicleCommands;
using Manager.Domain.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Manager.API.Controllers
{
    [Route("api/v1/vehicles")]
    public class VehicleController: BaseController
    {
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
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

        [HttpPost]
        public IActionResult Save(VehicleCommand command)
        {
            try
            {
                var vehicle = _service.New(command);
                return ReturnResponse(vehicle, null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(ex);
            }
        }

        [HttpPut]
        public IActionResult Save(VehicleUpdateCommand command)
        {
            try
            {
                var vehicle = _service.Update(command);
                return ReturnResponse(vehicle, null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                return ReturnResponse(_service.Delete(id), null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(ex);
            }
        }
    }
}
