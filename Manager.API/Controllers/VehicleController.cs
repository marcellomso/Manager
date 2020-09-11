using Manager.Domain.Commands.VehicleCommands;
using Manager.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Manager.API.Controllers
{
    [Route("api/v1/Vehicles/")]
    public class VehicleController: BaseController
    {
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
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

        [HttpPost]
        public IActionResult Save(VehicleCommand command)
        {
            try
            {
                var vehicle = _service.New(command);
                return WriteResponse(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Save(UpdateVehicleCommand command)
        {
            try
            {
                var vehicle = _service.Update(command);
                return WriteResponse(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return WriteResponse(_service.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
