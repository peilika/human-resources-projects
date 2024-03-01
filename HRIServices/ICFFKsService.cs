using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface ICFFKsService
	{
		Task<List<CFFKs>> GetAllAsync();
		Task<CFFKs> GetByFfkIdAsync(int ffkId);
		Task<bool> AddAsync(CFFKs cFFKs);
		Task<bool> UpdateAsync(CFFKs cFFKs);
		Task<bool> DeleteAsync(int ffkId);
		Task<CFFKs> GetByFirstKindIdAsync(string firstKindId);
	}
}
