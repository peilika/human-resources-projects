using EFCore;
using HRIServices;
using HRServices;

namespace HRUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            {
                builder.Services.AddTransient<IUsersService, UsersService>();
                builder.Services.AddTransient<ICFFKsService, CFFKsService>();
                builder.Services.AddTransient<ISSsService, SSsService>();
                builder.Services.AddTransient<ISSDsService, SSDsService>();
                builder.Services.AddTransient<IHFDsService, HFDsService>();
            }
			builder.Services.AddControllersWithViews();
			{
                builder.Services.AddTransient<ICFSKsService, CFSKsService>();
                builder.Services.AddTransient<ICMKsService, CMKsService>();
                builder.Services.AddTransient<ICMsService, CMsService>();
                builder.Services.AddTransient<ICFTKsService, CFTKsService>();
            }
			builder.Services.AddControllersWithViews();
			{
				builder.Services.AddTransient<IPowersService, PowersService>();
				builder.Services.AddTransient<ICPCsService, CPCsService>();
				builder.Services.AddTransient<IPYsService, PYsService>();
				builder.Services.AddTransient<IHumanResourcesService, HumanResourcesService>();
				builder.Services.AddTransient<IClientsService, ClientsService>();
                builder.Services.AddTransient<ISGService, SGservice>();
            }
			builder.Services.AddControllersWithViews();
            {
                builder.Services.AddTransient<MyDbContext>();
            }
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            {
                //∆Ù”√∑÷≤º Ωª∫¥Ê
                builder.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = "127.0.0.1";
                    options.InstanceName = "cache_";//±‹√‚ªÏ¬“
                });
            }

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Shared/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Users}/{action=Login}/{id?}");

            app.Run();
        }
    }
}