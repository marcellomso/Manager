﻿using Manager.Domain.Commands.VendoresCommands;
using Manager.Domain.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Manager.API.Controllers
{
    [Route("api/v1/vendores")]
    public class VendorController : BaseController
    {
        private readonly IVendorService _service;

        public VendorController(IVendorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
            => ReturnResponse(_service.Get(), null);

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public IActionResult Get(int id)
            => ReturnResponse(_service.Get(id), null);

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Save(VendorCommand command)
        {
            try
            {
                var vendor = _service.New(command);
                return ReturnResponse(vendor, null);
            }
            catch (Exception ex)
            {
                return ReturnResponse(ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Save(VendorUpdateCommand command)
        {
            try
            {
                var vendor = _service.Update(command);
                return ReturnResponse(vendor, null);
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
