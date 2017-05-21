using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReviewCounter.Models;

namespace ReviewCounter.Migrations
{
    [DbContext(typeof(ReviewCountingContext))]
    partial class ReviewCountingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ReviewCounter.Models.Output", b =>
                {
                    b.Property<int>("OutputId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProcessOutput");

                    b.HasKey("OutputId");

                    b.ToTable("Output");
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

                    b.Property<int?>("OutputId");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("RevieweeMemberId");

                    b.Property<int>("Ticket");

                    b.Property<bool>("closed");

                    b.HasKey("ReviewId");

                    b.HasIndex("OutputId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RevieweeMemberId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("ReviewCounter.Models.ReviewTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ReviewId");

                    b.Property<int?>("ReviewerMemberId");

                    b.Property<int>("Time");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.HasIndex("ReviewerMemberId");

                    b.ToTable("ReviewTime");
                });

            modelBuilder.Entity("ReviewCounter.Models.Review", b =>
                {
                    b.HasOne("ReviewCounter.Models.Output", "Output")
                        .WithMany()
                        .HasForeignKey("OutputId");

                    b.HasOne("ReviewCounter.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("ReviewCounter.Models.Member", "Reviewee")
                        .WithMany()
                        .HasForeignKey("RevieweeMemberId");
                });

            modelBuilder.Entity("ReviewCounter.Models.ReviewTime", b =>
                {
                    b.HasOne("ReviewCounter.Models.Review", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewId");

                    b.HasOne("ReviewCounter.Models.Member", "Reviewer")
                        .WithMany()
                        .HasForeignKey("ReviewerMemberId");
                });
        }
    }
}
