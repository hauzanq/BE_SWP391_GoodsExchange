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
                        RoleId = new Guid("CA5AF2D0-6B92-49BB-91FF-2E5D9F1279D4"),
                        RoleName = "Buyer",
                    },
                    new Role()
                    {
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        RoleName = "Seller",
                    }
                );

            #endregion

            #region Users
            builder.Entity<User>().HasData
                (
                    new User()      // Administrator
                    {
                        UserId = new Guid("0AF02748-9D43-4110-81E5-93D9ECE8CFDA"),
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        DateOfBirth = new DateTime(1985, 6, 15),
                        PhoneNumber = "555-1234567",
                        UserImageUrl = "",
                        UserName = "admin",
                        Password = "123456789",
                        IsActive = true
                    },
                    new User()      // Moderator
                    {
                        UserId = new Guid("B6B6E80F-CC04-43E3-800F-A3C89B3BA017"),
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        DateOfBirth = new DateTime(1992, 3, 10),
                        PhoneNumber = "555-7654321",
                        UserImageUrl = "",
                        UserName = "moderator",
                        Password = "123456789",
                        IsActive = true
                    },
                    new User        // Buyer
                    {
                        UserId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        FirstName = "Michael",
                        LastName = "Johnson",
                        Email = "michael.johnson@example.com",
                        DateOfBirth = new DateTime(1978, 11, 22),
                        PhoneNumber = "555-2468013",
                        UserImageUrl = "",
                        UserName = "buyer1",
                        Password = "123456789",
                        IsActive = true
                    },
                    new User        // Buyer
                    {
                        UserId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        FirstName = "Emily",
                        LastName = "Davis",
                        Email = "emily.davis@example.com",
                        DateOfBirth = new DateTime(1990, 7, 1),
                        PhoneNumber = "555-3691258",
                        UserImageUrl = "",
                        UserName = "buyer2",
                        Password = "123456789",
                        IsActive = true
                    },
                    new User        // Seller
                    {
                        UserId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        FirstName = "David",
                        LastName = "Lee",
                        Email = "david.lee@example.com",
                        DateOfBirth = new DateTime(1982, 4, 30),
                        PhoneNumber = "555-4725836",
                        UserImageUrl = "",
                        UserName = "seller",
                        Password = "123456789",
                        IsActive = true
                    }
                );

            #endregion

            #region UserRoles
            builder.Entity<UserRole>().HasData
                (
                    new UserRole()
                    {
                        UserId = new Guid("0AF02748-9D43-4110-81E5-93D9ECE8CFDA"),
                        RoleId = new Guid("E398CEE3-6381-4A52-AAF5-20A2E9B54810")
                    },
                    new UserRole()
                    {
                        UserId = new Guid("B6B6E80F-CC04-43E3-800F-A3C89B3BA017"),
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693")
                    },
                    new UserRole()
                    {
                        UserId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        RoleId = new Guid("CA5AF2D0-6B92-49BB-91FF-2E5D9F1279D4")
                    },
                    new UserRole()
                    {
                        UserId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        RoleId = new Guid("CA5AF2D0-6B92-49BB-91FF-2E5D9F1279D4")
                    },
                    new UserRole()
                    {
                        UserId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B")
                    }
                );
            #endregion

            #region Catgories
            builder.Entity<Category>().HasData
                (
                    new Category()
                    {
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB"),
                        CategoryName = "Stationery"
                    },
                    new Category()
                    {
                        CategoryId = new Guid("CE74FC86-9CDF-4805-960C-E4647F21F6CF"),
                        CategoryName = "Drawing Supplies"
                    },
                    new Category()
                    {
                        CategoryId = new Guid("F0FDE948-4E6D-4412-A417-3EAC5F927D44"),
                        CategoryName = "Books and Materials"
                    },
                    new Category()
                    {
                        CategoryId = new Guid("E0B58109-B173-442A-86D5-972E0BC3E093"),
                        CategoryName = "Tech Devices"
                    }
                );

            #endregion

            #region Products
            builder.Entity<Product>().HasData
                (
                    new Product
                    {
                        ProductId = new Guid("EEE1A0C9-77C3-4FC1-B6A2-DA34CF31C219"),
                        ProductName = "Ballpoint Pen",
                        Description = "Premium ballpoint pen for everyday use",
                        Price = 2.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 4, 15),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = false,
                        ApprovedDate = new DateTime(2023, 4, 16),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    },
                    new Product
                    {
                        ProductId = new Guid("EF26CAEC-CEBE-47CC-8E2F-BAECBF5047FC"),
                        ProductName = "Mechanical Pencil",
                        Description = "Durable mechanical pencil with 0.5mm lead",
                        Price = 4.50f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 5, 1),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = false,
                        ApprovedDate = new DateTime(2023, 5, 2),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    },
                    new Product
                    {
                        ProductId = new Guid("DC3E969C-5A30-4028-8A96-DB3F0DCD53DE"),
                        ProductName = "Colored Pencils",
                        Description = "Set of 24 high-quality colored pencils",
                        Price = 9.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 3, 20),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = false,
                        ApprovedDate = new DateTime(2023, 3, 21),
                        CategoryId = new Guid("CE74FC86-9CDF-4805-960C-E4647F21F6CF")
                    },
                    new Product
                    {
                        ProductId = new Guid("864A10B2-8045-469D-A4F3-D52433195FA5"),
                        ProductName = "Sketchbook",
                        Description = "A5 size sketchbook with acid-free pages",
                        Price = 12.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 6, 1),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = false,
                        ApprovedDate = new DateTime(2023, 6, 2),
                        CategoryId = new Guid("CE74FC86-9CDF-4805-960C-E4647F21F6CF")
                    },
                    new Product
                    {
                        ProductId = new Guid("77E9BB4D-286E-4F0D-AB61-FAC48C135CAB"),
                        ProductName = "Chemistry Textbook",
                        Description = "High school-level chemistry textbook",
                        Price = 29.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 2, 10),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = false,
                        ApprovedDate = new DateTime(2023, 2, 11),
                        CategoryId = new Guid("F0FDE948-4E6D-4412-A417-3EAC5F927D44")
                    },
                    new Product
                    {
                        ProductId = new Guid("D96CCBB3-39C2-4D9E-B829-1705216664FA"),
                        ProductName = "Mathematics Workbook",
                        Description = "Grade 7 mathematics practice workbook",
                        Price = 14.99f,
                        IsActive = false,
                        UploadDate = new DateTime(2023, 7, 1),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 7, 2),
                        CategoryId = new Guid("F0FDE948-4E6D-4412-A417-3EAC5F927D44")
                    },
                    new Product
                    {
                        ProductId = new Guid("6EDE9679-64BD-48AF-A1EF-B04F55EE8FA3"),
                        ProductName = "Graphing Calculator",
                        Description = "Scientific calculator with graphing capabilities",
                        Price = 59.99f,
                        IsActive = false,
                        UploadDate = new DateTime(2023, 3, 15),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 3, 16),
                        CategoryId = new Guid("E0B58109-B173-442A-86D5-972E0BC3E093")
                    },
                    new Product
                    {
                        ProductId = new Guid("79E9860D-EFDC-43B6-8CA2-B077798F62EA"),
                        ProductName = "Tablet Computer",
                        Description = "High-performance tablet for educational use",
                        Price = 299.99f,
                        IsActive = false,
                        UploadDate = new DateTime(2023, 5, 20),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 5, 21),
                        CategoryId = new Guid("E0B58109-B173-442A-86D5-972E0BC3E093")
                    },
                    new Product
                    {
                        ProductId = new Guid("EC851619-5B2F-4A01-B2C8-EA4EC62C85CE"),
                        ProductName = "Ruler",
                        Description = "Durable 30cm plastic ruler",
                        Price = 1.50f,
                        IsActive = false,
                        UploadDate = new DateTime(2023, 4, 1),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 4, 2),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    },
                    new Product
                    {
                        ProductId = new Guid("D6AFE29B-0A86-4E4F-B29D-28571E906767"),
                        ProductName = "Highlighter Set",
                        Description = "Set of 4 fluorescent highlighters",
                        Price = 3.99f,
                        IsActive = false,
                        UploadDate = new DateTime(2023, 6, 15),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 6, 16),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    }
                );
            #endregion

            #region ProductImage

            builder.Entity<ProductImage>().HasData
                (
                    new ProductImage
                    {
                        Id = new Guid("10EE7295-86AB-4D60-85C2-69184F77DB09"),
                        ProductId = new Guid("EEE1A0C9-77C3-4FC1-B6A2-DA34CF31C219"),
                        ImagePath = "https://example.com/product-image-1.jpg",
                        Caption = "Product Image 1",
                        DateCreated = new DateTime(2023, 5, 1),
                        FileSize = 512 * 1024
                    },
                    new ProductImage
                    {
                        Id = new Guid("6CEF5AE9-F514-47E8-A44D-D22F821CC87F"),
                        ProductId = new Guid("EF26CAEC-CEBE-47CC-8E2F-BAECBF5047FC"),
                        ImagePath = "https://example.com/product-image-2.jpg",
                        Caption = "Product Image 2",
                        DateCreated = new DateTime(2023, 5, 2),
                        FileSize = 478 * 1024
                    }, 
                    new ProductImage
                    {
                        Id = new Guid("673E0857-D289-4E81-9F3A-E182768C614E"),
                        ProductId = new Guid("DC3E969C-5A30-4028-8A96-DB3F0DCD53DE"),
                        ImagePath = "https://example.com/product-image-3.jpg",
                        Caption = "Product Image 3",
                        DateCreated = new DateTime(2023, 5, 3),
                        FileSize = 389 * 1024
                    },
                    new ProductImage
                    {
                        Id = new Guid("797B9709-BFAD-4B97-B809-FF4E4BC4645E"),
                        ProductId = new Guid("864A10B2-8045-469D-A4F3-D52433195FA5"),
                        ImagePath = "https://example.com/product-image-4.jpg",
                        Caption = "Product Image 4",
                        DateCreated = new DateTime(2023, 5, 4),
                        FileSize = 402 * 1024
                    },
                    new ProductImage
                    {
                        Id = new Guid("A12F506C-360C-4F56-8570-703BC90F9563"),
                        ProductId = new Guid("77E9BB4D-286E-4F0D-AB61-FAC48C135CAB"),
                        ImagePath = "https://example.com/product-image-5.jpg",
                        Caption = "Product Image 5",
                        DateCreated = new DateTime(2023, 5, 5),
                        FileSize = 612 * 1024
                    },
                    new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        ProductId = new Guid("D96CCBB3-39C2-4D9E-B829-1705216664FA"),
                        ImagePath = "https://example.com/product-image-6.jpg",
                        Caption = "Product Image 6",
                        DateCreated = new DateTime(2023, 5, 6),
                        FileSize = 478 * 1024
                    },
                    new ProductImage
                    {
                        Id = new Guid("46B98CFE-34B3-43C1-AAE3-D6761261E4E4"),
                        ProductId = new Guid("6EDE9679-64BD-48AF-A1EF-B04F55EE8FA3"),
                        ImagePath = "https://example.com/product-image-7.jpg",
                        Caption = "Product Image 7",
                        DateCreated = new DateTime(2023, 5, 7),
                        FileSize = 389 * 1024
                    },
                    new ProductImage
                    {
                        Id = new Guid("3FEB6D01-9304-4F62-B167-66A691631250"),
                        ProductId = new Guid("79E9860D-EFDC-43B6-8CA2-B077798F62EA"),
                        ImagePath = "https://example.com/product-image-8.jpg",
                        Caption = "Product Image 8",
                        DateCreated = new DateTime(2023, 5, 8),
                        FileSize = 402 * 1024
                    },
                    new ProductImage
                    {
                        Id = new Guid("4DFC8842-DA3E-461F-A795-F9360244694E"),
                        ProductId = new Guid("EC851619-5B2F-4A01-B2C8-EA4EC62C85CE"),
                        ImagePath = "https://example.com/product-image-9.jpg",
                        Caption = "Product Image 9",
                        DateCreated = new DateTime(2023, 5, 9),
                        FileSize = 612 * 1024
                    },
                    new ProductImage
                    {
                        Id = new Guid("A7583394-592B-46E5-B015-81EB6DF2FB0B"),
                        ProductId = new Guid("D6AFE29B-0A86-4E4F-B29D-28571E906767"),
                        ImagePath = "https://example.com/product-image-10.jpg",
                        Caption = "Product Image 10",
                        DateCreated = new DateTime(2023, 5, 10),
                        FileSize = 478 * 1024
                    }
                );

            #endregion

            #region Reports

            builder.Entity<Report>().HasData
                (
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Spam content",
                        CreateDate = new DateTime(2023, 5, 15),
                        SenderId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        ReceiverId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        ProductId = new Guid("EEE1A0C9-77C3-4FC1-B6A2-DA34CF31C219"),
                        IsApprove = false,
                       
                        IsActive = true
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Inappropriate content",
                        CreateDate = new DateTime(2023, 6, 1),
                        SenderId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        ReceiverId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        ProductId = new Guid("EF26CAEC-CEBE-47CC-8E2F-BAECBF5047FC"),
                        IsApprove = true,
                        IsActive = false
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Copyright infringement",
                        CreateDate = new DateTime(2023, 4, 20),
                        SenderId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        ReceiverId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        ProductId = new Guid("DC3E969C-5A30-4028-8A96-DB3F0DCD53DE"),
                        IsApprove = false,
                        IsActive = true
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Misleading information",
                        CreateDate = new DateTime(2023, 7, 1),
                        SenderId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        ReceiverId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        ProductId = new Guid("864A10B2-8045-469D-A4F3-D52433195FA5"),
                        IsApprove = true,
                        IsActive = false
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Hate speech",
                        CreateDate = new DateTime(2023, 3, 10),
                        SenderId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        ReceiverId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        ProductId = new Guid("864A10B2-8045-469D-A4F3-D52433195FA5"),
                        IsApprove = false,
                        IsActive = true
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Illegal activity",
                        CreateDate = new DateTime(2023, 9, 1),
                        SenderId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        ReceiverId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        ProductId = new Guid("77E9BB4D-286E-4F0D-AB61-FAC48C135CAB"),
                        IsApprove = true,
                        IsActive = false
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Violation of terms of service",
                        CreateDate = new DateTime(2023, 11, 15),
                        SenderId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        ReceiverId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        ProductId = new Guid("6EDE9679-64BD-48AF-A1EF-B04F55EE8FA3"),
                        IsApprove = false,
                        IsActive = true
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Harassment",
                        CreateDate = new DateTime(2023, 8, 1),
                        SenderId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        ReceiverId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        ProductId = new Guid("79E9860D-EFDC-43B6-8CA2-B077798F62EA"),
                        IsApprove = true,
                        IsActive = false
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Fraud",
                        CreateDate = new DateTime(2023, 2, 1),
                        SenderId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        ReceiverId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        ProductId = new Guid("EC851619-5B2F-4A01-B2C8-EA4EC62C85CE"),
                        IsApprove = false,
                        IsActive = true
                    },
                    new Report
                    {
                        ReportId = Guid.NewGuid(),
                        Reason = "Violation of privacy",
                        CreateDate = new DateTime(2023, 12, 1),
                        SenderId = new Guid("50248CA1-B632-4E16-B1A4-9AADD8E08E7C"),
                        ReceiverId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        ProductId = new Guid("D6AFE29B-0A86-4E4F-B29D-28571E906767"),
                        IsApprove = true,
                        IsActive = false
                    }
                );
            #endregion

            #region Rates

            #endregion
        }
    }
}
