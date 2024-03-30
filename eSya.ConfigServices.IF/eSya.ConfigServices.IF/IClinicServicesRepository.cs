using eSya.ConfigServices.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigServices.IF
{
    public interface IClinicServicesRepository
    {
        #region Map Clinic Service Consultation
        Task<List<DO_ApplicationCodes>> GetClinicbyBusinesskey(int businessKey);
        Task<List<DO_ApplicationCodes>> GetConsultationbyClinicIdandBusinesskey(int clinicId, int businessKey);
        Task<List<DO_ServiceCode>> GetActiveServices();
        Task<List<DO_MapClinicServiceLink>> GetClinicServiceLinkbyBusinesskey(int businessKey);
        Task<DO_ReturnParameter> AddOrUpdateClinicServiceLink(DO_MapClinicServiceLink obj);
        #endregion
    }
}
