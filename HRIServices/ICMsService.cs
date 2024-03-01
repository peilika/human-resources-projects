using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface ICMsService
	{
		Task<List<CMs>> GetAllAsync();
		Task<List<CMs>> GetAsync(string majorKindId,string majorId);
		Task<CMs> GetByMajorId(string majorId, string majorKinfId);
	}
}
