using eSya.ConfigServices.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigServices.IF
{
    public interface ICommonMethodRepository
    {
        Task<List<DO_BusinessLocation>> GetBusinessKey();
        Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeType(int codetype);
    }
}
