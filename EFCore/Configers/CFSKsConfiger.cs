using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Configers
{
	public class CFSKsConfiger : IEntityTypeConfiguration<CFSKs>
	{
		public void Configure(EntityTypeBuilder<CFSKs> builder)
		{
			builder.ToTable(nameof(CFSKs));
		}
	}
}
