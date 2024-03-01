using EFCore.Configers;
using EFCore.Models;
using HREF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class MyDbContext : DbContext
    {
        /// <summary>
        /// 配置连接字符串
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // 在这里配置数据库提供程序
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HR_DB;Integrated Security=True;Encrypt=False;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfiguration(new JurisdictionConfiger());

		}

		public DbSet<Users> Users {  get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<CFFKs> CFFKs { get; set; }
        public DbSet<CPCs> CPCs { get; set; }
        public DbSet<CMKs> CMKs { get; set; }
        public DbSet<CMs> CMs { get; set; }
        public DbSet<SSs> SSses { get; set; }
        public DbSet<SSDs> SSDs { get; set; }
        public DbSet<CFSKs> CFSKs { get; set;}
        public DbSet<CFTKs> CFTKs { get; set;}
        public DbSet<EMRs> EMRs { get; set; }
        public DbSet<EEs> EEs { get; set; }
        public DbSet<PYs> PYs { get; set; }
        public DbSet<HFDs> HFDs { get; set; }
        public DbSet<HFs> HFs { get; set; }
        public DbSet<SG> sGs { get; set; }
        public DbSet<SGD2> SGD2 { get; set; }
		public DbSet<salary_standard> salary_standard { get; set; }
		public DbSet<salary_standard_detail> salary_standard_detail { get; set; }
		public DbSet<salary_grant> salary_grant { get; set; }
		public DbSet<salary_grant_details> salary_grant_details { get; set; }

        public DbSet<Jurisdictions> Jurisdictions { get; set;}
        public DbSet<RolesJurisdictions> RolesJurisdictions { get; set; }
        public DbSet<ERs> ERs { get; set; }
	}
}
