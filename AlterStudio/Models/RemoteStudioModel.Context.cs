    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

namespace AlterStudio.Models
{
    public partial class RemoteStudioEntities : DbContext
    {
        public RemoteStudioEntities()
            : base("name=RemoteStudioEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Curators> Curators { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Services> Services { get; set; }
    }
}
