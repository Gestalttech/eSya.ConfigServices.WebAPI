using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigServices.DO
{
    public class DO_MapClinicServiceLink
    {
        public int BusinessKey { get; set; }
        public int ClinicId { get; set; }
        public int ConsultationId { get; set; }
        public int ServiceId { get; set; }
        public int VisitRule { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string? ClinicDesc { get; set; }
        public string? ConsultationDesc { get; set; }
        public string? ServiceDesc { get; set; }
    }
}
