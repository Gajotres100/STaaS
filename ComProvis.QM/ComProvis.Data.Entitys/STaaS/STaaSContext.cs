using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ComProvis.Data.Entitys.STaaS
{
    public class STaaSContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public STaaSContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DiskSpace> DiskSpaces { get; set; }
        public DbSet<DiskSpaceLifeCycle> DiskSpaceLifeCycles { get; set; }
        public DbSet<DiskSpaceUsers> DiskSpaceUserses { get; set; }
        public DbSet<DiskSizeUnits> DiskSizeUnitses { get; set; }
        public DbSet<DiskSpaceState> DiskSpaceStates { get; set; }
        public DbSet<DiskUnitRolePermission> DiskUnitRolePermissions { get; set; }
        public DbSet<DiskUnits> DiskUnitses { get; set; }
        public DbSet<DiskUnitShare> DiskUnitShares { get; set; }
        public DbSet<DiskUnitShareGroup> DiskUnitShareGroups { get; set; }
        public DbSet<DiskUnitType> DiskUnitTypes { get; set; }
        public DbSet<DiskUnitUserPermission> DiskUnitUserPermissions { get; set; }
        public DbSet<Products> Productses { get; set; }
        public DbSet<ProductsHistory> ProductsHistorys { get; set; }
        public DbSet<SpGetDiskSpaceInfo> SpGetDiskSpaceInfos { get; set; }
        public DbSet<Roles> Roles { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MkpConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable(nameof(Company), "STaaS");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.ContactEmail).HasMaxLength(255);

                entity.Property(e => e.ContactFirstName).HasMaxLength(255);

                entity.Property(e => e.ContactLastName).HasMaxLength(255);

                entity.Property(e => e.ExternalId)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DiskUnits>(entity =>
            {
                entity.HasKey(e => e.DiskUnitId);

                entity.ToTable(nameof(DiskUnits), "STaaS");

                entity.Property(e => e.Extension).HasMaxLength(260);

                entity.Property(e => e.FullName)
                    .HasMaxLength(521);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(260);


                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.DiskUnitParent)
                    .WithMany(p => p.InverseDiskUnitParent)
                    .HasForeignKey(d => d.DiskUnitParentId);


                entity.HasOne(d => d.DiskUnitType)
                    .WithMany(p => p.DiskUnits)
                    .HasForeignKey(d => d.DiskUnitTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<DiskSpace>(entity =>
            {  
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.DiskSpaces)
                    .HasForeignKey(d => d.CompanyId);
            });

            modelBuilder.Entity<DiskSizeUnits>(entity =>
            {
                entity.ToTable(nameof(DiskSizeUnits), "STaaS.Codebook");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DiskUnitShareGroup>(entity =>
            {
                entity.HasKey(e => e.ShareGroupId);

                entity.ToTable(nameof(DiskUnitShareGroup), "STaaS");

                entity.Property(e => e.ShareName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.ShareWith).IsRequired();

                entity.Property(e => e.ShortIdentifier)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.HasOne(d => d.DiskSpace)
                    .WithMany(p => p.DiskUnitShareGroup)
                    .HasForeignKey(d => d.DiskSpaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiskUnitShareGroup_DiskSpace");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("Products", "STaaS.Codebook");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NumOfUsers).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.DiskSizeUnit)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DiskSizeUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductsHistory>(entity =>
            {
                entity.HasKey(e => e.RowId);

                entity.ToTable("ProductsHistory", "STaaS.Codebook");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasColumnType("char(1)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsHistory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<DiskUnitUserPermission>(entity =>
            {
                entity.ToTable("DiskUnitUserPermission", "STaaS");

                entity.HasOne(d => d.DiskUnit)
                    .WithMany(p => p.DiskUnitUserPermission)
                    .HasForeignKey(d => d.DiskUnitId)
                    .HasConstraintName("FK_DiskUnitUserPermission_DiskUnits");
            });

            modelBuilder.Entity<DiskSpaceLifeCycle>(entity =>
            {
                entity.ToTable("DiskSpaceLifeCycle", "STaaS");

                entity.HasOne(d => d.DiskSpace)
                    .WithMany(p => p.DiskSpaceLifeCycle)
                    .HasForeignKey(d => d.DiskSpaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiskSpaceLifeCycle_DiskSpace");

                entity.HasOne(d => d.DiskSpaceState)
                    .WithMany(p => p.DiskSpaceLifeCycle)
                    .HasForeignKey(d => d.DiskSpaceStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiskSpaceLifeCycle_DiskSpaceState");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DiskSpaceLifeCycle)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiskSpaceLifeCycle_Products");
            });

            modelBuilder.Entity<DiskSpaceUsers>(entity =>
            {
                entity.ToTable("DiskSpaceUsers", "STaaS");

                entity.HasOne(d => d.DiskSpace)
                    .WithMany(p => p.DiskSpaceUsers)
                    .HasForeignKey(d => d.DiskSpaceId)
                    .HasConstraintName("FK_DiskSpaceUsers_DiskSpace");
            });

            modelBuilder.Entity<DiskSpaceState>(entity =>
            {
                entity.ToTable("DiskSpaceState", "STaaS.Codebook");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DiskUnitRolePermission>(entity =>
            {
                entity.ToTable("DiskUnitRolePermission", "STaaS");

                entity.HasOne(d => d.DiskUnit)
                    .WithMany(p => p.DiskUnitRolePermission)
                    .HasForeignKey(d => d.DiskUnitId)
                    .HasConstraintName("FK_DiskUnitRolePermission_DiskUnits");
            });

            modelBuilder.Entity<DiskUnitShare>(entity =>
            {
                entity.ToTable("DiskUnitShare", "STaaS");

                entity.HasOne(d => d.DiskUnit)
                    .WithMany(p => p.DiskUnitShare)
                    .HasForeignKey(d => d.DiskUnitId)
                    .HasConstraintName("FK_DiskUnitShare_DiskUnits");

                entity.HasOne(d => d.ShareGroup)
                    .WithMany(p => p.DiskUnitShare)
                    .HasForeignKey(d => d.ShareGroupId)
                    .HasConstraintName("FK_DiskUnitShare_DiskUnitShareGroup");
            });

            modelBuilder.Entity<DiskUnitType>(entity =>
            {
                entity.ToTable("DiskUnitType", "STaaS.Codebook");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
