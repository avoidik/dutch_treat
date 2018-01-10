using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data
{
    public class WebApplicationSeeder
    {
        private readonly ApplicationContext _context;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _manager;

        public WebApplicationSeeder(ApplicationContext context, IHostingEnvironment hosting, UserManager<StoreUser> manager)
        {
            _context = context;
            _hosting = hosting;
            _manager = manager;
        }

        public async Task SeedDatabase()
        {
            _context.Database.Migrate();

            var user = await _manager.FindByEmailAsync("root@localhost");
            if(user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "root@localhost",
                    Email = "root@localhost"
                };
                var result = await _manager.CreateAsync(user, "P@ssw0rd!");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Unable to create default user!");
                }
            }

            if(!_context.Products.Any())
            {
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/Seed/seed.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);

                if(!_context.Orders.Any())
                {
                    var order = new Order()
                    {
                        OrderDate = DateTime.Now,
                        OrderNumber = "1234",
                        User = user,
                        Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                    };
                    _context.Orders.Add(order);
                }

                _context.SaveChanges();
            }
        }
    }
}
