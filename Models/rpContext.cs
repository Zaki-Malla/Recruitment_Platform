using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Platform.Models
{
    public class rpContext : IdentityDbContext<UserModel, RoleModel, int>
    {
        private readonly IConfiguration _configuration;

        public rpContext() { }

        public rpContext(DbContextOptions<rpContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<UserModel> TbUser { get; set; }
        public virtual DbSet<RoleModel> TbRole { get; set; }
        public virtual DbSet<NotificationModel> TbNotifications { get; set; }
        public virtual DbSet<OffersForOpportunitiesModel> TbOffersForOpportunities { get; set; }
        public virtual DbSet<OpportunitiesOfferedModel> TbOpportunitiesOffered { get; set; }
        public virtual DbSet<UserInformationModel> TbUserInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // العلاقة بين UserModel و UserInformationModel (واحد لواحد)
            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.Information)
                .WithOne(i => i.User)
                .HasForeignKey<UserInformationModel>(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade); // عند حذف المستخدم، يتم حذف معلوماته

            // العلاقة بين UserModel و OpportunitiesOfferedModel (واحد إلى متعدد)
            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Opportunities)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.PublisherId)
                .OnDelete(DeleteBehavior.Restrict); // منع الحذف التتابعي لتجنب مسارات الحذف المتعددة

            // العلاقة بين UserModel و OffersForOpportunitiesModel (واحد إلى متعدد)
            modelBuilder.Entity<OffersForOpportunitiesModel>()
                .HasOne(of => of.User)
                .WithMany(u => u.Offers)
                .HasForeignKey(of => of.UserId)
                .OnDelete(DeleteBehavior.Restrict); // منع الحذف التتابعي

            // العلاقة بين UserModel و NotificationModel (واحد إلى متعدد)
            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade); // حذف الإشعارات عند حذف المستخدم

            // العلاقة بين OpportunitiesOfferedModel و OffersForOpportunitiesModel (واحد إلى متعدد)
            modelBuilder.Entity<OffersForOpportunitiesModel>()
                .HasOne(of => of.Opportunity)
                .WithMany(o => o.Offers)
                .HasForeignKey(of => of.OpportunityId)
                .OnDelete(DeleteBehavior.Restrict); // منع الحذف التتابعي

            // العلاقة بين OffersForOpportunitiesModel و UserModel
            modelBuilder.Entity<OffersForOpportunitiesModel>()
                .HasOne(of => of.User)
                .WithMany(u => u.Offers)
                .HasForeignKey(of => of.UserId)
                .OnDelete(DeleteBehavior.Cascade); // حذف العرض عند حذف المستخدم

            // العلاقة بين OpportunitiesOfferedModel و UserModel (واحد لواحد)
            modelBuilder.Entity<OpportunitiesOfferedModel>()
                .HasOne(o => o.User)
                .WithMany(u => u.Opportunities)
                .HasForeignKey(o => o.PublisherId)
                .OnDelete(DeleteBehavior.Restrict); // منع الحذف التتابعي لتجنب مسارات الحذف المتعددة
        }


    }

}

