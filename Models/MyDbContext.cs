using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutU> AboutUs { get; set; }
        public virtual DbSet<Categoryy> Categoryys { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Getcountd> Getcountds { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Locationn> Locationns { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<User0> User0s { get; set; }
        public virtual DbSet<Userrole> Userroles { get; set; }
        public virtual DbSet<Visa> Visas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=JOR15_User57;PASSWORD=Wavetik7777;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR15_USER57")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<AboutU>(entity =>
            {
                entity.HasKey(e => e.IdAbout)
                    .HasName("SYS_C00270694");

                entity.ToTable("ABOUT_US");

                entity.HasIndex(e => e.Email, "SYS_C00270695")
                    .IsUnique();

                entity.HasIndex(e => e.Phonenum, "SYS_C00270696")
                    .IsUnique();

                entity.Property(e => e.IdAbout)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_ABOUT");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Phonenum)
                    .HasPrecision(14)
                    .HasColumnName("PHONENUM");
            });

            modelBuilder.Entity<Categoryy>(entity =>
            {
                entity.HasKey(e => e.IdCat)
                    .HasName("SYS_C00270683");

                entity.ToTable("CATEGORYY");

                entity.Property(e => e.IdCat)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_CAT");

                //entity.Property(e => e.CatImg)
                //    .HasMaxLength(200)
                //    .IsUnicode(false)
                //    .HasColumnName("CAT_IMG");

                entity.Property(e => e.CatName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CAT_NAME");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.IdContact)
                    .HasName("SYS_C00270701");

                entity.ToTable("CONTACT");

                entity.Property(e => e.IdContact)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_CONTACT");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("FULL_NAME");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.IdFeed)
                    .HasName("SYS_C00270720");

                entity.ToTable("FEEDBACK");

                entity.Property(e => e.IdFeed)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_FEED");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Feedback1)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("FEEDBACK");

                entity.Property(e => e.Feedid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FEEDID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("FULL_NAME");

                entity.HasOne(d => d.Feed)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.Feedid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FEEDID");
            });

            modelBuilder.Entity<Getcountd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GETCOUNTD");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Salary)
                    .HasColumnType("NUMBER(38,1)")
                    .HasColumnName("SALARY");
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.HasKey(e => e.IdHall)
                    .HasName("SYS_C00270690");

                entity.ToTable("HALL");

                entity.Property(e => e.IdHall)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_HALL");

                entity.Property(e => e.Catid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATID");

                entity.Property(e => e.HallDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("HALL_DESC");

                entity.Property(e => e.HallName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HALL_NAME");

                entity.Property(e => e.Img1)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMG1");

                entity.Property(e => e.Img2)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMG2");

                entity.Property(e => e.Img3)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMG3");

                //entity.Property(e => e.Price)
                //    .HasMaxLength(50)
                //    .IsUnicode(false)
                //    .HasColumnName("PRICE");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Halls)
                    .HasForeignKey(d => d.Catid)
                    .HasConstraintName("FK_CATEG");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.HasKey(e => e.IdHome)
                    .HasName("SYS_C00270706");

                entity.ToTable("HOME");

                entity.Property(e => e.IdHome)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_HOME");

                entity.Property(e => e.Aboutid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ABOUTID");

                entity.Property(e => e.Contactid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CONTACTID");

                entity.Property(e => e.Img1)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMG1");

                entity.Property(e => e.Img2)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMG2");

                entity.Property(e => e.Img3)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMG3");

                entity.HasOne(d => d.About)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.Aboutid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ABOUTID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.Contactid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CONTACTID");
            });

            modelBuilder.Entity<Locationn>(entity =>
            {
                entity.HasKey(e => e.Locationid)
                    .HasName("SYS_C00274633");

                entity.ToTable("LOCATIONN");

                entity.Property(e => e.Locationid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOCATIONID");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION_NAME");

                entity.Property(e => e.Pic)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("PIC");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("SYS_C00270724");

                entity.ToTable("LOGIN");

                entity.HasIndex(e => e.Email, "SYS_C00270725")
                    .IsUnique();

                entity.Property(e => e.IdLog)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_LOG");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Roleeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEEID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Rolee)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Roleeid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ROLEEID_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USERID");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.IdPay)
                    .HasName("SYS_C00243400");

                entity.ToTable("PAYMENT");

                entity.Property(e => e.IdPay)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_PAY");

                entity.Property(e => e.CorCost)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("COR_COST");

                entity.Property(e => e.Pay)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PAY");

                entity.Property(e => e.PayDate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAY_DATE");

                entity.Property(e => e.StdPay)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STD_PAY");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("SYS_C00270738");

                entity.ToTable("RESERVATION");

                entity.Property(e => e.IdBook)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_BOOK");

                entity.Property(e => e.EndEvent)
                    .HasPrecision(6)
                    .HasColumnName("END_EVENT");

                entity.Property(e => e.Hallid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("HALLID");

                entity.Property(e => e.Payid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PAYID");

                entity.Property(e => e.StartEvent)
                    .HasPrecision(6)
                    .HasColumnName("START_EVENT");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Hallid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_HALLID");

                entity.HasOne(d => d.Pay)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Payid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PAYMID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USER_BOOKINGID");
            });

            modelBuilder.Entity<User0>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("SYS_C00270712");

                entity.ToTable("USER0");

                entity.HasIndex(e => e.Email, "SYS_C00270713")
                    .IsUnique();

                entity.HasIndex(e => e.Phonenum, "SYS_C00270714")
                    .IsUnique();

                entity.Property(e => e.IdUser)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Phonenum)
                    .HasPrecision(14)
                    .HasColumnName("PHONENUM");

                entity.Property(e => e.ProfilePic)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PROFILE_PIC");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User0s)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ROLEID");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.HasKey(e => e.Roleid)
                    .HasName("SYS_C00270680");

                entity.ToTable("USERROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<Visa>(entity =>
            {
                entity.HasKey(e => e.IdPayment)
                    .HasName("SYS_C00270733");

                entity.ToTable("VISA");

                entity.Property(e => e.IdPayment)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_PAYMENT");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CARD_NAME");

                entity.Property(e => e.CardNumber)
                    .HasPrecision(16)
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.Cvc)
                    .HasPrecision(3)
                    .HasColumnName("CVC");

                entity.Property(e => e.ExprDate)
                    .HasPrecision(4)
                    .HasColumnName("EXPR_DATE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
