using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        DateOfBirth = new DateTime(1985, 6, 15),
                        PhoneNumber = "555-1234567",
                        UserImageUrl = "",
                        UserName = "admin",
                        Password = "123456789",
                        RoleId = new Guid("E398CEE3-6381-4A52-AAF5-20A2E9B54810")
                    },
                    new User()
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
                        RoleId = new Guid("3D446530-061E-4A88-AE6C-1B6A6190A693")
                    },
                    new User()
                    {
                        UserId = new Guid("82C47D9C-B386-4050-A42C-95A220639C54"),
                        FirstName = "Michael",
                        LastName = "Johnson",
                        Email = "michael.johnson@example.com",
                        DateOfBirth = new DateTime(1978, 11, 22),
                        PhoneNumber = "555-2468013",
                        UserImageUrl = "",
                        UserName = "customerhihi",
                        Password = "123456",
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B")
                    },
                    new User()
                    {
                        UserId = new Guid("FDA6E282-E429-4364-A445-136B570E2FDE"),
                        FirstName = "Emily",
                        LastName = "Davis",
                        Email = "emily.davis@example.com",
                        DateOfBirth = new DateTime(1990, 7, 1),
                        PhoneNumber = "555-3691258",
                        UserImageUrl = "",
                        UserName = "customerhaha",
                        Password = "123456",
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B")
                    },
                    new User()
                    {
                        UserId = new Guid("D6446689-2743-460B-82C3-D25B21F87B13"),
                        FirstName = "David",
                        LastName = "Lee",
                        Email = "david.lee@example.com",
                        DateOfBirth = new DateTime(1982, 4, 30),
                        PhoneNumber = "555-4725836",
                        UserImageUrl = "",
                        UserName = "customerhehe",
                        Password = "123456",
                        RoleId = new Guid("D81F428F-9572-47F1-A980-69DE7A1E348B")
                    }
                );

            #endregion

            #region Catgories
            builder.Entity<Category>().HasData
                (
                    new Category { CategoryId = new Guid("119E60E0-789A-47E2-A280-E0C1A9A7032F"), CategoryName = "School Supplies" },
                    new Category { CategoryId = Guid.NewGuid(), CategoryName = "Art Supplies" },
                    new Category { CategoryId = Guid.NewGuid(), CategoryName = "Electronics" }
                );

            #endregion
            
        }
    }
}
