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
                        RoleName = "User",
                    }
                );

            #endregion

            #region Users

            var avatar = "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1";

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
                        UserImageUrl = avatar,
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
                        UserImageUrl = avatar,
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
                        UserImageUrl = avatar,
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
                        UserImageUrl = avatar,
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
                        UserImageUrl = avatar,
                        UserName = "haugiang",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0"),
                        FirstName = "Minh",
                        LastName = "Phuoc",
                        Email = "minhphuoc@gmail.com",
                        DateOfBirth = new DateTime(2003, 4, 30),
                        PhoneNumber = "0123456789",
                        UserImageUrl = avatar,
                        UserName = "minhphuoc",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("8130b91b-2e32-4aad-8479-3e01dd8813e3"),
                        FirstName = "Hậu",
                        LastName = "Giang",
                        Email = "haugiang01@gmail.com",
                        DateOfBirth = new DateTime(2003, 4, 30),
                        PhoneNumber = "0123456789",
                        UserImageUrl = avatar,
                        UserName = "haugiang45",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("273c8fa3-b3f4-4c6d-87da-07bc63a4c436"),
                        FirstName = "Minh",
                        LastName = "Trần",
                        Email = "minhtran@gmail.com",
                        DateOfBirth = new DateTime(1995, 1, 15),
                        PhoneNumber = "0987654321",
                        UserImageUrl = avatar,
                        UserName = "minhtran",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf"),
                        FirstName = "Thảo",
                        LastName = "Nguyễn",
                        Email = "thaonguyen@gmail.com",
                        DateOfBirth = new DateTime(2000, 12, 5),
                        PhoneNumber = "0912345678",
                        UserImageUrl = avatar,
                        UserName = "thaonguyen",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("15b2d60f-bdfc-4ce1-8b03-f8b8c3d7480d"),
                        FirstName = "Anh",
                        LastName = "Phạm",
                        Email = "anhpham@gmail.com",
                        DateOfBirth = new DateTime(1998, 7, 23),
                        PhoneNumber = "0908765432",
                        UserImageUrl = avatar,
                        UserName = "anhpham",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a"),
                        FirstName = "Nam",
                        LastName = "Lê",
                        Email = "namle@gmail.com",
                        DateOfBirth = new DateTime(1996, 11, 30),
                        PhoneNumber = "0901234567",
                        UserImageUrl = avatar,
                        UserName = "namle",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("ce914c08-8b01-483a-8b16-1c0e27284cc8"),
                        FirstName = "Lan",
                        LastName = "Hoàng",
                        Email = "lanhoang@gmail.com",
                        DateOfBirth = new DateTime(1999, 3, 14),
                        PhoneNumber = "0934567890",
                        UserImageUrl = avatar,
                        UserName = "lanhoang",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("bf0b0b85-e98e-4da7-af44-b7198685cb78"),
                        FirstName = "Tú",
                        LastName = "Đỗ",
                        Email = "tudo@gmail.com",
                        DateOfBirth = new DateTime(2002, 5, 18),
                        PhoneNumber = "0981234567",
                        UserImageUrl = avatar,
                        UserName = "tudo",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("fefd096b-eb6f-4843-a9fb-fdde2f08e960"),
                        FirstName = "Quỳnh",
                        LastName = "Phan",
                        Email = "quynhphan@gmail.com",
                        DateOfBirth = new DateTime(2001, 9, 21),
                        PhoneNumber = "0908761234",
                        UserImageUrl = avatar,
                        UserName = "quynhphan",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("97108917-856a-42af-937b-7b0e2e735b20"),
                        FirstName = "Hương",
                        LastName = "Trịnh",
                        Email = "huongtrinh@gmail.com",
                        DateOfBirth = new DateTime(1997, 6, 27),
                        PhoneNumber = "0937894561",
                        UserImageUrl = avatar,
                        UserName = "huongtrinh",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("0da4bfc5-3a37-4a66-91d5-fe9e1d086564"),
                        FirstName = "Khang",
                        LastName = "Võ",
                        Email = "khangvo@gmail.com",
                        DateOfBirth = new DateTime(2004, 8, 10),
                        PhoneNumber = "0923456789",
                        UserImageUrl = avatar,
                        UserName = "khangvo",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("f58236aa-5912-489a-8ffb-001d61e611c8"),
                        FirstName = "Nguyen",
                        LastName = "Van A",
                        Email = "nguyenvana@gmail.com",
                        DateOfBirth = new DateTime(2002, 5, 20),
                        PhoneNumber = "0902345678",
                        UserImageUrl = avatar,
                        UserName = "nguyenvana",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("803b1549-478c-421b-bd4b-2b6111836609"),
                        FirstName = "Le",
                        LastName = "Thi B",
                        Email = "lethib@gmail.com",
                        DateOfBirth = new DateTime(2001, 8, 14),
                        PhoneNumber = "0903456789",
                        UserImageUrl = avatar,
                        UserName = "lethib",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("7c87c075-2c90-4c6e-8e8b-b903ec331072"),
                        FirstName = "Tran",
                        LastName = "Van C",
                        Email = "tranvanc@gmail.com",
                        DateOfBirth = new DateTime(2000, 12, 1),
                        PhoneNumber = "0904567890",
                        UserImageUrl = avatar,
                        UserName = "tranvanc",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("f370712b-0452-479c-a0d4-b4d2a7ace38f"),
                        FirstName = "Pham",
                        LastName = "Thi D",
                        Email = "phamthid@gmail.com",
                        DateOfBirth = new DateTime(2003, 11, 30),
                        PhoneNumber = "0905678901",
                        UserImageUrl = avatar,
                        UserName = "phamthid",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693"),
                        EmailConfirm = true
                    },
                    new User()
                    {
                        UserId = new Guid("a6f939d3-eb8a-4884-8084-b1dc99559167"),
                        FirstName = "Do",
                        LastName = "Van E",
                        Email = "dovane@gmail.com",
                        DateOfBirth = new DateTime(2004, 3, 10),
                        PhoneNumber = "0906789012",
                        UserImageUrl = avatar,
                        UserName = "dovane",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693"),
                        EmailConfirm = true
                    }
                );

            #endregion

            #region Catgories
            builder.Entity<Category>().HasData
                (
                    new Category { CategoryId = new Guid("119E60E0-789A-47E2-A280-E0C1A9A7032F"), CategoryName = "School Supplies" },
                    new Category { CategoryId = new Guid("765FA035-D385-4AE3-A86B-7E4BEA643060"), CategoryName = "Stationery" },
                    new Category { CategoryId = new Guid("D7FDE8AB-4995-4252-8C34-0D6A4077F1E3"), CategoryName = "Textbooks" },
                    new Category { CategoryId = new Guid("93838A5D-B3C7-4C47-BA22-F6E26755203F"), CategoryName = "Lab Equipment" }

                );

            #endregion

            #region Product + ProductImage

            var userUploadIds = new[]
            {
                new Guid("82C47D9C-B386-4050-A42C-95A220639C54"),
                new Guid("FDA6E282-E429-4364-A445-136B570E2FDE"),
                new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                new Guid("82C47D9C-B386-4050-A42C-95A220639C54"),
                new Guid("4c99eef2-baca-4394-8d45-47e5f805e0f0"),
                new Guid("09481859-e811-4584-b2dd-0a74c8adfaaf"),
                new Guid("d3bbeb03-d683-47cc-9ef0-0a2b36c2788a"),
                new Guid("97108917-856a-42af-937b-7b0e2e735b20")


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
            var random = new Random();
            var minPrice = 1000;
            var maxPrice = 1000000;
            var productNames = new List<string>
            {
                "Elegant Fountain Pen",
                "Assorted Sticky Notes",
                "High-Quality Ruler Set",
                "Premium Ink Refills",
                "Spiral Notebook",
                "Mechanical Pencil Set",
                "Coloring Pencil Pack",
                "Student Backpack",
                "Introduction to Algebra",
                "History of Ancient Civilizations",
                "Biology: Principles and Explorations",
                "Physics for Beginners",
                "Digital Thermometer",
                "Glass Beaker Set",
                "Microscope with LED Illumination",
                "Laboratory Measuring Cylinder"
            };

            var descriptions = new List<string>
            {
                "An elegant fountain pen with a sleek design, ideal for professional writing and note-taking.",
                "A pack of colorful sticky notes for quick reminders, notes, and organizing your tasks.",
                "A set of durable rulers made from high-quality materials, perfect for accurate measurements and drawings.",
                "A set of premium ink refills suitable for fountain pens, ensuring smooth and consistent writing.",
                "A spiral-bound notebook with a durable cover, ideal for taking notes and journaling.",
                "A set of high-quality mechanical pencils with refillable lead, perfect for precise writing.",
                "A pack of vibrant coloring pencils for creative drawing and coloring activities.",
                "A spacious and ergonomic backpack designed to carry all your school supplies comfortably.",
                "A comprehensive textbook covering fundamental algebraic concepts and problem-solving techniques.",
                "An in-depth textbook exploring the history and achievements of ancient civilizations from around the world.",
                "A detailed textbook offering a thorough overview of biological principles and scientific explorations.",
                "An introductory textbook designed to help students understand the basic principles of physics.",
                "A digital thermometer with high precision for measuring temperatures in scientific experiments.",
                "A set of durable glass beakers with clear measurement markings for laboratory use.",
                "A high-quality microscope featuring LED illumination for clear and detailed observation of specimens.",
                "A precision measuring cylinder designed for accurate measurement of liquids in laboratory settings."
            };

            for (int i = 1; i <= 25; i++)
            {
                var productId = Guid.NewGuid();
                var categoryId = categoryIds[i % categoryIds.Count];
                var userUploadId = userUploadIds[i % userUploadIds.Length];
                var imagePath = images[i % images.Length];
                var descriptionIndex = random.Next(descriptions.Count);
                var nameIndex = random.Next(productNames.Count);

                products.Add(new Product
                {
                    ProductId = productId,
                    ProductName = productNames[nameIndex],
                    Description = descriptions[descriptionIndex],
                    IsActive = true,
                    UploadDate = DateTime.UtcNow,
                    UserUploadId = userUploadId,
                    IsApproved = true,
                    IsReviewed = true,
                    ApprovedDate = DateTime.UtcNow,
                    CategoryId = categoryId
                });

                productImages.Add(new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    ImagePath = imagePath,
                    Caption = $"Image for {productNames[nameIndex]}",
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
