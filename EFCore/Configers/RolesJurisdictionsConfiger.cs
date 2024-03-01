using EFCore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Configers
{
	public class RolesJurisdictionsConfiger : IEntityTypeConfiguration<RolesJurisdictions>
	{
		public void Configure(EntityTypeBuilder<RolesJurisdictions> builder)
		{
			//建表  
			builder.ToTable(nameof(RolesJurisdictions));
		}
	}
}
