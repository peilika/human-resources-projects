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
	/// <summary>
	/// 薪酬标准单详细信息表
	/// </summary>
	public class SSDsConfiger : IEntityTypeConfiguration<SSDs>
	{
		public void Configure(EntityTypeBuilder<SSDs> builder)
		{
			builder.ToTable(nameof(SSDs));
		}
	}
}
