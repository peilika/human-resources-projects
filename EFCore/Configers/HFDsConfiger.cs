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
	public class HFDsConfiger : IEntityTypeConfiguration<HFDs>
	{
		public void Configure(EntityTypeBuilder<HFDs> builder)
		{
			//建表  
			builder.ToTable(nameof(HFDs));
		}
	}
}
