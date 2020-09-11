using Manager.Domain.Commands.OpportunityCommands;
using Manager.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Manager.API.Controllers
{
    [Route("api/v1/opportunities")]
    public class OpportunityController: BaseController
    {
        private readonly IOpportunityService _service;

        public OpportunityController(IOpportunityService service)
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
        public IActionResult Save(OpportunityCommand command)
        {
            try
            {
                var opportunity = _service.New(command);
                return WriteResponse(opportunity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
