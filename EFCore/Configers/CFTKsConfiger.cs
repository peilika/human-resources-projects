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
    public class CFTKsConfiger : IEntityTypeConfiguration<CFTKs>
    {
        public void Configure(EntityTypeBuilder<CFTKs> builder)
        {
            builder.ToTable(nameof(CFTKs));
        }
    }
}
