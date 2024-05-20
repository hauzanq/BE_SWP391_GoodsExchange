using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                        UserName = "davidlee",
                        Password = "passworddef",
                        Status = false
                    }
                );
        }
    }
}
