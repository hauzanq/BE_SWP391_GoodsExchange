﻿// <auto-generated />
using System;
using GoodsExchange.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoodsExchange.Data.Migrations
{
    [DbContext(typeof(GoodsExchangeDbContext))]
    [Migration("20240721052927_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GoodsExchange.Data.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"),
                            CategoryName = "School Supplies"
                        },
                        new
                        {
                            CategoryId = new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"),
                            CategoryName = "Art Supplies"
                        },
                        new
                        {
                            CategoryId = new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"),
                            CategoryName = "Electronics"
                        });
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.PreOrder", b =>
                {
                    b.Property<Guid>("PreOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("BuyerConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SellerConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PreOrderId");

                    b.HasIndex("BuyerId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("SellerId");

                    b.ToTable("PreOrders");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserUploadId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserUploadId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("e84825c6-790f-45e0-b20e-223c2ceb0925"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6688),
                            CategoryId = new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"),
                            Description = "Description for product 1",
                            IsActive = true,
                            IsApproved = true,
                            Price = 10f,
                            ProductName = "Product 1",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6684),
                            UserUploadId = new Guid("fda6e282-e429-4364-a445-136b570e2fde")
                        },
                        new
                        {
                            ProductId = new Guid("ca0f15a4-12e0-4ae3-90cb-6c5b8989c4cb"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6721),
                            CategoryId = new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"),
                            Description = "Description for product 2",
                            IsActive = true,
                            IsApproved = true,
                            Price = 20f,
                            ProductName = "Product 2",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6721),
                            UserUploadId = new Guid("d6446689-2743-460b-82c3-d25b21f87b13")
                        },
                        new
                        {
                            ProductId = new Guid("f4d91682-b24a-4c81-91b4-16a0f8cf81e5"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6726),
                            CategoryId = new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"),
                            Description = "Description for product 3",
                            IsActive = true,
                            IsApproved = true,
                            Price = 30f,
                            ProductName = "Product 3",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6725),
                            UserUploadId = new Guid("82c47d9c-b386-4050-a42c-95a220639c54")
                        },
                        new
                        {
                            ProductId = new Guid("0e09f661-e9f0-4ef8-9794-a2df01b58ea2"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6729),
                            CategoryId = new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"),
                            Description = "Description for product 4",
                            IsActive = true,
                            IsApproved = true,
                            Price = 40f,
                            ProductName = "Product 4",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6729),
                            UserUploadId = new Guid("fda6e282-e429-4364-a445-136b570e2fde")
                        },
                        new
                        {
                            ProductId = new Guid("fa15a70b-6b98-4842-b9d3-03bc6099d617"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6739),
                            CategoryId = new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"),
                            Description = "Description for product 5",
                            IsActive = true,
                            IsApproved = true,
                            Price = 50f,
                            ProductName = "Product 5",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6738),
                            UserUploadId = new Guid("d6446689-2743-460b-82c3-d25b21f87b13")
                        },
                        new
                        {
                            ProductId = new Guid("eb9ecc0b-b16b-4bb0-811f-f955bbeb6660"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6747),
                            CategoryId = new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"),
                            Description = "Description for product 6",
                            IsActive = true,
                            IsApproved = true,
                            Price = 60f,
                            ProductName = "Product 6",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6746),
                            UserUploadId = new Guid("82c47d9c-b386-4050-a42c-95a220639c54")
                        },
                        new
                        {
                            ProductId = new Guid("bb95dae9-946b-4cc7-988b-0ac4bb726635"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6751),
                            CategoryId = new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"),
                            Description = "Description for product 7",
                            IsActive = true,
                            IsApproved = true,
                            Price = 70f,
                            ProductName = "Product 7",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6750),
                            UserUploadId = new Guid("fda6e282-e429-4364-a445-136b570e2fde")
                        },
                        new
                        {
                            ProductId = new Guid("8e9b7a70-f1ac-427f-bec9-c02ad71fd4e5"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6754),
                            CategoryId = new Guid("d7fde8ab-4995-4252-8c34-0d6a4077f1e3"),
                            Description = "Description for product 8",
                            IsActive = true,
                            IsApproved = true,
                            Price = 80f,
                            ProductName = "Product 8",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6753),
                            UserUploadId = new Guid("d6446689-2743-460b-82c3-d25b21f87b13")
                        },
                        new
                        {
                            ProductId = new Guid("25fadc22-7fed-4f85-9a8c-6b248ce48b5b"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6757),
                            CategoryId = new Guid("119e60e0-789a-47e2-a280-e0c1a9a7032f"),
                            Description = "Description for product 9",
                            IsActive = true,
                            IsApproved = true,
                            Price = 90f,
                            ProductName = "Product 9",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6757),
                            UserUploadId = new Guid("82c47d9c-b386-4050-a42c-95a220639c54")
                        },
                        new
                        {
                            ProductId = new Guid("78a50459-bd84-40d4-97b4-cdcea4a8bd01"),
                            ApprovedDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6764),
                            CategoryId = new Guid("765fa035-d385-4ae3-a86b-7e4bea643060"),
                            Description = "Description for product 10",
                            IsActive = true,
                            IsApproved = true,
                            Price = 100f,
                            ProductName = "Product 10",
                            UploadDate = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6763),
                            UserUploadId = new Guid("fda6e282-e429-4364-a445-136b570e2fde")
                        });
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f671abc8-1924-4c13-bc70-910a3cb2e425"),
                            Caption = "Image for product 1",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6698),
                            FileSize = 1024L,
                            ImagePath = "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg",
                            ProductId = new Guid("e84825c6-790f-45e0-b20e-223c2ceb0925")
                        },
                        new
                        {
                            Id = new Guid("355b927d-6578-45bc-93ca-7e4cbd5cf219"),
                            Caption = "Image for product 2",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6723),
                            FileSize = 2048L,
                            ImagePath = "https://img.lovepik.com/element/40145/4924.png_860.png",
                            ProductId = new Guid("ca0f15a4-12e0-4ae3-90cb-6c5b8989c4cb")
                        },
                        new
                        {
                            Id = new Guid("e70a3ee4-dda5-4e0f-8b83-ef5e3854f4c3"),
                            Caption = "Image for product 3",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6727),
                            FileSize = 3072L,
                            ImagePath = "https://img.lovepik.com/element/40154/8917.png_300.png",
                            ProductId = new Guid("f4d91682-b24a-4c81-91b4-16a0f8cf81e5")
                        },
                        new
                        {
                            Id = new Guid("540b779f-67a6-44b9-b080-681d9303e3f6"),
                            Caption = "Image for product 4",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6731),
                            FileSize = 4096L,
                            ImagePath = "https://img.lovepik.com/element/40148/8397.png_300.png",
                            ProductId = new Guid("0e09f661-e9f0-4ef8-9794-a2df01b58ea2")
                        },
                        new
                        {
                            Id = new Guid("b36f172e-1a2e-496b-a30b-ac1575c84783"),
                            Caption = "Image for product 5",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6742),
                            FileSize = 5120L,
                            ImagePath = "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png",
                            ProductId = new Guid("fa15a70b-6b98-4842-b9d3-03bc6099d617")
                        },
                        new
                        {
                            Id = new Guid("d573b968-7a40-4f68-a2cf-e88032d8c09c"),
                            Caption = "Image for product 6",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6748),
                            FileSize = 6144L,
                            ImagePath = "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png",
                            ProductId = new Guid("eb9ecc0b-b16b-4bb0-811f-f955bbeb6660")
                        },
                        new
                        {
                            Id = new Guid("8363d684-77b0-4c28-882a-f5fbb0be57de"),
                            Caption = "Image for product 7",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6752),
                            FileSize = 7168L,
                            ImagePath = "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg",
                            ProductId = new Guid("bb95dae9-946b-4cc7-988b-0ac4bb726635")
                        },
                        new
                        {
                            Id = new Guid("9e3bb7d5-80e3-4049-87b4-b597fccf748c"),
                            Caption = "Image for product 8",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6755),
                            FileSize = 8192L,
                            ImagePath = "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg",
                            ProductId = new Guid("8e9b7a70-f1ac-427f-bec9-c02ad71fd4e5")
                        },
                        new
                        {
                            Id = new Guid("297d6ab9-ba65-4313-8d9c-f83306ebbdf2"),
                            Caption = "Image for product 9",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6759),
                            FileSize = 9216L,
                            ImagePath = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU",
                            ProductId = new Guid("25fadc22-7fed-4f85-9a8c-6b248ce48b5b")
                        },
                        new
                        {
                            Id = new Guid("143b4171-d19f-4d5d-b154-251b94e94f59"),
                            Caption = "Image for product 10",
                            DateCreated = new DateTime(2024, 7, 21, 5, 29, 26, 980, DateTimeKind.Utc).AddTicks(6765),
                            FileSize = 10240L,
                            ImagePath = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU",
                            ProductId = new Guid("78a50459-bd84-40d4-97b4-cdcea4a8bd01")
                        });
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Rating", b =>
                {
                    b.Property<Guid>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("NumberStars")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RatingId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Report", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsApprove")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReportId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"),
                            RoleName = "Administrator"
                        },
                        new
                        {
                            RoleId = new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"),
                            RoleName = "Moderator"
                        },
                        new
                        {
                            RoleId = new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"),
                            RoleName = "Customer"
                        });
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PreOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TransactionId");

                    b.HasIndex("PreOrderId")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("EmailConfirm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("0af02748-9d43-4110-81e5-93d9ece8cfda"),
                            DateOfBirth = new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            EmailConfirm = false,
                            FirstName = "John",
                            IsActive = false,
                            LastName = "Doe",
                            Password = "$2a$11$DOhmqxUcRU925GGFOzfYsu3HqfTSBgDdwW.pNcLgdtzI4m6WV6nWK",
                            PhoneNumber = "555-1234567",
                            RoleId = new Guid("e398cee3-6381-4a52-aaf5-20a2e9b54810"),
                            UserImageUrl = "",
                            UserName = "admin"
                        },
                        new
                        {
                            UserId = new Guid("b6b6e80f-cc04-43e3-800f-a3c89b3ba017"),
                            DateOfBirth = new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "moderator@gmail.com",
                            EmailConfirm = false,
                            FirstName = "Jane",
                            IsActive = false,
                            LastName = "Smith",
                            Password = "$2a$11$/delu8/axUCsovbhjuqM5e7aLkLg1dOnhdD3JP4yhAHzd6.BXj916",
                            PhoneNumber = "555-7654321",
                            RoleId = new Guid("3d446530-061e-4a88-ae6c-1b6a6190a693"),
                            UserImageUrl = "",
                            UserName = "moderator"
                        },
                        new
                        {
                            UserId = new Guid("82c47d9c-b386-4050-a42c-95a220639c54"),
                            DateOfBirth = new DateTime(1978, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "customerhihi@gmail.com",
                            EmailConfirm = false,
                            FirstName = "Michael",
                            IsActive = false,
                            LastName = "Johnson",
                            Password = "$2a$11$k3Q0X2A7d4BzflmYO/an5ejlHZcKgcm0ap3vk37Ua.Z3PY9L/Kx1C",
                            PhoneNumber = "555-2468013",
                            RoleId = new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"),
                            UserImageUrl = "",
                            UserName = "customerhihi"
                        },
                        new
                        {
                            UserId = new Guid("fda6e282-e429-4364-a445-136b570e2fde"),
                            DateOfBirth = new DateTime(1990, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "customerhaha@gmail.com",
                            EmailConfirm = false,
                            FirstName = "Emily",
                            IsActive = false,
                            LastName = "Davis",
                            Password = "$2a$11$Lwc.dHYw4Dzz4jepKZFpze5hsisBLJj60g1YqHEt7T2Pl2EjjAn/a",
                            PhoneNumber = "555-3691258",
                            RoleId = new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"),
                            UserImageUrl = "",
                            UserName = "customerhaha"
                        },
                        new
                        {
                            UserId = new Guid("d6446689-2743-460b-82c3-d25b21f87b13"),
                            DateOfBirth = new DateTime(1982, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "customerhehe@gmail.com",
                            EmailConfirm = false,
                            FirstName = "David",
                            IsActive = false,
                            LastName = "Lee",
                            Password = "$2a$11$Nm9ivNzEqCEMrVbgb03zk.ujTuzKVp/tj2ouJZw5PrUOTdmrt7SoS",
                            PhoneNumber = "555-4725836",
                            RoleId = new Guid("d81f428f-9572-47f1-a980-69de7a1e348b"),
                            UserImageUrl = "",
                            UserName = "customerhehe"
                        });
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.PreOrder", b =>
                {
                    b.HasOne("GoodsExchange.Data.Models.User", "Buyer")
                        .WithMany("PreOrderToBuyers")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GoodsExchange.Data.Models.Product", "Product")
                        .WithOne("PreOrder")
                        .HasForeignKey("GoodsExchange.Data.Models.PreOrder", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoodsExchange.Data.Models.User", "Seller")
                        .WithMany("PreOrderToSellers")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Product");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Product", b =>
                {
                    b.HasOne("GoodsExchange.Data.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoodsExchange.Data.Models.User", "UserUpload")
                        .WithMany("Products")
                        .HasForeignKey("UserUploadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("UserUpload");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.ProductImage", b =>
                {
                    b.HasOne("GoodsExchange.Data.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Rating", b =>
                {
                    b.HasOne("GoodsExchange.Data.Models.Product", "Product")
                        .WithOne("Rate")
                        .HasForeignKey("GoodsExchange.Data.Models.Rating", "ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GoodsExchange.Data.Models.User", "Receiver")
                        .WithMany("RatingsReceived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GoodsExchange.Data.Models.User", "Sender")
                        .WithMany("RatingsGiven")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Report", b =>
                {
                    b.HasOne("GoodsExchange.Data.Models.Product", "Product")
                        .WithMany("Reports")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GoodsExchange.Data.Models.User", "Receiver")
                        .WithMany("ReportsReceived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GoodsExchange.Data.Models.User", "Sender")
                        .WithMany("ReportsMade")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Transaction", b =>
                {
                    b.HasOne("GoodsExchange.Data.Models.PreOrder", "PreOrder")
                        .WithOne("Transaction")
                        .HasForeignKey("GoodsExchange.Data.Models.Transaction", "PreOrderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PreOrder");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.User", b =>
                {
                    b.HasOne("GoodsExchange.Data.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.PreOrder", b =>
                {
                    b.Navigation("Transaction")
                        .IsRequired();
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Product", b =>
                {
                    b.Navigation("PreOrder")
                        .IsRequired();

                    b.Navigation("ProductImages");

                    b.Navigation("Rate")
                        .IsRequired();

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("GoodsExchange.Data.Models.User", b =>
                {
                    b.Navigation("PreOrderToBuyers");

                    b.Navigation("PreOrderToSellers");

                    b.Navigation("Products");

                    b.Navigation("RatingsGiven");

                    b.Navigation("RatingsReceived");

                    b.Navigation("ReportsMade");

                    b.Navigation("ReportsReceived");
                });
#pragma warning restore 612, 618
        }
    }
}