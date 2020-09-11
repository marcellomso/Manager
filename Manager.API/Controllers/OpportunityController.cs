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
                return ReturnResponse(ex);
            }
        }

        [HttpPut]
        public IActionResult Save(OpportunityUpdateCommand command)
        {
            try
            {
                var opportunity = _service.Update(command);
                return ReturnResponse(opportunity, null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
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

        [HttpPut]
        [Route("{id:int}/cancel")]
        public IActionResult Cancel(int id)
        {
            try
            {
                bool opportunity = _service.Cancel(id);
                return ReturnResponse(opportunity, null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}/accept")]
        public IActionResult Accept(int id)
        {
            try
            {
                bool opportunity = _service.Accept(id);
                return ReturnResponse(opportunity, null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(ex);
            }
        }
    }
}
