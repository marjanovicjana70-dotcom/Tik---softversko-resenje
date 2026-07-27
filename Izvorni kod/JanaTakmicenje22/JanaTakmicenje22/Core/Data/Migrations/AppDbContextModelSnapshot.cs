using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JanaTakmicenje22.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable
namespace JanaTakmicenje22.Core.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("JanaTakmicenje22.Core.Models.Postignuca", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
                b.Property<string>("Description").IsRequired().HasColumnType("TEXT");
                b.Property<string>("Emoji").IsRequired().HasColumnType("TEXT");
                b.Property<string>("Name").IsRequired().HasColumnType("TEXT");
                b.Property<int>("RequiredChallenges").HasColumnType("INTEGER");
                b.HasKey("Id");
                b.ToTable("Postignuca");
            });

            modelBuilder.Entity("JanaTakmicenje22.Core.Models.Challenge", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
                b.Property<string>("Description").IsRequired().HasColumnType("TEXT");
                b.Property<int>("Order").HasColumnType("INTEGER");
                b.Property<string>("Title").IsRequired().HasColumnType("TEXT");
                b.Property<int>("XPReward").HasColumnType("INTEGER");
                b.HasKey("Id");
                b.ToTable("Challenges");
            });

            modelBuilder.Entity("JanaTakmicenje22.Core.Models.Note", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
                b.Property<string>("Content").IsRequired().HasColumnType("TEXT");
                b.Property<DateTime>("CreatedAt").HasColumnType("TEXT");
                b.Property<DateTime>("UpdatedAt").HasColumnType("TEXT");
                b.Property<string>("Title").IsRequired().HasColumnType("TEXT");
                b.Property<int>("UserId").HasColumnType("INTEGER");
                b.HasKey("Id");
                b.HasIndex("UserId");
                b.ToTable("Notes");
            });

            modelBuilder.Entity("JanaTakmicenje22.Core.Models.User", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
                b.Property<DateTime>("CreatedAt").HasColumnType("TEXT");
                b.Property<string>("Email").IsRequired().HasColumnType("TEXT");
                b.Property<DateTime?>("LastActivityDate").HasColumnType("TEXT");
                b.Property<int>("Level").HasColumnType("INTEGER");
                b.Property<string>("PasswordHash").IsRequired().HasColumnType("TEXT");
                b.Property<int>("Streak").HasColumnType("INTEGER");
                b.Property<int>("TotalChallengesCompleted").HasColumnType("INTEGER");
                b.Property<string>("Username").IsRequired().HasColumnType("TEXT");
                b.Property<int>("XP").HasColumnType("INTEGER");
                b.HasKey("Id");
                b.ToTable("Users");
            });

            modelBuilder.Entity("JanaTakmicenje22.Core.Models.UserPostignuca", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
                b.Property<int>("BadgeId").HasColumnType("INTEGER");
                b.Property<DateTime>("EarnedAt").HasColumnType("TEXT");
                b.Property<int>("UserId").HasColumnType("INTEGER");
                b.HasKey("Id");
                b.HasIndex("BadgeId");
                b.HasIndex("UserId");
                b.ToTable("UserPostignuca");
            });

            modelBuilder.Entity("JanaTakmicenje22.Core.Models.UserChallenge", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER");
                b.Property<int>("ChallengeId").HasColumnType("INTEGER");
                b.Property<DateTime?>("CompletedAt").HasColumnType("TEXT");
                b.Property<bool>("IsCompleted").HasColumnType("INTEGER");
                b.Property<int>("UserId").HasColumnType("INTEGER");
                b.HasKey("Id");
                b.HasIndex("ChallengeId");
                b.HasIndex("UserId");
                b.ToTable("UserChallenges");
            });
#pragma warning restore 612, 618
        }
    }
}
