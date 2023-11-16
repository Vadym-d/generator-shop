using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SeederDB
    {
        public static void SeedData(IServiceProvider services)
        {
            try
            {
                using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                    var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var roleName = "Admin";
                    var result = managerRole.CreateAsync(new DbRole
                    {
                        Name = roleName
                    }).Result;

                    string email = "admin@gmail.com";
                    var user = new DbUser
                    {
                        Email = email,
                        FirstName = "Степан",
                        LastName = "Непан",
                        UserName = email,
                        PhoneNumber = "+380974355566",
                    };
                    result = manager.CreateAsync(user, "12345678").Result;
                    result = manager.AddToRoleAsync(user, roleName).Result;

                    roleName = "User";
                    result = managerRole.CreateAsync(new DbRole
                    {
                        Name = roleName
                    }).Result;

                    email = "user@gmail.com";
                    user = new DbUser
                    {
                        Email = email,
                        UserName = email,
                        FirstName = "Вадим",
                        LastName = "Лірник",
                        PhoneNumber = "+380974355116",
                    };
                    result = manager.CreateAsync(user, "12345678").Result;
                    result = manager.AddToRoleAsync(user, roleName).Result;

                    var orderStatuses = new List<OrderStatus>
                    {
                        new OrderStatus{Status ="Нове"},
                        new OrderStatus{Status ="Скасоване" },
                        new OrderStatus{Status ="Відхилене"},
                        new OrderStatus{Status ="У процесі обробки"},
                        new OrderStatus{Status ="Відправлене"},
                        new OrderStatus{Status ="Успішно виконане"}
                    };
                    context.OrderStatuses.AddRange(orderStatuses);

                    var generatorCategories = new List<GeneratorCategory>
                    {
                        new GeneratorCategory{Category="Бензинові"},
                        new GeneratorCategory{Category="Дизельні"},
                        new GeneratorCategory{Category="Газ-бензин"},
                    };
                    context.GeneratorCategories.AddRange(generatorCategories);

                    context.SaveChanges();

                    var generators = new List<Generator>
                    {
                        new Generator{Name="Senci SCD 7500 Q", Power=5 , ImageUrl="https://generator.ua/25120-large_default/generator-dizelnij-senci-scd-7500-q.webp"
                        ,PowerOutput=12,FuelConsuming=1.6,Tank=12.5 ,Price=41900, Weight=45, GenratorCategoryId=generatorCategories.FirstOrDefault(x=>x.Category.Equals("Дизельні")).Id },
                        new Generator{Name="WattStream WS22-FS", Power=16, ImageUrl="https://generator.ua/25182-large_default/generator-dizelnij-wattstream-ws22-fs.webp"
                        ,PowerOutput=12,FuelConsuming=3.6,Tank=60  ,Price=334480, Weight=750, GenratorCategoryId=generatorCategories.FirstOrDefault(x=>x.Category.Equals("Дизельні")).Id },
                        new Generator{Name="Konner&Sohnen KS 8102HDE", Power=6 , ImageUrl="https://generator.ua/20055-large_default/generator-konnersohnen-ks-8102hde.webp"
                        ,PowerOutput=12,FuelConsuming=1.25,Tank=15 ,Price=52799, Weight=68, GenratorCategoryId=generatorCategories.FirstOrDefault(x=>x.Category.Equals("Дизельні")).Id },
                        new Generator{Name="Hyundai DHY 5000L", Power=4.2 , ImageUrl="https://generator.ua/18048-large_default/generator-hyundai-dhy-5000l.webp"
                        ,PowerOutput=12,FuelConsuming=1,Tank=12.5 ,Price=32966, Weight=60, GenratorCategoryId=generatorCategories.FirstOrDefault(x=>x.Category.Equals("Дизельні")).Id },

                        new Generator{Name="PROFI-TEC PE-1200G", Power=0.9 , ImageUrl="https://generator.ua/26464-large_default/generator-benzinovij-profi-tec-pe-1200g.webp"
                        ,PowerOutput=12,FuelConsuming=0.7,Tank=3.5 ,Price=7380, Weight=20, GenratorCategoryId=generatorCategories.FirstOrDefault(x=>x.Category.Equals("Бензинові")).Id },
                        new Generator{Name="Konner&Sohnen 7000E", Power=5 , ImageUrl="https://generator.ua/17402-large_default/generator-konnersohnen-7000e.webp"
                        ,PowerOutput=12,FuelConsuming=1.4,Tank=25 ,Price=36299, Weight=74, GenratorCategoryId=generatorCategories.FirstOrDefault(x=>x.Category.Equals("Бензинові")).Id },

                        new Generator{Name="Genergy Natura 3000", Power=2.7 , ImageUrl="https://generator.ua/28024-large_default/generator-benzin-gaz-genergy-natura-3000.webp"
                        ,PowerOutput=12,FuelConsuming=1,Tank=12 ,Price=34900, Weight=57, GenratorCategoryId=generatorCategories.FirstOrDefault(x=>x.Category.Equals("Газ-бензин")).Id },
                        new Generator{Name="Senci SC 4000 E", Power=3.2 , ImageUrl="https://generator.ua/27315-large_default/generator-gazovo-benzinovij-senci-sc-4000-e-d.webp"
                        ,PowerOutput=12,FuelConsuming=1.7,Tank=15 ,Price=24900, Weight=49, GenratorCategoryId=generatorCategories.FirstOrDefault(x=>x.Category.Equals("Газ-бензин")).Id }
                    };
                    context.Generators.AddRange(generators);

                    var deliveryAddresses = new List<DeliveryAddress>()
                    {
                        new DeliveryAddress{City="Київ",Address="Холоднояренська 11" },
                        new DeliveryAddress{City="Київ",Address="Луганська 42" },
                        new DeliveryAddress{City="Вінниця",Address="Шевченка 112" }
                    };
                    context.DeliveryAddresses.AddRange(deliveryAddresses);

                    context.SaveChanges();

                    var orders = new List<Order>()
                    {
                        new Order{Date="12-05-2023",DbUser=manager.FindByIdAsync("1").Result,OrderStatusId=1,DeliveryAddressId=1},
                        new Order{Date="15-05-2023",DbUser=manager.FindByIdAsync("1").Result,OrderStatusId=4,DeliveryAddressId=2},
                        new Order{Date="7-05-2023",DbUser=manager.FindByIdAsync("2").Result,OrderStatusId=5,DeliveryAddressId=3}
                    };
                    context.Orders.AddRange(orders);

                    context.SaveChanges();

                    var orderDetails = new List<OrderDetails>() 
                    {
                        new OrderDetails { GeneratorId = 1, OrderId = 1, Price=41900,Quantity=2  },
                        new OrderDetails { GeneratorId = 5, OrderId = 1, Price=7380,Quantity=1  },
                        new OrderDetails { GeneratorId = 8, OrderId = 2, Price=22900,Quantity=3  },
                        new OrderDetails { GeneratorId = 2, OrderId = 3, Price=344480,Quantity=1  },
                    };
                    context.OrderDetails.AddRange(orderDetails);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
