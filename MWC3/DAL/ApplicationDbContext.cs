namespace MWC3.DAL
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Microsoft.AspNet.Identity.EntityFramework;

    using MWC3.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<Grape> Grapes { get; set; }
        public DbSet<GrapeColor> GrapeColors { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LanguageInfo> LanguageInfo { get; set; }
        public DbSet<BottleType> BottleTypes { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<WineColor> WineColors { get; set; }
        public DbSet<Wine> Wines { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewType> ReviewTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Qualification>()
                .HasOptional(q => q.Region)
                .WithMany()
                .HasForeignKey(x => x.RegionId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Wine>()
                .HasRequired(q => q.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Wine>()
                .HasOptional(q => q.Qualification)
                .WithMany()
                .HasForeignKey(x => x.QualificationId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .HasOptional(q => q.Business)
                .WithMany()
                .HasForeignKey(x => x.BusinessId).WillCascadeOnDelete(false);

        }




    }
}