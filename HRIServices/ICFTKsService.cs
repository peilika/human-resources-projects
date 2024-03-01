using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
    public interface ICFTKsService
    {
        Task<List<CFTKs>> GetAllAsync();
        Task<bool> AddAsync(CFTKs cFTKs);
        Task<CFTKs> GetByFtkId(int ftkId);
        Task<bool> UpdateAsync(CFTKs cFTKs);
        Task<bool> DeleteAsync(int ftkId);
		Task<List<CFTKs>> GetCFSKsByFFKIdAsync(string firstKindId,string secondKindId,string thridKindId);
        Task<CFTKs> GetByThirdKindId(string ThirdKindId);
	}
}
