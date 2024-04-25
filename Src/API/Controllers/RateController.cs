using API.Attributes;
using API.Utils;
using Common.Helpers;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DTO.Models;
using Service.Interfaces.Unit;
using DTO.ViewModel.Admin.Request;
using DTO.ViewModel;
using Ethereum.Contract.Interfaces;
using Nethereum.Hex.HexTypes;

namespace API.Controllers
{
    public class RateController : ControllerBase
    {
        private readonly IServiceUnit _service;
        public RateController(IServiceUnit service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getRate")]
        public async Task<ActionResult<ServiceResult<decimal>>> getRate()
        {
            try
            {
                var result = await _service.Rate.GetRate();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));

            }
        }

        [HttpPost]
        [Route("updatePercentage")]
        public async Task<ActionResult<ServiceResult<string>>> updatePercentage(UpdatePercentageRequest request)
        {
            try
            {
                var result = await _service.Rate.UpdatePercentage(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));

            }
        }

        [HttpPost]
        [Route("UpdateContract")]
        public async Task<ActionResult<ServiceResult<string>>> RateUpdateFunction()
        {
            try
            {
                var result = await _service.Rate.RateUpdateFunction();
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));

            }
        }

    }
}
