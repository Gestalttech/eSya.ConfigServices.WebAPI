using eSya.ConfigServices.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigServices.IF
{
    public interface IServiceCodesRepository
    {
        
        #region Service Codes
        Task<List<DO_ServiceType>> GetServiceTypes();
        Task<List<DO_ServiceGroup>> GetServiceGroups();
        Task<List<DO_ServiceClass>> GetServiceClasses();
        Task<List<DO_ServiceCode>> GetServiceCodes();
        Task<DO_ServiceCode> GetServiceCodeByID(int ServiceID);
        Task<DO_ReturnParameter> AddOrUpdateServiceCode(DO_ServiceCode obj);
        #endregion

        #region Map Services to Business 
        Task<List<DO_ServiceCode>> GetServiceBusinessLink(int businessKey);
        Task<DO_ServiceBusinessLink> GetBusinessLocationServices(int businessKey, int serviceId);
        Task<DO_ReturnParameter> AddOrUpdateBusinessLocationServices(DO_ServiceBusinessLink obj );
        #endregion

        #region Map Business to Services
        Task<List<DO_ServiceBusinessLink>> GetServiceBusinessLocations(int ServiceId);
        Task<DO_ServiceBusinessLink> GetServiceBusinessLocationParameters(int serviceId, int businessKey);
       Task<DO_ReturnParameter> UpdateServiceBusinessLocations(DO_ServiceBusinessLink obj);
        #endregion
    }
}
