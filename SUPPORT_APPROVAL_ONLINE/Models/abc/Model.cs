namespace SUPPORT_APPROVAL_ONLINE.Models.abc
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<tbl_Customer> tbl_Customer { get; set; }
        public virtual DbSet<tbl_Group> tbl_Group { get; set; }
        public virtual DbSet<tbl_Permission> tbl_Permission { get; set; }
        public virtual DbSet<tbl_Request> tbl_Request { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Customer>()
                .HasMany(e => e.tbl_Request)
                .WithRequired(e => e.tbl_Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Permission>()
                .Property(e => e.allow)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Request>()
                .Property(e => e.currentError)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Request>()
                .Property(e => e.afterError)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Request>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Request>()
                .Property(e => e.pcb)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.U_username)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.U_password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.U_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.U_email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.U_stamp)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Request)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.U_Id_Create)
                .WillCascadeOnDelete(false);
        }
    }
}
