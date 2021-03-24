using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KidAdvisor.Models;
using KidAdvisor.Services;

namespace KidAdvisor.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class BusinessesController : ControllerBase
    {

        private readonly ILogger<BusinessesController> _logger;
        private readonly IBusinessService _businessService;

        public BusinessesController(ILogger<BusinessesController> logger, IBusinessService businessService)
        {
            _logger = logger;
            _businessService = businessService;
        }

        [HttpGet]
        public IEnumerable<BusinessModel> Get()
        {
            var result = _businessService.GetBusinesses();
            return result;
        }

        [HttpGet("{id}")]
        public BusinessModel Get(Guid id)
        {
            var result = _businessService.GetBusiness(id);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] BusinessModel businessModel)
        {
            try
            {
                var result = this._businessService.UpdateBusiness(businessModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] BusinessModel businessModel)
        {
            try
            {
                businessModel.OwnerId = new Guid("9F1C7846-19C5-42E7-B2C6-265934A42214");
                var result = this._businessService.InsertBusiness(businessModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this._businessService.DeleteBusiness(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
