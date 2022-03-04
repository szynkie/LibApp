using System;
using System.Collections.Generic;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                /* Seed data to MembershipTypes */

                if (context.MembershipTypes.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }

                var MembershipTypesToAdd = new List<MembershipType>{
                    new MembershipType
                    {
                        Id = 1,
                        Name = "Pay as You Go",
                        SignUpFee = 0,
                        DurationInMonths = 0,
                        DiscountRate = 0
                    },
                    new MembershipType
                    {
                        Id = 2,
                        Name = "Monthly",
                        SignUpFee = 30,
                        DurationInMonths = 1,
                        DiscountRate = 10
                    },
                    new MembershipType
                    {
                        Id = 3,
                        Name = "Quaterly",
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
                    new MembershipType
                    {
                        Id = 4,
                        Name = "Yearly",
                        SignUpFee = 300,
                        DurationInMonths = 12,
                        DiscountRate = 20
                    }
                };

                foreach (MembershipType MsT in MembershipTypesToAdd)
                {
                    context.MembershipTypes.AddRange(MsT);
                }



                /* Seed data to Books */

                var BooksToAdd = new List<Book>{
                    new Book
                    {
                        Name = "Book 1",
                        AuthorName = "Author 1",
                        GenreId = 1,
                        DateAdded = DateTime.Now.AddDays(-1),
                        ReleaseDate = DateTime.Now.AddDays(-3),
                        NumberInStock = 12,
                        NumberAvailable = 12,
                    },
                    new Book
                    {
                        Name = "Book 1",
                        AuthorName = "Author 1",
                        GenreId = 2,
                        DateAdded = DateTime.Now.AddDays(-1),
                        ReleaseDate = DateTime.Now.AddDays(-3),
                        NumberInStock = 12,
                        NumberAvailable = 12,
                    },
                    new Book
                    {
                        Name = "Book 1",
                        AuthorName = "Author 1",
                        GenreId = 3,
                        DateAdded = DateTime.Now.AddDays(-1),
                        ReleaseDate = DateTime.Now.AddDays(-3),
                        NumberInStock = 12,
                        NumberAvailable = 12,
                    },
                    new Book
                    {
                        Name = "Book 1",
                        AuthorName = "Author 1",
                        GenreId = 4,
                        DateAdded = DateTime.Now.AddDays(-1),
                        ReleaseDate = DateTime.Now.AddDays(-3),
                        NumberInStock = 12,
                        NumberAvailable = 12,
                    }
                };

                foreach (Book book in BooksToAdd)
                {
                    context.Books.AddRange(book);
                }



                /* Seed data to Customers */

                var CustomersToAdd = new List<Customer>{
                    new Customer
                    {
                           Name = "Customer 1",
                           HasNewsletterSubscribed = true,
                           MembershipTypeId = 1,
                           Birthdate = DateTime.Now.AddYears(-10),
                    },
                    new Customer
                    {
                           Name = "Customer 1",
                           HasNewsletterSubscribed = true,
                           MembershipTypeId = 2,
                           Birthdate = DateTime.Now.AddYears(-10),
                    },
                    new Customer
                    {
                           Name = "Customer 1",
                           HasNewsletterSubscribed = true,
                           MembershipTypeId = 3,
                           Birthdate = DateTime.Now.AddYears(-10),
                    },
                    new Customer
                    {
                           Name = "Customer 1",
                           HasNewsletterSubscribed = true,
                           MembershipTypeId = 4,
                           Birthdate = DateTime.Now.AddYears(-10),
                    }
                };

                foreach (Customer cust in CustomersToAdd)
                {
                    context.Customers.AddRange(cust);
                }



                /* Seed data to Rentals */

                var RentalsToAdd = new List<Rental>();

                foreach (Book book in BooksToAdd)
                {
                    foreach (Customer cust in CustomersToAdd)
                    {
                        RentalsToAdd.Add(
                            new Rental
                            {
                                Book = book,
                                Customer = cust,
                                DateRented = DateTime.Now.AddDays(book.GenreId + cust.MembershipTypeId)
                            }
                        );
                    }
                }

                foreach (Rental rental in RentalsToAdd)
                {
                    context.Rentals.AddRange(rental);
                }


                /* Update data in context */

                context.SaveChanges();
            }
        }
    }
}