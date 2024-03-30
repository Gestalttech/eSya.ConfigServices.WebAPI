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
    public class ClinicServicesRepository: IClinicServicesRepository
    {
        private readonly IStringLocalizer<ClinicServicesRepository> _localizer;
        public ClinicServicesRepository(IStringLocalizer<ClinicServicesRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Map Clinic Service Consultation
        public async Task<List<DO_ApplicationCodes>> GetClinicbyBusinesskey(int businessKey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result =await db.GtEsopcls.Where(w => w.BusinessKey == businessKey && w.ActiveStatus)
                   .Join(db.GtEcapcds,
                    b => b.ClinicId,
                    l => l.ApplicationCode,
                    (b, l) => new { b, l })
                    .Select(x=> new DO_ApplicationCodes
                    {
                        ApplicationCode = x.b.ClinicId,
                        CodeDesc = x.l.CodeDesc,
                    }).OrderBy(x => x.CodeDesc).ToListAsync();
                    var Distinctclinics = result.GroupBy(y=>y.ApplicationCode).Select(y => y.First());
                    return  Distinctclinics.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ApplicationCodes>> GetConsultationbyClinicIdandBusinesskey(int clinicId,int businessKey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = await db.GtEsopcls.Where(w => w.BusinessKey == businessKey && w.ClinicId==clinicId && w.ActiveStatus)
                   .Join(db.GtEcapcds,
                    b => b.ConsultationId,
                    l => l.ApplicationCode,
                    (b, l) => new { b, l })
                    .Select(x => new DO_ApplicationCodes
                    {
                        ApplicationCode = x.b.ConsultationId,
                        CodeDesc = x.l.CodeDesc,
                    }).OrderBy(x => x.CodeDesc).ToListAsync();
                    var Distinctclinics = result.GroupBy(y => y.ApplicationCode).Select(y => y.First());
                    return Distinctclinics.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ServiceCode>> GetActiveServices()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEssrms.Where(x=>x.ActiveStatus)
                        .Select(r => new DO_ServiceCode
                        {
                            ServiceId = r.ServiceId,
                            ServiceDesc = r.ServiceDesc,
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_MapClinicServiceLink>> GetClinicServiceLinkbyBusinesskey(int businessKey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result =await db.GtEsclsls.Where(w => w.BusinessKey == businessKey)
                        .Join(db.GtEssrms,
                        b => b.ServiceId,
                        s => s.ServiceId,
                        (b, s) => new { b, s }
                        )
                        .Join(db.GtEcapcds,
                        bs => bs.b.ClinicId,
                        c => c.ApplicationCode,
                        (bs, c) => new { bs, c })
                        .Join(db.GtEcapcds,
                        bss => bss.bs.b.ConsultationId,
                        cs => cs.ApplicationCode,
                        (bss, cs) => new { bss, cs })
                        .Select(x => new DO_MapClinicServiceLink
                         {
                            BusinessKey=x.bss.bs.b.BusinessKey,
                            ClinicId = x.bss.bs.b.ClinicId,
                            ConsultationId = x.bss.bs.b.ConsultationId,
                            ServiceId = x.bss.bs.b.ServiceId,
                            VisitRule = x.bss.bs.b.VisitRule,
                            ActiveStatus = x.bss.bs.b.ActiveStatus,
                            ServiceDesc=x.bss.bs.s.ServiceDesc,
                            ClinicDesc=x.bss.c.CodeDesc,
                            ConsultationDesc=x.cs.CodeDesc
                        }).ToListAsync();

                    return  result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> AddOrUpdateClinicServiceLink(DO_MapClinicServiceLink obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        var linkexists = db.GtEsclsls.Where(w => w.BusinessKey == obj.BusinessKey && w.ServiceId == obj.ServiceId
                                   && w.ClinicId == obj.ClinicId && w.ConsultationId == obj.ConsultationId).FirstOrDefault();
                        if (linkexists != null)
                        {
                            linkexists.VisitRule = obj.VisitRule;
                            linkexists.ActiveStatus = obj.ActiveStatus;
                            linkexists.ModifiedBy = obj.UserID;
                            linkexists.ModifiedOn = DateTime.Now;
                            linkexists.ModifiedTerminal = obj.TerminalID;
                        }
                        else
                        {
                            var link = new GtEsclsl
                            {
                               BusinessKey=obj.BusinessKey,
                               ClinicId=obj.ClinicId,
                               ConsultationId=obj.ConsultationId,
                               ServiceId=obj.ServiceId,
                               VisitRule=obj.VisitRule,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormId,
                                CreatedBy = obj.UserID,
                                CreatedOn = DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEsclsls.Add(link);
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
    }
}
