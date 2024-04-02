using eSya.ConfigServices.DL.Entities;
using eSya.ConfigServices.DO;
using eSya.ConfigServices.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigServices.DL.Repository
{
    public class ServiceCodesRepository: IServiceCodesRepository
    {
        private readonly IStringLocalizer<ServiceCodesRepository> _localizer;
        public ServiceCodesRepository(IStringLocalizer<ServiceCodesRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Service Codes
        public async Task<List<DO_ServiceType>> GetServiceTypes()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrties
                                 .Select(x => new DO_ServiceType
                                 {
                                     ServiceTypeId = x.ServiceTypeId,
                                     ServiceTypeDesc = x.ServiceTypeDesc,
                                     PrintSequence = x.PrintSequence,
                                     ActiveStatus = x.ActiveStatus
                                 }
                        ).OrderBy(o => o.PrintSequence).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ServiceGroup>> GetServiceGroups()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrgrs
                                 .Select(x => new DO_ServiceGroup
                                 {
                                     ServiceTypeId = x.ServiceTypeId,
                                     ServiceGroupId = x.ServiceGroupId,
                                     ServiceGroupDesc = x.ServiceGroupDesc,
                                     ServiceCriteria = x.ServiceCriteria,
                                     PrintSequence = x.PrintSequence,
                                     ActiveStatus = x.ActiveStatus
                                 }
                        ).OrderBy(g => g.PrintSequence).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ServiceClass>> GetServiceClasses()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrcls
                                 .Select(x => new DO_ServiceClass
                                 {
                                     ServiceGroupId = x.ServiceGroupId,
                                     ServiceClassId = x.ServiceClassId,
                                     ServiceClassDesc = x.ServiceClassDesc,
                                     ParentId = x.ParentId,
                                     PrintSequence = x.PrintSequence,
                                     ActiveStatus = x.ActiveStatus
                                 }
                        ).OrderBy(g => g.PrintSequence).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ServiceCode>> GetServiceCodes()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrms
                                 .Select(x => new DO_ServiceCode
                                 {
                                     ServiceId = x.ServiceId,
                                     ServiceClassId = x.ServiceClassId,
                                     ServiceFor = x.ServiceFor,
                                     ServiceDesc = x.ServiceDesc,
                                     ServiceShortDesc = x.ServiceShortDesc,
                                     Gender = x.Gender,
                                     InternalServiceCode = x.InternalServiceCode,
                                     ActiveStatus = x.ActiveStatus
                                 }
                        ).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ServiceCode> GetServiceCodeByID(int ServiceID)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrms
                        .Where(i => i.ServiceId == ServiceID)
                                 .Select(x => new DO_ServiceCode
                                 {
                                     ServiceId = x.ServiceId,
                                     ServiceFor = x.ServiceFor,
                                     ServiceDesc = x.ServiceDesc,
                                     ServiceShortDesc = x.ServiceShortDesc,
                                     InternalServiceCode = x.InternalServiceCode,
                                     Gender = x.Gender,
                                     ActiveStatus = x.ActiveStatus,
                                 }
                        ).FirstOrDefaultAsync();

                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> AddOrUpdateServiceCode(DO_ServiceCode obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (obj.ServiceId == 0)
                        {
                            var RecordExist = db.GtEssrms.Where(w => w.ServiceDesc == obj.ServiceDesc || (w.ServiceShortDesc == obj.ServiceShortDesc && w.ServiceShortDesc != null && w.ServiceShortDesc != "")).Count();
                            if (RecordExist > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0106", Message = string.Format(_localizer[name: "W0106"]) };
                            }
                            else
                            {
                                //var internalcode = obj.InternalServiceCode;
                               // var newServiceId = db.GtEssrms.Select(a => (int)a.ServiceId).DefaultIfEmpty().Max() + 1;
                                // If internal service code pattern is defined need to remove and should be unique InternalServiceCode
                                //var pattern = db.GtEssrcgs.Where(w => w.ServiceClassId == obj.ServiceClassId && w.ActiveStatus).FirstOrDefault();
                                //if (pattern != null)
                                //{
                                //    var internalserId = db.GtEssrms.Where(w => w.ServiceClassId == obj.ServiceClassId).Count() + 1;
                                //    string digits = (Math.Pow(10, pattern.IntSccode)).ToString();
                                //    digits = digits + internalserId.ToString();
                                //    digits = digits.Substring(digits.Length - pattern.IntSccode, pattern.IntSccode);
                                //    internalcode = pattern.IntScpattern + digits;
                                //}

                                var newServiceId = db.GtEssrms.Select(a => (int)a.ServiceId).DefaultIfEmpty().Max() + 1;

                                var svclassstatus = db.GtEssrcls.Where(x => x.ServiceClassId == obj.ServiceClassId).FirstOrDefault();
                                if (svclassstatus != null)
                                {
                                    svclassstatus.UsageStatus = true;
                                }
                                await db.SaveChangesAsync();

                               
                                    var InternalServiceCodeExist = db.GtEssrms.Where(w => w.InternalServiceCode == obj.InternalServiceCode).Count();
                                    if (InternalServiceCodeExist > 0)
                                    {
                                        return new DO_ReturnParameter() { Status = false, StatusCode = "W0109", Message = string.Format(_localizer[name: "W0109"]) };
                                    }
                                
                                var servicecode = new GtEssrm
                                {
                                    ServiceId = newServiceId,
                                    ServiceClassId = obj.ServiceClassId,
                                    ServiceFor = obj.ServiceFor,
                                    ServiceDesc = obj.ServiceDesc,
                                    ServiceShortDesc = obj.ServiceShortDesc,
                                    Gender = obj.Gender,
                                    InternalServiceCode = obj.InternalServiceCode,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormId,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = obj.CreatedOn,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtEssrms.Add(servicecode);
                                
                            }
                        }
                        else
                        {
                            var updatedServiceCode = db.GtEssrms.Where(w => w.ServiceId == obj.ServiceId).FirstOrDefault();
                            if (updatedServiceCode.ServiceDesc != obj.ServiceDesc)
                            {
                                var RecordExist = db.GtEssrms.Where(w => w.ServiceDesc == obj.ServiceDesc).Count();
                                if (RecordExist > 0)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0107", Message = string.Format(_localizer[name: "W0107"]) };
                                }
                            }
                            if (updatedServiceCode.ServiceShortDesc != obj.ServiceShortDesc)
                            {
                                var RecordExist = db.GtEssrms.Where(w => w.ServiceShortDesc == obj.ServiceShortDesc).Count();
                                if (RecordExist > 0)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0108", Message = string.Format(_localizer[name: "W0108"]) };
                                }
                            }
                            if (updatedServiceCode.InternalServiceCode != obj.InternalServiceCode)
                            {
                                var RecordExist = db.GtEssrms.Where(w => w.InternalServiceCode == obj.InternalServiceCode).Count();
                                if (RecordExist > 0)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0109", Message = string.Format(_localizer[name: "W0109"]) };
                                }
                            }
                            updatedServiceCode.ServiceFor = obj.ServiceFor;
                            updatedServiceCode.ServiceDesc = obj.ServiceDesc;
                            updatedServiceCode.ServiceShortDesc = obj.ServiceShortDesc;
                            updatedServiceCode.Gender = obj.Gender;
                            updatedServiceCode.InternalServiceCode = obj.InternalServiceCode;
                            updatedServiceCode.ActiveStatus = obj.ActiveStatus;
                            updatedServiceCode.ModifiedBy = obj.UserID;
                            updatedServiceCode.ModifiedOn = obj.CreatedOn;
                            updatedServiceCode.ModifiedTerminal = obj.TerminalID;
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region Map Services to Business 
        public async Task<List<DO_ServiceCode>> GetServiceBusinessLink(int businessKey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                        var sm = await db.GtEssrms.Where(w => w.ActiveStatus == true)
                                         .Select(m => new DO_ServiceCode()
                                         {
                                             BusinessKey = businessKey,
                                             ServiceId = m.ServiceId,
                                             ServiceClassId=m.ServiceClassId,
                                             ServiceDesc=m.ServiceDesc,
                                             ActiveStatus=m.ActiveStatus,
                                             ServiceCost=0,
                                             BusinessLinkStatus=false
                                         }).ToListAsync();


                    foreach (var obj in sm)
                    {
                        GtEssrbl getlocDesc = db.GtEssrbls.Where(c => c.BusinessKey == businessKey && c.ServiceId == obj.ServiceId).FirstOrDefault();
                        if (getlocDesc != null)
                        {
                           
                            obj.BusinessLinkStatus = getlocDesc.ActiveStatus;
                        }
                        else
                        {
                            obj.BusinessLinkStatus = false;
                        }
                    }
                        return sm;
                    
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ServiceBusinessLink> GetBusinessLocationServices(int businessKey,int serviceId)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrbls.Where(w => w.BusinessKey == businessKey && w.ServiceId==serviceId)
                        .Join(db.GtEssrms,
                        b => b.ServiceId,
                        s => s.ServiceId,
                        (b, s) => new { b, s }
                        )
                        .Join(db.GtEssrcls,
                        bs => bs.s.ServiceClassId,
                        c => c.ServiceClassId,
                        (bs, c) => new { bs, c })
                                 .Select(x => new DO_ServiceBusinessLink
                                 {
                                     ServiceId = x.bs.b.ServiceId,
                                     ServiceDesc = x.bs.s.ServiceDesc,
                                     ServiceClassDesc = x.c.ServiceClassDesc,
                                     ServiceCost = x.bs.b.ServiceCost,
                                     ActiveStatus = x.bs.b.ActiveStatus,
                                     l_ServiceParameter =db.GtEspasms.Where(x=>x.BusinessKey==businessKey && x.ServiceId==serviceId)
                                    .Select(p => new DO_eSyaParameter
                                     {
                                         ParameterID = p.ParameterId,
                                         ParmAction = p.ParmAction,
                                         ParmPerc = p.ParmPerc,
                                         ParmValue = p.ParmValue,
                                     }).ToList()
                                 }
                        ).FirstOrDefaultAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> AddOrUpdateBusinessLocationServices(DO_ServiceBusinessLink obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                       
                            var SerExist = db.GtEssrbls.Where(w => w.ServiceId == obj.ServiceId && w.BusinessKey == obj.BusinessKey).FirstOrDefault();
                        if (SerExist != null)
                        {
                            SerExist.ServiceCost = obj.ServiceCost;
                            SerExist.ActiveStatus = obj.ActiveStatus;
                            SerExist.ModifiedBy = obj.UserID;
                            SerExist.ModifiedOn = obj.CreatedOn;
                            SerExist.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();

                            foreach (DO_eSyaParameter sp in obj.l_ServiceParameter)
                            {
                                var sPar = db.GtEspasms.Where(x => x.ServiceId == obj.ServiceId && x.ParameterId == sp.ParameterID && x.BusinessKey == obj.BusinessKey).FirstOrDefault();
                                if (sPar != null)
                                {
                                    sPar.ParmAction = sp.ParmAction;
                                    sPar.ParmPerc = sp.ParmPerc;
                                    sPar.ParmValue = sp.ParmValue;
                                    sPar.ActiveStatus = obj.ActiveStatus;
                                    sPar.ModifiedBy = obj.UserID;
                                    sPar.ModifiedOn = System.DateTime.Now;
                                    sPar.ModifiedTerminal = obj.TerminalID;
                                    await db.SaveChangesAsync();
                                }
                                else
                                {
                                    var sParameter = new GtEspasm
                                    {
                                        BusinessKey = obj.BusinessKey,
                                        ServiceId = obj.ServiceId,
                                        ParameterId = sp.ParameterID,
                                        ParmPerc = sp.ParmPerc,
                                        ParmAction = sp.ParmAction,
                                        ParmValue = sp.ParmValue,
                                        ActiveStatus = sp.ActiveStatus,
                                        FormId = obj.FormId,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,

                                    };
                                    db.GtEspasms.Add(sParameter);
                                    await db.SaveChangesAsync();
                                }
                            }
                        }

                        else
                        {
                            var blink = new GtEssrbl
                            {
                                BusinessKey = obj.BusinessKey,
                                ServiceId = obj.ServiceId,
                                ServiceCost = obj.ServiceCost,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormId,
                                CreatedBy = obj.UserID,
                                CreatedOn = obj.CreatedOn,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEssrbls.Add(blink);
                            await db.SaveChangesAsync();
                            foreach (DO_eSyaParameter sp in obj.l_ServiceParameter)
                            {
                                var sPar = db.GtEspasms.Where(x => x.ServiceId == obj.ServiceId && x.ParameterId == sp.ParameterID && x.BusinessKey == obj.BusinessKey).FirstOrDefault();
                                if (sPar != null)
                                {
                                    sPar.ParmAction = sp.ParmAction;
                                    sPar.ParmPerc = sp.ParmPerc;
                                    sPar.ParmValue = sp.ParmValue;
                                    sPar.ActiveStatus = obj.ActiveStatus;
                                    sPar.ModifiedBy = obj.UserID;
                                    sPar.ModifiedOn = System.DateTime.Now;
                                    sPar.ModifiedTerminal = obj.TerminalID;
                                    await db.SaveChangesAsync();
                                }
                                else
                                {
                                    var sParameter = new GtEspasm
                                    {
                                        BusinessKey = obj.BusinessKey,
                                        ServiceId = obj.ServiceId,
                                        ParameterId = sp.ParameterID,
                                        ParmPerc = sp.ParmPerc,
                                        ParmAction = sp.ParmAction,
                                        ParmValue = sp.ParmValue,
                                        ActiveStatus = sp.ActiveStatus,
                                        FormId = obj.FormId,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,

                                    };
                                    db.GtEspasms.Add(sParameter);
                                    await db.SaveChangesAsync();
                                }
                            }
                        }
                        
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }

        #endregion

        #region Map Business to Services
        public async Task<List<DO_ServiceBusinessLink>> GetServiceBusinessLocations(int ServiceId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns.Where(w => w.ActiveStatus)
                   .GroupJoin(db.GtEssrbls.Where(w => w.ServiceId == ServiceId),
                    b => b.BusinessKey,
                    l => l.BusinessKey,
                    (b, l) => new { b, l })
                   .SelectMany(z => z.l.DefaultIfEmpty(),
                    (a, s) => new DO_ServiceBusinessLink
                    {
                        ServiceId = ServiceId,
                        BusinessKey = a.b.BusinessKey,
                        LocationDescription = a.b.LocationDescription,
                        ServiceCost = s == null ? 0 : s.ServiceCost,
                        ActiveStatus = s == null ? false : s.ActiveStatus
                    }).ToListAsync();
                    return await bk;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ServiceBusinessLink> GetServiceBusinessLocationParameters(int serviceId,int businessKey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrbls.Where(w => w.BusinessKey == businessKey && w.ServiceId == serviceId)
                        .Join(db.GtEssrms,
                        b => b.ServiceId,
                        s => s.ServiceId,
                        (b, s) => new { b, s }
                        )
                        .Join(db.GtEssrcls,
                        bs => bs.s.ServiceClassId,
                        c => c.ServiceClassId,
                        (bs, c) => new { bs, c })
                                 .Select(x => new DO_ServiceBusinessLink
                                 {
                                     ServiceId = x.bs.b.ServiceId,
                                     ServiceDesc = x.bs.s.ServiceDesc,
                                     ServiceClassDesc = x.c.ServiceClassDesc,
                                     ServiceCost = x.bs.b.ServiceCost,
                                     ActiveStatus = x.bs.b.ActiveStatus,
                                     l_ServiceParameter = db.GtEspasms.Where(x => x.BusinessKey == businessKey && x.ServiceId == serviceId)
                                    .Select(p => new DO_eSyaParameter
                                    {
                                        ParameterID = p.ParameterId,
                                        ParmAction = p.ParmAction,
                                        ParmPerc = p.ParmPerc,
                                        ParmValue = p.ParmValue,
                                    }).ToList()
                                 }
                        ).FirstOrDefaultAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> UpdateServiceBusinessLocations(DO_ServiceBusinessLink obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        var SerExist = db.GtEssrbls.Where(w => w.ServiceId == obj.ServiceId && w.BusinessKey == obj.BusinessKey).FirstOrDefault();
                        if (SerExist != null)
                        {
                            SerExist.ServiceCost = obj.ServiceCost;
                            SerExist.ActiveStatus = obj.ActiveStatus;
                            SerExist.ModifiedBy = obj.UserID;
                            SerExist.ModifiedOn = obj.CreatedOn;
                            SerExist.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();

                            foreach (DO_eSyaParameter sp in obj.l_ServiceParameter)
                            {
                                var sPar = db.GtEspasms.Where(x => x.ServiceId == obj.ServiceId && x.ParameterId == sp.ParameterID && x.BusinessKey == obj.BusinessKey).FirstOrDefault();
                                if (sPar != null)
                                {
                                    sPar.ParmAction = sp.ParmAction;
                                    sPar.ParmPerc = sp.ParmPerc;
                                    sPar.ParmValue = sp.ParmValue;
                                    sPar.ActiveStatus = obj.ActiveStatus;
                                    sPar.ModifiedBy = obj.UserID;
                                    sPar.ModifiedOn = System.DateTime.Now;
                                    sPar.ModifiedTerminal = obj.TerminalID;
                                    await db.SaveChangesAsync();
                                }
                                else
                                {
                                    var sParameter = new GtEspasm
                                    {
                                        BusinessKey = obj.BusinessKey,
                                        ServiceId = obj.ServiceId,
                                        ParameterId = sp.ParameterID,
                                        ParmPerc = sp.ParmPerc,
                                        ParmAction = sp.ParmAction,
                                        ParmValue = sp.ParmValue,
                                        ActiveStatus = sp.ActiveStatus,
                                        FormId = obj.FormId,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,

                                    };
                                    db.GtEspasms.Add(sParameter);
                                    await db.SaveChangesAsync();
                                }
                            }
                        }

                        else
                        {
                            var blink = new GtEssrbl
                            {
                                BusinessKey = obj.BusinessKey,
                                ServiceId = obj.ServiceId,
                                ServiceCost = obj.ServiceCost,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormId,
                                CreatedBy = obj.UserID,
                                CreatedOn = obj.CreatedOn,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEssrbls.Add(blink);
                            await db.SaveChangesAsync();
                            foreach (DO_eSyaParameter sp in obj.l_ServiceParameter)
                            {
                                var sPar = db.GtEspasms.Where(x => x.ServiceId == obj.ServiceId && x.ParameterId == sp.ParameterID && x.BusinessKey == obj.BusinessKey).FirstOrDefault();
                                if (sPar != null)
                                {
                                    sPar.ParmAction = sp.ParmAction;
                                    sPar.ParmPerc = sp.ParmPerc;
                                    sPar.ParmValue = sp.ParmValue;
                                    sPar.ActiveStatus = obj.ActiveStatus;
                                    sPar.ModifiedBy = obj.UserID;
                                    sPar.ModifiedOn = System.DateTime.Now;
                                    sPar.ModifiedTerminal = obj.TerminalID;
                                    await db.SaveChangesAsync();
                                }
                                else
                                {
                                    var sParameter = new GtEspasm
                                    {
                                        BusinessKey = obj.BusinessKey,
                                        ServiceId = obj.ServiceId,
                                        ParameterId = sp.ParameterID,
                                        ParmPerc = sp.ParmPerc,
                                        ParmAction = sp.ParmAction,
                                        ParmValue = sp.ParmValue,
                                        ActiveStatus = sp.ActiveStatus,
                                        FormId = obj.FormId,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,

                                    };
                                    db.GtEspasms.Add(sParameter);
                                    await db.SaveChangesAsync();
                                }
                            }
                        }

                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
        #endregion

        
    }
}
