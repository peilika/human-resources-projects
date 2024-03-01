using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
	public interface ICMKsService
	{
		Task<List<CMKs>> GetAllAsync();
		Task<CMKs> GetByMajorKindId(string majorKindId);
	}
}
