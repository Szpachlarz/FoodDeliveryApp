using Microsoft.AspNetCore.Identity;
using FoodDeliveryApp.Data.Enum;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Data;

namespace FoodDeliveryApp.Data
{
    public class Seed
    {
        //public static void SeedData(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        //        context.Database.EnsureCreated();

        //        if (!context.Authors.Any())
        //        {
        //            context.Authors.AddRange(new List<Author>()
        //            {
        //                new Author()
        //                {
        //                    Names = "Arkadiusz",
        //                    Surname = "Gront",
        //                    Nationality = "żydowska",
        //                    DateOfBirth = "2000-02-26",
        //                    DateOfDeath = null,
        //                },
        //                new Author()
        //                {
        //                    Names = "Rafał",
        //                    Surname = "Gomola",
        //                    Nationality = "albańska",
        //                    DateOfBirth = "2000-11-13",
        //                    DateOfDeath = "2023-05-27",
        //                }
        //            });
        //            context.SaveChanges();
        //        }
        //        if (!context.Publishers.Any())
        //        {
        //            context.Publishers.AddRange(new List<Publisher>()
        //            {
        //                new Publisher()
        //                {
        //                    Name = "Wydawnictwo Politechniki Śląskiej",
        //                    Country = "Polska",
        //                    City = "Gliwice",
        //                },
        //            });
        //            context.SaveChanges();
        //        }
        //        if (!context.Categories.Any())
        //        {
        //            context.Categories.AddRange(new List<Category>()
        //            {
        //                new Category()
        //                {
        //                    Name = "Grzybiarstwo",
        //                },
        //            });
        //            context.SaveChanges();
        //        }
        //        if (!context.Books.Any())
        //        {
        //            context.Books.AddRange(new List<Book>()
        //            {
        //                new Book()
        //                {
        //                    Title = "Atlas grzybów",
        //                    OriginalTitle = null,
        //                    ISBN = 1234567890123,
        //                    PublicationYear = DateTime.Parse("1970-01-01"),
        //                    FirstPublicationYear = DateTime.Parse("1970-01-01"),
        //                    Language = "polski",
        //                    OriginalLanguage = null,
        //                    Translation = null,
        //                    PageCount = 123,
        //                    Series = null,
        //                    Description = "Wyjątkowy atlas grzybów jadalnych i trujących",
        //                    CreatedAt = DateTime.Now,
        //                    UpdatedAt = DateTime.Now,
        //                    CoverImagePath = "https://www.swiatksiazki.pl/media/catalog/product/cache/eaf55611dc9c3a2fa4224fad2468d647/8/8/8899906773988.jpg",
        //                    PublisherId = 1,
        //                },
        //            });
        //            context.SaveChanges();
        //        }
        //        if (!context.BookCategories.Any())
        //        {
        //            context.BookCategories.AddRange(new List<BookCategory>()
        //            {
        //                new BookCategory()
        //                {
        //                    BookId = 1,
        //                    CategoryId = 1,
        //                },
        //            });
        //            context.SaveChanges();
        //        }
        //        if (!context.BookAuthors.Any())
        //        {
        //            context.BookAuthors.AddRange(new List<BookAuthor>()
        //            {
        //                new BookAuthor()
        //                {
        //                    BookId = 1,
        //                    AuthorId = 1,
        //                },
        //                new BookAuthor()
        //                {
        //                    BookId = 1,
        //                    AuthorId = 2,
        //                },
        //            });
        //            context.SaveChanges();
        //        }
        //    }
        //}

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Restaurant))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Restaurant));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@mail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);

                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "administrator",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Status = 0,
                    };
                    await userManager.CreateAsync(newAdminUser, "Haslo@123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@mail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "uzytkownik",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Status = 0,
                    };
                    await userManager.CreateAsync(newAppUser, "Haslo@456");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string restaurantUserEmail = "restaurant@mail.com";

                var restaurantUser = await userManager.FindByEmailAsync(restaurantUserEmail);
                if (restaurantUser == null)
                {
                    var newRestaurantUser = new User()
                    {
                        UserName = "restauracja",
                        Email = restaurantUserEmail,
                        EmailConfirmed = true,
                        Status = 0,
                    };
                    await userManager.CreateAsync(newRestaurantUser, "Haslo@789");
                    await userManager.AddToRoleAsync(newRestaurantUser, UserRoles.Restaurant);
                }
            }
        }
    }
}