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
                        ProductImageUrl = "https://example.com/ballpoint-pen.jpg",
                        Price = 2.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 4, 15),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 4, 16),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    },
                    new Product
                    {
                        ProductId = new Guid("EF26CAEC-CEBE-47CC-8E2F-BAECBF5047FC"),
                        ProductName = "Mechanical Pencil",
                        Description = "Durable mechanical pencil with 0.5mm lead",
                        ProductImageUrl = "https://example.com/mechanical-pencil.jpg",
                        Price = 4.50f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 5, 1),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 5, 2),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    },
                    new Product
                    {
                        ProductId = new Guid("DC3E969C-5A30-4028-8A96-DB3F0DCD53DE"),
                        ProductName = "Colored Pencils",
                        Description = "Set of 24 high-quality colored pencils",
                        ProductImageUrl = "https://example.com/colored-pencils.jpg",
                        Price = 9.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 3, 20),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 3, 21),
                        CategoryId = new Guid("CE74FC86-9CDF-4805-960C-E4647F21F6CF")
                    },
                    new Product
                    {
                        ProductId = new Guid("864A10B2-8045-469D-A4F3-D52433195FA5"),
                        ProductName = "Sketchbook",
                        Description = "A5 size sketchbook with acid-free pages",
                        ProductImageUrl = "https://example.com/sketchbook.jpg",
                        Price = 12.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 6, 1),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 6, 2),
                        CategoryId = new Guid("CE74FC86-9CDF-4805-960C-E4647F21F6CF")
                    },
                    new Product
                    {
                        ProductId = new Guid("77E9BB4D-286E-4F0D-AB61-FAC48C135CAB"),
                        ProductName = "Chemistry Textbook",
                        Description = "High school-level chemistry textbook",
                        ProductImageUrl = "https://example.com/chemistry-textbook.jpg",
                        Price = 29.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 2, 10),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 2, 11),
                        CategoryId = new Guid("F0FDE948-4E6D-4412-A417-3EAC5F927D44")
                    },
                    new Product
                    {
                        ProductId = new Guid("D96CCBB3-39C2-4D9E-B829-1705216664FA"),
                        ProductName = "Mathematics Workbook",
                        Description = "Grade 7 mathematics practice workbook",
                        ProductImageUrl = "https://example.com/math-workbook.jpg",
                        Price = 14.99f,
                        IsActive = true,
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
                        ProductImageUrl = "https://example.com/graphing-calculator.jpg",
                        Price = 59.99f,
                        IsActive = true,
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
                        ProductImageUrl = "https://example.com/tablet-computer.jpg",
                        Price = 299.99f,
                        IsActive = true,
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
                        ProductImageUrl = "https://example.com/ruler.jpg",
                        Price = 1.50f,
                        IsActive = true,
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
                        ProductImageUrl = "https://example.com/highlighter-set.jpg",
                        Price = 3.99f,
                        IsActive = true,
                        UploadDate = new DateTime(2023, 6, 15),
                        UserUploadId = new Guid("99D274E6-FA23-4D1C-8F8A-097B3886CAAD"),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 6, 16),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
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
                        IsApprove = false,
                        IsActive = true
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
                        IsApprove = false,
                        IsActive = true
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
                        IsApprove = false,
                        IsActive = true
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
                        IsApprove = false,
                        IsActive = true
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
                        IsApprove = false,
                        IsActive = true
                    }
                );
            #endregion

            #region Rates

            #endregion
        }
    }
}
