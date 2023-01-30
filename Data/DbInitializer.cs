using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;

namespace RazorShoesProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any Customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded with customers
            }

            // Customers
            var customer001 = new Customer
            {
                Name = "Joe Collins",
                DateOfBirth = DateTime.Parse("2002-02-10"),
                Email = "joe.collins@mail.com",
                PhoneNumber = "+44 01234 567567",
                //Address = address001
            };

            var customer002 = new Customer
            {
                Name = "Hayden Howarth",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "HaydenHowarth@jyahoo.com",
                PhoneNumber = "+44 0118 9508597",
             // Address = { HouseNumber = 101, RoadName = "The Avenue", PostTown = "ROCHESTER", Postcode = "ME92 0DC" }
            };

            var customer003 = new Customer
            {
                Name = "George Thomas",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "GeorgeThomas@yahoo.com",
                PhoneNumber = "+44 01494 539567",
              //Address = { HouseNumber = 26, RoadName = "Grange Road", PostTown = "DONCASTER", Postcode = "DN24 6EL" }
            };

            var customer004 = new Customer
            {
                Name = "Jonathan Wyatt",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "JonathanWyatt@yahoo.com",
                PhoneNumber = "+44 01908 503153",
              //Address = { HouseNumber = 31, RoadName = "London Road", PostTown = "EDINBURGH", Postcode = "EH63 2FM" }
            };

            var customer005 = new Customer
            {
                Name = "Nathan Wong",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "NathanWong@yahoo.com",
                PhoneNumber = "+44 01296 424544",
             // Address = { HouseNumber = 65, RoadName = "Alexander Road", PostTown = "SLOUGH", Postcode = "SL0 7WL" }
            };

            var customer006 = new Customer
            {
                Name = "Mia Crawford",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "MiaCrawford@yahoo.com",
                PhoneNumber = "+44 01623 646539",
             // Address = { HouseNumber = 12, RoadName = "Windsor Road", PostTown = "SHEFFIELD", Postcode = "S50 1DZ" }
            };

            var customer007 = new Customer
            {
                Name = "Kiera Woods",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "KieraWoods@yahoo.com",
                PhoneNumber = "+44 01252 727555",
              //Address = { HouseNumber = 55, RoadName = "Park Road", PostTown = "EXETER", Postcode = "EX37 4VA" }
            };

            var customer008 = new Customer
            {
                Name = "Elizabeth Richardson",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "ElizabethRichardson@yahoo.com",
                PhoneNumber = "+44 01306 885178",
              //Address = { HouseNumber = 10, RoadName = "Stanley Road", PostTown = "NOTTINGHAM", Postcode = "NG69 7ZO" }
            };

            var customer009 = new Customer
            {
                Name = "Amber Conway",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "ElizabethRichardson@yahoo.com",
                PhoneNumber = "+44 0113 4960891",
              //Address = { HouseNumber = 89, RoadName = "The Drive", PostTown = "BLACKPOOL", Postcode = "FY44 8HK" }
            };

            var customer010 = new Customer
            {
                Name = "Luca Shah",
                DateOfBirth = DateTime.Parse("1994-03-10"),
                Email = "LucaShah@yahoo.com",
                PhoneNumber = "+44 0345 6779485",
              //Address = { HouseNumber = 460, RoadName = "Manchester Road", PostTown = "DORCHESTER", Postcode = "DT89 0TV" }
            };

            var customers = new Customer[]
            {
                customer001,
                customer002,
                customer003,
                customer004,
                customer005,
                customer006,
                customer007,
                customer008,
                customer009,
                customer010
            };

            // Shoes
            var shoe001 = new Shoe
            {
                Name = "Reebok Nano X2",
                Description = "From pistol squats to burpees, there's no shortage of moves to take your workout to the next level. Reach for these men's Reebok shoes to stay confident in or out of the gym.",
                Type = "Trainer",
                Brand = "Reebok",
                Gender = "Male",
                Price = 54.99
            };

            var shoe002 = new Shoe
            {
                Name = "Dynamight 2",
                Description = "The Skechers Dynamight 2 Trainers will have you smashing your personal best. Engineered with a memory foam insole and lightweight chunky sole for optimum comfort, the mesh upper will provide breathability.",
                Type = "Trainer",
                Brand = "Sketchers",
                Gender = "Female",
                Price = 59.99
            };

            var shoe003 = new Shoe
            {
                Name = "Terrex Eastrail R.RDY Waterproof",
                Description = "The adidas Terrex Eastrail R.RDY Waterproof Mens Walking Shoes are perfect for hitting the trails, crafted with a low profile ankle collar coupled with a pull loop to the heel and a lace fastening that provides a secure fit.",
                Type = "Walking Shoes",
                Brand = "Adidas",
                Gender = "Male",
                Price = 69.59
            };

            var shoe004 = new Shoe
            {
                Name = "Air Max LTD 3",
                Description = "Elevate your rotation with the Air Max LTD 3 Trainers from Nike which have been crafted with a visible Max Air unit in the heel which works with the full-length foam midsole to deliver responsive cushioning for all-day comfort. ",
                Type = "Trainer",
                Brand = "Nike",
                Gender = "Male",
                Price = 99.99
            };

            var shoe005 = new Shoe
            {
                Name = "Air Max Invigor",
                Description = "Inspired by the iconic Air Max 95, the Invigor trainers have been engineered with a breathe tech upper which is light, airy and full of texture. ",
                Type = "Trainer",
                Brand = "Nike",
                Gender = "Male",
                Price = 29.49
            };

            var shoe006 = new Shoe
            {
                Name = "Retaliate 2",
                Description = "Step up your running or everyday sneaker collection with these Retaliate 2 Trainers from Puma.",
                Type = "Sneaker",
                Brand = "Puma",
                Gender = "Male",
                Price = 34.49
            };

            var shoe007 = new Shoe
            {
                Name = "Zoom Lite 3",
                Description = "The Nike Court Zoom Lite 3 Women's Tennis Shoes, crafted with a low profile ankle collar coupled with a lace fastening that helps to lock your foot down into the shoe.",
                Type = "Tennis Shoes",
                Brand = "Nike",
                Gender = "Female",
                Price = 39.99
            };

            var shoe008 = new Shoe
            {
                Name = "Rapid Support",
                Description = "The Womens Karrimor Rapid Support Running Shoes are perfect for the next time hitting the track or street, crafted with a low profile ankle collar teamed with a lace fastening for a secure fit.",
                Type = "Running Shoes",
                Brand = "Karrimor",
                Gender = "Female",
                Price = 45.00
            };

            var shoe009 = new Shoe
            {
                Name = "Nano X2",
                Description = "A lightweight knit upper provides fantastic ventilation to help keep your feet cool while the cushioned insole offers a comfortable ride, completed with the signature Reebok branding.",
                Type = "Trainers",
                Brand = "Reebok",
                Gender = "Female",
                Price = 89.99
            };

            var shoe010 = new Shoe
            {
                Name = "Royalglide Ld99",
                Description = "Made from Ld99 to help achieve a premium look, these lace ups feature a contrasting leather stripe across each side.",
                Type = "Trainer",
                Brand = "Nike",
                Gender = "Unisex",
                Price = 94.49
            };

            var shoes = new Shoe[]
            {
                shoe001,
                shoe002,
                shoe003,
                shoe004,
                shoe005,
                shoe006,
                shoe007,
                shoe008,
                shoe009,
                shoe010
            };

            // Orders
            var order001 = new Order
            {
                PlannedDelivery = (DateTime.Parse("2023-01-19")),
                ActualDelivery = (DateTime.Parse("2023-01-20")),
                OrderStatus = "Delivered",
                Customer = customer002,
                Payment = new Payment { CardNumber = 1357864212341234, CVVNumber = 123, ExpiryDate = (DateTime.Parse("01/2025")) },
                IsPaid = true
            };

            var order002 = new Order
            {
                PlannedDelivery = (DateTime.Parse("2023-01-19")),
                OrderStatus = "Not Paid",
                Customer = customer004,
                IsPaid = false
            };

            var order003 = new Order
            {
                PlannedDelivery = (DateTime.Parse("2023-01-28")),
                OrderStatus = "Processing",
                Customer = customer006,
                Payment = new Payment { CardNumber = 1234246835790324, CVVNumber = 224, ExpiryDate = DateTime.Parse("05/2024")},
                IsPaid = true
            };

            var order004 = new Order
            {
                PlannedDelivery = (DateTime.Parse("2023-01-19")),
                OrderStatus = "Processing",
                Customer = customer008,
                Payment = new Payment { CardNumber = 8796365413578642, CVVNumber = 887, ExpiryDate = (DateTime.Parse("05/2023")) },
                IsPaid = true
            };

            var order005 = new Order
            {
                PlannedDelivery = (DateTime.Parse("2023-01-30")),
                OrderStatus = "Out for Delivery",
                Customer = customer004,
                Payment = new Payment { CardNumber = 3456127822331144, CVVNumber = 465, ExpiryDate = (DateTime.Parse("11/2024")) },
                IsPaid = true
            };

            var orders = new Order[]
            {
                order001,
                order002,
                order003,
                order004,
                order005,
            };

            var items = new OrderItem[]
            {
                new OrderItem 
                { 
                    Order = order001, 
                    Shoe = shoe002, 
                    SalePrice = 69.59, 
                    Quantity = 1, 
                    Size = 11 
                },
                new OrderItem 
                { 
                    Order = order002, 
                    Shoe = shoe004, 
                    SalePrice = 99.99, 
                    Quantity = 1, 
                    Size = 9 
                },
                new OrderItem 
                { 
                    Order = order003, 
                    Shoe = shoe006, 
                    SalePrice = 34.49, 
                    Quantity = 1, 
                    Size = 10 
                },
                new OrderItem 
                { 
                    Order = order004, 
                    Shoe = shoe008, 
                    SalePrice = 45.00, 
                    Quantity = 1, 
                    Size = 7 },
                new OrderItem 
                { 
                    Order = order005, 
                    Shoe = shoe010, 
                    SalePrice = 94.49, 
                    Quantity = 1, 
                    Size = 8 
                }
            };

            // Images
            var image001 = new Image 
            { 
                ImageUrl = "~/images/Nano X2 Training Shoes Mens.jpg", 
                ShoeId = 1 
            };
            var image002 = new Image 
            { 
                ImageUrl = "~/images/Dynamight 2 Trainers Womens.jpg", 
                ShoeId = 2 
            };
            var image003 = new Image { 
                ImageUrl = "~/images/Terrex Eastrail R.RDY Waterproof Mens Walking Shoes.jpg", 
                ShoeId = 3 
            };
            var image004 = new Image 
            { 
                ImageUrl = "~/images/Air Max LTD 3 Men's Shoe.jpg", 
                ShoeId = 4 
            };
            var image005 = new Image 
            { 
                ImageUrl = "~/images/Air Max Invigor Trainers Mens.jpg", 
                ShoeId = 5 
            };
            var image006 = new Image 
            { 
                ImageUrl = "~/images/Retaliate 2 Trainers Mens.jpg", 
                ShoeId = 6
            };
            var image007 = new Image 
            { 
                ImageUrl = "~/images/Zoom Lite 3 Women's Tennis Shoes.jpg", 
                ShoeId = 7
            };
            var image008 = new Image 
            { 
                ImageUrl = "~/images/Rapid Support Womens Running Shoes.jpg", 
                ShoeId = 8
            };
            var image009 = new Image 
            { 
                ImageUrl = "~/images/Nano X2 Training Shoes Ladies.jpg", 
                ShoeId = 9
            };
            var image010 = new Image 
            { 
                ImageUrl = "~/images/Royalglide Ld99.jpg", 
                ShoeId = 10
            };

            var images = new Image[]
            {
                image001,
                image002,
                image003,
                image004,
                image005,
                image006,
                image007,
                image008,
                image009,
                image010
            };

            context.Shoes.AddRange(shoes);
            context.Customers.AddRange(customers);
            context.Orders.AddRange(orders);
            context.OrderItems.AddRange(items);
            context.Images.AddRange(images);
            context.SaveChanges();
        }
    }
}