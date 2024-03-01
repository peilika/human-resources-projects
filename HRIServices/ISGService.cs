using EFCore.Models;
using HRUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
    public interface ISGService
    {
		Task<FenYeT<SG>> GetByFenYeByCheckStatusIsFalseAsync(FenYeT<SG> fenYeT);
		Task<List<SGD2>> sb(string n);
		Task<List<SSDs>> sb1(string n);
	}
}
