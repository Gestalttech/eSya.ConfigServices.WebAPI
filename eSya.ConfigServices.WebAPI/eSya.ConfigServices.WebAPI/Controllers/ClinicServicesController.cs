using eSya.ConfigServices.DL.Repository;
using eSya.ConfigServices.DO;
using eSya.ConfigServices.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigServices.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClinicServicesController : ControllerBase
    {
        private readonly IClinicServicesRepository _clinicServicesRepository;
        public ClinicServicesController(IClinicServicesRepository clinicServicesRepository)
        {
            _clinicServicesRepository = clinicServicesRepository;
        }
        #region Map Clinic Service Consultation
        [HttpGet]
        public async Task<IActionResult> GetClinicbyBusinesskey(int businessKey)
        {
            var keys = await _clinicServicesRepository.GetClinicbyBusinesskey(businessKey);
            return Ok(keys);
        }
        [HttpGet]
        public async Task<IActionResult> GetConsultationbyClinicIdandBusinesskey(int clinicId, int businessKey)
        {
            var clinics = await _clinicServicesRepository.GetConsultationbyClinicIdandBusinesskey(clinicId, businessKey);
            return Ok(clinics);
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveServices()
        {
            var services = await _clinicServicesRepository.GetActiveServices();
            return Ok(services);
        }
        [HttpGet]
        public async Task<IActionResult> GetClinicServiceLinkbyBusinesskey(int businessKey)
        {
            var ac = await _clinicServicesRepository.GetClinicServiceLinkbyBusinesskey(businessKey);
            return Ok(ac);
        }
       
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateClinicServiceLink(DO_MapClinicServiceLink obj)
        {
            var msg = await _clinicServicesRepository.AddOrUpdateClinicServiceLink(obj);
            return Ok(msg);
        }
        #endregion
    }
}
