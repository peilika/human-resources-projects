using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface ICFSKsService
	{
		Task<List<CFSKs>> GetAllAsync();
		Task<bool> AddAsync(CFSKs cFSKs);
		Task<CFSKs> GetBySecondKindIdAsync(string secondKindId);
		Task<bool> UpdateAsync(CFSKs cFSKs);
		Task<CFSKs> GetByFskIdAsync(int fskId); 
		Task<bool> DeleteAsync(int fskId);
		Task<List<CFSKs>> GetCFSKsByFFKIdAsync(string firstKindId);
	}
}
