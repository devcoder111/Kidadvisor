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
    public class BusinessController : ControllerBase
    {

        private readonly ILogger<BusinessController> _logger;
        private readonly IBusinessService _businessService;

        public BusinessController(ILogger<BusinessController> logger, IBusinessService businessService)
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
        public BusinessModel GetBusiness(Guid id)
        {
            var result = _businessService.GetBusiness(id);
            return result;
        }

        [HttpPut]
        public IActionResult UpdateBusiness([FromBody] BusinessModel businessModel)
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
        public IActionResult CreateBusiness([FromBody] BusinessModel businessModel)
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
        public IActionResult DeleteBusiness(Guid id)
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
