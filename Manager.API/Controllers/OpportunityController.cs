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
            => ReturnResponse(_service.Get(), null);

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
            => ReturnResponse(_service.Get(id), null);

        [HttpPost]
        public IActionResult Save(OpportunityCommand command)
        {
            try
            {
                var opportunity = _service.New(command);
                return ReturnResponse(opportunity, null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(null, ex);
            }
        }

    }
}
