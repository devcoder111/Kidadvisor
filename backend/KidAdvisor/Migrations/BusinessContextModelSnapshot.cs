// <auto-generated />
using System;
using KidAdvisor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KidAdvisor.Migrations
{
    [DbContext(typeof(BusinessContext))]
    partial class BusinessContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("KidAdvisor.Entities.Business", b =>
                {
                    b.Property<Guid>("BusinessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Appartement")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("BusinessNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LastEditorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Province")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("RecordCreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("BusinessId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Businesses");

                    b.HasData(
                        new
                        {
                            BusinessId = new Guid("3fded86a-2f50-4c7d-8403-81f58bfed969"),
                            Appartement = "15F",
                            BusinessNumber = 0,
                            City = "NewYork",
                            Country = "USA",
                            DateCreated = new DateTime(2021, 2, 3, 5, 38, 11, 903, DateTimeKind.Local).AddTicks(4331),
                            LastEditDate = new DateTime(2021, 2, 3, 5, 38, 11, 903, DateTimeKind.Local).AddTicks(4389),
                            LastEditorId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Pizza Hut",
                            OwnerId = new Guid("9f1c7846-19c5-42e7-b2c6-265934a42214"),
                            PostalCode = "5000",
                            RecordCreatorId = new Guid("00000000-0000-0000-0000-000000000000"),
                            StreetAddress = "6135 Hyatt Trail Suit"
                        });
                });

            modelBuilder.Entity("KidAdvisor.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LastEditorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("RecordCreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("9f1c7846-19c5-42e7-b2c6-265934a42214"),
                            DateCreated = new DateTime(2021, 2, 3, 5, 38, 11, 867, DateTimeKind.Local).AddTicks(619),
                            FirstName = "Mike",
                            LastEditDate = new DateTime(2021, 2, 3, 5, 38, 11, 868, DateTimeKind.Local).AddTicks(8793),
                            LastEditorId = new Guid("1939f2b1-6eb4-4c46-87a9-9e7de111a789"),
                            LastName = "Tyson",
                            RecordCreatorId = new Guid("1939f2b1-6eb4-4c46-87a9-9e7de111a789"),
                            UserNumber = 0
                        });
                });

            modelBuilder.Entity("KidAdvisor.Entities.Business", b =>
                {
                    b.HasOne("KidAdvisor.Entities.User", "Owner")
                        .WithMany("Businesses")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("KidAdvisor.Entities.User", b =>
                {
                    b.Navigation("Businesses");
                });
#pragma warning restore 612, 618
        }
    }
}
