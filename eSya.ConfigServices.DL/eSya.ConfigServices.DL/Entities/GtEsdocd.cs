﻿using System;
using System.Collections.Generic;

namespace eSya.ConfigServices.DL.Entities
{
    public partial class GtEsdocd
    {
        public GtEsdocd()
        {
            GtEsdos2s = new HashSet<GtEsdos2>();
        }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public string DoctorShortName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string DoctorRegnNo { get; set; } = null!;
        public int Isdcode { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string? EmailId { get; set; }
        public int DoctorClass { get; set; }
        public int DoctorCategory { get; set; }
        public string TraiffFrom { get; set; } = null!;
        public string? Password { get; set; }
        public int SeniorityLevel { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }

        public virtual ICollection<GtEsdos2> GtEsdos2s { get; set; }
    }
}
