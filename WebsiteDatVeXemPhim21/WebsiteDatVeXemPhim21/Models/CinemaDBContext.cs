using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class CinemaDBContext : DbContext
    {
        public CinemaDBContext()
        {
        }

        public CinemaDBContext(DbContextOptions<CinemaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<CinemaMovieShowTime> CinemaMovieShowTimes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieGenre> MovieGenres { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<ShowTime> ShowTimes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CinemaDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.Property(e => e.CinemaId).HasColumnName("CinemaID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CinemaName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CinemaMovieShowTime>(entity =>
            {
                entity.ToTable("Cinema_Movie_ShowTime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CinemaId).HasColumnName("CinemaID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.ShowTimeId).HasColumnName("ShowTimeID");

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.CinemaMovieShowTimes)
                    .HasForeignKey(d => d.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cinema_Mo__Cinem__2F10007B");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.CinemaMovieShowTimes)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cinema_Mo__Movie__2E1BDC42");

                entity.HasOne(d => d.ShowTime)
                    .WithMany(p => p.CinemaMovieShowTimes)
                    .HasForeignKey(d => d.ShowTimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cinema_Mo__ShowT__300424B4");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Disciption)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Disciption).IsRequired();

                entity.Property(e => e.MovieName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Movie_Genre");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Genre)
                    .WithMany()
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movie_Gen__Genre__32E0915F");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movie_Gen__Movie__31EC6D26");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.BookingDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OrderDetail");

                entity.Property(e => e.CinemaId).HasColumnName("CinemaID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.SeatId).HasColumnName("SeatID");

                entity.Property(e => e.ShowTimeId).HasColumnName("ShowTimeID");

                entity.HasOne(d => d.Cinema)
                    .WithMany()
                    .HasForeignKey(d => d.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Cinem__5535A963");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Movie__5629CD9C");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__534D60F1");

                entity.HasOne(d => d.Seat)
                    .WithMany()
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__SeatI__5441852A");

                entity.HasOne(d => d.ShowTime)
                    .WithMany()
                    .HasForeignKey(d => d.ShowTimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__ShowT__571DF1D5");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.RatingId).HasColumnName("RatingID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Rating1).HasColumnName("Rating");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ratings__MovieID__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ratings__UserID__4222D4EF");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.SeatId).HasColumnName("SeatID");

                entity.Property(e => e.CinemaId).HasColumnName("CinemaID");

                entity.Property(e => e.SeatNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seats__CinemaID__35BCFE0A");
            });

            modelBuilder.Entity<ShowTime>(entity =>
            {
                entity.Property(e => e.ShowTimeId).HasColumnName("ShowTimeID");

                entity.Property(e => e.ShowDate).HasColumnType("date");

                entity.Property(e => e.ShowTime1).HasColumnName("ShowTime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserRole)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("user_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
