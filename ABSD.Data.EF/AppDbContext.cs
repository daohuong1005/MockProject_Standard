using ABSD.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABSD.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Premise> Premises { get; set; }
        public DbSet<RoleOrganization> RoleOrganizations { get; set; }
        public DbSet<ServicePremises> ServicePremises { get; set; }
        public DbSet<ServiceOrganization> ServiceOrganizations { get; set; }
        public DbSet<TrustRegion> Regions { get; set; }
        public DbSet<TrustDistrict> Districts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ClientSupport> ClientSupports { get; set; }
        public DbSet<ContractContent> ContractContents { get; set; }
        public DbSet<Criterion> Criterions { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<ServiceAccreditation> ServiceAccreditations { get; set; }
        public DbSet<ServiceClientSupport> ServiceClientSupports { get; set; }
        public DbSet<ServiceCriterionSupport> ServiceCriterionSupports { get; set; }
        public DbSet<ServiceIntervention> ServiceInterventions { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Accreditation> Accreditations { get; set; }
        public DbSet<Funding> Fundings { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ServiceOrganization>().HasKey(x => new { x.OrganizationId, x.ServiceId });
            builder.Entity<ServicePremises>().HasKey(x => new { x.ServiceId, x.PremiseId });
            builder.Entity<RoleOrganization>().HasKey(x => new { x.OrganizationId, x.RoleId });

            builder.Entity<ContractContent>().HasKey(x => new { x.ContentId, x.ContractId });
            builder.Entity<ServiceAccreditation>().HasKey(x => new { x.AccreditationId, x.ServiceId });
            builder.Entity<ServiceClientSupport>().HasKey(x => new { x.ServiceId, x.ClientSupportId });
            builder.Entity<ServiceCriterionSupport>().HasKey(x => new { x.ServiceId, x.CriterionId });
            builder.Entity<ServiceIntervention>().HasKey(x => new { x.ServiceId, x.InterventionId });

            builder.Entity<Service>().HasOne(s => s.Funding).WithMany(f => f.Services).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Service>().HasOne(s => s.Contract).WithOne(c => c.Service).HasForeignKey<Service>(s => s.ContractId);
            builder.Entity<ServiceAccreditation>().HasKey(x => new { x.AccreditationId, x.ServiceId });
            builder.Entity<Service>().HasOne(s => s.Funding).WithMany(f => f.Services).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<int>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<int>>().ToTable("AppClaims")
                   .HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserToken<int>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
        }
    }
}