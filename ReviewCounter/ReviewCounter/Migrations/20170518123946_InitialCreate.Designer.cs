using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReviewCounter.Models;

namespace ReviewCounter.Migrations
{
    [DbContext(typeof(ReviewCountingContext))]
    [Migration("20170518123946_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReviewCounter.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("MemberId");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("ReviewCounter.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ProjectId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ReviewCounter.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("RevieweeMemberId");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RevieweeMemberId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("ReviewCounter.Models.ReviewTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MemberId");

                    b.Property<int?>("ReviewId");

                    b.Property<int>("Time");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("ReviewId");

                    b.ToTable("ReviewTime");
                });

            modelBuilder.Entity("ReviewCounter.Models.Review", b =>
                {
                    b.HasOne("ReviewCounter.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("ReviewCounter.Models.Member", "Reviewee")
                        .WithMany()
                        .HasForeignKey("RevieweeMemberId");
                });

            modelBuilder.Entity("ReviewCounter.Models.ReviewTime", b =>
                {
                    b.HasOne("ReviewCounter.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");

                    b.HasOne("ReviewCounter.Models.Review", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewId");
                });
        }
    }
}
