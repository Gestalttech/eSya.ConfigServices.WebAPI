﻿using System;
using System.Collections.Generic;

namespace eSya.ConfigServices.DL.Entities
{
    public partial class GtEsspcd
    {
        public GtEsspcd()
        {
            GtEsdos2s = new HashSet<GtEsdos2>();
        }

        public int SpecialtyId { get; set; }
        public string SpecialtyDesc { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string SpecialtyType { get; set; } = null!;
        public string SpecialtyGroup { get; set; } = null!;
        public string? MedicalIcon { get; set; }
        public string? FocusArea { get; set; }
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
