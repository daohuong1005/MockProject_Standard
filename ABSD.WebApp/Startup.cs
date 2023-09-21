using ABSD.Application.Implements;
using ABSD.Application.Interfaces;
using ABSD.Application.Mapper;
using ABSD.Data;
using ABSD.Data.EF;
using ABSD.Data.EF.Repositories;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ABSD.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                            o => o.MigrationsAssembly("ABSD.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // config password and lock when login fail
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.RequireUniqueEmail = true;
            });

            services.AddTransient<DbInitializer>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            //Repositories
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ITrustRegionRepository, TrustRegionRepository>();
            services.AddTransient<ITrustDistrictRepository, TrustDistrictRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IAppRoleRepository, AppRoleRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IPremiseRepository, PremiseRepository>();
            services.AddTransient<IRoleOrganizationRepository, RoleOrganizationRepository>();
            services.AddTransient<IServicePremiseRepository, ServicePremiseRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IParticipationRepository, ParticipationRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IFundingRepository, FundingRepository>(); 
            services.AddTransient<IContentRepository, ContentRepository>();
            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IContractContentRepository, ContractContentRepository>();
            services.AddTransient<IClientSupportRepository, ClientSupportRepository>();
            services.AddTransient<ICriterionRepository, CriterionRepository>();
            services.AddTransient<IInterventionRepository, InterventionRepository>();
            services.AddTransient<IServiceCriterionSupportRepository, ServiceCriterionSupportRepository>();
            services.AddTransient<IServiceClientSupportRepository, ServiceClientSupportRepository>();


            //Services
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IPremiseService, PremiseService>();
            services.AddTransient<IServiceService, ServiceService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ITrustRegionService, TrustRegionService>();
            services.AddTransient<ITrustDistrictService, TrustDistrictService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IServiceTypeService, ServiceTypeService>();
            services.AddTransient<IParticipationService, ParticipationService>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContentService, ContentService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IContractContentService, ContractContentService>();
            services.AddTransient<IClientSupportService, ClientSupportService>();
            services.AddTransient<ICriterionService, CriterionService>();
            services.AddTransient<IInterventionService, InterventionService>();
            services.AddTransient<IServiceClientSupportService, ServiceClientSupportService>();
            services.AddTransient<IServiceCriterionSupportService, ServiceCriterionSupportService>();
            services.AddTransient<IParticipationService,ParticipationService>();
            services.AddTransient<IContactService,ContactService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Service}/{action=Index}/{id?}");
            });
        }
    }
}