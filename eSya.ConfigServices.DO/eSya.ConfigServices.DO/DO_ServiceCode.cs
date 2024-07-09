using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigServices.DO
{
    public class DO_ServiceCode
    {
        public int ServiceId { get; set; }
        public int ServiceClassId { get; set; }
        public int ServiceFor { get; set; }
        public string ServiceDesc { get; set; } = null!;
        public string? ServiceShortDesc { get; set; }
        public string Gender { get; set; } = null!;
        public string? InternalServiceCode { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TerminalID { get; set; }
        public string? ServiceTypeDesc { get; set; }
        public string? ServiceGroupDesc { get; set; }
        public string? ServiceClassDesc { get; set; }
        public bool BusinessLinkStatus { get; set; }
        //public List<DO_eSyaParameter> l_ServiceParameter { get; set; }
        public int BusinessKey { get; set; }
        public decimal ServiceCost { get; set; }
    }
    public class DO_ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string ServiceTypeDesc { get; set; }
        public int PrintSequence { get; set; }
        public bool ActiveStatus { get; set; }

    }
    public class DO_ServiceGroup
    {
        public int ServiceGroupId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceGroupDesc { get; set; }
        public int ServiceCriteria { get; set; }
        public int PrintSequence { get; set; }
        public bool ActiveStatus { get; set; }

    }
    public class DO_ServiceClass
    {
        public int ServiceClassId { get; set; }
        public int ServiceGroupId { get; set; }
        public string ServiceClassDesc { get; set; }
        public bool IsBaseRateApplicable { get; set; }
        public int ParentId { get; set; }
        public int PrintSequence { get; set; }
        public bool ActiveStatus { get; set; }

    }
    public class DO_eSyaParameter
    {
        public int ParameterID { get; set; }
        public string? ParameterValue { get; set; }
        public bool ParmAction { get; set; }
        public decimal ParmValue { get; set; }
        public decimal ParmPerc { get; set; }
        public string? ParmDesc { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
