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
	public class CFFKsConfiger : IEntityTypeConfiguration<CFFKs>
	{
		public void Configure(EntityTypeBuilder<CFFKs> builder)
		{
			builder.ToTable(nameof(CFFKs));
		}
	}
}
