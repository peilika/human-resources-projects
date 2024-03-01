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
	/// 薪酬标准基本信息表
	/// </summary>
	public class SSsConfiger : IEntityTypeConfiguration<SSs>
	{
		public void Configure(EntityTypeBuilder<SSs> builder)
		{
			builder.ToTable(nameof(SSs));
		}
	}
}
