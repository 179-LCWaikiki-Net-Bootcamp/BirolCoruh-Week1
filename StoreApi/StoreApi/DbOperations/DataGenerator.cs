using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;

namespace StoreApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<StoreDbContext>>()))
            {
                if (context.Products.Any())
                {
                    return;
                }


                context.Products.AddRange(
                    new Product { Id = 1, Name = "Bofigo 50x100 Masa", Description = "Aynı gün kargoda !", Price = 46, Stock = 22},
                    new Product { Id = 2, Name = "Philips GC503 Ütü", Description = "5 yıl garantili ve ısıya dayanıklı", Price = 115, Stock = 43 },
                    new Product { Id = 3, Name = "Ikea Ingolf Sandalye", Description = "İsviçre'de üretildi, 2 yıl garantili", Price = 85, Stock = 13},
                    new Product { Id = 4, Name = "Tefal Z210 Tencere", Description = "Paslanmaz Çelikten Yapılmıştır ", Price = 140, Stock = 81},
                    new Product { Id = 5, Name = "Jumbo Tabak Seti", Description = "120 parçalık Tabak seti ", Price = 140, Stock = 81}
                );
                context.Categories.AddRange(
                    new Category { Id = 1, Name = "Mutfak Araçları" },
                    new Category { Id = 2, Name = "Ev Aletleri" },
                    new Category { Id = 3, Name = "Ofis Malzemeleri" }
                );

                context.SaveChanges();
            }
        }
    }
}
