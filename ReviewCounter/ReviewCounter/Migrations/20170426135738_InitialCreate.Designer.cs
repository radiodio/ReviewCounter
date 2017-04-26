using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReviewCounter.Models;

namespace ReviewCounter.Migrations
{
    [DbContext(typeof(ReviewCountingContext))]
    [Migration("20170426135738_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReviewCounter.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmployeeNumber");

                    b.Property<string>("Name");

                    b.HasKey("MemberId");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("ReviewCounter.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MemberId");

                    b.Property<string>("Name");

                    b.Property<string>("ProjectCode");

                    b.HasKey("ProjectId");

                    b.HasIndex("MemberId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ReviewCounter.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Memo");

                    b.Property<int?>("ProjectId");

                    b.Property<int>("Time");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("ReviewCounter.Models.Project", b =>
                {
                    b.HasOne("ReviewCounter.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("ReviewCounter.Models.Review", b =>
                {
                    b.HasOne("ReviewCounter.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });
        }
    }
}
