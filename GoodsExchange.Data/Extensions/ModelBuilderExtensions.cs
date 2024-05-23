using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
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
            builder.Entity<User>().HasData
                (
                    new User()
                    {
                        UserId = Guid.NewGuid(),
                        RoleId = new Guid("E398CEE3-6381-4A52-AAF5-20A2E9B54810"),
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        DateOfBirth = new DateTime(1985, 6, 15),
                        PhoneNumber = "555-1234567",
                        UserImageUrl = "",
                        UserName = "johndoe",
                        Password = "password123",
                        Status = true
                    },
                    new User()
                    {
                        UserId = Guid.NewGuid(),
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693"),
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        DateOfBirth = new DateTime(1992, 3, 10),
                        PhoneNumber = "555-7654321",
                        UserImageUrl = "",
                        UserName = "janesmith",
                        Password = "password456",
                        Status = true
                    },
                    new User
                    {
                        UserId = Guid.NewGuid(),
                        RoleId = new Guid("CA5AF2D0-6B92-49BB-91FF-2E5D9F1279D4"),
                        FirstName = "Michael",
                        LastName = "Johnson",
                        Email = "michael.johnson@example.com",
                        DateOfBirth = new DateTime(1978, 11, 22),
                        PhoneNumber = "555-2468013",
                        UserImageUrl = "",
                        UserName = "michaeljohnson",
                        Password = "password789",
                        Status = false
                    },
                    new User
                    {
                        UserId = Guid.NewGuid(),
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B"),
                        FirstName = "Emily",
                        LastName = "Davis",
                        Email = "emily.davis@example.com",
                        DateOfBirth = new DateTime(1990, 7, 1),
                        PhoneNumber = "555-3691258",
                        UserImageUrl = "",
                        UserName = "emilydavis",
                        Password = "passwordabc",
                        Status = true
                    },
                    new User
                    {
                        UserId = Guid.NewGuid(),
                        RoleId = new Guid("CA5AF2D0-6B92-49BB-91FF-2E5D9F1279D4"),
                        FirstName = "David",
                        LastName = "Lee",
                        Email = "david.lee@example.com",
                        DateOfBirth = new DateTime(1982, 4, 30),
                        PhoneNumber = "555-4725836",
                        UserImageUrl = "",
                        UserName = "davidlee",
                        Password = "passworddef",
                        Status = false
                    }
                );
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
            builder.Entity<Product>().HasData
                (
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Ballpoint Pen",
                        Description = "Premium ballpoint pen for everyday use",
                        ProductImageUrl = "https://example.com/ballpoint-pen.jpg",
                        Price = 2.99f,
                        Status = true,
                        UploadDate = new DateTime(2023, 4, 15),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 4, 16),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Mechanical Pencil",
                        Description = "Durable mechanical pencil with 0.5mm lead",
                        ProductImageUrl = "https://example.com/mechanical-pencil.jpg",
                        Price = 4.50f,
                        Status = true,
                        UploadDate = new DateTime(2023, 5, 1),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 5, 2),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Colored Pencils",
                        Description = "Set of 24 high-quality colored pencils",
                        ProductImageUrl = "https://example.com/colored-pencils.jpg",
                        Price = 9.99f,
                        Status = true,
                        UploadDate = new DateTime(2023, 3, 20),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 3, 21),
                        CategoryId = new Guid("CE74FC86-9CDF-4805-960C-E4647F21F6CF")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Sketchbook",
                        Description = "A5 size sketchbook with acid-free pages",
                        ProductImageUrl = "https://example.com/sketchbook.jpg",
                        Price = 12.99f,
                        Status = true,
                        UploadDate = new DateTime(2023, 6, 1),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 6, 2),
                        CategoryId = new Guid("CE74FC86-9CDF-4805-960C-E4647F21F6CF")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Chemistry Textbook",
                        Description = "High school-level chemistry textbook",
                        ProductImageUrl = "https://example.com/chemistry-textbook.jpg",
                        Price = 29.99f,
                        Status = true,
                        UploadDate = new DateTime(2023, 2, 10),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 2, 11),
                        CategoryId = new Guid("F0FDE948-4E6D-4412-A417-3EAC5F927D44")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Mathematics Workbook",
                        Description = "Grade 7 mathematics practice workbook",
                        ProductImageUrl = "https://example.com/math-workbook.jpg",
                        Price = 14.99f,
                        Status = true,
                        UploadDate = new DateTime(2023, 7, 1),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 7, 2),
                        CategoryId = new Guid("F0FDE948-4E6D-4412-A417-3EAC5F927D44")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Graphing Calculator",
                        Description = "Scientific calculator with graphing capabilities",
                        ProductImageUrl = "https://example.com/graphing-calculator.jpg",
                        Price = 59.99f,
                        Status = true,
                        UploadDate = new DateTime(2023, 3, 15),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 3, 16),
                        CategoryId = new Guid("E0B58109-B173-442A-86D5-972E0BC3E093")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Tablet Computer",
                        Description = "High-performance tablet for educational use",
                        ProductImageUrl = "https://example.com/tablet-computer.jpg",
                        Price = 299.99f,
                        Status = true,
                        UploadDate = new DateTime(2023, 5, 20),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 5, 21),
                        CategoryId = new Guid("E0B58109-B173-442A-86D5-972E0BC3E093")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Ruler",
                        Description = "Durable 30cm plastic ruler",
                        ProductImageUrl = "https://example.com/ruler.jpg",
                        Price = 1.50f,
                        Status = true,
                        UploadDate = new DateTime(2023, 4, 1),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 4, 2),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    },
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Highlighter Set",
                        Description = "Set of 4 fluorescent highlighters",
                        ProductImageUrl = "https://example.com/highlighter-set.jpg",
                        Price = 3.99f,
                        Status = true,
                        UploadDate = new DateTime(2023, 6, 15),
                        IsApproved = true,
                        ApprovedDate = new DateTime(2023, 6, 16),
                        CategoryId = new Guid("94D367D0-61D1-4979-BA88-99B2F83FE9EB")
                    }
                );
        }
    }
}
