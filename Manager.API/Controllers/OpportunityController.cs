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
            => ReturnResponse(_service.Get(VendorId), null);

        [HttpPost]
        public IActionResult Save(OpportunityCommand command)
        {
            try
            {
                var opportunity = _service.New(command, VendorId);
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
                var opportunity = _service.Update(command, VendorId);
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
                return ReturnResponse(_service.Delete(id, VendorId), null);
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
                bool opportunity = _service.Cancel(id, VendorId);
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
                bool opportunity = _service.Accept(id, VendorId);
                return ReturnResponse(opportunity, null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(ex);
            }
        }
    }
}
