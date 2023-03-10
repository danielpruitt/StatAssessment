// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StatAssesment.Data;

#nullable disable

namespace StatAssesment.Migrations
{
    [DbContext(typeof(StatAssessmentContext))]
    [Migration("20230301173205_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StatAssesment.Data.Models.KomarExtract", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ARReasonCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("AttachmentList")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("CarrierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("CheckDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("CheckNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("ClaimDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("ClaimNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("CreateDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("CustomerLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("CustomerReasonCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("DaysOutstanding")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("DocumentDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("InvoiceStoreNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("LoadId")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("MergeStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("OpenAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("OriginalAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("OriginalCustomer")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("PONumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("UnresolvedAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(2500)");

                    b.HasKey("Id");

                    b.ToTable("KomarExtract");
                });
#pragma warning restore 612, 618
        }
    }
}
