using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            #region Roles
            builder.Entity<Role>().HasData
                (
                    new Role()
                    {
                        RoleId = new Guid("E398CEE3-6381-4A52-AAF5-20A2E9B54810"),
                        RoleName = "Administrator",
                    },
                    new Role()
                    {
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693"),
                        RoleName = "Moderator",
                    },
                    new Role()
                    {
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        RoleName = "Customer",
                    }
                );

            #endregion

            #region Users
            builder.Entity<User>().HasData
                (
                    new User()
                    {
                        UserId = new Guid("0AF02748-9D43-4110-81E5-93D9ECE8CFDA"),
                        FirstName = "Minh",
                        LastName = "Khoa",
                        Email = "minhkhoa@gmail.com",
                        DateOfBirth = new DateTime(2003, 6, 15),
                        PhoneNumber = "0123456789",
                        UserImageUrl = "",
                        UserName = "admin",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("E398CEE3-6381-4A52-AAF5-20A2E9B54810"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("B6B6E80F-CC04-43E3-800F-A3C89B3BA017"),
                        FirstName = "Pham",
                        LastName = "Thanh",
                        Email = "phamthanh@gmail.com",
                        DateOfBirth = new DateTime(2003, 3, 10),
                        PhoneNumber = "0123456789",
                        UserImageUrl = "",
                        UserName = "moderator",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("82C47D9C-B386-4050-A42C-95A220639C54"),
                        FirstName = "Phuong",
                        LastName = "Thao",
                        Email = "phuongthao@gmail.com",
                        DateOfBirth = new DateTime(2003, 11, 22),
                        PhoneNumber = "0123456789",
                        UserImageUrl = "",
                        UserName = "phuongthao",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("FDA6E282-E429-4364-A445-136B570E2FDE"),
                        FirstName = "Quoc",
                        LastName = "Trieu",
                        Email = "quoctrieu@gmail.com",
                        DateOfBirth = new DateTime(2003, 7, 1),
                        PhoneNumber = "0123456789",
                        UserImageUrl = "",
                        UserName = "quoctrieu",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        FirstName = "Hau",
                        LastName = "Giang",
                        Email = "haugiang@gmail.com",
                        DateOfBirth = new DateTime(2003, 4, 30),
                        PhoneNumber = "0123456789",
                        UserImageUrl = "",
                        UserName = "haugiang",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    }
                );

            #endregion

            #region Catgories
            builder.Entity<Category>().HasData
                (
                    new Category { CategoryId = new Guid("119E60E0-789A-47E2-A280-E0C1A9A7032F"), CategoryName = "School Supplies" },
                    new Category { CategoryId = new Guid("765FA035-D385-4AE3-A86B-7E4BEA643060"), CategoryName = "Art Supplies" },
                    new Category { CategoryId = new Guid("D7FDE8AB-4995-4252-8C34-0D6A4077F1E3"), CategoryName = "Electronics" }
                );

            #endregion

            #region Product + ProductImage

            var userUploadIds = new[]
            {
                new Guid("82C47D9C-B386-4050-A42C-95A220639C54"),
                new Guid("FDA6E282-E429-4364-A445-136B570E2FDE"),
                new Guid("D6446689-2743-460B-82C3-D25B21F87B13")
            };

            var categoryIds = builder.Model.GetEntityTypes().First(t => t.ClrType == typeof(Category))
                                                            .GetSeedData()
                                                            .Select(e => (Guid)e["CategoryId"])
                                                            .ToList();

            var images = new[]
            {
              "https://png.pngtree.com/png-vector/20190130/ourlarge/pngtree-simple-and-cute-school-supplies-stationery-suppliesstationerystaplerpenpencil-casecorrection-fluidrubber-png-image_674963.jpg",
              "https://png.pngtree.com/element_origin_min_pic/16/09/23/1857e50467c5629.jpg",
              "https://img.lovepik.com/element/40145/4924.png_860.png",
              "https://img.lovepik.com/element/40154/8917.png_300.png",
              "https://img.lovepik.com/element/40148/8397.png_300.png",
              "https://img.lovepik.com/original_origin_pic/18/08/09/ad4800dc49f64e450ae5f7d2c15bbd69.png_wh300.png",
              "https://img.lovepik.com/png/20231021/School-office-supplies-binding-machine-stapler-book-stationery_289576_wh300.png",
              "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-cute.jpg",
              "https://tomau.vn/wp-content/uploads/tranh-to-mau-do-dung-hoc-tap-de-thuong.jpg",
              "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQClbO9Pb9b1e1cm18mublklMG69UYXdPgGgbeNGPutxgObEWNt0gMTNXmOHZInEp8O1ro&usqp=CAU",
              "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxXUh1O9kqmHicXzEZYoksQl0zKVwNW3KRoI2N39oO3Yyw33D03xmltVXOqTtbTa3gAfU&usqp=CAU",
              "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSjY5xXwr880MJU4ZMkHoS4Kk9uBvJlVOocsyyr8c-SZIhpInnWpbdrTbxLSwIGRTqLtQE&usqp=CAU",
              "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTzAg7kp7XyYhhU7MlQDH_Or2jTxIPZuyl3j_hA01ywVLBmo5qSkIeTbDE4C7DaKdDlyI&usqp=CAU",
              "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSo-afGGvOl-uAIciO_inSUbq8c2WrvHEA8zA&s",
              "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNhUHteq_2myNyWXDG0-tdUmElcHC5ZufUbA&s",
              "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEabvTjiSCtzyqA2S0JmFbTYH53qBm-WjioQ&s"
            };

            var products = new List<Product>();
            var productImages = new List<ProductImage>();

            for (int i = 1; i <= 10; i++)
            {
                var productId = Guid.NewGuid();
                var categoryId = categoryIds[i % categoryIds.Count];
                var userUploadId = userUploadIds[i % userUploadIds.Length];
                var imagePath= images[i % images.Length];
                products.Add(new Product
                {
                    ProductId = productId,
                    ProductName = $"Product {i}",
                    Description = $"Description for product {i}",
                    Price = 10.0f * i,
                    IsActive = true,
                    UploadDate = DateTime.UtcNow,
                    UserUploadId = userUploadId,
                    IsApproved = true,
                    ApprovedDate = DateTime.UtcNow,
                    CategoryId = categoryId
                });

                productImages.Add(new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    ImagePath = imagePath,
                    Caption = $"Image for product {i}",
                    DateCreated = DateTime.UtcNow,
                    FileSize = 1024 * i
                });
            }

            builder.Entity<Product>().HasData(products);
            builder.Entity<ProductImage>().HasData(productImages);
            #endregion
        }
    }
}
