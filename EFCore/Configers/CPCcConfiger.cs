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
	public class CPCcConfiger : IEntityTypeConfiguration<CPCs>
	{
		public void Configure(EntityTypeBuilder<CPCs> builder)
		{
			//建表  
			builder.ToTable(nameof(CPCs));
		}
	}
}
