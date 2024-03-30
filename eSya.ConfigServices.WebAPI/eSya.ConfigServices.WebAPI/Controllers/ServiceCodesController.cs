using eSya.ConfigServices.DO;
using eSya.ConfigServices.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigServices.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceCodesController : ControllerBase
    {
        private readonly IServiceCodesRepository _serviceCodesRepository;
        public ServiceCodesController(IServiceCodesRepository serviceCodesRepository)
        {
            _serviceCodesRepository = serviceCodesRepository;
        }

        #region Service Codes
        [HttpGet]
        public async Task<IActionResult> GetServiceTypes()
        {
            var ac = await _serviceCodesRepository.GetServiceTypes();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceGroups()
        {
            var ac = await _serviceCodesRepository.GetServiceGroups();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceClasses()
        {
            var ac = await _serviceCodesRepository.GetServiceClasses();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceCodes()
        {
            var ac = await _serviceCodesRepository.GetServiceCodes();
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceCodeByID(int ServiceID)
        {
            var ac = await _serviceCodesRepository.GetServiceCodeByID(ServiceID);
            return Ok(ac);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateServiceCode(DO_ServiceCode obj)
        {
            var msg = await _serviceCodesRepository.AddOrUpdateServiceCode(obj);
            return Ok(msg);
        }
        #endregion

        #region Map Services to Business 
        [HttpGet]
        public async Task<IActionResult> GetServiceBusinessLink(int businessKey)
        {
            var ac = await _serviceCodesRepository.GetServiceBusinessLink(businessKey);
            return Ok(ac);
        }

        [HttpGet]
        public async Task<IActionResult> GetBusinessLocationServices(int businessKey, int serviceId)
        {
            var ac = await _serviceCodesRepository.GetBusinessLocationServices(businessKey, serviceId);
            return Ok(ac);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateBusinessLocationServices(DO_ServiceBusinessLink obj)
        {
            var msg = await _serviceCodesRepository.AddOrUpdateBusinessLocationServices(obj);
            return Ok(msg);
        }
        #endregion

        #region Map Business to Services

        [HttpGet]
        public async Task<IActionResult> GetServiceBusinessLocations(int ServiceId)
        {
            var ac = await _serviceCodesRepository.GetServiceBusinessLocations(ServiceId);
            return Ok(ac);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceBusinessLocationParameters(int serviceId, int businessKey)
        {
            var ac = await _serviceCodesRepository.GetServiceBusinessLocationParameters(serviceId, businessKey);
            return Ok(ac);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateServiceBusinessLocations(DO_ServiceBusinessLink obj)
        {
            var msg = await _serviceCodesRepository.UpdateServiceBusinessLocations(obj);
            return Ok(msg);
        }
        #endregion
    }
}
