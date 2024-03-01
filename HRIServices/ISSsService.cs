using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface ISSsService
	{
		Task<List<SSs>> GetAll();
		Task<SSs> GetName(string name);
		Task<SSs> GetID(int id);
		Task<bool> AddSSsAsync(SSs sSs, List<SSDs> sSDs);
		Task<SSs> GetByStandardId(string standardId);
		Task<FenYeT<SSs>> GetByFenYeAsync(FenYeT<SSs> fenYeT);
		Task<bool> UpdateSSsAsync(SSs sSs, List<SSDs> sSDs);
		Task<FenYeT<SSs>> GetByFenYeByWhereAsync(FenYeT<SSs> fenYeT, string condition);
		Task<FenYeT<SSs>> GetByFenYeByCheckStatusIsFalseAsync(FenYeT<SSs> fenYeT);
		Task<SSs> GetSID(string sName);
	}
}
